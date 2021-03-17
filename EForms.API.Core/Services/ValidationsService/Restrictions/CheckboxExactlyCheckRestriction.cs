using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class CheckboxExactlyCheckRestriction : ValidateDoubleInput
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int rightOperandToInt = StringToIntConverstion(rightOperand);
            int userAnswerToInt = StringToIntConverstion(userAnswer);

            // Check if rightOperandToInt and user's answer is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0 && userAnswerToInt != 0)
                // Check if the restriction is fullfilled 
                if (userAnswerToInt == rightOperandToInt)
                    return true;

            return false;
        }
    }
}
