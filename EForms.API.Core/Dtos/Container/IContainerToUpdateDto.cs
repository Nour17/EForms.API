using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Dtos.Container
{
    public interface IContainerToUpdateDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
    }
}
