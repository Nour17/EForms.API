using EForms.API.Core.Extensions;
using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;
using System;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class DateIsBefore : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            DateTime? rightOperandToDateTime = restriction.RightOperand.StringToDateConverstion();
            DateTime? userAnswerToDateTime = userAnswer.StringToDateConverstion();

            // Check if rightOperandToDateTime and user's answer is not null as it is an indicator that the conversion is done successfuly
            if (userAnswerToDateTime != null && rightOperandToDateTime != null)
                // Check if the restriction is fullfilled 
                if (userAnswerToDateTime < rightOperandToDateTime)
                    return true;

            return false;
        }
    }
}
