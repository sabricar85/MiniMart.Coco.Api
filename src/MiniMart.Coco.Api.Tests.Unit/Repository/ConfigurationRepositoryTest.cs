using System;
using Xunit;
using MiniMart.Coco.Api.Repository;
using MiniMart.Coco.Api.Data;
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Dtos;
using Moq;

namespace MiniMart.Coco.Api.Tests.Unit.Repository
{
    public class ConfigurationRepositoryTest
    {
        public ConfigurationRepository Sut { get; set; }
        public DataContext DbContext { get; set; }
        public class The_Constructor : ConfigurationRepositoryTest
        {
            [Fact]
            public void Should_throw_an_ArgumentNullException_when_DataContext_is_null()
            {
                // arrange
                DataContext dbContext = null;
                // act & assert
                Assert.Throws<ArgumentNullException>(nameof(dbContext), () => new ConfigurationRepository(dbContext));
            }
        }
        public class The_Method_ConfigureDataBase : ConfigurationRepositoryTest
        {
            [Fact]
            public void Should_return_NotNull()
            {
                // arrange
                var dbBuilder = new DbContextOptionsBuilder<DataContext>();
                
                this.DbContext = new DataContext(dbBuilder.Options);
                this.Sut = new ConfigurationRepository(this.DbContext);

                var resource = this.Sut.ConfigureDataBase();
                Assert.NotNull(resource);
            }
 
        }
    }
}
