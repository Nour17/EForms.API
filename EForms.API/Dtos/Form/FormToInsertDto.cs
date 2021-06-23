using EForms.API.Dtos.Container;
using EForms.API.Dtos.Question;
using EForms.API.Dtos.Section;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EForms.API.Dtos.Form
{
    public class FormToInsertDto : IContainerToInsertDto
    {
        [Required]
        public string Header { get; set; }
        public string Description { get; set; }
        [Required]
        public int? ColumnRepresentation { get; set; } = 1;
        public int Position { get; set; } = 0;
        [Required]
        public string UserId { get; set; }
        public List<SectionToInsertDto> Sections { get; set; } = null;
        public List<QuestionToInsertDto> Questions { get; set; } = null;
        List<QuestionToInsertDto> IContainerToInsertDto.Questions { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
