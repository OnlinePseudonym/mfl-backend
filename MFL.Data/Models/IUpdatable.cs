using System;

namespace MFL.Data.Models
{
    public interface IUpdatable
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
