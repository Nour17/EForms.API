using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Question;
using EForms.API.Infrastructure.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.Interfaces
{
    public interface IContainerService    {
        IContainerElement PopulateContainer<T>(IContainerToCreateDto containerToInsertDto);
        IContainerElement AddListOfQuestions(IContainerElement container, List<QuestionToInsertDto> questionsToInsertDto);
        void SimpleUpdateContainer<T>(ref T oldContainer, IContainerToUpdateDto newContainer);
    }
}
