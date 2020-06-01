using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Tests.Test
{
    [TestFixture]
    class SampleTestClass
    {

        [Test, Category("Sample")]
        public void DummyPassTest()
        {
            Assert.True(true);
        }

        [Test, Category("Sample")]
        public void DummyFailTest()
        {
            Assert.True(false);
        }
    }
}
