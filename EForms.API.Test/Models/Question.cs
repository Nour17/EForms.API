using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Test.Models
{
    public class Question
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public bool IsRequired { get; set; } = false;
        public int Genre { get; set; }
        public int Type { get; set; }
        public Options Options { get; set; }
        public Range Range { get; set; }
        public Restriction Restriction { get; set; }
    }
}
