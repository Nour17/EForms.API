using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class CheckboxAtLeastCheckRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int rightOperandToInt = StringToIntConverstion(rightOperand);
            int userAnswerToInt = StringToIntConverstion(userAnswer);

            // Check if rightOperandToInt and user's answer is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0 && userAnswerToInt != 0)
                // Check if the restriction is fullfilled 
                if (userAnswerToInt >= rightOperandToInt)
                    return true;

            return false;
        }
        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            return false;
        }
    }
}
