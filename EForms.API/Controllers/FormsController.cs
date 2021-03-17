using EForms.API.Core.Dtos.Form;
using EForms.API.Repository.Data.Repositories.Interfaces;
using EForms.API.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Dtos.Section;
using System.Collections.Generic;
using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Answer;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IContainerService _containerService;
        private readonly IFormService _formService;
        private readonly IFormRepository _formRepository;

        public FormsController(IContainerService containerService,
                                IFormService formService,
                                IFormRepository formRepository)
        {
            _containerService = containerService;
            _formService = formService;
            _formRepository = formRepository;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateFullForm(FormToInsertDto formToInsertDto)
        {
            if (formToInsertDto.Questions.Count == 0 || formToInsertDto.Questions == null)
            {
                return BadRequest("Form must atleast have one question!!");
            }

            Form formToCreate = (Form)_containerService.PopulateContainer<Form>(formToInsertDto);

            _containerService.AddListOfQuestions<Form>(ref formToCreate, formToInsertDto.Questions);

            // List of sections to hold the newly created sections from the request
            var sectionsToBeAdded = new List<Section>();

            // Loop through the Sections in the incoming request
            foreach (SectionToInsertDto sectionToInsertDto in formToInsertDto.Sections)
            {
                Section sectionToCreate = (Section)_containerService.PopulateContainer<Section>(sectionToInsertDto);

                _containerService.AddListOfQuestions<Section>(ref sectionToCreate, sectionToInsertDto.Questions);

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
        public async Task<IActionResult> CreateSimpleForm(IContainerToCreateDto containerToInsertDto)
        {
            Form formToCreate = (Form) _containerService.PopulateContainer<Form>(containerToInsertDto);

            var createdForm = await _formRepository.AddForm<Form>(formToCreate);

            return Ok(createdForm);
        }

        [HttpGet]
        public async Task<IActionResult> ListForms()
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
        public async Task<IActionResult> UpdateForm(string id, FormToUpdateDto formToUpdateDto)
        {
            // Get the form document / object from the DB
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            _containerService.SimpleUpdateContainer<Form>(ref fetchedForm, formToUpdateDto);

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

        [HttpPost("{id}/answer")]
        public async Task<IActionResult> AnswerForm([FromRoute] string id, [FromBody] FormAnswersDto formAnswersDto)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            // Add all user's answers on one form at once
            List<ErrorMessage> errorMessages = _formService.ValidateFormAnswers(ref fetchedForm, formAnswersDto);

            if (errorMessages.Count == 0 )
            {
                var updatedForm = await _formRepository.UpdateForm<Form>(id, fetchedForm);

                return Ok(updatedForm);
            }

            return Ok(errorMessages);
        }
    }
}
