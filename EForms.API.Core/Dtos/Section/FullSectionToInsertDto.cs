using EForms.API.Core.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Section
{
    public class FullSectionToInsertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
