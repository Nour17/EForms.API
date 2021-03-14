using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class DateBetweenRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            return false;
        }

        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            DateTime? rightOperandToDateTime = StringToDateConverstion(rightOperand);
            DateTime? extraOperandToDateTime = StringToDateConverstion(extraOperand);
            DateTime? userAnswerToDateTime = StringToDateConverstion(userAnswer);

            // Check if rightOperandToDateTime and user's answer is not null as it is an indicator that the conversion is done successfuly
            if (rightOperandToDateTime != null && extraOperandToDateTime != null && userAnswerToDateTime != null)
                // Check if the restriction is fullfilled 
                if (rightOperandToDateTime < userAnswerToDateTime && userAnswerToDateTime > extraOperandToDateTime)
                    return true;

            return false;
        }
    }
}
