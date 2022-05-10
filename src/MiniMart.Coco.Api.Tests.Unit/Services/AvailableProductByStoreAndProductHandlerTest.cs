﻿using Xunit;
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
    public class AvailableProductByStoreAndProductHandlerTest
    {

        public AvailableProductByStoreAndProductHandler Sut { get; set; }

        public Mock<IProductRepository> productRepository { get; set; }
        private AvailableProductByStoreAndProductRequest request { get; set; }
        public AvailableProductByStoreAndProductHandlerTest()
        {
            this.request = new AvailableProductByStoreAndProductRequest()
            {
                StoreID = 1,
                ProductID =2
            };

            this.productRepository = new Mock<IProductRepository>();
            this.productRepository.Setup(m => m.GetAvailableProductByStoreAndProduct(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new AvailableProductsResponse());
            this.Sut = new AvailableProductByStoreAndProductHandler(productRepository.Object);
        }
        public class The_Method_Handle : AvailableProductByStoreAndProductHandlerTest
        {
            [Fact]
            public async void Should_call_repository()
            {
                // Arrange

                // Act
                var result = await this.Sut.Handle(this.request, CancellationToken.None);

                // Assert
                productRepository.Verify(m => m.GetAvailableProductByStoreAndProduct(this.request.StoreID, this.request.ProductID), Times.Once);
            }

            [Fact]
            public async void Should_Handle_GetAvailableStore_not_Return_null()
            {
                // arrange

                this.request = new AvailableProductByStoreAndProductRequest();
                request.StoreID = 1;
                request.ProductID = 1;

                // act
                var result = await this.Sut.Handle(this.request, CancellationToken.None);

                // assert
                Assert.NotNull(result);
            }
            [Fact]
            public async Task Should_return_expected_report_name_when_execution_is_successful()
            {
                // arrange
                AvailableProductsResponse response = new AvailableProductsResponse();

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
