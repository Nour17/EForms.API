using EForms.API.Core.Dtos.Form;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EForms.API.Core.Services.Interfaces;
using System;
using Contracts;
using AutoMapper;
using EForms.API.Core.Models;
using EForms.API.Core.Dtos.Answer;
using System.Collections.Generic;
using System.Collections;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    { 
        private readonly IFormService _formService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FormsController(IFormService formService,
                                ILoggerManager logger,
                                IMapper mapper)
        {
            _formService = formService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateForm(FormToInsertDto formToInsertDto)
        {
            try
            {
                // Check availability of at least one question on the entire form either in questions or a specific section
                bool isValid = _formService.IsReceivedFormValid(formToInsertDto);
                if (!isValid)
                {
                    return BadRequest("Incoming Form is invalid: Form must atleast have one question!!");
                }

                var createdForm = await _formService.AddForm(formToInsertDto);

                return Ok(createdForm);
            }
            catch (Exception ex)
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

            // Update Using AutoMapper

            return Ok(fetchedForm);
        }

        [HttpPost("{id}/answer")]
        public async Task<IActionResult> AnswerForm([FromRoute] string id, [FromBody] FormAnswersDto formAnswersDto)
        {
            var fetchedForm = await _formService.GetForm(id);

            try
            {
                // Add all user's answers on one form at once
                var formWithAnswers = _formService.ValidateFormAnswers(fetchedForm, formAnswersDto);
                var isFormUpdated = await _formService.UpdateForm(id, fetchedForm);

                return Ok(isFormUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
