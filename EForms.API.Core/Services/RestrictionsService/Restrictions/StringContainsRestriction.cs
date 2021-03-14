using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class StringContainsRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            if (userAnswer.Contains(rightOperand))
                return true;
            return false;
        }

        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            return false;
        }
    }
}
