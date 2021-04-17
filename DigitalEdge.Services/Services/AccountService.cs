using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ServiceStack;
using DigitalEdge.Domain;
using DigitalEdge.Repository;

namespace DigitalEdge.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppSettings _appSettings;

        public AccountService(IAccountRepository accountRepository, Microsoft.Extensions.Options.IOptions<AppSettings> appSettings)
        {
            this._accountRepository = accountRepository;
                _appSettings = appSettings.Value;

        }
        public List<UserModel> getData()
        {
            List<UserModel> users = _accountRepository.GetData().Where(x=>x.IsDeleted==false).Select(x => new UserModel(x.Id,x.FirstName, x.LastName, x.Email, x.PhoneNo, x.RoleId, x.IsDeleted, x.Gender,x.RoleName)).ToList();

         
            if (users == null)
                return null;
                return (users);
        }  
        public List<UserRolesModel> getRoles()
        {
                List<UserRolesModel> userRoles = _accountRepository.GetRoles().Where(x=>x.IsDeleted==false).Select(x => new UserRolesModel(x.RoleId ,x.RoleName,x.Description,x.IsDeleted,x.CreatedBy,x.ModifiedBy)).ToList();
            if (userRoles == null)
                return null;
                return (userRoles);
        }
        public UserModel GetData(long id)
        {
                Users user = _accountRepository.GetData(id);
                UserModel userModel= new UserModel(user.Id,user.Password,user.IsSuperAdmin.ToString(),user.FirstName, user.LastName, user.Email, user.PhoneNo, user.RoleId, user.IsDeleted, user.Gender);
                if (userModel == null)
                    return null;
                return (userModel);
        }

        public AppointmentsModel  GetAppointment(long id)
        {
            Appointment appointment = _accountRepository.GetAppointment(id);
            AppointmentsModel appointmentModel = new AppointmentsModel(appointment.AppointmentId, appointment.ClientId, appointment.FacilityId, appointment.ServicePointId, appointment.AppointmentDate, appointment.DateCreated, appointment.DateEdited, appointment.EditedBy, appointment.CreatedBy, appointment.AppointmentStatus);
            if (appointmentModel == null)
                return null;
            return (appointmentModel);

        }
        public UserModel ValidateUser(string email, string password)
        {
            Users user = _accountRepository.GetLogin(email, password);
                if (user == null)
                    return null;
                UserModel userModel = new UserModel(user.Id,user.FirstName, user.LastName, user.Email, user.PhoneNo, user.RoleId, user.IsDeleted, user.Gender, "");
            return (userModel);
        }
        public ClientModel ValidateClient(RegistrationModel data)
        {
            Client user = _accountRepository.GetClient(data);
            if (user == null)
                return null;
            ClientModel userModel = new ClientModel(user.ClientId,user.FirstName,user.LastName, user.ArtNo);
            return (userModel);
        }

        public string AddUser(UserModel user)
        {
            //Users userData = new Users(user.Id, user.Password, false, user.FirstName, user.LastName, user.Email, user.ClientPhoneNo,Convert.ToInt64(user.RoleId), false, user.Gender);
            Users userData = new Users(user.Id, user.FirstName, user.Password, user.Email, user.RoleId, false, false);
            string result = this._accountRepository.createuser(userData);

            return result;
            


        }
        public string AddFacilityUser(UserBindingModel user)
        {                   
                Facility userData = new Facility(user.FacilityId, user.FacilityName,user.FacilityContactNumber, user.FacilityTypeId, user.IsAvailable, user.Address);
                string result = this._accountRepository.facilitywithdistrictcreateuser(userData);
                return result;

            //else {
            //    UserFacility userData = new UserFacility(Convert.ToInt32(user.Facility), user.UserId, false, user.UserFacilityId, Convert.ToInt32(user.ServicePoint));
            //    result = this._accountRepository.facilitycreateuser(userData);
            //    return result;
            //}         
        }
        public string AddServicePoint(ServicePointModel user)
        {
            ServicePoint userData = new ServicePoint(user.ServicePointId,user.ServicePointName , user.FacilityId);

        string reuslt= this._accountRepository.servicecreateuser(userData);
            return reuslt;
        }
        public void UpdateUser(UserModel user)
        {

            Users adduser = new Users(user.Id, user.Password, false, user.FirstName, user.LastName, user.Email, user.PhoneNo, user.RoleId ,user.IsDeleted, user.Gender);

            this._accountRepository.updateUser(adduser);
        }
        public void UpdateAppointment (RegistrationModel appointment)
        {
            Appointment updateuser = new Appointment(appointment.AppointmentId, Convert.ToInt64(appointment.ClientId), Convert.ToInt64(appointment.FacilityId), Convert.ToInt64(appointment.ServicePointId), Convert.ToDateTime(appointment.AppointmentDate), appointment.GetInteractionDateAndTime(), Convert.ToDateTime(appointment.AppointmentDate), appointment.AppointmentStatus, appointment.Detail, appointment.DateEdited, appointment.EditedBy);
            this._accountRepository.UpdateAppointment(updateuser);
        }
        public void UpdateClient(RegistrationModel client) 
        {
            Client updateclient = new Client(client.ClientId, client.FirstName, client.LastName, Convert.ToDateTime(client.DateOfBirth), Convert.ToDateTime(client.EnrollmentDate), client.FacilityId, client.ClientStatusId, client.StatusCommentId, client.ArtNo, client.SexId, client.ClientTypeId, client.ServicePointId, client.LanguageId, client.Address,
                client.EnrolledBy, client.EnrolledByPhone, client.GeneralComment, client.ClientPhoneNo, client.AlternativePhoneNumber1, client.PhoneVerifiedByAnalyst, client.PhoneVerifiedByFacilityStaff);
            this._accountRepository.UpdateClient(updateclient);
        }


        public void DeleteUser(UserModel user)
        {
            Users deleteUser = new Users(user.Id, user.Password, Convert.ToBoolean(user.IsSuperAdmin), user.FirstName, user.LastName, user.Email, user.PhoneNo, Convert.ToInt32(user.RoleId), Convert.ToBoolean(user.IsDeleted), user.Gender);

            this._accountRepository.Delete(deleteUser);
        }
        public string GetToken(UserModel user)
        {
            if (user == null)
                return null;
            string RoleName = _accountRepository.GetRoleName(Convert.ToInt64(user.RoleId));
            if (RoleName == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.FirstName.ToString()),
                     new Claim(ClaimTypes.Role, RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return (token);
        }

        public List<UserBindingModel> getFacilityData()
        {
            List<UserBindingModel> userFacility= _accountRepository.getFacilityDetails().Where(x=>x.IsAvailable == false).ToList();

            if (userFacility == null)
                return null;
            return (userFacility);
        }
        public List<FacilityModel> getFacilityUserData()
        {
            List<FacilityModel> facilitydetails= _accountRepository.getFacilityUserDetails().ToList();

            if (facilitydetails == null)
                return null;
            return (facilitydetails);
        }
        public void UpdateFacilityUser(UserBindingModel user)
        {
            UserFacility deletefacilityUser = new UserFacility(user.FacilityId ,user.UserId,user.IsAvailable, user.UserFacilityId , user.ServicePointId);

            this._accountRepository.updateUserFacility(deletefacilityUser);
        } 
        public void UpdateFacility(FacilityModel updateFacility)
        {
            Facility updateFacilityDetails = new Facility(updateFacility.FacilityId,updateFacility.FacilityName, updateFacility.FacilityContactNumber, updateFacility.FacilityTypeId, updateFacility.IsAvailable, updateFacility.Address);
            this._accountRepository.updateFacility(updateFacilityDetails);
            
        }
        public void UpdateServicePoint(ServicePointModel servicePoint)
        {
            ServicePoint updatefacilityUser = new ServicePoint(servicePoint.ServicePointId,servicePoint.ServicePointName,servicePoint.FacilityId );

            this._accountRepository.updateServicePoint(updatefacilityUser);
        }   

        public List<ServicePointModel> getServiceData()
        {
            List<ServicePointModel> userFacility = _accountRepository.getServiceDetails().ToList();

            if (userFacility == null)
                return null;
            return (userFacility);
        }

        public string AddAppointment(RegistrationModel addappointment)
        {
            Appointment appointmentData = new Appointment(addappointment.Id, addappointment.ClientId, addappointment.FacilityId,
              addappointment.ServicePointId, addappointment.GetAppointmentDateAndTime(), addappointment.AppointmentStatus, addappointment.Detail,
                addappointment.DateCreated = addappointment.GetCreatedDate(), addappointment.DateEdited,addappointment.EditedBy,addappointment.CreatedBy);  
          string result=this._accountRepository.createappointment(appointmentData);
            return result;
        }
        public string AddClient(RegistrationModel addclient)
        {
            //Client clientData = new Client(addclient.Id, addclient.FirstName, addclient.MiddleName, addclient.LastName, Convert.ToInt64(addclient.ClientPhoneNo)
            //    , addclient.DateOfBirth, addclient.Age, addclient.CurrentAge, addclient.NextOfKinName, addclient.NextOfKinContact, addclient.NextOfClientID,
            //    addclient.DateCreated, addclient.DateEdited, addclient.EditedBy, addclient.CreatedBy);
            Client clientData = new Client(addclient.ClientId, addclient.FirstName, addclient.LastName, Convert.ToDateTime(addclient.DateOfBirth), Convert.ToDateTime(addclient.EnrollmentDate), addclient.FacilityId, addclient.ClientStatusId, addclient.StatusCommentId, addclient.ArtNo, addclient.SexId, addclient.ClientTypeId, addclient.ServicePointId, addclient.LanguageId, addclient.Address,
                addclient.EnrolledBy, addclient.EnrolledByPhone, addclient.GeneralComment, addclient.ClientPhoneNo, addclient.AlternativePhoneNumber1, addclient.PhoneVerifiedByAnalyst, addclient.PhoneVerifiedByFacilityStaff);

           string result =  this._accountRepository.createclient(clientData);

            return result;
        }
        public List<ClientModel> getClient()
        {
            throw new NotImplementedException();
        }
        
    }
}
