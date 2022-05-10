using System;
using System.Threading;
using System.Threading.Tasks;
using MiniMart.Coco.Api.Controllers;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MiniMart.Coco.Api.Dtos;
using MiniMart.Coco.Api.Profiles;

namespace MiniMart.Coco.Api.Tests.Unit.Controllers
{
    public class ConfigurationControllerTest
    {
        private Mock<IMediator> MediatorMock { get; set; }

        private ConfigurationController Sut { get; set; }

        public ConfigurationControllerTest()
        {
            this.MediatorMock = new Mock<IMediator>();

            this.Sut = new ConfigurationController(this.MediatorMock.Object);
        }
        public class TheConstructor : ConfigurationControllerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_mediator_is_null()
            {
                // arrange
                IMediator mediator = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(mediator), () => new ConfigurationController(mediator));
            }
        }
        public class TheMethod_getAvailableProduct : ConfigurationControllerTest
        {
            public ConfigurationQuery Request { get; set; }
            public TheMethod_getAvailableProduct()
            {
                this.Request = new ConfigurationQuery()
                {

                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange
                // act                
                await this.Sut.Setup();

            }




        }
    }
}
