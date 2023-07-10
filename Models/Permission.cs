using Microsoft.AspNetCore.Mvc;
using PermitRegistrationSystem.Models.Base;

namespace PermitRegistrationSystem.Models
{
    public class Permission : Entity
    {
        public int PersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDaysOff { get; set; }
        public string Reason { get; set; }
    }
}
