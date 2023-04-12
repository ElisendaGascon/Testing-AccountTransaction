using System;

namespace DataAccess.Entities
{
    public interface IUpdateableEntity
    {
        DateTime LastUpdated { get; set; }
    }
}
