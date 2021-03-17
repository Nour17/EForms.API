using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class StringDontContainsRestriction : ValidateDoubleInput
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            if (!userAnswer.Contains(rightOperand))
                return true;
            return false;
        }
    }
}
