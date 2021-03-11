using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class MinStringLengthRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int minLengthToInt;
            // Convert maxlength value from string to int and copy the value to maxLengthToInt variable
            bool stringToIntConversion = StringToIntConverstion(rightOperand, out minLengthToInt);

            // Check if maxLengthToInt is not 0 and the conversion is done successfuly
            if (minLengthToInt != 0 && stringToIntConversion)
                // Check if the restriction is fullfilled 
                if (userAnswer.Length > minLengthToInt)
                    return true;

            return false;
        }
    }
}
