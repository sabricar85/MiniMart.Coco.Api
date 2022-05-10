using System;
using Xunit;
using MiniMart.Coco.Api.Repository;
using MiniMart.Coco.Api.Data;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos;

namespace MiniMart.Coco.Api.Tests.Unit.Repository
{
    public class StoreRepositoryTest
    {
         public StoreRepository Sut { get; set; }
        public DataContext DbContext { get; set; }
        public class The_Constructor : StoreRepositoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_DataContext_is_null()
            {
                // arrange
                DataContext dbContext = null;
                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(dbContext), () => new StoreRepository(dbContext));
            }
        }
        public class The_Method_GetAvailableStore : StoreRepositoryTest
        {
            [Fact]
            public void Should_return_value()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new StoreRepository(this.DbContext);
 
                var resource = this.Sut.GetAvailableStore(DateTime.Now);
                Assert.NotNull(resource);
            }
        }
    }
}
