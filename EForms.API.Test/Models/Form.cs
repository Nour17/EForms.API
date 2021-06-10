using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Test.Models
{
    public class Form
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; } = 1;
        public int Position { get; set; } = 0;
        public string UserId { get; set; }
        public List<Section> Sections { get; set; } = null;
        public List<Question> Questions { get; set; } = null;
    }
}
