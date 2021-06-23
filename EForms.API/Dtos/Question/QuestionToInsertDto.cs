using System.ComponentModel.DataAnnotations;
using EForms.API.Dtos.Option;
using EForms.API.Dtos.Range;
using EForms.API.Dtos.Restriction;

namespace EForms.API.Dtos.Question
{
    public class QuestionToInsertDto
    {
        // Question related data
        [Required]
        public string Header { get; set; }
        public string Description { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public bool IsRequired { get; set; } = false;
        public int Genre { get; set; }
        [Required]
        public int Type { get; set; }
        // Options Related Data
        public OptionsToAddDto Options { get; set; }
        // Range Related Data
        public RangeToInsertDto Range { get; set; }
        // Restriction realted data
        public RestrictionToInsertDto Restriction { get; set; }
    }
}
