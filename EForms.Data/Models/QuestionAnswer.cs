using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Models
{
    public class QuestionAnswer
    {
        public string UserId { get; set; }
        public Answer Answer { get; set; }
    }
}
