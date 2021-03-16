using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services
{
    public class FormService : IFormService
    {
        private readonly IQuestionService _questionService;

        public FormService(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public List<FormAnswer> AnswerForm(ref Form form, FormAnswersDto formAnswers)
        {
            FormAnswer userAnswers = new FormAnswer();
            userAnswers.UserId = formAnswers.UserId;

            // Loop over each question's answer
            // Check if the user's answers passes the questions restriction if any
            // If yes add the question
            foreach (FormAnswerDto answerDto in formAnswers.Answers)
            {
                Answer answerToCreate = createAnswer(answerDto.QuestionId, answerDto.Header, answerDto.UserAnswer);
                Question question = _questionService.GetQuestion(form, answerDto.QuestionId);
                bool isAcceptable = false;
                if (question.Restriction != null)
                {
                    isAcceptable = RestrictionsFactory.ApplyRestriction(question.Restriction,
                                                                        answerDto.UserAnswer,
                                                                        question.Restriction.RightOperand,
                                                                        question.Restriction.ExtraOperand);

                    if (isAcceptable == false)
                    {
                        // Create Form errors model to add the error into it
                    }
                }

                userAnswers.Answers.Add(answerToCreate);
            }

            // Empty object to hold the stored sections in the fetchedForm
            var existedFormAnswers = new List<FormAnswer>();

            // Check whether the fethed form is answered before by other users or not
            /*
             * If any answers were already in the fetched form
             * a copy should be done to add to it the newly formAnswers
            */
            if (form.FormAnswers != null)
                existedFormAnswers = form.FormAnswers;

            existedFormAnswers.Add(userAnswers);

            return existedFormAnswers;
        }

        private Answer createAnswer(string questionId, string header, string userAnswer)
        {
            Answer answer = new Answer
            {
                QuestionId = questionId,
                Header = header,
                UserAnswer = userAnswer
            };

            return answer;
        }

        //private Question GetQuestionFromDocument(string sectionId, string questionId)
        //{
        //    var question = new Question();

        //    if (sectionId == null)
        //    {
        //        question = GetQuestion<Form>(ref fetchedForm, questionId);
        //    }
        //    else
        //    {
        //        var section = _sectionService.GetSectionFromForm(ref fetchedForm, sectionId);
        //        question = GetQuestion<Section>(ref section, questionId);
        //    }

        //    return question;
        //}
    }
}
