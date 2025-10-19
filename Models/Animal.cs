using CA1_AnimalShelter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_AnimalShelter.Models
{
    public abstract class Animal : IAnimal, IAdoptable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract string Species { get; }
        public bool IsVaccinated { get; set; }
        public bool NeedsFoster { get; set; }
        public bool IsAvailableForAdoption { get; set; }
        public string Location { get; set; }

        public virtual string GetInfo()
        {
            return $"{Species} - {Name}, Age: {Age}, Vaccinated: {IsVaccinated}, Location: {Location}";
        }

      }

    }
