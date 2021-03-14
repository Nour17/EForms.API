using EForms.API.Core.Dtos.Form;
using EForms.API.Repository.Data.Repositories.Interfaces;
using EForms.API.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Dtos.Section;
using System.Collections.Generic;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IQuestionService _questionService;

        public FormsController(IFormRepository formRepository,
                                IQuestionService questionService)
        {
            _formRepository = formRepository;
            _questionService = questionService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateFullForm(FullFormToInsertDto formToInsertDto)
        {
            // Create new Form object with the incoming properties from the request payload
            Form formToCreate = new Form
            {
                Name = formToInsertDto.Name,
                Description = formToInsertDto.Description,
                ColumnRepresentation = formToInsertDto.ColumnRepresentation
            };

            // Loop through the Questions in the incoming request
            foreach(QuestionToInsertDto questionToInsertDto in formToInsertDto.Questions)
            {
                // Add each question individualy into the form
                _questionService.InsertQuestion<Form>(ref formToCreate, questionToInsertDto);
            }

            // List of sections to hold the newly created sections from the request
            var sectionsToBeAdded = new List<Section>();

            // Loop through the Sections in the incoming request
            foreach (FullSectionToInsertDto sectionToInsertDto in formToInsertDto.Sections)
            {
                // Create new Section object with the incoming properties from the request payload
                Section sectionToCreate = new Section
                {
                    Name = sectionToInsertDto.Name,
                    Description = sectionToInsertDto.Description,
                    ColumnRepresentation = sectionToInsertDto.ColumnRepresentation
                };

                // Loop through the Questions in each section in the incoming request
                foreach (QuestionToInsertDto questionToInsertDto in sectionToInsertDto.Questions)
                {
                    _questionService.InsertQuestion<Section>(ref sectionToCreate, questionToInsertDto);
                }

                // Add the newly created section to the sectionsToBeAdded list
                sectionsToBeAdded.Add(sectionToCreate);
            }

            // Copy the sectionsToBeAdded list to the newly created form
            formToCreate.Sections = sectionsToBeAdded;

            // Add the form to the DB
            var createdForm = await _formRepository.AddForm<Form>(formToCreate);

            return Ok(createdForm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSimpleForm(SimpleFormToInsertDto formToInsertDto)
        {
            // Create new Form object with the incoming properties from the request payload
            Form formToCreate = new Form
            {
                Name = formToInsertDto.Name,
                Description = formToInsertDto.Description,
                ColumnRepresentation = formToInsertDto.ColumnRepresentation
            };

            var createdForm = await _formRepository.AddForm<Form>(formToCreate);

            return Ok(createdForm);
        }

        [HttpGet]
        public async Task<IActionResult> ListAllForms()
        {
            var forms = await _formRepository.GetForms<Form>();

            // Check the forms existence in the DB
            if (forms == null)
                return NotFound();

            return Ok(forms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetForm(string id)
        {
            var form = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (form == null)
                return NotFound("This form doesn't exist!!");

            return Ok(form);
        }

        // Simple update for forms, only update Name, Description and Grid Layout.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSimpleForm(string id, FormToUpdateDto formToUpdateDto)
        {
            // Get the form document / object from the DB
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            // Check each value if sent or not and then proceed with the replacement.
            if (formToUpdateDto.Name != null)
                fetchedForm.Name = formToUpdateDto.Name;

            if (formToUpdateDto.Description != null)
                fetchedForm.Description = formToUpdateDto.Description;

            if (formToUpdateDto.ColumnRepresentation != 0)
                fetchedForm.ColumnRepresentation = formToUpdateDto.ColumnRepresentation;

            var updatedForm = await _formRepository.UpdateForm<Form>(id, fetchedForm);

            return Ok(updatedForm);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(string id)
        {
            var result = await _formRepository.RemoveForm<Form>(id);

            if (!result)
                return NotFound("This form doesn't exist!!");

            return Ok(result);
        }
    }
}
