using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models
{
    public class FormAnswer
    {
        public string UserId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
