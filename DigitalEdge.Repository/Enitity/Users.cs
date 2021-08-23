using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalEdge.Repository
{
  public class Users
    {
        private int v1;
        private string v2;
        private bool v3;
        private string v4;
        private string v5;
        private string v6;
        private int v7;
        private int v8;
        private bool v9;
        private bool v10;
        private string v11;

        public Users()
        {
        }
        // Create User
        public Users(long id, string firstName, string lastName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted, bool isActive, long? facilityId, long? districtId, long? provinceId, string phoneNo, DateTime? dateCreated, long? createdBy)
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
            this.FacilityId = facilityId;
            this.DistrictId = districtId;
            this.ProvinceId = provinceId;
            this.PhoneNo = phoneNo;
            this.DateCreated = dateCreated;
            this.CreatedBy = createdBy;

        }
        
        
        // Update User
        public Users(long id, string firstName, string lastName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted, bool isActive, long? facilityId, long? districtId, long? provinceId, string phoneNo, DateTime? dateCreated, long? createdBy, DateTime? dateEdited, long? editedBy)
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
            this.FacilityId = facilityId;
            this.DistrictId = districtId;
            this.ProvinceId = provinceId;
            this.PhoneNo = phoneNo;
            this.DateCreated = dateCreated;
            this.CreatedBy = createdBy;
            this.DateEdited = dateEdited;
            this.EditedBy = editedBy;

        }

        public Users(long id, string password, bool isSuperAdmin, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, long? provinceId, long? districtId, long? facilityId, DateTime? dateCreated, long? createdBy)
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
            this.ProvinceId = provinceId;
            this.DistrictId = districtId;
            this.FacilityId = facilityId;
        }

        public Users(int v1, string v2, bool v3, string v4, string v5, string v6, int v7, int v8, bool v9, bool v10, string v11)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
            this.v7 = v7;
            this.v8 = v8;
            this.v9 = v9;
            this.v10 = v10;
            this.v11 = v11;
        }

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

        public long? FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility Facilities { get; set; }

        public long? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District Districts { get; set; }

        public long? ProvinceId { get; set; }


        [ForeignKey("ProvinceId")]
        public virtual Province Provinces { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }


        public long? CreatedBy { get; set; }

        public long? EditedBy { get; set; }




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
