using EForms.API.Core.Extensions;
using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class MaxStringLength : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            int rightOperandToInt = restriction.RightOperand.StringToIntConverstion();

            // Check if rightOperandToInt is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0)
                // Check if the restriction is fullfilled 
                if (userAnswer.Length < rightOperandToInt)
                    return true;

            return false;
        }
    }
}
