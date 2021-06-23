using AutoMapper;
using EForms.API.Core.Models;
using EForms.API.Dtos.Answer;
using EForms.API.Dtos.Form;
using EForms.API.Dtos.Option;
using EForms.API.Dtos.Question;
using EForms.API.Dtos.Range;
using EForms.API.Dtos.Restriction;
using EForms.API.Dtos.Section;
using EForms.API.Infrastructure.Models;
using MongoDB.Bson;

namespace EForms.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Dtos To Core
            CreateMap<FormToInsertDto, FormCore>();
            CreateMap<SectionToInsertDto, SectionCore>();
            CreateMap<QuestionToInsertDto, QuestionCore>();
            CreateMap<OptionsToAddDto, OptionsCore>();
            CreateMap<RangeToInsertDto, RangeCore>();
            CreateMap<RestrictionToInsertDto, RestrictionCore>();
            CreateMap<FormAnswersToInsertDto, FormAnswersCore>();
            CreateMap<AnswerToInsertDto, AnswerCore>();

            // Core To Infrastructure
            CreateMap<FormCore, Form>().ReverseMap();
            CreateMap<SectionCore, Section>().ReverseMap()
                        .BeforeMap((s, d) => d.InternalId = ObjectId.GenerateNewId().ToString());
            CreateMap<QuestionCore, Question>().ReverseMap()
                        .BeforeMap((s, d) => d.InternalId = ObjectId.GenerateNewId().ToString());
            CreateMap<OptionsCore, Options>().ReverseMap();
            CreateMap<RangeCore, Range>().ReverseMap();
            CreateMap<RestrictionCore, Restriction>().ReverseMap();
            CreateMap<FormAnswersCore, FormAnswers>().ReverseMap();
            CreateMap<AnswerCore, Answer>().ReverseMap();
        }
    }
}
