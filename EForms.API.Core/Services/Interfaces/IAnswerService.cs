using EForms.API.Core.Dtos.Form;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IAnswerService
    {
        public FormAnswersToReturnDto ValidateFormAnswers(Form form, FormAnswersToInsertDto formAnswers);
    }
}
