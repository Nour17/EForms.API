using System;
using System.Collections.Generic;
using EForms.API.Core.Models.Interfaces;

namespace EForms.API.Core.Models
{
    public class FormCore : IElementCore, IContainerCore, ITrackerCore
    {
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<SectionCore> Sections { get; set; }
        public List<QuestionCore> Questions { get; set; }
        public List<FormAnswersCore> FormAnswers { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
