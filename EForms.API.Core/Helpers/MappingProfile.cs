using AutoMapper;
using EForms.API.Core.Dtos.Form;
using EForms.API.Core.Dtos.Option;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Dtos.Range;
using EForms.API.Core.Dtos.Restriction;
using EForms.API.Core.Dtos.Section;
using EForms.API.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace EForms.API.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FormToInsertDto, Models.Form>();
            CreateMap<SectionToInsertDto, Models.Section>()
                        .BeforeMap((s, d) => d.InternalId = ObjectId.GenerateNewId().ToString());
            CreateMap<QuestionToInsertDto, Models.Question>()
                        .BeforeMap((s, d) => d.InternalId = ObjectId.GenerateNewId().ToString());
            CreateMap<OptionsToAddDto, Models.Options>();
            CreateMap<RangeToAddDto, Models.Range>();
            CreateMap<RestrictionToAddDto, Models.Restriction>();

            CreateMap<Models.Form, Infrastructure.Models.Form>();
            CreateMap<Models.Section, Infrastructure.Models.Section>();
            CreateMap<Models.Question, Infrastructure.Models.Question>();
            CreateMap<Models.Options, Infrastructure.Models.Options>();
            CreateMap<Models.Range, Infrastructure.Models.Range>();
            CreateMap<Models.Restriction, Infrastructure.Models.Restriction>();
        }
    }
}
