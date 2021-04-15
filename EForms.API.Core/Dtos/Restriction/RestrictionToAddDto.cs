using System;
using System.ComponentModel.DataAnnotations;

namespace EForms.API.Core.Dtos.Restriction
{
    public class RestrictionToAddDto
    {
        [Required]
        [Range(1, 21, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Condition { get; set; }
        [Required]
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
        public string CustomErrorMessage { get; set; }
    }
}
