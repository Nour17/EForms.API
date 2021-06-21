using System.Collections.Generic;

namespace EForms.API.Infrastructure.Models.Interfaces
{
    public interface IContainer
    {
        public int ColumnRepresentation { get; set; }
        public List<Question> Questions { get; set; }
    }
}
