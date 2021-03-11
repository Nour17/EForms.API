using EForms.API.Services.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.RestrictionsServcie.Restrictions
{
    public class MaxStringLengthRestriction : RestrictionAbstract
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int maxLengthToInt = 0;
            // Convert maxlength value from string to int and copy the value to maxLengthToInt variable
            bool stringToIntConversion = int.TryParse(rightOperand, out maxLengthToInt);

            // Check if maxLengthToInt is not 0 and the conversion is done successfuly
            if (maxLengthToInt != 0 && stringToIntConversion)
                // Check if the restriction is fullfilled 
                if (userAnswer.Length < maxLengthToInt)
                    return true;

            return false;
        }
    }
}
