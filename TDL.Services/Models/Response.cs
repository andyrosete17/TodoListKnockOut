using TDL.Common.Interfaces;

namespace TDL.Services.Models
{
    public class Response : IEntity
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
