using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
  public class Users
    {
        
        

        public Users()
        {
        }
        public Users(long id, string password, Boolean isSuperAdmin, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, string gender)
        {
            this.Id = id;
            this.Password = password;
            this.IsSuperAdmin = isSuperAdmin;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.Gender = gender;
        }
        public Users(string password, Boolean isSuperAdmin, string firstName, string email, long roleId)
        {
            this.Password = password;
            this.IsSuperAdmin = isSuperAdmin;
            this.FirstName = firstName;
            this.Email = email;
            this.RoleId = roleId;
        }
        public Users(long id, string firstName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted)
        {   
            this.Id = id;
            this.FirstName = firstName;
            this.Password = password;
            this.Email = email;
            this.RoleId = roleId;
            this.IsSuperAdmin = isSuperAdmin;
            this.IsDeleted = isDeleted;

        }

        //public Users(long id, string firstName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted)
        //{
        //    this.Id = id;
        //    this.FirstName = firstName;
        //    this.Password = password;       
        //    this.Email = email;
        //    this.RoleId = roleId;
        //    this.IsSuperAdmin = isSuperAdmin;
        //    this.IsDeleted = isDeleted;
            
        //}

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
        public bool IsSuperAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public long RoleId { get; set; }


        [ForeignKey("RoleId")]
        public virtual UserRoles UserRoles { get; set; }
    }
}
