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

namespace MiniMart.Coco.Api.Tests.Unit.Controllers
{
    public class ProductControllerTest
    {
        private Mock<IMediator> MediatorMock { get; set; }

        private ProductController Sut { get; set; }

        public ProductControllerTest()
        {
            this.MediatorMock = new Mock<IMediator>();

            this.Sut = new ProductController(this.MediatorMock.Object);
        }
        public class TheConstructor : ProductControllerTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_mediator_is_null()
            {
                // arrange
                IMediator mediator = null;

                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(mediator), () => new ProductController(mediator));
            }
        }
        public class TheMethod_getAvailableProduct : ProductControllerTest
        {
            public AvailableProductsRequest Request { get; set; }
            public TheMethod_getAvailableProduct()
            {
                this.Request = new AvailableProductsRequest()
                {
                   
                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange
                // act                
                await this.Sut.getAvailableProduct( );

            }
 
 


        }

        public class TheMethod_getAvailableProducts : ProductControllerTest
        {
            public AvailableProductByStoreRequest Request { get; set; }
            public TheMethod_getAvailableProducts()
            {
                this.Request = new AvailableProductByStoreRequest()
                {
                    StoreID =1
                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange

                // act                
                await this.Sut.getAvailableProducts(this.Request);

                // assert
                this.MediatorMock.Verify(
                    mdt => mdt.Send(
                            It.Is<AvailableProductByStoreRequest>(
                                m => m.StoreID == this.Request.StoreID),
                            CancellationToken.None),
                    Times.Once());
            }
            [Fact]
            public async Task Should_return_mediator_send_method_response_when_it_is_not_null()
            {
                // arrange                
                var response = new AvailableProductsResponse();

                this.MediatorMock.Setup(
                      mdt => mdt.Send(
                          It.Is<AvailableProductByStoreRequest>(
                              m => m.StoreID == this.Request.StoreID
                              ),
                          CancellationToken.None)).ReturnsAsync(response);


                // act
                ActionResult<AvailableProductsResponse> actual = await this.Sut.getAvailableProducts(this.Request);

                // assert
                actual.Value.Should().BeSameAs(response);
            }

        }

        public class TheMethod_AvailableProductByStoreAndProduct : ProductControllerTest
        {
            public AvailableProductByStoreAndProductRequest Request { get; set; }
            public TheMethod_AvailableProductByStoreAndProduct()
            {
                this.Request = new AvailableProductByStoreAndProductRequest()
                {
                    StoreID = 1,
                    ProductID =2
                };
            }
            [Fact]
            public async Task Should_send_query_to_mediator_for_dispatching()
            {
                // arrange

                // act                
                await this.Sut.getAvailableProductByStoreAndProduct(this.Request);

                // assert
                this.MediatorMock.Verify(
                    mdt => mdt.Send(
                            It.Is<AvailableProductByStoreAndProductRequest>(
                                m => m.StoreID == this.Request.StoreID 
                                 &&   m.ProductID == this.Request.ProductID),
                            CancellationToken.None),
                    Times.Once());
            }
            [Fact]
            public async Task Should_return_mediator_send_method_response_when_it_is_not_null()
            {
                // arrange                
                var response = new AvailableProductsResponse();

                this.MediatorMock.Setup(
                      mdt => mdt.Send(
                          It.Is<AvailableProductByStoreAndProductRequest>(
                              m => m.StoreID == this.Request.StoreID
                                    && m.ProductID == this.Request.ProductID 
                              ),
                          CancellationToken.None)).ReturnsAsync(response);


                // act
                ActionResult<AvailableProductsResponse> actual = await this.Sut.getAvailableProductByStoreAndProduct(this.Request);

                // assert
                actual.Value.Should().BeSameAs(response);
            }

        }

    }
  

}
   
 
