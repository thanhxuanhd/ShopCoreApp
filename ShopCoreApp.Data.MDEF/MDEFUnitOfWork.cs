using ShopCoreApp.Infrastructure.Interfaces;

namespace ShopCoreApp.Data.MDEF
{
    public class MDEFUnitOfWork : IUnitOfWork
    {
        private readonly AppMDDbContext _context;

        public MDEFUnitOfWork(AppMDDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}