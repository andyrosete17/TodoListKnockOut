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
        Response GetAllData();

        [OperationContract]
        Response AddNewItem(string description);

        [OperationContract]
        Response RemoveData(Guid id);

        [OperationContract]
        Response ChangeStatus(Guid id, string status);
    }
}
