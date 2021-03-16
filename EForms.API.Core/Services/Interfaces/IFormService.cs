using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Question;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IFormService
    {
        List<FormAnswer> AnswerForm(ref Form form, FormAnswersDto formAnswers);
    }
}
