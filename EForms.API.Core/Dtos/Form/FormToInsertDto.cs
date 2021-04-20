using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Dtos.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Form
{
    public class FormToInsertDto : IContainerToCreateDto
    {
        [Required]
        public string Header { get; set; }
        public string Description { get; set; }
        [Required]
        public int ColumnRepresentation { get; set; } = 1;
        public int Position { get; set; } = 0;
        [Required]
        public string UserId { get; set; }
        public List<SectionToInsertDto> Sections { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
