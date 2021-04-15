using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Option
{
    public class OptionsToAddDto
    {
        public bool OtherOption { get; set; }
        public List<String> CustomOptions { get; set; }
    }
}
