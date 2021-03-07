using System;

namespace EForms.API.Models
{
    public enum RestrictionType
    {
        // Normal Text
        StringLength = 1,
        StringContains = 2,
        // Number
        IsNumber = 3,
        NumberIsLessThan = 4,
        NumberIsLessThanOrEqual = 5,
        NumberIsBiggerThan = 6,
        NumberIsBiggerThanOrEqual = 7,
        NumberEqual = 8,
        NumberDoNotEqual = 9,
        NumberIsBetween = 10,
        NumberIsNotBetween = 11,
        // Date
        DateIsAfter = 12,
        DateEqual = 13,
        DateIsBefore = 14,
        DateIsBetween = 15,
        DateIsNotBetween = 16,
        // Checkbox
        AtLeastChecked = 17,
        ExactlyChecked = 18,
        AtMostChecked = 19
    }

    public class Restriction
    {
        public RestrictionType Condition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
    }
}
