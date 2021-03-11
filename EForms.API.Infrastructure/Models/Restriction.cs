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
        IsNumber = 5,
        NumberIsLessThan = 6,
        NumberIsLessThanOrEqual = 7,
        NumberIsBiggerThan = 8,
        NumberIsBiggerThanOrEqual = 9,
        NumberEqual = 10,
        NumberDoNotEqual = 11,
        NumberIsBetween = 12,
        NumberIsNotBetween = 13,
        // Date
        DateIsAfter = 14,
        DateEqual = 15,
        DateIsBefore = 16,
        DateIsBetween = 17,
        DateIsNotBetween = 18,
        // Checkbox
        AtLeastChecked = 19,
        ExactlyChecked = 20,
        AtMostChecked = 21
    }

    public class Restriction
    {
        public RestrictionType Condition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
    }
}
