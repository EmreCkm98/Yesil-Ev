using System;
using System.Collections.Generic;
using System.Linq;
using YesilEvCF.Core.Context;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Repos;
using YesilEvCF.DAL.Abstract;
using YesilEvCF.DTOs;
using YesilEvCF.Mapping;

namespace YesilEvCF.DAL.Concrete
{
    public class SearchHistoryDAL : EFRepoBase<YesilEvDbContext, SearchHistory>, ISearchHistoryDAL
    {
        public void DeleteSearchHistory(SearchHistory searchHistory)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            var search = proDAL.GetByFilter(x => x.SearchHistoryID == searchHistory.SearchHistoryID).SingleOrDefault();
            proDAL.Delete(search);
            proDAL.SaveChanges();
        }

        public List<SearchHistoryDTO> GetAllSearchHistory()
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            ProDAL<Product> productDAL = new ProDAL<Product>();

            var searchList = proDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (sh, p) => new
            {
                searchHistory = sh,
                product = p
            }).Select(x => new SearchHistoryDTO
            {
                ProductID = x.product.ProductID,
                SearchDate = x.searchHistory.SearchDate,
                CreatedDate = x.searchHistory.CreatedDate,
                IsActive = x.searchHistory.IsActive,
                ModifiedDate = x.searchHistory.ModifiedDate
            }).ToList();

            return searchList;
        }

        public SearchHistory GetSearchHistoryByID(int id)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            var search = proDAL.GetById(id);
            return search;
        }

        public List<SearchHistory> GetSearchHistoryByProductID(int id)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            var search = proDAL.GetByFilter(x => x.ProductID == id).ToList();
            return search;
        }

        public SearchHistory GetSearchHistoryByProductName(SearchHistoryDTO searchHistoryDTO)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            var search = MappingConfig.Mapper.Map<SearchHistory>(searchHistoryDTO);
            var selectedSearch = proDAL.GetByFilter(x => x.ProductID == search.ProductID && x.SearchDate == search.SearchDate).FirstOrDefault();
            return selectedSearch;
        }

        public List<SearchHistory> GetSearchHistoryBySearchDate(DateTime dateTime)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            var search = proDAL.GetByFilter(x => x.SearchDate == dateTime).ToList();
            return search;
        }
    }
}
