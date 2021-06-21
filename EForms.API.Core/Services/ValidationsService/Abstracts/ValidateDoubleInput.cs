using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.ValidationsService.Abstractions
{
    public abstract class ValidateDoubleInput : ValidationService
    {
        public abstract bool checkRestriction(string userAnswer, string rightOperand);
    }
}
