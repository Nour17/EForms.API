using EForms.API.Core.Dtos.Container;

namespace EForms.API.Core.Dtos.Section
{
    public class SectionToUpdateDto : IContainerToUpdateDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int ColumnRepresentation { get; set; }
    }
}
