using MiniMart.Coco.Api.Data;
using MiniMart.Coco.Api.Domain;
using MiniMart.Coco.Api.Dtos;
using MiniMart.Coco.Api.Dtos.Requests;
using MiniMart.Coco.Api.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace MiniMart.Coco.Api.Repository
{
    public class VirtualCartRepository : IVirtualCartRepository
    {
        private readonly DataContext dbContext;
          
        private const int Unitsame =  1;
        private const int Pay2Take3 = 3;
        private const int SecondUnit = 2;
        private const int deleteUneUnit = 1;
        public VirtualCartRepository(DataContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<DeletedProductResponse> DeleteProduct(DeletedProductRequest DeletedProductRequest)
        {
            DeletedProductResponse DeletedProductResponse = new DeletedProductResponse();
            Stock stock = await getstock(DeletedProductRequest.ProductDelete.ProductID, DeletedProductRequest.StoreID);
            stock.ProductCount = stock.ProductCount + deleteUneUnit;
            int  rowAfected=  dbContext.SaveChanges();
            if (rowAfected>0)
            {
                List<Voucher> vouchers = new List<Voucher>();
                if (DeletedProductRequest.VoucherCode != null)
                {
                    vouchers = getVouchers(DeletedProductRequest.VoucherCode);
                    DeletedProductResponse.Voucher = ValidVoucherBAsicData(vouchers, DeletedProductRequest.StoreID);
                }
                else
                {
                    DeletedProductResponse.Voucher = new ApplyVoucherDto { applyVoucher = false, Message = Localization.Messages.VoucherNoApply };
                }
                


                DeletedProductResponse.product = await populateProduct(DeletedProductRequest.ProductDelete, vouchers, DeletedProductRequest.StoreID, DeletedProductResponse.Voucher.applyVoucher);
                DeletedProductResponse.product.Quantity = DeletedProductResponse.product.Quantity - deleteUneUnit;
                DeletedProductResponse.VoucherCode = DeletedProductRequest.VoucherCode;
                DeletedProductResponse.StoreID = DeletedProductRequest.StoreID;
            }
            DeletedProductResponse.TotalPrice = DeletedProductRequest.TotalPrice - DeletedProductRequest.TotalPriceProducts + DeletedProductResponse.product.TotalPriceProducts;
            return DeletedProductResponse;
        }
        public async Task<AddedProductsResponse> AddProducts(AddedProductsRequest AddedProductsRequest)
        {
            AddedProductsResponse AddedProductsResponse = new AddedProductsResponse();
            try
            {
                AddedProductsResponse.Products = new List<ProductDto>();

                List<Voucher> vouchers = new List<Voucher>();

                if (AddedProductsRequest.VoucherCode != null)
                {
                    vouchers = getVouchers(AddedProductsRequest.VoucherCode);
                    AddedProductsResponse.Voucher = ValidVoucherBAsicData(vouchers, AddedProductsRequest.StoreID);
                }
                else
                {
                    AddedProductsResponse.Voucher = new ApplyVoucherDto { applyVoucher = false, Message = Localization.Messages.VoucherNoApply };
                }
           

                foreach (var productCart in AddedProductsRequest.Products)
                {

                     ProductDto ProductDto = new ProductDto();
                    Stock stock = await getstock(productCart.ProductID, AddedProductsRequest.StoreID);

                    bool update = false;
                    if (stock != null && stock.ProductCount > productCart.Quantity)
                    {
                        stock.ProductCount = stock.ProductCount - productCart.Quantity;
                        dbContext.SaveChanges();
                        update = true;
                    }

                    if (update)
                     {
                        ProductDto = await populateProduct(productCart, vouchers, AddedProductsRequest.StoreID, AddedProductsResponse.Voucher.applyVoucher);
                        ProductDto.Action = true; 
                    }
                    else
                    {
                        ProductDto.Action = false;
                        ProductDto.ID = productCart.ProductID;
                    }
                       
                  AddedProductsResponse.Products.Add(ProductDto);
                }
                AddedProductsResponse.VoucherCode = AddedProductsRequest.VoucherCode;
                AddedProductsResponse.TotalPrice  = AddedProductsResponse.Products.Sum(e => e.TotalPriceProducts);
                AddedProductsResponse.StoreID =    AddedProductsRequest.StoreID;
            }
            catch (Exception e)
            {
                throw e;
            }
          
            return AddedProductsResponse;
        }

        private async Task<ProductDto> populateProduct (QueryProductDto productCart, List<Voucher> vouchers, int StoreID, bool applyVoucher)
        {
            Product productBD = await getProduct(productCart, StoreID);

            ProductDto ProductDto = new ProductDto();

            ProductDto.ID = productCart.ProductID;
            ProductDto.CategoryID = productCart.CategoryID;
            ProductDto.Quantity = productCart.Quantity;

            ProductDto.Action = true;

            Discount discount = null;

            if (applyVoucher)
            {
                discount = getvoucherDiscount(vouchers, productCart, StoreID);
            }
            ProductDto.Price = CalculatePriceWithDiscount(discount, productBD, productCart.Quantity);

            if (ProductDto.Price.PriceWithDiscount != null)
            {
                ProductDto.TotalPriceProducts = (decimal)ProductDto.Price.PriceWithDiscount * productCart.Quantity;
            }
            else
            {
                ProductDto.TotalPriceProducts = (productCart.Quantity * ProductDto.Price.OrginalPrice);
            }

            return ProductDto;
        }

        private async Task<Product>  getProduct(QueryProductDto product, int StoreID)
        {

            var Product = await (from p in dbContext.Products
                                 join s in dbContext.Stocks on p.ID equals s.ProductID
                                 where     p.ID.Equals(product.ProductID) && s.StoreID.Equals(StoreID) //&& s.ProductCount >= product.Quantity
                                 select p).FirstOrDefaultAsync();

            if (Product == null)
            {
                throw new Exception("Product not found");
            }

            return  Product;
        }

        private ApplyVoucherDto ValidVoucherBAsicData( List<Voucher> vouchers, int Store)
        {
            ApplyVoucherDto ApplyVoucher = new ApplyVoucherDto();
            ApplyVoucher.applyVoucher = true;

            if (vouchers.Count > 0)
            {
                switch (true)
                {
                    case true when (this.IsVoucherDateExpired(vouchers)):
                        ApplyVoucher.applyVoucher = false;
                        ApplyVoucher.Message = Localization.Messages.InvalidDate;
                        break;
                    case true when (IsInvalidVoucherSpecificDate(vouchers)):
                        ApplyVoucher.applyVoucher = false;
                        ApplyVoucher.Message = Localization.Messages.InvalidDayOfWeek;
                        break;
                    case true when (IsInvalidVoucherStore(vouchers, Store)):
                        ApplyVoucher.applyVoucher = false;
                        ApplyVoucher.Message = Localization.Messages.InvalidStore;
                        break;
                }
            }
            else
            {
                ApplyVoucher = new ApplyVoucherDto();
                ApplyVoucher.applyVoucher = false;
                ApplyVoucher.Message = Localization.Messages.InvalidVoucher; 
            }
       
            return ApplyVoucher;
        }
        private Discount getvoucherDiscount(List<Voucher> vouchers, QueryProductDto ProductDto, int StoreID)
        {
            int DateOfWeek = (int)DateTime.Today.DayOfWeek;
            Discount discount = null;

            Voucher Voucher = (from v in vouchers
                               where
                                      v.StoreID == StoreID
                                     && (v.DayNumber == DateOfWeek || v.DayNumber == null)
                                     && v.CategoryID == ProductDto.CategoryID
                                     && (v.ExpirationDate >= DateTime.Today && v.StartDate <= DateTime.Today)
                               select v).FirstOrDefault();
        
            if (Voucher != null && !ExistVoucherProductRestriction(Voucher, ProductDto.ProductID))
            {
                discount = dbContext.Discount.Where(d => d.ID.Equals(Voucher.DiscountID)).FirstOrDefault();
            }
            return discount;
        }
 
        private bool IsVoucherDateExpired(List<Voucher> vouchers)
        {
            int vouchersCount = vouchers.Where(v => v.ExpirationDate >= DateTime.Today && v.StartDate <= DateTime.Today).Count();
            return (vouchersCount == 0);
        }
        private bool IsInvalidVoucherSpecificDate(List<Voucher> vouchers)
        {
            bool invalid = false;

            int DateOfWeek = (int)DateTime.Today.DayOfWeek;
             bool  alldays = (vouchers.Where(v => v.DayNumber == null).Count() > 0);

            if (!alldays)
            {
                invalid  = (vouchers.Where(v => v.DayNumber == DateOfWeek).Count() == 0);
            }

            return invalid;
        }
        private bool IsInvalidVoucherStore(List<Voucher> vouchers, int storeID)
        {
            int vouchersCount = (vouchers.Where(v => v.StoreID == storeID).Count());
            return (vouchersCount == 0);
        }
        private  bool  ExistVoucherProductRestriction(Voucher voucher, int productID)
        {
           bool restriction = false;
           var restrictionProducts =   dbContext.VoucherProductRestrictions.SingleOrDefault(n => n.VoucherID.Equals(voucher.ID) && n.ProductID == productID);
          
            if (restrictionProducts != null)
            {
                restriction = true;
            }
            return restriction;
        }
        private List<Voucher> getVouchers(string voucherCode)
        {
            List<Voucher> vouchers = new List<Voucher>();
            if (voucherCode != string.Empty)
            {
                vouchers  = dbContext.Vouchers.Where(v => v.Code == voucherCode).ToList();
            }
            return vouchers;

        }
        private async Task<Stock> getstock(int productID, int StoreID)
        {
         
            var StockQuery = await (from stock in dbContext.Stocks
                                    where stock.ProductID == productID && stock.StoreID.Equals(StoreID)
                                    select stock).FirstOrDefaultAsync();

            return StockQuery;
        }

        private PriceDto CalculatePriceWithDiscount(Discount discount, Product product, int  quaintityProduct)
        {
            PriceDto PriceDto = new PriceDto();
            PriceDto.OrginalPrice = product.Price;

            decimal Totaldiscount = 0;

            if (discount != null)
            {
                switch (discount.DiscountTypeID)
                {
                    case Pay2Take3:
                        if (quaintityProduct >= 3 && quaintityProduct <= discount.LimitUnitOff)
                        {
                            
                              Totaldiscount = PriceDto.OrginalPrice * (quaintityProduct / 3);
                        }
                        else
                        {
                            Totaldiscount = PriceDto.OrginalPrice * 2;
                        }
                        PriceDto.PriceWithDiscount = ((PriceDto.OrginalPrice * quaintityProduct) - Totaldiscount) / quaintityProduct;
                        break;
                    case SecondUnit:
                        if (quaintityProduct >= 2)
                        {
                            decimal discountPUnit =   ((PriceDto.OrginalPrice * discount.PercentOff) / 100);
                            Totaldiscount = discountPUnit * (quaintityProduct / 2);
                            PriceDto.PriceWithDiscount = ((PriceDto.OrginalPrice * quaintityProduct) - Totaldiscount)  / quaintityProduct; ;

                        }
                        break;
                    case Unitsame:
                        decimal discountPerUnit = ((PriceDto.OrginalPrice  * discount.PercentOff) / 100);
                        Totaldiscount = discountPerUnit * quaintityProduct;
                        PriceDto.PriceWithDiscount = ((PriceDto.OrginalPrice * quaintityProduct) - Totaldiscount) / quaintityProduct;
                        break;
                }
            }
            return PriceDto;
        }
    }
}    

