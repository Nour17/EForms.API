using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class NumberBetweenRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            return false;
        }
        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            int rightOperandToInt = StringToIntConverstion(rightOperand);
            int extraOperandToInt = StringToIntConverstion(extraOperand);
            int userAnswerToInt = StringToIntConverstion(userAnswer);

            // Check if rightOperandToInt, extraOperandToInt and user's answer is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0 && userAnswerToInt != 0 && extraOperandToInt != 0)
                // Check if the restriction is fullfilled 
                if (rightOperandToInt < userAnswerToInt && userAnswerToInt > extraOperandToInt)
                    return true;

            return false;
        }
    }
}
