using Microsoft.EntityFrameworkCore;

namespace DriveOfCity.Infra
{
    public interface IRepositorioBase<T> where T : class
    {
        DbSet<T> Get();
        T Get(object id);
    }
 
}
