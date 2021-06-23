using EForms.API.Core.Models;
using Xunit;

namespace EForms.API.Test.Core
{
    public class ContainerServiceTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCorrectFormFromDataGeneraor), MemberType = typeof(TestDataGenerator))]
        public void CreateContainer_ValidFormShouldWork(FormCore form)
        {
            
        }
    }
}
