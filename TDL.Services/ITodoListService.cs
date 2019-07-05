namespace TDL.Services
{
    using System;
    using System.ServiceModel;
    using TDL.Services.Models;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITodoListService" in both code and config file together.
    [ServiceContract]
    public interface ITodoListService
    {
        [OperationContract]
        string GetAllData();

        [OperationContract]
        string AddNewItem(string description);

        [OperationContract]
        string RemoveData(Guid id);

        [OperationContract]
        string ChangeStatus(Guid id, string status);
    }
}
