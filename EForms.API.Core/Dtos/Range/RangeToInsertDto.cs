using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EForms.API.Core.Dtos.Range
{
    public class RangeToInsertDto
    {
        [Required]
        public string LeftLabel { get; set; }
        [Required]
        public string RightLabel { get; set; }
        [Required]
        public int LeftValue { get; set; }
        [Required]
        public int RightValue { get; set; }
    }
}
