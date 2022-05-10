using Xunit;
using System.Threading.Tasks;
using MiniMart.Coco.Api.Services;
using MiniMart.Coco.Api.Tests.Unit.Services;
using MiniMart.Coco.Api.Services.Store;
using Moq;
using MiniMart.Coco.Api.Repository;
using System;
using MiniMart.Coco.Api.Dtos.Responses;
using System.Threading;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos;
using System.Collections.Generic;

namespace MiniMart.Coco.Api.Tests.Unit.Services
{
    public class StoreHandlerTest  
    {
        public StoreHandler Sut { get; set; }
        public Mock<IStoreRepository> StoreRepository { get; set; }
        private availableStoreRequest availableStoreRequest { get; set; }
        public StoreHandlerTest()
        {
            this.availableStoreRequest = new availableStoreRequest()
            {
                DateTimeQuery = DateTime.Now
            };
            
            this.StoreRepository = new Mock<IStoreRepository>();
            this.StoreRepository.Setup(m => m.GetAvailableStore(It.IsAny<DateTime>())).ReturnsAsync(new AvailableStoreResponse());
            this.Sut = new StoreHandler(StoreRepository.Object);  
        }
        public class The_Method_Handle : StoreHandlerTest
        {
            [Fact]
            public async void Should_call_repository()
            {
                // Arrange

                // Act
                var result = await this.Sut.Handle(this.availableStoreRequest, CancellationToken.None);

                // Assert
                StoreRepository.Verify(m => m.GetAvailableStore(this.availableStoreRequest.DateTimeQuery), Times.Once);
            }

            [Fact]
            public async void Should_Handle_GetAvailableStore_not_Return_null()
            {
                // arrange

                this.availableStoreRequest = new availableStoreRequest();
                availableStoreRequest.DateTimeQuery = new DateTime();
               
                // act
                var result = await this.Sut.Handle(availableStoreRequest, CancellationToken.None);

                // assert
                Assert.NotNull(result);
            }
            [Fact]
            public async Task Should_return_expected_report_name_when_execution_is_successful()
            {
                // arrange
                this.availableStoreRequest = new availableStoreRequest();
                availableStoreRequest.DateTimeQuery = new DateTime();
                AvailableStoreResponse AvailableStoreResponse = new AvailableStoreResponse();
                List<StoreDto> stores  = new List<StoreDto>();
            
                StoreDto store = new StoreDto();
                store.Name = "coco mall";
                store.Address = "address";
                store.StartHour = "10:00";
                store.EndHour = "10:00";
 
                stores.Add(store);
                AvailableStoreResponse.Stores = stores;
                // act
                var result = await this.Sut.Handle(availableStoreRequest , CancellationToken.None);

                // assert
                Assert.NotEqual(AvailableStoreResponse, result);
            }
        }
    }
}
