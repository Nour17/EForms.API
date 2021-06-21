using EForms.API.Core.Dtos.Option;
using EForms.API.Core.Dtos.Range;
using EForms.API.Core.Dtos.Restriction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EForms.API.Core.Dtos.Question
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
