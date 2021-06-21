namespace EForms.API.Core.Models
{
    public class Restriction
    {
        public int Condition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
        public string CustomErrorMessage { get; set; }
    }
}
