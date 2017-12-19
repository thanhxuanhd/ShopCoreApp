using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Call save change DbContext
        void Commit();
    }
}
