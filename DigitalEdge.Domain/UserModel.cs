using System;
using Microsoft.AspNetCore.Http;

namespace DigitalEdge.Domain
{
    public class UserModel
    {
        public UserModel()
        {
        }
        public UserModel(long id, string firstName, string lastName, string email, string phoneNo, string roleId, Boolean isDeleted, string gender, string roleName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNo = phoneNo;
            this.RoleId = roleId;
            this.IsDeleted = isDeleted;
            this.Gender = gender;
            this.RoleName = roleName;
        }
        public UserModel(long id, string password, string isSuperAdmin, string firstName, string lastName, string email, string phoneNo, string roleId, Boolean isDeleted, string gender)
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
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string RoleId { get; set; }
        public string IsSuperAdmin { get; set; }
        public Boolean IsDeleted { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }
    }
    public class FormFileData
    {
        public IFormFileCollection FileToUpload { get; set; }
    }
}

