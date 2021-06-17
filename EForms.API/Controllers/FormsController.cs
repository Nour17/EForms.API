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
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;
using EForms.API.Infrastructure.Models.Answers;
using Answer = EForms.API.Infrastructure.Models.Answer;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    { 
        private readonly IFormService _formService;
        private readonly IAnswerService _answerService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FormsController(IFormService formService,
                                IAnswerService answerService,
                                ILoggerManager logger,
                                IMapper mapper)
        {
            _formService = formService;
            _answerService = answerService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> CreateForm(FormToInsertDto formToInsertDto)
        {
            try
            {
                // Check availability of at least one question on the entire form either in questions or a specific section
                bool isValid = _formService.IsValid(formToInsertDto);
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
        public async Task<IActionResult> AnswerForm([FromRoute] string id, [FromBody] FormAnswersToInsertDto formAnswersDto)
        {
            var fetchedForm = await _formService.GetForm(id);

            try
            {
                var validatedForm = _answerService.ValidateFormAnswers(fetchedForm, formAnswersDto);

                if (!validatedForm.IsValid)
                    return BadRequest(validatedForm.Answers);

                FormAnswers x = new FormAnswers();
                x.UserId = validatedForm.UserId;
                List<Answer> answers = new List<Answer>();
               
                foreach(var answer in validatedForm.Answers)
                {
                    if (answer.Answer != null)
                        answers.Add(new StringAnswer
                        {
                            QuestionId = answer.QuestionId,
                            Answer = answer.Answer
                        });
                    else
                    {
                        answers.Add(new ListOfStringsAnswer
                        {
                            QuestionId = answer.QuestionId,
                            Answers = answer.Answers
                        });
                    }
                }
                x.Answers = answers;
                return Ok(answers);
                fetchedForm.FormAnswers.Add(x);

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
