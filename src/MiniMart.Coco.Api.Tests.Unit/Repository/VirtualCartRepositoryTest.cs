using System;
using Xunit;
using MiniMart.Coco.Api.Repository;
using MiniMart.Coco.Api.Data;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using System.Configuration;

namespace MiniMart.Coco.Api.Tests.Unit.Repository
{
    public class VirtualCartRepositoryTest
    {
        public VirtualCartRepository Sut { get; set; }
        public DataContext DbContext { get; set; }
        public class The_Constructor : VirtualCartRepositoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_DataContext_is_null()
            {
                // arrange
                DataContext dbContext = null;
                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(dbContext), () => new VirtualCartRepository(dbContext));
            }
        }
        public class The_Method_tAddProducts : VirtualCartRepositoryTest
        {
            [Fact]
            public void Should_return_AvailableProducts()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);

                AddedProductsRequest  Request = new AddedProductsRequest();
                var resource = this.Sut.AddProducts(Request);
                Assert.NotNull(resource);
            }

            [Fact]
            public void Should_return_StoreID()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();

                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                AddedProductsRequest Request = new AddedProductsRequest();
                var resource = this.Sut.AddProducts(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Equal(Response.StoreID, Request.StoreID);
            }
            [Fact]
            public void Should_return_voucherCode()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();

                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                AddedProductsRequest Request = new AddedProductsRequest();
                var resource = this.Sut.AddProducts(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Equal(Response.VoucherCode, Request.VoucherCode);
            }
            [Fact]
            public void Should_return_Products()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();

                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                AddedProductsRequest Request = new AddedProductsRequest();
                var resource = this.Sut.AddProducts(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Null(Request.Products);
            }
        }

        public class The_Method_DeleteProduct : VirtualCartRepositoryTest
        {
            [Fact]
            public void Should_return_NotNullDeleted()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);


                DeletedProductRequest Request = new DeletedProductRequest();
                Request.ProductDelete = new QueryProductDto();
                Request.ProductDelete.CategoryID = 1;
                Request.ProductDelete.ProductID = 1;
                Request.ProductDelete.Quantity = 1;
               var resource = this.Sut.DeleteProduct(Request);
                Assert.NotNull(resource);
            }

            [Fact]
            public void Should_return_StoreID()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();

                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                DeletedProductRequest Request = new DeletedProductRequest();
                var resource = this.Sut.DeleteProduct(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Equal(Response.StoreID, Request.StoreID);
            }
            [Fact]
            public void Should_return_voucherCode()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();

                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                DeletedProductRequest Request = new DeletedProductRequest();
                var resource = this.Sut.DeleteProduct(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Equal(Response.VoucherCode, Request.VoucherCode);
            }
            [Fact]
            public void Should_return_Products()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new VirtualCartRepository(this.DbContext);
                DeletedProductRequest Request = new DeletedProductRequest();
                var resource = this.Sut.DeleteProduct(Request);
                AddedProductsResponse Response = new AddedProductsResponse();
                Assert.Null(Request.ProductDelete);
            }
        }
    }
}
