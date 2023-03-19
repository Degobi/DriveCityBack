using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Infra
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly ContextDataBase _context;

        public RepositorioBase(ContextDataBase context)
        {
            _context = context;
        }

        public DbSet<T> Get()
        {
            return this._context.Set<T>();
        }

        public T Get(object id)
        {
            return this._context.Set<T>().Find(id);
        }
    }
}
