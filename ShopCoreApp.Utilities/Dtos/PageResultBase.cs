using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Utilities.Dtos
{
    public abstract class PageResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)RowCount / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
            set
            {
                PageCount = value;
            }
        }
        public int RowCount { get; set; }
        public int FirstRowOnPage
        {
            get
            {
                return (CurrentPage - 1) * PageSize + 1;
            }
        }

        public int LastRowOnPage
        {
            get
            {
                return Math.Min(CurrentPage * PageSize, RowCount);
            }
        }
    }
}
