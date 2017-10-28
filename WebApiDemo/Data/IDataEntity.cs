using System;

namespace WebApiDemo.Data
{
    public interface IDataEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
    }
}
