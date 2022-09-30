using System;
using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;

namespace YesilEvCF.DAL.Abstract
{
    public interface IProductStatusDAL : IRepo<ProductStatus>
    {
        ProductStatus AddProductStatus(ProductStatus productStatus);
        List<ApprovementStatus> GetAllApprovementStatus();
        List<Product> GetPendingProducts();
        List<Product> GetApprovedProducts();
        List<Product> GetRejectedProducts();
        ProductStatus GetProductStatusByProductID(int id);
        ProductStatus GetProductStatusByProductInfo(string productName, Guid barcode);
        void DeleteProductStatus(ProductStatus productStatus);
        void UpdateProductStatus(ProductStatus productStatus);
    }
}
