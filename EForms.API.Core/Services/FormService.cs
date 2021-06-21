using AutoMapper;
using EForms.API.Core.Dtos.Answer;
using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Section;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Core.Services.RestrictionsServices.Factory;
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Answers;
using EForms.API.Infrastructure.Models.Interfaces;
using EForms.API.Repository.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EForms.API.Core.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormService(IFormRepository formRepository,
                           IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public async Task<Form> AddForm(FormToInsertDto formToAdd)
        {
            var infrastructureFormToAdd = _mapper.Map<Form>(formToAdd);

            // Add the form to the DB
            var addedForm = await _formRepository.AddForm<Form>(infrastructureFormToAdd);

            if (addedForm == null)
                throw new Exception("Adding form to DB failed!!");

            return addedForm;
        }
        public async Task<Form> GetForm(string id)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                throw new Exception("This form doesn't exist!!");

            return fetchedForm;
        }
        public async Task<bool> UpdateForm(string id, Form updatedForm)
        {
            var form = await _formRepository.UpdateForm<Form>(id, updatedForm);

            return form;
        }
        public async Task<List<Form>> GetForms()
        {
            var fetchedForms = await _formRepository.GetForms<Form>();

            // Check the form existence in the DB
            if (fetchedForms == null)
                throw new Exception("No forms exist!!");

            return fetchedForms;
        }
        // Check availability of at least one question on the entire form either in questions or a specific section
        public bool IsValid(FormToInsertDto formToInsert)
        {
            // Check if form have any questions
            if (containQuestions<FormToInsertDto>(formToInsert))
            {
                return true;
            }

            // Check if form's sections have any questions in atleast one of them
            if (formToInsert.Sections != null)
            {
                foreach (SectionToInsertDto sectionToInsert in formToInsert.Sections)
                {
                    if (containQuestions<SectionToInsertDto>(sectionToInsert))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private bool containQuestions<T>(IContainerToInsertDto questionContainer)
        {
            if (questionContainer.Questions != null)
            {
                if (questionContainer.Questions.Count != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
