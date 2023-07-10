using PermitRegistrationSystem.Models.Base;

namespace PermitRegistrationSystem.Models;

public class Person : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
}