
using System;
using Microsoft.AspNetCore.Http;

namespace DigitalEdge.Domain
{
    public class UserModel
    {
        public UserModel()
        {
        }
        public UserModel(long id, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, long? facilityId, long? districtId, long? provinceId, DateTime? dateCreated, long? createdBy)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.FacilityId = facilityId;
            this.DistrictId = districtId;
            this.ProvinceId = provinceId;
            this.DateCreated = dateCreated;
            this.CreatedBy = createdBy;
        }

        public UserModel(long id, string password, bool isSuperAdmin, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, long? provinceId, long? districtId, long? facilityId, DateTime? dateCreated, long? createdBy)
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

        public UserModel(long id, string firstName, string lastName, string email, string phoneNo, long roleId, bool isDeleted, long? facilityId, long? districtId, long? provinceId, DateTime? dateCreated, long? createdBy, DateTime? dateEdited, long? editedBy) 
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.FacilityId = facilityId;
            this.ProvinceId = provinceId;
            this.DistrictId = districtId;
            this.DateCreated = dateCreated;
            this.CreatedBy = createdBy;
            this.DateEdited = dateEdited;
            this.EditedBy = editedBy;
        }

        public UserModel(long id, string firstName, string lastName, string email, string phoneNo, long roleId, bool isDeleted, long? facilityId, long? districtId, long? provinceId, DateTime? dateCreated, long? createdBy, DateTime? dateEdited, long? editedBy, 
            string roleName, string districtName, string provinceName) 
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNo = phoneNo;
            RoleId = roleId;
            IsDeleted = isDeleted;
            FacilityId = facilityId;
            DistrictId = districtId;
            ProvinceId = provinceId;
            DateCreated = dateCreated;
            CreatedBy = createdBy;
            DateEdited = dateEdited;
            EditedBy = editedBy;
            RoleName = roleName;
            DistrictName = districtName;
            ProvinceName = provinceName;

              
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public long RoleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsDeleted { get; set; }

        public long? FacilityId { get; set; }

        public string FacilityName { get; set; }

        public string DistrictName { get; set; }

        public string ProvinceName { get; set; }

        public long? ProvinceId { get; set; }

        public long? DistrictId { get; set; }

        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        public long? CreatedBy { get; set; }

        public long? EditedBy { get; set; }

        public DateTime GetDateToday()
        {
            return DateTime.Now;
        }


    }
    
    public class FormFileData
    {
        public IFormFileCollection FileToUpload { get; set; }
    }


}

