using System;
using System.Collections.Generic;
using System.Linq;
using YesilEvCF.Core.Context;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Repos;
using YesilEvCF.DAL.Abstract;

namespace YesilEvCF.DAL.Concrete
{
    public class ProductStatusDAL : EFRepoBase<YesilEvDbContext, ProductStatus>, IProductStatusDAL
    {
        public ProductStatus AddProductStatus(ProductStatus productStatus)
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            var productStatusDb = productStatusDAL.Add(productStatus);
            productStatusDAL.SaveChanges();
            return productStatusDb;
        }

        public void DeleteProductStatus(ProductStatus productStatus)
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            var productStatusDb = productStatusDAL.GetByFilter(x => x.TrackingNumber == productStatus.TrackingNumber).SingleOrDefault();
            productStatusDAL.Delete(productStatusDb);
            productStatusDAL.SaveChanges();
        }

        public List<ApprovementStatus> GetAllApprovementStatus()
        {
            ProDAL<ApprovementStatus> productStatusDAL = new ProDAL<ApprovementStatus>();
            var productStatusesDb = productStatusDAL.GetAll();
            return productStatusesDb;
        }

        public List<Product> GetApprovedProducts()
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Category> categoryDAL = new ProDAL<Category>();
            ProDAL<Manufacturer> manifactureDAL = new ProDAL<Manufacturer>();
            var products = productStatusDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (ps, p) => new
            {
                productStatus = ps,
                product = p
            }).Where(x => x.productStatus.ApprovementStatusID == 2).Join(userDAL.GetEntity(), x => x.product.UserID, y => y.UserID, (p, u) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = u
            }).Join(categoryDAL.GetEntity(), x => x.product.CategoryID, y => y.CategoryID, (p, c) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = c
            }).Join(manifactureDAL.GetEntity(), x => x.product.ManufacturerID, y => y.ManufacturerID, (p, m) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = p.category,
                manifacture = m
            }).Select(x => new Product
            {
                ProductName = x.product.ProductName,
                Barcode = x.product.Barcode,
                Manufacturer = x.manifacture,
                ManufacturerID = x.manifacture.ManufacturerID,
                ProductFrontImage = x.product.ProductFrontImage,
                ProductBackImage = x.product.ProductBackImage,
                CategoryID = x.category.CategoryID,
                Category = x.category,
                User = x.user,
                UserID = x.user.UserID,
                ShowUser = x.product.ShowUser,
                IsActive = x.productStatus.IsActive,
                CreatedDate = x.productStatus.CreatedDate,
                ModifiedDate = x.productStatus.ModifiedDate,
            }).ToList();
            return products;
        }

        public List<Product> GetPendingProducts()
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Category> categoryDAL = new ProDAL<Category>();
            ProDAL<Manufacturer> manifactureDAL = new ProDAL<Manufacturer>();
            var products = productStatusDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (ps, p) => new
            {
                productStatus = ps,
                product = p
            }).Where(x => x.productStatus.ApprovementStatusID == 1).Join(userDAL.GetEntity(), x => x.product.UserID, y => y.UserID, (p, u) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = u
            }).Join(categoryDAL.GetEntity(), x => x.product.CategoryID, y => y.CategoryID, (p, c) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = c
            }).Join(manifactureDAL.GetEntity(), x => x.product.ManufacturerID, y => y.ManufacturerID, (p, m) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = p.category,
                manifacture = m
            }).Select(x => new Product
            {
                ProductName = x.product.ProductName,
                Barcode = x.product.Barcode,
                Manufacturer = x.manifacture,
                ManufacturerID = x.manifacture.ManufacturerID,
                ProductFrontImage = x.product.ProductFrontImage,
                ProductBackImage = x.product.ProductBackImage,
                CategoryID = x.category.CategoryID,
                Category = x.category,
                User = x.user,
                UserID = x.user.UserID,
                ShowUser = x.product.ShowUser,
                IsActive = x.productStatus.IsActive,
                CreatedDate = x.productStatus.CreatedDate,
                ModifiedDate = x.productStatus.ModifiedDate,
            }).ToList();
            return products;
        }

        public ProductStatus GetProductStatusByProductID(int id)
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            var productStatus = productStatusDAL.GetByFilter(x => x.ProductID == id).SingleOrDefault();
            return productStatus;
        }

        public ProductStatus GetProductStatusByProductInfo(string productName, Guid barcode)
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            var productStatus = productStatusDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (ps, p) => new
            {
                productStatus = ps,
                product = p
            }).Where(x => x.product.ProductName == productName && x.product.Barcode == barcode).Select(x => new ProductStatus
            {
                TrackingNumber = x.productStatus.TrackingNumber,
                CreatedDate = x.productStatus.CreatedDate,
                ModifiedDate = x.productStatus.ModifiedDate,
                IsActive = x.productStatus.IsActive,
                Detail = x.productStatus.Detail,
                ApprovementStatusID = x.productStatus.ApprovementStatusID,
                UserID = x.productStatus.UserID,
                ProductID = x.productStatus.ProductID,
            }).FirstOrDefault();
            return productStatus;
        }

        public List<Product> GetRejectedProducts()
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Category> categoryDAL = new ProDAL<Category>();
            ProDAL<Manufacturer> manifactureDAL = new ProDAL<Manufacturer>();
            var products = productStatusDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (ps, p) => new
            {
                productStatus = ps,
                product = p
            }).Where(x => x.productStatus.ApprovementStatusID == 3).Join(userDAL.GetEntity(), x => x.product.UserID, y => y.UserID, (p, u) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = u
            }).Join(categoryDAL.GetEntity(), x => x.product.CategoryID, y => y.CategoryID, (p, c) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = c
            }).Join(manifactureDAL.GetEntity(), x => x.product.ManufacturerID, y => y.ManufacturerID, (p, m) => new
            {
                productStatus = p.productStatus,
                product = p.product,
                user = p.user,
                category = p.category,
                manifacture = m
            }).Select(x => new Product
            {
                ProductName = x.product.ProductName,
                Barcode = x.product.Barcode,
                Manufacturer = x.manifacture,
                ManufacturerID = x.manifacture.ManufacturerID,
                ProductFrontImage = x.product.ProductFrontImage,
                ProductBackImage = x.product.ProductBackImage,
                CategoryID = x.category.CategoryID,
                Category = x.category,
                User = x.user,
                UserID = x.user.UserID,
                ShowUser = x.product.ShowUser,
                IsActive = x.productStatus.IsActive,
                CreatedDate = x.productStatus.CreatedDate,
                ModifiedDate = x.productStatus.ModifiedDate,
            }).ToList();
            return products;
        }

        public void UpdateProductStatus(ProductStatus productStatus)
        {
            ProDAL<ProductStatus> productStatusDAL = new ProDAL<ProductStatus>();
            productStatusDAL.Update(productStatus);
            productStatusDAL.SaveChanges();
        }
    }
}
