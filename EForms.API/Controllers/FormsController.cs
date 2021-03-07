using EForms.API.Dtos.Form;
using EForms.API.Models;
using EForms.API.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;

        public FormsController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(FormToInsertDto formToInsertDto)
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

            return Ok(forms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetForm(string id)
        {
            var forms = await _formRepository.GetForm<Form>(id);

            return Ok(forms);
        }

        // Simple update for forms, only update Name, Description and Grid Layout.
        [HttpPut("{id}")]
        public async Task<IActionResult> SimpleUpdateForm(string id, FormToUpdateDto formToUpdateDto)
        {
            // Get the form document / object from the DB
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            Form formToUpdate = new Form
            {
                Name = formToUpdateDto.Name,
                Description = formToUpdateDto.Description,
                ColumnRepresentation = formToUpdateDto.ColumnRepresentation
            };

            var updatedForm = await _formRepository.UpdateForm<Form>(id, formToUpdate);

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
