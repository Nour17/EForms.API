using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Range
{
    public class RangeToAddDto
    {
        public string LeftLabel { get; set; }
        public string RightLabel { get; set; }
        public int LeftValue { get; set; }
        public int RightValue { get; set; }
    }
}
