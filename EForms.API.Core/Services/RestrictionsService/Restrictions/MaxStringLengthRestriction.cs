using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class MaxStringLengthRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int maxLengthToInt;
            // Convert maxlength value from string to int and copy the value to maxLengthToInt variable
            bool stringToIntConversion = StringToIntConverstion(rightOperand, out maxLengthToInt);

            // Check if maxLengthToInt is not 0 and the conversion is done successfuly
            if (maxLengthToInt != 0 && stringToIntConversion)
                // Check if the restriction is fullfilled 
                if (userAnswer.Length < maxLengthToInt)
                    return true;

            return false;
        }
    }
}
