using EForms.API.Core.Dtos.Section;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Infrastructure.Models;
using EForms.API.Repository.Data.Repositories.Interfaces;
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
    public class SectionsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly ISectionService _sectionService;

        public SectionsController(IFormRepository formRepository,
                                ISectionService sectionService)
        {
            _formRepository = formRepository;
            _sectionService = sectionService;
        }

        [HttpPost("{formId}/section")]
        public async Task<IActionResult> CreateSimpleSection(string formId, [FromBody] SimpleSectionToInsertDto sectionToInsertDto)
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
            /*
             * If any sections were already in the fetched form
             * a copy should be done to add to it the newly section
            */
            if (fetchedForm.Sections != null)
                existedSections = fetchedForm.Sections;

            existedSections.Add(sectionToCreate);

            fetchedForm.Sections = existedSections;

            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

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

            // If not found return 404
            if (fetchedForm.Sections == null)
                return NotFound("Sections not found!!");

            return Ok(fetchedForm.Sections);
        }

        // Simple update for forms, only update Name, Description and Grid Layout.
        [HttpPut("{formId}/section/{sectionId}")]
        public async Task<IActionResult> SimpleUpdateSection(string formId, string sectionId, SectionToUpdateDto sectionToUpdateDto)
        {
            /*
             *  Simple Section Update Logic:
             *      1- Get the form (parent) document from DB
             *      2- Get the intended section to be swapped
             *      3- Update the fetched section
             *      7- Update the form document
             */

            Form fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This Form doesnt exist!!");

            Section fetchedSection = _sectionService.GetSectionFromForm(ref fetchedForm, sectionId);

            // Check the section existence in the fetched form
            if (fetchedSection == null)
                return NotFound("This Section doesnt exist!!");

           _sectionService.UpdateSimpleSection(ref fetchedSection, sectionToUpdateDto);

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
