using EForms.API.Core.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Container
{
    public interface IContainerToCreateDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
