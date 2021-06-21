using EForms.API.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EForms.API.Test.Core
{
    public class ContainerServiceTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFormFromDataGeneraor), MemberType = typeof(TestDataGenerator))]
        public void CreateContainer_ValidFormShouldWork(Form form)
        {

        }
    }
}
