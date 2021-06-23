using System.ComponentModel.DataAnnotations;

namespace EForms.API.Dtos.Range
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
