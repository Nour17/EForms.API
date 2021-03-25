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
        public List<String> Options { get; set; }
        // Restriction realted data
        public int RestrictionCondition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
        public string CustomErrorMessage { get; set; }
    }
}
