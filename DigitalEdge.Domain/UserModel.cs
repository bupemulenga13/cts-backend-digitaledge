using System;
using Microsoft.AspNetCore.Http;

namespace DigitalEdge.Domain
{
    public class UserModel
    {
        public UserModel()
        {
        }
        public UserModel(long id, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, long? facilityId, string roleName, long? provinceId, long? districtId)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.FacilityId = facilityId;
            this.RoleName = roleName;
            this.ProvinceId = provinceId;
            this.DistrictId = districtId;
        }
        public UserModel(long id, string firstName, string password, string email, long roleId, bool isSuperAdmin, bool isDeleted )
        {
            this.Id = id;
            this.FirstName = firstName;
            this.Password = password;
            this.Email = email;
            this.RoleId = roleId;
            this.IsSuperAdmin = isSuperAdmin;
            this.IsDeleted = isDeleted;

        }
        public UserModel(long id, string password, string isSuperAdmin, string firstName, string lastName, string email, string phoneNo, long roleId, Boolean isDeleted, long? provinceId, long? districtId, long? facilityId)
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
            this.ProvinceId = provinceId;
            this.DistrictId = districtId;
            this.FacilityId = facilityId;

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
        public long? ProvinceId { get; set; }

        public long? DistrictId { get; set; }

        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        


    }
    public class FormFileData
    {
        public IFormFileCollection FileToUpload { get; set; }
    }
}

