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
using System.Collections.Generic;

namespace MiniMart.Coco.Api.Tests.Unit.Controllers
{
    public class VirtualCartControllerTest
    {
        private Mock<IMediator> MediatorMock { get; set; }

        private VirtualCartController Sut { get; set; }

        public VirtualCartControllerTest()
        {
            this.MediatorMock = new Mock<IMediator>();

            this.Sut = new VirtualCartController(this.MediatorMock.Object);
        }

        public class TheConstructor : VirtualCartControllerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_mediator_is_null()
            {
                // arrange
                IMediator mediator = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(mediator), () => new VirtualCartController(mediator));
            }
        }
        public class TheMethod_AddProducts : VirtualCartControllerTest
        {
            public AddedProductsRequest Request { get; set; }
            public TheMethod_AddProducts()
            {
                this.Request = new AddedProductsRequest()
                {
                    VoucherCode = "165456132135",
                    StoreID = 1,
                    Products = new List<QueryProductDto>(),
                   
                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange

                // act                
                await this.Sut.AddProducts(this.Request);

                // assert
                this.MediatorMock.Verify(
                    mdt => mdt.Send(
                            It.Is<AddedProductsRequest>(
                                m => m.StoreID == this.Request.StoreID
                              && m.VoucherCode == this.Request.VoucherCode
                              && m.StoreID == this.Request.StoreID
                               && m.Products == this.Request.Products),
                            CancellationToken.None),
                    Times.Once());
            }

            [Fact]
            public async Task Should_return_mediator_send_method_response_when_it_is_not_null()
            {
                // arrange                
                var response = new AddedProductsResponse();

                this.MediatorMock.Setup(
                      mdt => mdt.Send(
                          It.Is<AddedProductsRequest>(
                                  m => m.StoreID == this.Request.StoreID
                              && m.VoucherCode == this.Request.VoucherCode
                              && m.StoreID == this.Request.StoreID
                               && m.Products == this.Request.Products),
                          CancellationToken.None)).ReturnsAsync(response);


                // act
                ActionResult<AddedProductsResponse> actual = await this.Sut.AddProducts(this.Request);

                // assert
                actual.Value.Should().BeSameAs(response);
            }

        }
        public class TheMethod_DeletedProducts : VirtualCartControllerTest
        {
            public DeletedProductRequest Request { get; set; }
            public TheMethod_DeletedProducts()
            {
                this.Request = new DeletedProductRequest()
                {
                    ProductDelete = new QueryProductDto(),
                    TotalPrice = 100,
                    StoreID = 1,
                    VoucherCode = "5456kjhkjhsdfdsf"
                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange

                // act                
                await this.Sut.DeletedProduct(this.Request);

                // assert
                this.MediatorMock.Verify(
                    mdt => mdt.Send(
                            It.Is<DeletedProductRequest>(
                                m => m.ProductDelete == this.Request.ProductDelete
                              && m.TotalPrice == this.Request.TotalPrice
                              && m.StoreID == this.Request.StoreID
                              && m.VoucherCode == this.Request.VoucherCode),
                            CancellationToken.None),
                    Times.Once());
            }

            [Fact]
            public async Task Should_return_mediator_send_method_response_when_it_is_not_null()
            {
                // arrange                
                var response = new DeletedProductResponse();

                this.MediatorMock.Setup(
                      mdt => mdt.Send(
                          It.Is<DeletedProductRequest>(
                               m => m.ProductDelete == this.Request.ProductDelete
                              && m.TotalPrice == this.Request.TotalPrice
                              && m.StoreID == this.Request.StoreID
                              && m.VoucherCode == this.Request.VoucherCode),
                          CancellationToken.None)).ReturnsAsync(response);


                // act
                ActionResult<DeletedProductResponse> actual = await this.Sut.DeletedProduct(this.Request);

                // assert
                actual.Value.Should().BeSameAs(response);
            }

        }

    }
}
