using System;
using System.ComponentModel.DataAnnotations;

namespace EForms.API.Core.Dtos.Restriction
{
    public class RestrictionToAddDto
    {
        public string SectionId { get; set; }
        [Required]
        public string QuestionId { get; set; }
        [Required]
        [Range(1, 21, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int RestrictionType { get; set; }
        [Required]
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
    }
}
