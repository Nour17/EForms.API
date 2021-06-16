using AutoMapper;
using Contracts;
using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Section;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using EForms.API.Repository.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Core.Services
{
    public class FormService : IFormService
    {
        private readonly ILoggerManager _logger;
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormService(ILoggerManager logger,
                           IFormRepository formRepository,
                           IMapper mapper)
        {
            _logger = logger;
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public async Task<Form> AddForm(FormToInsertDto formToAdd)
        {
            var infrastructureFormToAdd = _mapper.Map<Form>(formToAdd);

            // Add the form to the DB
            var addedForm = await _formRepository.AddForm<Form>(infrastructureFormToAdd);

            if (addedForm == null)
                throw new Exception("Adding form to DB failed!!");

            return addedForm;
        }
        public async Task<Form> GetForm(string id)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                throw new Exception("This form doesn't exist!!");

            return fetchedForm;
        }
        public async Task<bool> UpdateForm(string id, Form updatedForm)
        {
            var form = await _formRepository.UpdateForm<Form>(id, updatedForm);

            return form;
        }
        public async Task<List<Form>> GetForms()
        {
            var fetchedForms = await _formRepository.GetForms<Form>();

            // Check the form existence in the DB
            if (fetchedForms == null)
                throw new Exception("No forms exist!!");

            return fetchedForms;
        }
        // Check availability of at least one question on the entire form either in questions or a specific section
        public bool IsReceivedFormValid(FormToInsertDto formToInsert)
        {
            _logger.LogInfo("Checking validity of Incoming Form");

            // Check if form have any questions
            if (containQuestions<FormToInsertDto>(formToInsert))
            {
                _logger.LogInfo("Incoming Form is valid");
                return true;
            }

            // Check if form's sections have any questions in atleast one of them
            if (formToInsert.Sections != null)
            {
                foreach (SectionToInsertDto sectionToInsert in formToInsert.Sections)
                {
                    if (containQuestions<SectionToInsertDto>(sectionToInsert))
                    {
                        _logger.LogInfo("Incoming Form is valid");
                        return true;
                    }
                }
            }

            _logger.LogError("Incoming Form is invalid");
            return false;
        }
        public Form ValidateFormAnswers(Form form, FormAnswersDto formAnswers)
        {
            /*
             * Answering a form process
             *      1 - Create a new FormAnswer Instance
             *      2 - Create a new List of answers List<Answer>
             *      3 - Create a new List of errors
             *      3 - Loop through the recieved answers
             *          A - Get each answer's question from the received form
             *          b - Apply the question's restriction if any
             *                  I - IF the answer violated the given restriction 
             *                          i - Add the question' Id along with the error message whether it is default in the system or customized by the user
             *                          ii - Add the wrong answer in the List<Answer> to be returned to the User
             *                  II - IF the answer is acceptable
             *                          i - Add the answer to the List<Answer>
             *      4 - IF the List of ErrorMessages is empty ( ADD FormAnswers TO DB AND SHOULD RETURN Http/200 )
             *          A - Add the user's id into the FormAnswer instance
             *          B - Add the List<Answer> into the FormAnswer instance
             *          C - Update the FormAnswer list in the refrenced FORM.
             *      5 - IF the List of ErrorMessages is not empty ( DONT ADD FormAnswers TO DB AND SHOULD RETURN Http/406)
             *      6 - Return the List<ErrorMessages>
             */

            AnsweredForm formAnswerToCreate = new AnsweredForm();

            try
            {
                List<Answer> answers = insertAnswersCasually(form, formAnswers);
                // If all given answers is valid
                // Add the user's id to the formAnswer object
                formAnswerToCreate.UserId = formAnswers.UserId;
                // Add the user's answers to the formAnswer object
                formAnswerToCreate.Answers = answers;
                // Add the formAnswerObject to the existed answers in the form
                form.FormAnswers.Add(formAnswerToCreate);

                return form;
            } catch (Exception)
            {
                // If the form have a single invalid answer return the formError List to the User
                _logger.LogInfo("Validate answered form");
                List<ErrorMessage> errorMessages = catchInvalidAnswer(form, formAnswers);
                string x = "";
                foreach(ErrorMessage errorMessage in errorMessages)
                {
                    x += $"{errorMessage.QuestionId}: {errorMessage.Content}"; 
                }
                throw new Exception(x);
            }
        }
        // Add questions' answers and whenever an invalid answer exist halt (throw exception) and proceed with collecting all the wrong answers25525  
        private List<Answer> insertAnswersCasually(Form form, FormAnswersDto formAnswers)
        {
            List<Answer> answers = new List<Answer>();

            foreach (FormAnswerDto answerDto in formAnswers.Answers)
            {
                Question question = getQuestionFromDocument(form, answerDto.QuestionId);

                if (question.Restriction == null || isAnswerValid(question, answerDto.Answer))
                    answers.Add(createAnswer(answerDto.QuestionId, answerDto.Answer));
                else
                    throw new ArgumentException();
            }

            return answers;
        }
        private List<ErrorMessage> catchInvalidAnswer(Form form, FormAnswersDto formAnswers)
        {
            List<ErrorMessage> errorMessages = new List<ErrorMessage>();

            foreach (FormAnswerDto answerDto in formAnswers.Answers)
            {
                Question question = getQuestionFromDocument(form, answerDto.QuestionId);

                if (question.Restriction != null && !isAnswerValid(question, answerDto.Answer))
                    errorMessages.Add(createErrorMessage(answerDto.QuestionId, question.Restriction.CustomErrorMessage));
            }

            return errorMessages;
        }
        private bool isAnswerValid(Question question, string userAnswer)
        {
            return RestrictionsFactory.ApplyRestriction(question.Restriction, userAnswer);
        }
        private Answer createAnswer(string questionId, string userAnswer)
        {
            try
            {
                return new Answer
                {
                    QuestionId = questionId,
                    Content = userAnswer
                };
            } catch(Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new answer for question {questionId}: {ex.Message}", ex);
            }
        }
        private ErrorMessage createErrorMessage(string questionId, string errorMessage)
        {
            try
            {
                return new ErrorMessage
                {
                    QuestionId = questionId,
                    Content = errorMessage
                };
            } catch(Exception ex)
            {
                throw new System.ArgumentException($"Could not populate the new error message for question {questionId}: {ex.Message}", ex);
            }
        }
        private Question getQuestionFromDocument(Form form, string questionId)
        {
            // If the question found in Form questions
            Question question = form.Questions.Find(x => x.InternalId == questionId);

            // If the question is in a certain section
            if (question == null)
            {
                // loop over all the sections until the question is found
                foreach (Section section in form.Sections)
                {
                    question = section.Questions.Find(x => x.InternalId == questionId);
                    if (question != null)
                        break;
                }
            }

            return question;
        }
        private bool containQuestions<T>(IContainerToCreateDto questionContainer)
        {
            if (questionContainer.Questions != null)
            {
                if (questionContainer.Questions.Count != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
