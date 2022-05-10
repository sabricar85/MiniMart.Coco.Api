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
namespace MiniMart.Coco.Api.Tests.Unit.Controllers
{
    public class StoreControllerTest
    {
        private Mock<IMediator> MediatorMock { get; set; }

        private StoreController Sut { get; set; }

        public StoreControllerTest()
        {
            this.MediatorMock = new Mock<IMediator>();

            this.Sut = new StoreController(this.MediatorMock.Object);
        }
        public class TheConstructor : StoreControllerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_mediator_is_null()
            {
                // arrange
                IMediator mediator = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(mediator), () => new StoreController(mediator));
            }
        }
        public class TheMethod_GetAvailableStore : StoreControllerTest
        {
            public availableStoreRequest Request { get; set; }
            public TheMethod_GetAvailableStore()
            {
                this.Request = new availableStoreRequest()
                {
                    DateTimeQuery = DateTime.Now
                 };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange

                // act                
                await this.Sut.get(this.Request);

                // assert
                this.MediatorMock.Verify(
                    mdt => mdt.Send(
                            It.Is<availableStoreRequest>(
                                m => m.DateTimeQuery == this.Request.DateTimeQuery),
                            CancellationToken.None),
                    Times.Once());
            }

            [Fact]
            public async Task Should_return_mediator_send_method_response_when_it_is_not_null()
            {
                // arrange                
                var response = new AvailableStoreResponse();
                      
                this.MediatorMock.Setup(
                      mdt => mdt.Send(
                          It.Is<availableStoreRequest>(
                              m => m.DateTimeQuery == this.Request.DateTimeQuery
                              ),
                          CancellationToken.None)).ReturnsAsync(response);


                // act
                ActionResult<AvailableStoreResponse> actual = await this.Sut.get(this.Request);

                // assert
                actual.Value.Should().BeSameAs(response);
            }

        }
    }
}
