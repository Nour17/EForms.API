using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models
{
    public class FormAnswer
    {
        public string UserId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
