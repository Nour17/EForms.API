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

        public void UpdateSimpleSection(ref Section oldSection, SectionToUpdateDto newSection)
        {
            // Check each value if sent or not and then proceed with the replacement.
            if (newSection.Name != null)
                oldSection.Name = newSection.Name;

            if (newSection.Description != null)
                oldSection.Description = newSection.Description;

            if (newSection.ColumnRepresentation != 0)
                oldSection.ColumnRepresentation = newSection.ColumnRepresentation;
        }
    }
}