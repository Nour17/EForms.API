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

        public IContainerElement CreateContainer<T>(IContainerToCreateDto containerToInsertDto)
        {
            IContainerElement container = null;
            try
            {
                // Create the container object ( Form, Section ), if problem occurred a NullReferenceException is thrown
                container = initializeContainerInstance<T>();

                // Populate the container object ( Form, Section ) with basic information, if problem occurred a ArgumentException is thrown
                container = populateContainerWithBasicInfo(container, containerToInsertDto);

                if (containerToInsertDto.Questions != null)
                    container = addListOfQuestionsIntoContainer(container, containerToInsertDto.Questions);

                return container;
            } catch (Exception ex)
            {
                string errorMessage = $"Problem while creating container process \n{ex.Message}";
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    errorMessage += "\n" + innerException.Message;
                    innerException = innerException.InnerException;
                }
                _logger.LogError(errorMessage + "\n" + ex.StackTrace);
                throw new Exception(errorMessage);
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
        private IContainerElement initializeContainerInstance<T>()
        {
            _logger.LogInfo("Attempt to initialize new container");
            try
            {
                IContainerElement container = null;

                // Check for the passed generic class whether it is form or section
                Type containerType = typeof(T);
                _logger.LogInfo($"Initialize new container of type: {containerType}");
                if (containerType == typeof(Form))
                {
                    container = new Form();
                }
                else if (containerType == typeof(Section))
                {
                    container = new Section();
                } else
                {
                    throw new InvalidCastException("Problem while detecting container type: Not form or section");
                }

                return container;
            }
            catch (InvalidCastException ex)
            {
                throw new NullReferenceException($"Could not initialize a new container", ex);
            }
        }
        private IContainerElement populateContainerWithBasicInfo(IContainerElement container, IContainerToCreateDto containerToInsertDto)
        {
            _logger.LogInfo($"Populating {container.GetType().Name} with basic data");

            try
            {
                // Create new Container object with the incoming properties from the request payload
                container.Header = containerToInsertDto.Header;
                container.Description = containerToInsertDto.Description;
                container.Position = containerToInsertDto.Position;
                container.ColumnRepresentation = containerToInsertDto.ColumnRepresentation;

                return container;
            } catch (Exception ex)
            {
                throw new ArgumentException($"Could not populate the new container: {ex.Message}", ex);
            }
        }
        private IContainerElement addListOfQuestionsIntoContainer(IContainerElement container, List<QuestionToInsertDto> questionsToInsertDto)
        {
            List<Question> questionsToBeAdded = new List<Question>();

            try
            {
                _logger.LogInfo($"Adding list of question into {container.GetType().Name}");

                // Loop through the Questions from the incoming request
                foreach (QuestionToInsertDto questionToInsertDto in questionsToInsertDto)
                {
                    // Add each question individualy into the form
                    questionsToBeAdded.Add(_questionService.CreateQuestion(questionToInsertDto));
                }

                // Assign the list of created questions to the parent container element
                container.Questions = questionsToBeAdded;

                return container;
            }
            catch (Exception ex)
            {
                throw new Exception($"Adding questions Failed because of {ex.Message}", ex);
            }
        }
    }
}
