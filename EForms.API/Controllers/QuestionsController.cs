using EForms.API.Core.Dtos.Question;
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
    public class QuestionsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly ISectionService _sectionService;
        private readonly IQuestionService _questionService;

        public QuestionsController(IFormRepository formRepository,
                                  ISectionService sectionService,
                                  IQuestionService questionService)
        {
            _formRepository = formRepository;
            _sectionService = sectionService;
            _questionService = questionService;
        }

        [HttpPost("form/{formId}/question")]
        public async Task<IActionResult> AddQuestion([FromRoute] string formId, QuestionToInsertDto questionToInsertDto)
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
                var fetchedSection = _sectionService.GetSectionFromForm(ref fetchedForm, questionToInsertDto.SectionId);

                _questionService.InsertQuestion<Section>(ref fetchedSection, questionToInsertDto);
            }


            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

            return Ok(updatedForm);
        }

        [HttpPost("form/{formId}/questionanswer")]
         public async Task<IActionResult> AnswerQuestion([FromRoute] string formId, QuestionToAnswerDto questionToAnswerDto)
         {
             var fetchedForm = await _formRepository.GetForm<Form>(formId);

             // Check the form existence in the DB
             if (fetchedForm == null)
                 return NotFound("This form doesn't exist!!");

             var question = new Question();

             if (questionToAnswerDto.SectionId == null)
             {
                question = _questionService.GetQuestion<Form>(fetchedForm, questionToAnswerDto.QuestionId);
             }
             else
             {
                var section = _sectionService.GetSectionFromForm(ref fetchedForm, questionToAnswerDto.SectionId);
                question = _questionService.GetQuestion<Section>(section, questionToAnswerDto.QuestionId);
             }

            bool isAcceptableAnwser = false;

             if (question.Restriction != null)
                   isAcceptableAnwser = _questionService.CheckAnswer(question, questionToAnswerDto.Answer);

            Answer answer = new Answer
            {
                Header = question.Header,
                UserAnswer = questionToAnswerDto.Answer
            };

            question.QuestionAnswers = updatedQuestionAnswers(questionToAnswerDto.UserId, answer, question);

            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

            return Ok(updatedForm);
        }

        private List<QuestionAnswer> updatedQuestionAnswers(string userId, Answer answer, Question question)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer
            {
                UserId = userId,
                Answer = answer
            };

            var existedAnswersInQuestion = new List<QuestionAnswer>();

            if (question.QuestionAnswers != null)
                existedAnswersInQuestion = question.QuestionAnswers;

            existedAnswersInQuestion.Add(questionAnswer);

            return existedAnswersInQuestion;
        }

        private Form updatedFormAnswers(string userId, Answer answer, Form form, Question question)
        {
            var usersAnswers = form.FormAnswers.FirstOrDefault(x => x.UserId == userId);
            var oldFormId = form.FormAnswers.FindIndex(x => x.UserId == userId);

            if (usersAnswers == null)
            {
                var existedAnswersInUserFormAnswers = new List<Answer>();
                existedAnswersInUserFormAnswers.Add(answer);
                usersAnswers = new FormAnswer
                {
                    UserId = userId,
                    Answers = existedAnswersInUserFormAnswers
                };
            }
            else
            {
                usersAnswers.Answers.Add(answer);
            }


            form.FormAnswers.RemoveAt(oldFormId);
            form.FormAnswers.Insert(oldFormId, usersAnswers);

            return form;
        }
    }
}
