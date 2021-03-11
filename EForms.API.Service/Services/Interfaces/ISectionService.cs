using EForms.API.Dtos.Section;
using EForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.Interfaces
{
    public interface ISectionService
    {
        Section GetSectionFromForm(ref Form form, string sectionId);
        void UpdateSection(ref Form parentForm, ref Section oldSection, SectionToUpdateDto newSection);
    }
}
