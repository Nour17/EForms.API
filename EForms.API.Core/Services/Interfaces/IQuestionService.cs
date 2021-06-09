﻿using EForms.API.Core.Dtos.Question;
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IQuestionService
    {
        Question CreateQuestion(QuestionToInsertDto questionToInsertDto);
        Question GetQuestion<T>(T parentElement, string questionId);
    }
}
