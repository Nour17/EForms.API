using EForms.API.Core.Services.ValidationsService;
using EForms.API.Infrastructure.Models;
using EForms.API.Core.Services.ValidationsService.Restrictions;
using System;
using System.Globalization;
using EForms.API.Core.Services.ValidationsService.Abstractions;

namespace EForms.API.Core.Services.RestrictionsServices.Factory
{
    public static class RestrictionsFactory
    {
        public static bool ApplyRestriction(Restriction restriction, string userAnswer)
        {
            // Create new instance based on the restriction type
            var restrictionObject = CreateRestriction(restriction);

            if (restrictionObject == null)
            {
                return false;
            }

            if(string.IsNullOrWhiteSpace(restriction.RightOperand) && string.IsNullOrWhiteSpace(restriction.ExtraOperand))
            {
                // Call the checkRestriction with one parameter which dont include any operand to be checked with
                ValidateSingleInput checker = (ValidateSingleInput)restrictionObject;
                return checker.checkRestriction(userAnswer);
            }
            // Check if the restriction type have extraOperand
            else if (restriction.HaveExtraOperand())
            {
                // If yes validate the extraOperand sent
                if (string.IsNullOrWhiteSpace(restriction.ExtraOperand))
                    return false;
                // Call the checkRestriction with three parameters which include the extra operand logic
                ValidateTripleInput checker = (ValidateTripleInput)restrictionObject;
                return checker.checkRestriction(userAnswer, restriction.RightOperand, restriction.ExtraOperand);
            }
            else 
            {
                // If no Call the checkRestriction with two parameters which exclude the extra operand logic
                ValidateDoubleInput checker = (ValidateDoubleInput)restrictionObject;
                return checker.checkRestriction(userAnswer, restriction.RightOperand);
            }
        }

        public static ValidationService CreateRestriction(Restriction restriction)
        {
            switch (restriction.Condition)
            {
                // Normal Text Restriction Check
                case RestrictionType.MaxStringLength:
                    return new MaxStringLengthRestriction();
                case RestrictionType.MinStringLength:
                    return new MinStringLengthRestriction();
                case RestrictionType.StringContains:
                    return new StringContainsRestriction();
                case RestrictionType.StringDontContains:
                    return new StringDontContainsRestriction();
                // Number Restriction Check
                // I rather remove this condition
                case RestrictionType.IsNumberInteger:
                    return new NumberTypeIsIntegerRestriction();
                case RestrictionType.IsNumberDouble:
                    return new NumberTypeIsDoubleRestriction();
                case RestrictionType.NumberIsLessThan:
                    return new NumberLessThanRestriction();
                case RestrictionType.NumberIsLessThanOrEqual:
                    return new NumberLessThanOrEqualRestriction();
                case RestrictionType.NumberIsBiggerThan:
                    return new NumberBiggerThanRestriction();
                case RestrictionType.NumberIsBiggerThanOrEqual:
                    return new NumberBiggerThanOrEqualRestriction();
                case RestrictionType.NumberEqual:
                    return new NumberEqualRestriction();
                case RestrictionType.NumberDoNotEqual:
                    return new NumberNotEqualRestriction();
                case RestrictionType.NumberIsBetween:
                    return new NumberBetweenRestriction();
                case RestrictionType.NumberIsNotBetween:
                    return new NumberNotBetweenRestriction();
                // Date Restriction check
                case RestrictionType.DateIsAfter:
                    return new DateAfterRestriction();
                case RestrictionType.DateEqual:
                    return new DateEqualRestriction();
                case RestrictionType.DateIsBefore:
                    return new DateBeforeRestriction();
                case RestrictionType.DateIsBetween:
                    return new DateBetweenRestriction();
                case RestrictionType.DateIsNotBetween:
                    return new DateNotBetweenRestriction();
                // Checkbox Restriction check
                case RestrictionType.AtLeastChecked:
                    return new CheckboxAtLeastCheckRestriction();
                case RestrictionType.ExactlyChecked:
                    return new CheckboxAtMostCheckRestriction();
                case RestrictionType.AtMostChecked:
                    return new CheckboxExactlyCheckRestriction();
                // Question type restrictions
                case RestrictionType.Email:
                    return new EmailTypeRestriction();
                case RestrictionType.URL:
                    return new URLTypeRestriction();
            }
            return null;
        }

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
            if (isSuccess)
                return null;

            return convertedValue;
        }
    }
}
