using EForms.API.Dtos.Question;
using EForms.API.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EForms.API.Models;
using EForms.API.Services.Interfaces;

namespace EForms.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly ISectionService _sectionService;
        private readonly IQuestionService _questionService;

        public QuestionController(IFormRepository formRepository,
                                  ISectionService sectionService,
                                  IQuestionService questionService)
        {
            _formRepository = formRepository;
            _sectionService = sectionService;
            _questionService = questionService;
        }

        [HttpPost("form/{formId}/question")]
        public async Task<IActionResult> AddQuestionTest([FromRoute] string formId, QuestionToInsertDto questionToInsertDto)
        {
            // The insertion process steps are:
            /*
             *  1- Get the form element
             *      a - The element's Id should be sent in the header
             *      b - Retreive the document by Id from the database
             *      c - Check if the document is NOT null
             *  2- Check whether the insertion is in form or section
             *  3- IF SECTION: Get the intended section from the form
             *  4- Create the Question object with the given data in the payload
             *  5- IF SECTION: Update the Section property with the new section
             *  6- Update the Form document.
             *  7- Return approval with the Question's object if created successfuly or error if not.
             */

            var fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            if (questionToInsertDto.SectionId == null)
            {
                _questionService.InsertQuestion<Form>(ref fetchedForm, questionToInsertDto);
            }
            else
            {
                var section = _sectionService.GetSectionFromForm(ref fetchedForm, questionToInsertDto.SectionId);

                _questionService.InsertQuestion<Section>(ref section, questionToInsertDto);
            }


            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

            return Ok(updatedForm);
        }

        [HttpPost("form/{formId}/answer")]
         public async Task<IActionResult> AnswerQuestion([FromRoute] string formId, QuestionToAnswerDto questionToAnswerDto)
         {
             var fetchedForm = await _formRepository.GetForm<Form>(formId);

             // Check the form existence in the DB
             if (fetchedForm == null)
                 return NotFound("This form doesn't exist!!");

             if (questionToAnswerDto.SectionId == null)
             {
                var question = _questionService.GetQuestion<Form>(ref fetchedForm, questionToAnswerDto.QuestionId);
                return Ok(question);
             }
             else
             {
                var section = _sectionService.GetSectionFromForm(ref fetchedForm, questionToAnswerDto.SectionId);
                var question = _questionService.GetQuestion<Section>(ref section, questionToAnswerDto.QuestionId);
                return Ok(question);
             }
         }
    }
}
