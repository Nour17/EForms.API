using EForms.API.Core.Services.ValidationsService.Restrictions;
using EForms.API.Core.Models;
using EForms.API.Infrastructure.Enums;
using EForms.API.Core.Services.ValidationService;
using System;

namespace EForms.API.Core.Services.RestrictionsServices.Factory
{
    public static class RestrictionsFactory
    {
        public static bool ApplyRestriction(RestrictionCore restriction, string userAnswer)
        {
            // Create new instance based on the restriction type
            // var restrictionObject = CreateRestriction(restriction);
            // return restrictionObject.checkRestriction(userAnswer, restriction);

            Type type = Type.GetType(restriction.Condition.ToString());
            var restrictionObject =  (IRestriction)Activator.CreateInstance(type);
            return restrictionObject.checkRestriction(userAnswer, restriction);
        }

        public static IRestriction CreateRestriction(RestrictionCore restriction)
        {
            switch (restriction.Condition)
            {
                // Normal Text Restriction Check
                case RestrictionType.MaxStringLength:
                    return new MaxStringLength();
                case RestrictionType.MinStringLength:
                    return new MinStringLength();
                case RestrictionType.StringContains:
                    return new StringContains();
                case RestrictionType.StringDontContains:
                    return new StringDontContains();
                // Number Restriction Check
                // I rather remove this condition
                case RestrictionType.IsNumberInteger:
                    return new IsNumberInteger();
                case RestrictionType.IsNumberDouble:
                    return new IsNumberDouble();
                case RestrictionType.NumberIsLessThan:
                    return new NumberIsLessThan();
                case RestrictionType.NumberIsLessThanOrEqual:
                    return new NumberIsLessThanOrEqual();
                case RestrictionType.NumberIsBiggerThan:
                    return new NumberIsBiggerThan();
                case RestrictionType.NumberIsBiggerThanOrEqual:
                    return new NumberIsBiggerThanOrEqual();
                case RestrictionType.NumberEqual:
                    return new NumberEqual();
                case RestrictionType.NumberDoNotEqual:
                    return new NumberDoNotEqual();
                case RestrictionType.NumberIsBetween:
                    return new NumberIsBetween();
                case RestrictionType.NumberIsNotBetween:
                    return new NumberIsNotBetween();
                // Date Restriction check
                case RestrictionType.DateIsAfter:
                    return new DateIsAfter();
                case RestrictionType.DateIsEqual:
                    return new DateIsEqual();
                case RestrictionType.DateIsBefore:
                    return new DateIsBefore();
                case RestrictionType.DateIsBetween:
                    return new DateIsBetween();
                case RestrictionType.DateIsNotBetween:
                    return new DateIsNotBetween();
                // Checkbox Restriction check
                case RestrictionType.CheckboxAtLeastChecked:
                    return new CheckboxAtLeastChecked();
                case RestrictionType.CheckboxExactlyChecked:
                    return new CheckboxExactlyChecked();
                case RestrictionType.CheckboxAtMostChecked:
                    return new CheckboxAtMostChecked();
                // Question type restrictions
                case RestrictionType.EmailType:
                    return new EmailType();
                case RestrictionType.URLType:
                    return new URLType();
            }
            return null;
        }
    }
}
