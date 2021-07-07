using EForms.API.Core.Extensions;
using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class CheckboxExactlyChecked : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            int rightOperandToInt = restriction.RightOperand.StringToIntConverstion();
            int numberOfChosenAnswers = userAnswer.StringToIntConverstion();

            // Check if rightOperandToInt and user's answer is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0 && numberOfChosenAnswers != 0)
                // Check if the restriction is fullfilled 
                if (numberOfChosenAnswers == rightOperandToInt)
                    return true;

            return false;
        }
    }
}
