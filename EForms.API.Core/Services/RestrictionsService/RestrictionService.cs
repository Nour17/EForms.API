
using System;
using System.Globalization;

namespace EForms.API.Core.Services.RestrictionsService
{
    public abstract class RestrictionService
    {
        public abstract bool checkRestriction(string userAnswer, string rightOperand);
        public abstract bool checkRestriction(string userAnswer, string rightOperand, string extraOperand);
        public static int StringToIntConverstion(string operand)
        {
            int convertedValue;

            // Convert operand value from string to int and if successful copy the value to convertedValue variable
            // else return 0
            if (!(int.TryParse(operand, out convertedValue)))
                return 0;

            return convertedValue;
        }

        public static DateTime? StringToDateConverstion(string operand)
        {
            DateTime convertedValue;
            CultureInfo provider = CultureInfo.InvariantCulture;

            bool isSuccess = DateTime.TryParseExact(operand, "MM/dd/yyyy", provider, DateTimeStyles.None, out convertedValue);

            // Convert operand value from string to DateTime and if successful copy the value to convertedValue variable
            // else return null
            if(isSuccess)
                return null;

            return convertedValue;
        }
    }
}
