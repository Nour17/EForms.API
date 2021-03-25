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
            form.Sections.Add(section);
        }

        public Section GetSectionFromForm(ref Form form, string sectionId)
        {
            return form.Sections.FirstOrDefault(x => x.InternalId == sectionId);
        }
    }
}