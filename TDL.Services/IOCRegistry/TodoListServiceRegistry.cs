namespace TDL.Services.IOCRegistry
{
    using TDL.Services.Implementations.Repository;
    using TDL.Services.Interfaces.Repository;
    using Unity;
    using Unity.Lifetime;

    public static class TodoListServiceRegistry
    {
        public static UnityContainer container = new UnityContainer();

        public static void RegisterComponents()
        {
            ///Register generic repository
            container.RegisterType(typeof(ITodoListRepository<>), typeof(TodoListRepository<>), new TransientLifetimeManager());
        }
    }
}