using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Section
{
    public class SectionToInsertDto : IContainerToCreateDto
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Position { get; set; }

        [Required]
        public int ColumnRepresentation { get; set; }
        public List<QuestionToInsertDto> Questions { get; set; }
    }
}
