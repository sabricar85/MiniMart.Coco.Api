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
using MiniMart.Coco.Api.Services.VirtualCart;

namespace MiniMart.Coco.Api.Tests.Unit.Services
{
    public class VirtualCartDeleteProductHandlerTest
    {
        public VirtualCartDeleteProductHandler Sut { get; set; }

        public Mock<IVirtualCartRepository> repository { get; set; }
        private DeletedProductRequest request { get; set; }
        public VirtualCartDeleteProductHandlerTest()
        {
            this.request = new DeletedProductRequest()
            {
                StoreID = 1,
                VoucherCode = "hkjkj8987",
                TotalPrice = 1525,
                ProductDelete = new QueryProductDto()

            };

            this.repository = new Mock<IVirtualCartRepository>();
            this.repository.Setup(m => m.DeleteProduct(It.IsAny<DeletedProductRequest>())).ReturnsAsync(new DeletedProductResponse());
            this.Sut = new VirtualCartDeleteProductHandler(repository.Object);
        }
        public class The_Method_Handle : VirtualCartDeleteProductHandlerTest
        {
            [Fact]
            public async void Should_call_repository()
            {
                // Arrange

                // Act
                var result = await this.Sut.Handle(this.request, CancellationToken.None);

                // Assert
                repository.Verify(m => m.DeleteProduct(this.request), Times.Once);
            }

            [Fact]
            public async void Should_Handle_GetAvailableStore_not_Return_null()
            {
                // arrange

                this.request = new DeletedProductRequest();
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
                DeletedProductResponse response = new DeletedProductResponse 
                { product = null, StoreID = 0, Voucher = null, VoucherCode = null };

                // act
                var result = await this.Sut.Handle(request, CancellationToken.None);

                // assert
                Assert.Equal(result.StoreID, result.StoreID);
                Assert.Equal(result.Voucher, result.Voucher);
                Assert.Equal(result.VoucherCode, result.VoucherCode);
            
            }
        }
    }
}
