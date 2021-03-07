using EForms.API.Dtos.Question;
using EForms.API.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionController(IQuestionRepository questionRepository,
                                   IFormRepository formRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpPost]
        public void AddQuestion([FromRoute] string formId, QuestionToInsertDto questionToInsertDto)
        {
            // Get the section or form document to add the question into it
            // The insertion process steps are:
            /*
             *  1- Get the focused element whether it is a section or a form
             *      a - The element's Id should be sent in the header
             *      b - Retreive the document by Id from the database
             *      c - Check if the document is NOT null
             *  2- Create the Question object with the given data in the payload
             *  3- Update the parent element ( Section - Form ) document with the newly created Question.
             *  4- Add The Question into the Question collection for further analytics if needed.
             *  5- Return approval with the Question's object if created successfuly or error if not.
             */

        }
    }
}
