using CA1_AnimalShelter.Models;
using CA1_AnimalShelter.Services;

class Program
{
    static void Main()
    {
        var shelter = new ShelterService();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Animal Shelter Menu ===");
            Console.WriteLine("1. Add New Animal (Manager)");
            Console.WriteLine("2. View Animal Info (Vet)");
            Console.WriteLine("3. View Animals Needing Foster");
            Console.WriteLine("4. View Adoptable Animals (with filters)");
            Console.WriteLine("5. Exit");
            Console.Write("Select option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAnimal(shelter);
                    break;
                case "2":
                    ViewAnimal(shelter);
                    break;
                case "3":
                    foreach (var a in shelter.GetAnimalsNeedingFoster())
                        Console.WriteLine(a.GetInfo());
                    break;
                case "4":
                    ViewAdoptableAnimals(shelter);
                    break;
                case "5":
                    running = false;
                    break;
            }
        }
    }

    static void AddAnimal(ShelterService shelter)
    {
        Console.Write("Enter species (Canine/Feline/Insect/Arachnids/Fish/Reptile): ");
        string species = Console.ReadLine();
        Animal animal = species.ToLower() switch
        {
            "canine" => new Canine(),
            "feline" => new Feline(),
            "insect" => new Insect(),
            "arachnids" => new Arachnids(),
            "fish" => new Fish(),
            "reptile" => new Reptile(),
            _ => null
        };

        if (animal == null)
        {
            Console.WriteLine("Invalid species.");
            return;
        }

        Console.Write("Name: ");
        animal.Name = Console.ReadLine();
        Console.Write("Age: ");
        animal.Age = int.Parse(Console.ReadLine());
        Console.Write("Vaccinated (true/false): ");
        animal.IsVaccinated = bool.Parse(Console.ReadLine());
        Console.Write("Needs foster (true/false): ");
        animal.NeedsFoster = bool.Parse(Console.ReadLine());
        Console.Write("Available for adoption (true/false): ");
        animal.IsAvailableForAdoption = bool.Parse(Console.ReadLine());
        Console.Write("Location: ");
        animal.Location = Console.ReadLine();

        shelter.AddAnimal(animal);
        Console.WriteLine("Animal added successfully!");
    }

    static void ViewAnimal(ShelterService shelter)
    {
        Console.Write("Enter animal name: ");
        string name = Console.ReadLine();
        var animal = shelter.FindByName(name);
        if (animal == null)
        {
            Console.WriteLine("Animal not found.");
            return;
        }

        Console.WriteLine(animal.GetInfo());
        Console.Write("Update vaccination status? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.Write("Vaccinated (true/false): ");
            bool status = bool.Parse(Console.ReadLine());
            shelter.UpdateVaccination(name, status);
            Console.WriteLine("Updated successfully.");
        }
    }

    static void ViewAdoptableAnimals(ShelterService shelter)
    {
        Console.Write("Filter by species (or leave blank): ");
        string species = Console.ReadLine();
        Console.Write("Filter by vaccinated (true/false/blank): ");
        string vaccinatedInput = Console.ReadLine();
        bool? vaccinated = vaccinatedInput == "" ? null : bool.Parse(vaccinatedInput);
        Console.Write("Filter by location (or leave blank): ");
        string location = Console.ReadLine();

        var results = shelter.GetAvailableForAdoption(
            string.IsNullOrWhiteSpace(species) ? null : species,
            vaccinated,
            string.IsNullOrWhiteSpace(location) ? null : location
        );

        foreach (var a in results)
            Console.WriteLine(a.GetInfo());
    }
}
