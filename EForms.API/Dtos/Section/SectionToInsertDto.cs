using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Dtos.Section
{
    public class SectionToInsertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
    }
}
