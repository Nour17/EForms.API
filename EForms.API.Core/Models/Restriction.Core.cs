using EForms.API.Infrastructure.Enums;

namespace EForms.API.Core.Models
{
    public class RestrictionCore
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
