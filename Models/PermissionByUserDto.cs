using PermitRegistrationSystem.Models.Base;

namespace PermitRegistrationSystem.Models;

public class PermissionByUserDto 
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public IList<Permission> Permissions { get; set; }
}