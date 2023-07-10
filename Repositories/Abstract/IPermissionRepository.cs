using PermitRegistrationSystem.Models;

namespace PermitRegistrationSystem.Repositories.Abstract;

public interface IPermissionRepository
{
    public void Create(Permission permission);
    public void Update(Permission permission);
    public void Delete(int id);
    public IList<Permission> GetAll();
    public Permission GetById(int id);
    public PermissionByUserDto GetByPersonId(int personId);


}