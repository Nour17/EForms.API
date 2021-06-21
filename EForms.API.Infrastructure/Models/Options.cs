using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Infrastructure.Models
{
    public class Options
    {
        public bool OtherOption { get; set; } = false;
        public List<string> CustomOptions { get; set; }
    }
}
