using EForms.API.Core.Dtos.Container;

namespace EForms.API.Core.Dtos.Form
{
    public class FormToUpdateDto : IContainerToUpdateDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
    }
}
