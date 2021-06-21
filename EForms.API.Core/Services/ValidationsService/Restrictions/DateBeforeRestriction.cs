using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class DateBeforeRestriction : ValidateDoubleInput
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
    }
}
