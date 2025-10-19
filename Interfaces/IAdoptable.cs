using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_AnimalShelter.Interfaces
{
    public interface IAdoptable
    {
        bool IsAvailableForAdoption { get; set; }
    }
}
