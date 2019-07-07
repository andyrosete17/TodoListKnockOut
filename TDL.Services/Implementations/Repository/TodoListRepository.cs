namespace TDL.Services.Implementations.Repository
{
    using System;
    using System.Data.Entity;
    using TDL.Common.Interfaces;
    using TDL.Services.Interfaces;
    using TDL.Services.Interfaces.Repository;
    using TDL.Services.Models;

    public class TodoListRepository<TEntity> : ITodoListRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected ILocalDataContext DataContext { get; private set; }
        private readonly DbSet<TEntity> dbSet;
        Response response; 

        public TodoListRepository(ILocalDataContext dataContext)
        {
            this.response = new Response();
            this.DataContext = dataContext;
            this.dbSet = this.DataContext.GetDbSet<TEntity>();
        }
        public Response GetAll()
        {
            try
            {
                this.response.Result = this.dbSet;
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }
            return this.response;
        }

        public Response Get(Guid id)
        {
            try
            {
                this.response.Result = this.dbSet.Find(id);
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }
            return this.response;
        }

        public Response Get(int id)
        {
            try
            {
                this.response.Result = this.dbSet.Find(id);
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }

            return this.response;
        }

        public Response RemoveData(Guid id)
        {
            try
            {
                var entity = this.dbSet.Find(id);
                this.dbSet.Remove(entity);
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }

            return this.response;
        }

        public Response Create(Action<TEntity> setupProperty)
        {
            try
            {
                TEntity newEntity = new TEntity();
                setupProperty?.Invoke(newEntity);
                this.response.Result =  newEntity;
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }

            return this.response;
        }

        public virtual Response Create()
        {
            try
            {
                TEntity entity = new TEntity();
                this.dbSet.Add(entity);
                this.response.Result = entity;
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }

            return this.response;
        }

        public virtual Response CommitContextChanges()
        {
            try
            {
                this.response.Result = this.DataContext.SaveChanges();
                this.response.IsSuccess = true;
            }
            catch (Exception e)
            {
                GetError(e);
            }

            return this.response;
        }

        private void GetError(Exception e)
        {
            this.response.IsSuccess = false;
            this.response.Message = e.Message;
        }       
    }
}