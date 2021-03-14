using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Dtos.Section;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Form
{
    public class FullFormToInsertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
        public string UserId { get; set; }
        public List<FullSectionToInsertDto> Sections { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
