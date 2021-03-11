using EForms.API.Dtos.Question;
using EForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.Interfaces
{
    public interface IQuestionService
    {
        void InsertQuestion<T>(ref T parentElement, QuestionToInsertDto questionToInsertDto);
        Question GetQuestion<T>(ref T parentElement, string questionId);
        bool CheckAnswer(Question question, string userAnswer);

    }
}
