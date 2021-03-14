using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class MaxStringLengthRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int rightOperandToInt = StringToIntConverstion(rightOperand);

            // Check if rightOperandToInt is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0)
                // Check if the restriction is fullfilled 
                if (userAnswer.Length < rightOperandToInt)
                    return true;

            return false;
        }

        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            return false;
        }
    }
}
