using EForms.API.Core.Dtos.Form;
using EForms.API.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Dtos.Section;
using System.Collections.Generic;
using System;
using Contracts;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    { 
        private readonly IContainerService _containerService;
        private readonly IFormService _formService;
        private readonly ILoggerManager _logger;

        public FormsController(IContainerService containerService,
                                IFormService formService,
                                ILoggerManager logger)
        {
            _containerService = containerService;
            _formService = formService;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateForm(FormToInsertDto formToInsertDto)
        {
            // Check availability of at least one question on the entire form either in questions or a specific section
            bool isValid = _formService.IsReceivedFormValid(formToInsertDto);
            if (!isValid)
            {
                return BadRequest("Incoming Form is invalid: Form must atleast have one question!!");
            }

            try
            {
                Form formToCreate = (Form)_containerService.CreateContainer<Form>(formToInsertDto);

                // List of sections to hold the newly created sections from the request
                var sectionsToBeAdded = new List<Section>();

                if (formToInsertDto.Sections != null)
                {
                    // Loop through the Sections in the incoming request
                    foreach (SectionToInsertDto sectionToInsertDto in formToInsertDto.Sections)
                    {
                        Section sectionToCreate = (Section)_containerService.CreateContainer<Section>(sectionToInsertDto);

                        // Add the newly created section to the sectionsToBeAdded list
                        sectionsToBeAdded.Add(sectionToCreate);
                    }
                }

                // Copy the sectionsToBeAdded list to the newly created form
                formToCreate.Sections = sectionsToBeAdded;

                // Add the form to the DB
                var createdForm = await _formService.AddForm(formToCreate);

                return Ok(createdForm);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListForms()
        {
            var fetchedForms = await _formService.GetForms();

            return Ok(fetchedForms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetForm(string id)
        {
            var fetchedForm = await _formService.GetForm(id);

            return Ok(fetchedForm);
        }

        // Simple update for forms, only update Name, Description and Grid Layout.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm(string id, FormToUpdateDto formToUpdateDto)
        {
            var fetchedForm = await _formService.GetForm(id);

            _containerService.SimpleUpdateContainer<Form>(ref fetchedForm, formToUpdateDto);

           // var updatedForm = await _formRepository.UpdateForm<Form>(id, fetchedForm);

            //return Ok(updatedForm);
            return Ok(fetchedForm);
        }

        [HttpPost("{id}/answer")]
        public async Task<IActionResult> AnswerForm([FromRoute] string id, [FromBody] FormAnswersDto formAnswersDto)
        {
            var fetchedForm = await _formService.GetForm(id);

            try
            {
                // Add all user's answers on one form at once
                Form formWithAnswers = _formService.ValidateFormAnswers(fetchedForm, formAnswersDto);
                var isFormUpdated = await _formService.UpdateForm(id, fetchedForm);

                return Ok(isFormUpdated);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
