using EForms.API.Infrastructure.Models;
using EForms.API.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using EForms.API.Core.Dtos.Section;
using System.Linq;

namespace EForms.API.Core.Services
{
    public class SectionService : ISectionService
    {
        public Section GetSectionFromForm(ref Form form, string sectionId)
        {
            return form.Sections.FirstOrDefault(x => x.InternalId == sectionId);
        }

        public void UpdateSection(ref Form parentForm, ref Section oldSection, SectionToUpdateDto newSection)
        {
            // Simple Section Update Logic:
            /*
             *  1- (ALERT) BUG TO FIX - Copy the unchanged properties from the OLD section to the NEW one
             *  2- Get the OLD section Index from the sections List
             *  3- Remove the OLD section from the List
             *  4- Add the NEW section in the same index of the old one
            */
            Section sectionToUpdate = new Section
            {
                InternalId = oldSection.InternalId,
                Name = newSection.Name != null ? newSection.Name : oldSection.Name,
                Description = newSection.Description != null ? newSection.Description : oldSection.Description,
                ColumnRepresentation = newSection.ColumnRepresentation != 0 ? newSection.ColumnRepresentation : oldSection.ColumnRepresentation,
                Questions = oldSection.Questions
            };

            var oldSectionId = parentForm.Sections.IndexOf(oldSection);
            parentForm.Sections.Remove(oldSection);
            parentForm.Sections.Insert(oldSectionId, sectionToUpdate);
        }
    }
}