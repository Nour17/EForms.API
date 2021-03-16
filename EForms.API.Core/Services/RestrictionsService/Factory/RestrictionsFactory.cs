using EForms.API.Core.Services.RestrictionsService;
using EForms.API.Infrastructure.Models;
using EForms.API.Core.Services.RestrictionsService.Restrictions;

namespace EForms.API.Core.Services.RestrictionsServices.Factory
{
    public static class RestrictionsFactory
    {
        public static bool ApplyRestriction(Restriction restriction, 
                                            string userAnswer, 
                                            string rightOperand, 
                                            string extraOperand = null)
        {
            // Create new instance based on the restriction type
            var restrictionChecker = CreateRestriction(restriction);

            if (restrictionChecker == null)
            {
                return false;
            }

            // Check if the restriction type have extraOperand
            if (restriction.HaveExtraOperand())
            {
                // If yes validate the extraOperand sent
                if (string.IsNullOrWhiteSpace(extraOperand))
                    return false;
                // Call the checkRestriction with three parameters which include the extra operand logic
                return restrictionChecker.checkRestriction(userAnswer, rightOperand, extraOperand);
            }
            else
            {
                // If no Call the checkRestriction with two parameters which exclude the extra operand logic
                return restrictionChecker.checkRestriction(userAnswer, rightOperand);
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
                case RestrictionType.IsNumber:
                    return new NumberTypeRestriction();
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
            }
            return null;
        }
    }
}
