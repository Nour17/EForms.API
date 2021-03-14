using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class DateBeforeRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            DateTime? rightOperandToDateTime = StringToDateConverstion(rightOperand);
            DateTime? userAnswerToDateTime = StringToDateConverstion(userAnswer);

            // Check if rightOperandToDateTime and user's answer is not null as it is an indicator that the conversion is done successfuly
            if (userAnswerToDateTime != null && rightOperandToDateTime != null)
                // Check if the restriction is fullfilled 
                if (userAnswerToDateTime < rightOperandToDateTime)
                    return true;

            return false;
        }

        public override bool checkRestriction(string userAnswer, string rightOperand, string extraOperand)
        {
            return false;
        }
    }
}
