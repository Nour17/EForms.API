using System.Collections.Generic;
using EForms.API.Core.Models.Interfaces;

namespace EForms.API.Core.Models
{
    public class SectionCore : IElementCore, IContainerCore, IContainedCore
    {
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<QuestionCore> Questions { get; set; }
    }
}
