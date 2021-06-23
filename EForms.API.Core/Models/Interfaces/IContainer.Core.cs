using System.Collections.Generic;

namespace EForms.API.Core.Models.Interfaces
{
    public interface IContainerCore
    {
        public int ColumnRepresentation { get; set; }
        public List<QuestionCore> Questions { get; set; }
    }
}
