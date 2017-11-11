using System;

namespace WebApiDemo.Data
{
    public interface IDataEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
    }
}
