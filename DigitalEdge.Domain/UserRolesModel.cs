using System;

namespace DigitalEdge.Domain
{
   public class UserRolesModel
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public Boolean IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public UserRolesModel(long roleId, string roleName, string description, Boolean isDeleted, long createdBy, long modifiedBy)
        {
            this.RoleId = roleId;
            this.RoleName = roleName;
            this.Description = description;
            this.IsDeleted = isDeleted;
            this.CreatedBy = createdBy;
            this.ModifiedBy = modifiedBy;          
        }
    }
}
