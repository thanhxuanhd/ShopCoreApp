using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Utilities.Dtos
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public PageResult()
        {
            Result = new List<T>();
        }

        public IList<T> Result { get; set; }
       
    }
}
