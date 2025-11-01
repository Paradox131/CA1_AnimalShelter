using CA1_AnimalShelter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_AnimalShelter.Services
{
    public class ShelterService
    {
        private List<Animal> animals = new List<Animal>();
        private readonly string filePath = "Data/animals.txt";
        

        public ShelterService()
        {
            LoadData();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
            SaveData();
        }

        public List<Animal> GetAllAnimals() => animals;

        public List<Animal> GetAnimalsNeedingFoster() =>
            animals.Where(a => a.NeedsFoster).ToList();

        public List<Animal> GetAvailableForAdoption(string species = null, bool? vaccinated = null, string location = null)
        {
            var query = animals.Where(a => a.IsAvailableForAdoption);
            if (species != null) query = query.Where(a => a.Species.Equals(species, StringComparison.OrdinalIgnoreCase));
            if (vaccinated.HasValue) query = query.Where(a => a.IsVaccinated == vaccinated.Value);
            if (location != null) query = query.Where(a => a.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
            return query.ToList();
        }

        public Animal FindByName(string name) =>
            animals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void UpdateVaccination(string name, bool status)
        {
            var animal = FindByName(name);
            if (animal != null)
            {
                animal.IsVaccinated = status;
                SaveData();
            }
        }

        private void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var a in animals)
                {
                    writer.WriteLine($"{a.Species}|{a.Name}|{a.Age}|{a.IsVaccinated}|{a.NeedsFoster}|{a.IsAvailableForAdoption}|{a.Location}");
                }
            }
        }

        private void LoadData()
        {
            if (!File.Exists(filePath)) return;
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                Animal animal = parts[0] switch
                {
                    "Canine" => new Canine(),
                    "Feline" => new Feline(),
                    "Insect" => new Insect(),
                    "Arachnids" => new Arachnids(),
                    "Fish" => new Fish(),
                    "Reptile" => new Reptile(),
                    _=> null
                };
                if (animal != null)
                {
                    animal.Name = parts[1];
                    animal.Age = int.Parse(parts[2]);
                    animal.IsVaccinated = bool.Parse(parts[3]);
                    animal.NeedsFoster = bool.Parse(parts[4]);
                    animal.IsAvailableForAdoption = bool.Parse(parts[5]);
                    animal.Location = parts[6];
                    animals.Add(animal);
                }
            }
        }
    }
}
