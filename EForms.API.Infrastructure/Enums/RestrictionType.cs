namespace EForms.API.Infrastructure.Enums 
{
     public enum RestrictionType
    {
        // Normal Text
        MaxStringLength = 1,
        MinStringLength = 2,
        StringContains = 3,
        StringDontContains = 4,
        // Number
        IsNumberInteger = 5,
        IsNumberDouble = 6,
        NumberIsLessThan = 7,
        NumberIsLessThanOrEqual = 8,
        NumberIsBiggerThan = 9,
        NumberIsBiggerThanOrEqual = 10,
        NumberEqual = 11,
        NumberDoNotEqual = 12,
        NumberIsBetween = 13,
        NumberIsNotBetween = 14,
        // Date
        DateIsAfter = 15,
        DateIsEqual = 16,
        DateIsBefore = 17,
        DateIsBetween = 18,
        DateIsNotBetween = 19,
        // Checkbox
        CheckboxAtLeastChecked = 20,
        CheckboxExactlyChecked = 21,
        CheckboxAtMostChecked = 22,
        // Question Type
        EmailType = 23,
        URLType = 24,
    }
}