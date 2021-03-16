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
        public void AddSectionToForm(ref Form form, Section section)
        {
            // Empty object to hold the stored sections in the fetchedForm
            var existedSections = new List<Section>();

            // Check whether the fethed form have any previous section or not
            /*
             * If any sections were already in the fetched form
             * a copy should be done to add to it the newly section
            */
            if (form.Sections != null)
                existedSections = form.Sections;

            existedSections.Add(section);

            form.Sections = existedSections;
        }

        public Section GetSectionFromForm(ref Form form, string sectionId)
        {
            return form.Sections.FirstOrDefault(x => x.InternalId == sectionId);
        }
    }
}