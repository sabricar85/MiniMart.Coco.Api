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
using MiniMart.Coco.Api.Services.Product;
namespace MiniMart.Coco.Api.Tests.Unit.Services
{
   public class VirtualCartAddProductHandlerTest
    {
        public VirtualCartAddProductHandler Sut { get; set; }

        public Mock<IVirtualCartRepository> repository { get; set; }
        private AddedProductsRequest request { get; set; }
        public VirtualCartAddProductHandlerTest()
        {
            this.request = new AddedProductsRequest()
            {
                StoreID = 1,
                VoucherCode = "hkjkj8987",
                Products = new List<QueryProductDto>() 
    
            };

            this.repository = new Mock<IVirtualCartRepository>();
            this.repository.Setup(m => m.AddProducts(It.IsAny<AddedProductsRequest>())).ReturnsAsync(new AddedProductsResponse());
            this.Sut = new VirtualCartAddProductHandler(repository.Object);
        }
        public class The_Method_Handle : VirtualCartAddProductHandlerTest
        {
            [Fact]
            public async void Should_call_repository()
            {
                // Arrange

                // Act
                var result = await this.Sut.Handle(this.request, CancellationToken.None);

                // Assert
                repository.Verify(m => m.AddProducts(this.request), Times.Once);
            }

            [Fact]
            public async void Should_Handle_GetAvailableStore_not_Return_null()
            {
                // arrange

                this.request = new AddedProductsRequest();
                request.StoreID = 1;

                // act
                var result = await this.Sut.Handle(this.request, CancellationToken.None);

                // assert
                Assert.NotNull(result);
            }
            [Fact]
            public async Task Should_return_expected_report_name_when_execution_is_successful()
            {
                // arrange
                AddedProductsResponse response = new AddedProductsResponse();

                List<AvailableProductDto> AvailableProducts = new List<AvailableProductDto>();
                AvailableProductDto prod = new AvailableProductDto();
                AvailableProducts.Add(prod);
                // act
                var result = await this.Sut.Handle(request, CancellationToken.None);

                // assert
                Assert.NotEqual(response, result);
            }
        }
    }
}
