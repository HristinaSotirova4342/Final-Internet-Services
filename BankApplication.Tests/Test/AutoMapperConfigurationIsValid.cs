using BankApplication.Tests.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Tests.Test
{
    [TestFixture]
    public class AutoMapperConfigurationIsValid
    {
        [Test, Category("AutoMapper")]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = AutoMapperModule.CreateMapperConfiguration();

            // Act/Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}