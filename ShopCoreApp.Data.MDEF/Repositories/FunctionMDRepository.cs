using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.MDEF.Repositories
{
    public class FunctionMDRepository : MDEFRepository<Function, string>, IFunctionRepository
    {
        public FunctionMDRepository(AppMDDbContext context) : base(context)
        {
        }
    }
}

