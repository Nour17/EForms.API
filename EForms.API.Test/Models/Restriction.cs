using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Test.Models
{
    public class Restriction
    {
        public int Condition { get; set; }
        public string RightOperand { get; set; }
        public string ExtraOperand { get; set; }
        public string CustomErrorMessage { get; set; }
    }
}
