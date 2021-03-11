using EForms.API.Core.Dtos.Restriction;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Infrastructure.Models;
using EForms.API.Repository.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RestrictionsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly ISectionService _sectionService;
        private readonly IQuestionService _questionService;

        public RestrictionsController(IFormRepository formRepository,
                                       ISectionService sectionService,
                                       IQuestionService questionService)
        {
            _formRepository = formRepository;
            _sectionService = sectionService;
            _questionService = questionService;
        }

        [HttpPost("form/{formId}")]
        public async Task<IActionResult> UpdateQuestionWithRestriction(string formId, RestrictionToAddDto restrictionToAddDto)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(formId);

            // Check the form existence in the DB
            if (fetchedForm == null)
                return NotFound("This form doesn't exist!!");

            var question = new Question();
            var restrictionToInsert = new Restriction
            {
                Condition = (RestrictionType)restrictionToAddDto.RestrictionType,
                RightOperand = restrictionToAddDto.RightOperand,
                ExtraOperand = restrictionToAddDto.ExtraOperand
            };

            if (restrictionToAddDto.SectionId == null)
            {
                question = _questionService.GetQuestion<Form>(ref fetchedForm, restrictionToAddDto.QuestionId);
            }
            else
            {
                var section = _sectionService.GetSectionFromForm(ref fetchedForm, restrictionToAddDto.SectionId);
                question = _questionService.GetQuestion<Section>(ref section, restrictionToAddDto.QuestionId);
            }

            question.Restriction = restrictionToInsert;


            var updatedForm = await _formRepository.UpdateForm<Form>(formId, fetchedForm);

            return Ok(updatedForm);
        }
    }
}
