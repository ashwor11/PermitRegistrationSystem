using PermitRegistrationSystem.Models;

namespace PermitRegistrationSystem.Repositories.Abstract;

public interface IPersonRepository
{
    public void Create(Person person);
    public void Update(Person person);
    public void Delete(int id);
    public IList<Person> GetAll();
    public Person GetById(int id);
    public void GetPersonWithPermissions(int Id);
}