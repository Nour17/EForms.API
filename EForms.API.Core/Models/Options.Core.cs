using System.Collections.Generic;

namespace EForms.API.Core.Models
{
    public class OptionsCore
    {
        public bool OtherOption { get; set; } = false;
        public List<string> CustomOptions { get; set; }
    }
}
