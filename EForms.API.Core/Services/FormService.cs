using AutoMapper;
using EForms.API.Core.Models;
using EForms.API.Core.Models.Interfaces;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Infrastructure.Models;
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

        public async Task<FormCore> AddForm(FormCore formToAdd)
        {
            var infrastructureFormToAdd = _mapper.Map<Form>(formToAdd);

            // Add the form to the DB
            Form addedForm = await _formRepository.AddForm<Form>(infrastructureFormToAdd);

            if (addedForm == null)
                throw new Exception("Adding form to DB failed!!");

            var formToFormCore = _mapper.Map<FormCore>(addedForm);
            return formToFormCore;
        }
        public async Task<FormCore> GetForm(string id)
        {
            var fetchedForm = await _formRepository.GetForm<Form>(id);

            // Check the form existence in the DB
            if (fetchedForm == null)
                throw new Exception("This form doesn't exist!!");

            var formToFormCore = _mapper.Map<FormCore>(fetchedForm);
            return formToFormCore;
        }
        public async Task<bool> UpdateForm(string id, FormCore updatedForm)
        {
            var infrastructureFormToUpdate = _mapper.Map<Form>(updatedForm);

            var isUpdated = await _formRepository.UpdateForm<Form>(id, infrastructureFormToUpdate);

            return isUpdated;
        }
        public async Task<List<FormCore>> GetForms()
        {
            var fetchedForms = await _formRepository.GetForms<Form>();

            // Check the form existence in the DB
            if (fetchedForms == null)
                throw new Exception("No forms exist!!");

            var formsToFormCore = _mapper.Map<List<FormCore>>(fetchedForms);
            return formsToFormCore;
        }
        // Check availability of at least one question on the entire form either in questions or a specific section
        public bool IsValid(FormCore formToInsert)
        {
            // Check if form have any questions
            if (containQuestions<FormCore>(formToInsert))
            {
                return true;
            }

            // Check if form's sections have any questions in atleast one of them
            if (formToInsert.Sections != null)
            {
                foreach (SectionCore sectionToInsert in formToInsert.Sections)
                {
                    if (containQuestions<SectionCore>(sectionToInsert))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private bool containQuestions<T>(IContainerCore questionContainer)
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
