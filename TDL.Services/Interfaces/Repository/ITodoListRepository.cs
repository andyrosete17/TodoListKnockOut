namespace TDL.Services.Interfaces.Repository
{
    using System;
    using TDL.Common.Interfaces;
    using TDL.Services.Models;

    public interface ITodoListRepository<TEntity>
         where TEntity : IEntity
    {

        Response GetAll();

        Response Get(Guid id);

        Response Get(int id);

        Response Create(Action<TEntity> setupProperty);

        Response Create();

        Response CommitContextChanges();

        Response RemoveData(Guid id);
    }
}