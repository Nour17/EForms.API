using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EForms.API.Dtos.Container;
using EForms.API.Dtos.Question;

namespace EForms.API.Dtos.Section
{
    public class SectionToInsertDto : IContainerToInsertDto
    {
        [Required]
        public string Header { get; set; }
        public string Description { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public int? ColumnRepresentation { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
