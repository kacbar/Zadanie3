using WebApplication1.Animals;

public interface IAnimalService
{
    IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool CreateAnimal(CreateAnimal animal);
    Animal GetAnimalById(int id);
    bool UpdateAnimal(Animal animal);
    bool DeleteAnimal(int id);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    
    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }
    public bool CreateAnimal(CreateAnimal animal)
    {
        var newAnimal = new Animal
        {
            Name = animal.Name,
            Description = animal.Description,
            Category = animal.Category,
            Area = animal.Area
        };

        return _animalRepository.CreateAnimal(newAnimal);
    }
    public Animal GetAnimalById(int id)
    {
        return _animalRepository.GetAnimalById(id);
    }
    public bool UpdateAnimal(Animal animal)
    {
        return _animalRepository.UpdateAnimal(animal);
    }
    public bool DeleteAnimal(int id)
    {
        return _animalRepository.DeleteAnimal(id);
    }
}