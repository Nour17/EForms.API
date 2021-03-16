﻿using EForms.API.Core.Dtos.Question;
using EForms.API.Infrastructure.Models;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IQuestionService
    {
        void InsertQuestion<T>(ref T parentElement, QuestionToInsertDto questionToInsertDto);
        Question GetQuestion<T>(T parentElement, string questionId);
        Question GetQuestion(Form form, string questionId);
        bool CheckAnswer(Question question, string userAnswer);

    }
}
