using System;
using System.Globalization;

namespace EForms.API.Core.Extensions {
    public static class StringExtensions{
        public static int StringToIntConverstion(this string baseString)
        {
            int convertedValue;

            // Convert operand value from string to int and if successful copy the value to convertedValue variable
            // else return 0
            if (!(int.TryParse(baseString, out convertedValue)))
                return 0;

            return convertedValue;
        }
        public static DateTime? StringToDateConverstion(this string baseString)
        {
            DateTime convertedValue;
            CultureInfo provider = CultureInfo.InvariantCulture;

            bool isSuccess = DateTime.TryParseExact(baseString, "MM/dd/yyyy", provider, DateTimeStyles.None, out convertedValue);

            // Convert operand value from string to DateTime and if successful copy the value to convertedValue variable
            // else return null
            if(!isSuccess)
                return null;

            return convertedValue;
        }
    }
}