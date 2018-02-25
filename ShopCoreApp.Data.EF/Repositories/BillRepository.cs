using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class BillRepository : EFRepository<Bill, int>, IBillRepository
    {
        public BillRepository(AppDbContext context) : base(context)
        {
        }
    }
}
