using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TDL.Services.Models;

namespace TDL.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITodoListService" in both code and config file together.
    [ServiceContract]
    public interface ITodoListService
    {
        [OperationContract]
        Response GetAllData();

        [OperationContract]
        Response RemoveData(Guid id);

        [OperationContract]
        Response ChangeStatus(Guid id, string status);
    }
}
