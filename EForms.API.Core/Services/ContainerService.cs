using Contracts;
using EForms.API.Core.Dtos.Container;
using EForms.API.Core.Dtos.Question;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Infrastructure.Models;
using EForms.API.Infrastructure.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IQuestionService _questionService;
        private readonly ILoggerManager _logger;

        public ContainerService(IQuestionService questionService, ILoggerManager logger)
        {
            _questionService = questionService;
            _logger = logger;
        }

        public IContainerElement PopulateContainer<T>(IContainerToCreateDto containerToInsertDto)
        {
            try
            {
                IContainerElement container = null;

                // Check for the passed generic class whether it is form or section
                Type containerType = typeof(T);
                if (containerType == typeof(Form))
                {
                    container = new Form();
                }
                else if (containerType == typeof(Section))
                {
                    container = new Section();
                }

                _logger.LogInfo($"Populating {containerType.Name} with basic data");

                // Create new Container object with the incoming properties from the request payload
                container.Header = containerToInsertDto.Header;
                container.Description = containerToInsertDto.Description;
                container.Position = containerToInsertDto.Position;
                container.ColumnRepresentation = containerToInsertDto.ColumnRepresentation;

                return container;
            } catch (Exception ex)
            {
                _logger.LogError($"Populating Failed because of {ex}");
                throw new Exception($"Populating Failed because of {ex.Message}");
            }
            
        }
        public IContainerElement AddListOfQuestions(IContainerElement container, List<QuestionToInsertDto> questionsToInsertDto)
        {
            try
            {
                _logger.LogInfo($"Adding list of question into {container.GetType().Name}");

                // Loop through the Questions in the incoming request
                foreach (QuestionToInsertDto questionToInsertDto in questionsToInsertDto)
                {
                    // Add each question individualy into the form
                    var question = _questionService.InsertQuestion(container, questionToInsertDto);
                }

                return container;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Adding questions Failed because of {ex}");
                throw new Exception($"Adding questions Failed because of {ex.Message}");
            }
        }
        public void SimpleUpdateContainer<T>(ref T oldContainer, IContainerToUpdateDto newContainer)
        {
            // Create IContainerElement object whether it is form or section to access the Questions property
            IContainerElement containerElement = (IContainerElement)oldContainer;

            // Check each value if sent or not and then proceed with the replacement.
            if (newContainer.Header != null)
                containerElement.Header = newContainer.Header;

            if (newContainer.Description != null)
                containerElement.Description = newContainer.Description;

            if (newContainer.ColumnRepresentation != 0)
                containerElement.ColumnRepresentation = newContainer.ColumnRepresentation;

            // Override the sent property with the updated IContainerElement object and cast it back to generic type
            oldContainer = (T)containerElement;
        }
    }
}
