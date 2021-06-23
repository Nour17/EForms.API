using EForms.API.Core.Models.Interfaces;
using EForms.API.Infrastructure.Enums;

namespace EForms.API.Core.Models
{
    public class QuestionCore : IElementCore, IContainedCore
    {
        public string InternalId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public bool IsRequired { get; set; } = false;
        public QuestionGenre Genre { get; set; }
        public QuestionType Type { get; set; }
        public OptionsCore Options { get; set; }
        public RangeCore Range { get; set; }
        public RestrictionCore Restriction { get; set; }
        public bool IsOptionBased()
        {
            if (Type == QuestionType.Dropdown
             || Type == QuestionType.RadioButton
             || Type == QuestionType.CheckBox)
                return true;
            return false;
        }
        public bool IsRangeBased()
        {
            if (Type == QuestionType.RangeInput)
                return true;
            return false;
        }
    }
}
