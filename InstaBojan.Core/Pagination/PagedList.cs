using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Pagination
{
    public class PagedList<T>:List<T>
    {

       public int CurrentPage { get; set;}
        public int TotalPages { get; set;}

        public int PageSize { get; set;}

        public int TotalCount { get; set;}

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext=>CurrentPage < TotalPages;



        public PagedList(List<T> items, int currentPage, int totalPages, int pageSize, int totalCount)
        {
            AddRange(items);
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

    }
}
