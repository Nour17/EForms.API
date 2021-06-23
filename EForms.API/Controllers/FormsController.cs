using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EForms.API.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using EForms.API.Infrastructure.Models;
using Answer = EForms.API.Infrastructure.Models.Answer;
using EForms.API.Dtos.Form;
using AutoMapper;
using EForms.API.Core.Models;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        public FormsController(IFormService formService,
                                IAnswerService answerService,
                                IMapper mapper)
        {
            _formService = formService;
            _answerService = answerService;
            _mapper = mapper;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateForm(FormToInsertDto formToInsertDto)
        {
            try
            {
                var formCoreFromFormDto = _mapper.Map<FormCore>(formToInsertDto);
                // Check availability of at least one question on the entire form either in questions or a specific section
                bool isValid = _formService.IsValid(formCoreFromFormDto);
                if (!isValid)
                {
                    return BadRequest("Incoming Form is invalid: Form must atleast have one question!!");
                }

                var createdForm = await _formService.AddForm(formCoreFromFormDto);

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

        [HttpPost("{id}/answer")]
        public async Task<IActionResult> AnswerForm([FromRoute] string id, [FromBody] FormAnswersToInsertDto formAnswersDto)
        {
            var fetchedForm = await _formService.GetForm(id);

            try
            {
                var formAnswersCoreFromFormAnswersDto = _mapper.Map<FormAnswersCore>(formAnswersDto);
                var validatedForm = _answerService.ValidateFormAnswers(fetchedForm, formAnswersCoreFromFormAnswersDto);

                if (!validatedForm.IsValid)
                    return BadRequest(validatedForm.Answers);

                fetchedForm.FormAnswers.Add(validatedForm);

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
