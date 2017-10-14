using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Demo.Data
{
    public interface IDataEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime LastUpdatedDate { get; set; }
    }
}
