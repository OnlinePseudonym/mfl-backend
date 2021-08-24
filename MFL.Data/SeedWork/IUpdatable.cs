using System;

namespace MFL.Data.SeedWork
{
    public interface IUpdatable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
