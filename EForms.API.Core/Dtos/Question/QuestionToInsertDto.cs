using EForms.API.Core.Dtos.Option;
using EForms.API.Core.Dtos.Range;
using EForms.API.Core.Dtos.Restriction;
using System;
using System.Collections.Generic;

namespace EForms.API.Core.Dtos.Question
{
    public class QuestionToInsertDto
    {
        // Section Id if question is added into section
        public string SectionId { get; set; }
        // Question related data
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public bool IsRequired  { get; set; }
        public int Genre { get; set; }
        public int Type { get; set; }
        // Options Related Data
        public OptionsToAddDto Options { get; set; }
        // Range Related Data
        public RangeToAddDto Range { get; set; }
        // Restriction realted data
        public RestrictionToAddDto Restriction { get; set; }
    }
}
