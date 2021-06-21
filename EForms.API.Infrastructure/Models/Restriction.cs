namespace EForms.API.Infrastructure.Models
{
    public enum NumberType
    {
        Integer = 1,
        Double = 2
    }

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
        DateEqual = 16,
        DateIsBefore = 17,
        DateIsBetween = 18,
        DateIsNotBetween = 19,
        // Checkbox
        AtLeastChecked = 20,
        ExactlyChecked = 21,
        AtMostChecked = 22,
        // Question Type
        Email = 23,
        URL = 24,
    }

    public class Restriction
    {
        public RestrictionType Condition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
        public string CustomErrorMessage { get; set; } = "Please follow the question requirements";
        public bool HaveExtraOperand()
        {
            if (Condition == RestrictionType.NumberIsLessThanOrEqual
             || Condition == RestrictionType.NumberIsBiggerThanOrEqual
             || Condition == RestrictionType.NumberIsBetween
             || Condition == RestrictionType.NumberIsNotBetween
             || Condition == RestrictionType.DateIsBetween
             || Condition == RestrictionType.DateIsNotBetween)
                return true;
            return false;
        }
    }
}
