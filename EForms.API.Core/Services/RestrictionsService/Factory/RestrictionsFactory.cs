using EForms.API.Core.Services.RestrictionsService;
using EForms.API.Infrastructure.Models;
using EForms.API.Core.Services.RestrictionsService.Restrictions;

namespace EForms.API.Core.Services.RestrictionsServices.Factory
{
    public static class RestrictionsFactory
    {
        public static RestrictionService CreateRestriction(Restriction restriction)
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
                    return new StringContainsRestriction();
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
