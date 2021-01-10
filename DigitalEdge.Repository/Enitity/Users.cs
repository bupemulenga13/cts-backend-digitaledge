using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
  public class Users
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(15)]
        public string PhoneNo { get; set; } 
        public Boolean IsSuperAdmin { get; set; }
        public Boolean IsDeleted { get; set; }
        public string Gender { get; set; }
        public long?  RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual UserRoles UserRoles { get; set; }

        public Users()
        {
        }
        public Users(long id, string password, Boolean isSuperAdmin, string firstName, string lastName, string email, string phoneNo, long? roleId, Boolean isDeleted, string gender)
        {
            this.Id = id;
            this.Password = password;
            this.IsSuperAdmin = IsSuperAdmin;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.Gender = gender;
        }
        public Users(string password, Boolean isSuperAdmin, string firstName, string email, long? roleId)
        {
            this.Password = password;
            this.IsSuperAdmin = isSuperAdmin;
            this.FirstName = firstName;
            this.Email = email;
            this.RoleId = roleId;
        }
    }
}
