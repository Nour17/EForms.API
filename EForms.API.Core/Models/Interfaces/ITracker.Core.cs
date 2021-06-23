using System;

namespace EForms.API.Core.Models.Interfaces
{
    public interface ITrackerCore
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
