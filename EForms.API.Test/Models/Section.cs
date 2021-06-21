using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Test.Models
{
    public class Section
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public int ColumnRepresentation { get; set; } = 1;
        public List<Question> Questions { get; set; }
    }
}
