using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalEdge.Repository
{
    public class UserRoles
    {
        [Key]
        public long RoleId { get; set; }
        public string  RoleName { get; set; }
        public string   Description { get; set; }
        public Boolean IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
     
    }
}
