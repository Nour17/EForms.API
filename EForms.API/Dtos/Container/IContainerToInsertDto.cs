using System.Collections.Generic;
using EForms.API.Dtos.Question;

namespace EForms.API.Dtos.Container
{
    public interface IContainerToInsertDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int? ColumnRepresentation { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
