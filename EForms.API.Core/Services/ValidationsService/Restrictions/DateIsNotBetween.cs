using EForms.API.Core.Extensions;
using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;
using System;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class DateIsNotBetween : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            DateTime? rightOperandToDateTime = restriction.RightOperand.StringToDateConverstion();
            DateTime? extraOperandToDateTime = restriction.ExtraOperand.StringToDateConverstion();
            DateTime? userAnswerToDateTime = userAnswer.StringToDateConverstion();

            // Check if rightOperandToDateTime and user's answer is not null as it is an indicator that the conversion is done successfuly
            if (rightOperandToDateTime != null && extraOperandToDateTime != null && userAnswerToDateTime != null)
                // Check if the restriction is fullfilled 
                if (userAnswerToDateTime < rightOperandToDateTime || userAnswerToDateTime > extraOperandToDateTime)
                    return true;

            return false;
        }
    }
}
