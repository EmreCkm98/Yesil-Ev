using System;
using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface ISearchHistoryDAL : IRepo<SearchHistory>
    {
        List<SearchHistoryDTO> GetAllSearchHistory();
        SearchHistory GetSearchHistoryByProductName(SearchHistoryDTO searchHistoryDTO);
        SearchHistory GetSearchHistoryByID(int id);
        List<SearchHistory> GetSearchHistoryByProductID(int id);
        List<SearchHistory> GetSearchHistoryBySearchDate(DateTime dateTime);
        void DeleteSearchHistory(SearchHistory searchHistory);
    }
}
