using EForms.API.Core.Dtos.Section;
using EForms.API.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Core.Services.Interfaces
{
    public interface ISectionService
    {
        Section GetSectionFromForm(ref Form form, string sectionId);
        void AddSectionToForm(ref Form form, Section section);
    }
}
