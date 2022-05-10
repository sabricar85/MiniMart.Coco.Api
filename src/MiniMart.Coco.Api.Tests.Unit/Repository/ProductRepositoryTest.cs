using System;
using Xunit;
using MiniMart.Coco.Api.Repository;
using MiniMart.Coco.Api.Data;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos;
namespace MiniMart.Coco.Api.Tests.Unit.Repository
{
    public class ProductRepositoryTest
    {
        public DataContext DbContext { get; set; }
        public ProductRepository Sut { get; set; }
        public class The_Constructor : ProductRepositoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_DataContext_is_null()
            {
                // arrange
                DataContext dbContext = null;
                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(dbContext), () => new ProductRepository(dbContext));
            }
        }
        public class The_Method_GetAvailableStore : ProductRepositoryTest
        {
            [Fact]
            public void Should_return_AvailableProducts()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new ProductRepository(this.DbContext);

                var resource = this.Sut.GetAvailableProducts(null);
                Assert.NotNull(resource);
            }
            [Fact]
            public void Should_return_AvailableProductByStoreAndProduct()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new ProductRepository(this.DbContext);
                int store = 1;
                int product = 1;
                var resource = this.Sut.GetAvailableProductByStoreAndProduct(store, product);
                Assert.NotNull(resource);
            }
        }
    }
}
