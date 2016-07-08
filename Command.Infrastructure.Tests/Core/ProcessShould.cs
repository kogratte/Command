// //  -----------------------------------------------------------------------
// //  <copyright file="ProcessShould.cs" company="AXA France Service">
// //      Copyright (c) AXA France Service. All rights reserved.
// //  </copyright>
// //  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
// //  <created>07/07/2016 17:03</created>
// //  <modified>07/07/2016 17:06</modified>
// //  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using Infrastructure.Logger;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using SampleCommand;

    [TestClass]
    public class ProcessShould
    {
        [TestMethod]
        public void TEst()
        {
            Mock<InDataAnnotationCommand> commandMock = new Mock<InDataAnnotationCommand>(new Mock<IMessenger>().Object);
            commandMock.Setup(c => c.IsValid).Returns(true);
            Assert.IsTrue(commandMock.Object.IsValid);
        }
    }
}