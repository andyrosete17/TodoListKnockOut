namespace TDL.Services.Interfaces
{
    using System.Data.Entity;
    using TDL.Common.Interfaces;

    public interface ILocalDataContext
    {
        DbSet<T> GetDbSet<T>() where T : class, IEntity;

        int SaveChanges();
    }
}