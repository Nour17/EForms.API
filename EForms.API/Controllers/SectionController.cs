using EForms.API.Data.Repositories.Interfaces;
using EForms.API.Dtos.Section;
using EForms.API.Models;
using EForms.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly ISectionService _sectionService;

        public SectionController(IFormRepository formRepository,
                                ISectionService sectionService)
        {
            _formRepository = formRepository;
            _sectionService = sectionService;
        }

        [HttpPost("{formId}/section")]
        public async Task<IActionResult> CreateSection(string formId, [FromBody] SectionToInsertDto sectionToInsertDto)
        {
            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            // Create the new section and populate its values
            Section sectionToCreate = new Section
            {
                Name = sectionToInsertDto.Name,
                Description = sectionToInsertDto.Description,
                ColumnRepresentation = sectionToInsertDto.ColumnRepresentation
            };

            // Empty object to hold the stored sections in the fetchedForm
            var existedSections = new List<Section>();

            // Check whether the fethed form have any previous section or not
            try
            {
                /*
                 * If any sections were already in the fetched form
                 * a copy should be done to add to it the newly section
                 */
                if (fetchedForm.Sections != null)
                    existedSections = fetchedForm.Sections;

                existedSections.Add(sectionToCreate);
            } catch (ArgumentNullException e)
            {
                return BadRequest("Section cannot be added!" + e);
            }

            // BUG SHOULD BE FIXED HERE
            // The new form should only have the updated properties only
            Form formToUpdate = new Form
            {
                Name = fetchedForm.Name,
                Description = fetchedForm.Description,
                ColumnRepresentation = fetchedForm.ColumnRepresentation,
                Sections = existedSections,
                Questions = fetchedForm.Questions,
                FormAnswers = fetchedForm.FormAnswers
            };

            var updatedForm = await _formRepository.UpdateForm<Form>(formId, formToUpdate);

            return Ok(updatedForm);
        }

        [HttpGet("{formId}/section/{sectionId}")]
        public async Task<IActionResult> GetSection(string formId, string sectionId)
        {
            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return null;

            var section = _sectionService.GetSectionFromForm(ref fetchedForm, sectionId);

            // If found return 200/Section
            if (section != null)
                return Ok(section);

            return NotFound("Section not found!!");
        }

        [HttpGet("{formId}/sections")]
        public async Task<IActionResult> GetSections(string formId)
        {
            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            return Ok(fetchedForm.Sections);
        }

        // Simple update for forms, only update Name, Description and Grid Layout.
        [HttpPut("{formId}/section/{sectionId}")]
        public async Task<IActionResult> SimpleUpdateSection(string formId, string sectionId, SectionToUpdateDto sectionToUpdateDto)
        {
            /*
             *  Simple Section Update Logic:
             *      1- Get the form (parent) document from DB
             *      2- Using foreach loop fetch the intended section to be swapped
             *      3- (ALERT) BUG TO FIX - Copy the unchanged properties from the OLD section to the NEW one
             *      4- Get the OLD section Index from the sections List
             *      5- Remove the OLD section from the List
             *      6- Add the NEW section in the same index of the old one
             *      7- Update the form document
             */

            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return null;

            Section fetchedSection = _sectionService.GetSectionFromForm(ref fetchedForm, sectionId);

            _sectionService.UpdateSection(ref fetchedForm, ref fetchedSection, sectionToUpdateDto);

            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

            return Ok(updatedForm);
        }


        [HttpDelete("{formId}/section/{sectionId}")]
        public async Task<IActionResult> DeleteSection(string formId, string sectionId)
        {
            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            Section fetchedSection = _sectionService.GetSectionFromForm(ref fetchedForm, sectionId);

            // If found delete the section return 200/Form
            if (fetchedForm != null)
            {
                fetchedForm.Sections.Remove(fetchedSection);

                var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

                return Ok(updatedForm);
            }

            return NotFound("Section not found!!");
        }
    }
}
