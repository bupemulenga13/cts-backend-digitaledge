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
        public Users(long id, string password, Boolean isSuperAdmin, string firstName, string lastName, string email, long facilityId, long roleId, bool isDeleted, bool isActive, string phoneNo)
        {
            this.Id = id;
            this.Password = password;
            this.IsSuperAdmin = isSuperAdmin;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.FacilityId = facilityId;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.IsActive = isActive;
            PhoneNo = phoneNo;
        }
        public Users(string password, Boolean isSuperAdmin, string firstName, string email, long roleId)
        {
            this.Password = password;
            this.IsSuperAdmin = isSuperAdmin;
            this.FirstName = firstName;
            this.Email = email;
            this.RoleId = roleId;
        }
        public Users(long id, string firstName, string lastName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted, bool isActive, long facilityId, string phoneNo)
        {   
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Email = email;
            this.RoleId = roleId;
            this.IsSuperAdmin = isSuperAdmin;
            this.IsDeleted = isDeleted;
            this.IsActive = isActive;
            FacilityId = facilityId;
            PhoneNo = phoneNo;
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

        public long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facilities { get; set; }

        public long RoleId { get; set; }

        public bool GetSuperAdmin()
        {
            if (RoleId == 1)
            {
               return IsSuperAdmin = true;
            }

            return IsSuperAdmin = false;
        }


        [ForeignKey("RoleId")]
        public virtual UserRoles UserRoles { get; set; }
    }
}
