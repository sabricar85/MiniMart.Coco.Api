 
using Microsoft.EntityFrameworkCore;
using MiniMart.Coco.Api.Domain;

namespace MiniMart.Coco.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<WorkDaysStore> WorkDaysStores { get; set; }
        public DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<VoucherProductRestriction> VoucherProductRestrictions { get; set; }
        public DbSet<Discount> Discount { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<Voucher>().ToTable("Voucher");
            modelBuilder.Entity<WorkDaysStore>().ToTable("WorkDaysStore");
            modelBuilder.Entity<Discount>().ToTable("Discount");    ;
            modelBuilder.Entity<DiscountType>().ToTable("DiscountType");
            modelBuilder.Entity<VoucherProductRestriction>().ToTable("VoucherProductRestriction");
        }
    }
}
