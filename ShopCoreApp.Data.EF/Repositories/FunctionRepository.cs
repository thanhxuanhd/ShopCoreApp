using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class FunctionRepository : EFRepository<Function, string>, IFunctionRepository
    {
        public FunctionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
