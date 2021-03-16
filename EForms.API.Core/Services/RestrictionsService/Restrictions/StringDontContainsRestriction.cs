using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class StringDontContainsRestriction : ValidationService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            if (!userAnswer.Contains(rightOperand))
                return true;
            return false;
        }

        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            return false;
        }
    }
}
