using System;
using Microsoft.Extensions.Configuration;
using MiniMart.Coco.Api;
using Xunit;

namespace MiniMart.Coco.Api.Tests.Unit
{
    public class StartupTests
    {
        public class TheConstructor : StartupTests
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_configuration_is_null()
            {
                // arrange
                IConfiguration configuration = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(configuration), () => new Startup(configuration));
            }
        }
    }
}
