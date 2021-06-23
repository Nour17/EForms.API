using System;
using System.Collections.Generic;

namespace EForms.API.Dtos.Option
{
    public class OptionsToAddDto
    {
        public bool OtherOption { get; set; }
        public List<String> CustomOptions { get; set; }
    }
}
