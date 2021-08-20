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
        public List<UserModel> GetData()
        {
            List<UserModel> users = _accountRepository.GetData().Where(x => x.IsDeleted == false).Select(x => new UserModel(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNo, x.RoleId, x.IsDeleted, x.FacilityId, x.DistrictId, x.ProvinceId, x.DateCreated, x.CreatedBy, x.DateEdited, x.EditedBy)).ToList();


            if (users == null)
                return null;
            return (users);
        }
        public List<UserRolesModel> getRoles()
        {
            List<UserRolesModel> userRoles = _accountRepository.GetRoles().Where(x => x.IsDeleted == false).Select(x => new UserRolesModel(x.RoleId, x.RoleName, x.Description, x.IsDeleted, x.CreatedBy, x.ModifiedBy)).ToList();
            if (userRoles == null)
                return null;
            return (userRoles);
        }
        public UserModel GetData(long id)
        {
            Users user = _accountRepository.GetData(id);
            UserModel userModel = new UserModel(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNo, user.RoleId, user.IsDeleted, user.FacilityId, user.DistrictId, user.ProvinceId, user.DateCreated, user.CreatedBy, user.DateEdited, user.EditedBy);
            if (userModel == null)
                return null;
            return (userModel);
        }

        public AppointmentsModel GetAppointment(long id)
        {
            Appointment appointment = _accountRepository.GetAppointment(id);
            AppointmentsModel appointmentModel = new AppointmentsModel(appointment.AppointmentId, appointment.ClientId, appointment.FacilityId, appointment.ServiceTypeId, appointment.AppointmentDate, appointment.DateCreated, appointment.DateEdited, appointment.EditedBy, appointment.CreatedBy, appointment.AppointmentStatus);
            if (appointmentModel == null)
                return null;
            return (appointmentModel);

        }
        public UserModel ValidateUser(string email, string password)
        {
            Users user = _accountRepository.GetLogin(email, password);
            if (user == null)
                return null;
            UserModel userModel = new UserModel(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNo, user.RoleId, user.IsDeleted, user.FacilityId, user.DistrictId, user.ProvinceId, user.DateCreated, user.CreatedBy, user.DateEdited, user.EditedBy);
            return (userModel);
        }
        public ClientModel ValidateClient(RegistrationModel data)
        {
            Client user = _accountRepository.GetClient(data);
            if (user == null)
                return null;
            ClientModel userModel = new ClientModel(user.ClientId, user.FirstName, user.LastName, user.ArtNo);
            return (userModel);
        }

        public string AddUser(UserModel user)
        {
            if (user.RoleId == 1)
            {
                Users superAdminData = new Users(user.Id, user.FirstName, user.LastName, user.Password, user.Email, user.RoleId, true, false, true, user.FacilityId, user.DistrictId, user.ProvinceId, user.PhoneNo, user.DateCreated = user.GetDateToday(), user.CreatedBy);
                string result1 = this._accountRepository.CreateUser(superAdminData);
                return result1;

            }
            Users userData = new Users(user.Id, user.FirstName, user.LastName, user.Password, user.Email, user.RoleId, false, false, true, user.FacilityId, user.DistrictId, user.ProvinceId, user.PhoneNo, user.DateCreated = user.GetDateToday(), user.CreatedBy);
            string result = this._accountRepository.CreateUser(userData);
            return result;


        }
        public string CreateFacility(UserBindingModel facility)
        {
            Facility facilityData = new Facility(facility.FacilityId, facility.FacilityName, Convert.ToString(facility.FacilityContactNumber), facility.FacilityTypeId, facility.IsAvailable, facility.Address, facility.DateCreated = DateTime.Now, facility.CreatedBy, facility.DistrictId);
            string result = this._accountRepository.CreateFacility(facilityData);
            return result;
        }
        public string AddServicePoint(ServicePointModel user)
        {
            ServicePoint userData = new ServicePoint(user.ServicePointId, user.ServicePointName, user.FacilityId);

            string reuslt = this._accountRepository.servicecreateuser(userData);
            return reuslt;
        }
        public void UpdateUser(UserModel user)
        {
            if (user.RoleId == 1)
            {
                Users editUser = new Users(user.Id, user.FirstName, user.LastName, user.Password, user.Email, user.RoleId, true, false, true, user.FacilityId, user.DistrictId, user.ProvinceId, user.PhoneNo, user.DateCreated, user.CreatedBy, user.DateEdited = DateTime.Now, user.EditedBy);

                this._accountRepository.updateUser(editUser);
            }
            Users adduser = new Users(user.Id, user.FirstName, user.LastName, user.Password, user.Email, user.RoleId, false, false, true, user.FacilityId, user.DistrictId, user.ProvinceId, user.PhoneNo, user.DateCreated, user.CreatedBy, user.DateEdited = DateTime.Now, user.EditedBy);

            this._accountRepository.updateUser(adduser);
        }
        public void UpdateAppointment(RegistrationModel appointment)
        {
            Appointment updateuser = new Appointment(appointment.AppointmentId, Convert.ToInt64(appointment.ClientId), Convert.ToInt64(appointment.FacilityId), Convert.ToInt64(appointment.ServiceTypeId), Convert.ToDateTime(appointment.AppointmentDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), appointment.GetInteractionDateAndTime(), appointment.AppointmentStatus, appointment.Comment, appointment.DateCreated, appointment.DateEdit = appointment.GetDateCreated(), appointment.CreatedBy, appointment.EditedBy);
            this._accountRepository.UpdateAppointment(updateuser);
        }
        public void UpdateClient(RegistrationModel client)
        {
            Client updateclient = new Client(client.ClientId, client.FirstName, client.LastName, client.ArtNo, client.SexId, client.ClientTypeId, client.ClientStatusId,
                client.StatusCommentId, client.FacilityId, Convert.ToDateTime(client.DateOfBirth, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), client.Age, Convert.ToDateTime(client.EnrollmentDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat), Convert.ToString(client.ClientPhoneNo),
                Convert.ToString(client.AlternativePhoneNumber1), client.PhoneVerifiedByAnalyst, client.PhoneVerifiedByFacilityStaff, client.Zone, client.Village, client.HouseNo,  client.GISLocation, Convert.ToString(client.EnrolledByPhone), client.ServicePointId,
                client.LanguageId, client.EnrolledByName, client.GeneralComment, client.EnrollmentType, client.ClientRelationship, client.AccessToPhone,
                client.HamornizedMobilePhone, client.HarmonizedPhysicalAddress, client.DateCreated, client.DateEdit = client.GetDateToday(), client.CreatedBy, client.EditedBy);
            this._accountRepository.UpdateClient(updateclient);
        }

        public void DeleteUser(UserModel user)
        {
            Users deleteUser = new Users(user.Id, user.Password, user.IsSuperAdmin, user.FirstName, user.LastName, user.Email, user.PhoneNo, Convert.ToInt32(user.RoleId), Convert.ToBoolean(user.IsDeleted), user.ProvinceId, user.DistrictId, user.FacilityId, user.DateCreated, user.CreatedBy);

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
            List<UserBindingModel> userFacility = _accountRepository.getFacilityDetails().Where(x => x.IsAvailable == false).ToList();

            if (userFacility == null)
                return null;
            return (userFacility);
        }
        public List<FacilityModel> getFacilityUserData()
        {
            List<FacilityModel> facilitydetails = _accountRepository.getFacilityUserDetails().ToList();

            if (facilitydetails == null)
                return null;
            return (facilitydetails);
        }
        public void UpdateFacilityUser(UserBindingModel user)
        {
            UserFacility deletefacilityUser = new UserFacility(user.FacilityId, user.UserId, user.IsAvailable, user.UserFacilityId, user.ServicePointId);

            this._accountRepository.updateUserFacility(deletefacilityUser);
        }
        public void UpdateFacility(FacilityModel updateFacility)
        {
            Facility updateFacilityDetails = new Facility(updateFacility.FacilityId, updateFacility.DistrictId, updateFacility.FacilityName, updateFacility.FacilityContactNumber, updateFacility.FacilityTypeId, updateFacility.IsAvailable, updateFacility.Address, updateFacility.DateCreated, updateFacility.DateEdited, updateFacility.CreatedBy, updateFacility.EditedBy);
            this._accountRepository.updateFacility(updateFacilityDetails);

        }
        public void UpdateServicePoint(ServicePointModel servicePoint)
        {
            ServicePoint updatefacilityUser = new ServicePoint(servicePoint.ServicePointId, servicePoint.ServicePointName, servicePoint.FacilityId);

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
            Appointment appointmentData = new Appointment(addappointment.AppointmentId, addappointment.ClientId, addappointment.FacilityId, addappointment.ServiceTypeId,
                addappointment.GetAppointmentDateAndTime(), addappointment.AppointmentStatus, addappointment.Comment, addappointment.GetDateToday(), addappointment.CalculateDaysLate(), addappointment.CreatedBy);

            string result = this._accountRepository.createappointment(appointmentData);

            return result;
        }
        public string AddClient(RegistrationModel addclient)
        {

            Client clientData = new Client(addclient.ClientId, addclient.FirstName, addclient.LastName, addclient.ArtNo, addclient.SexId, addclient.ClientTypeId, addclient.ClientStatusId,
                addclient.StatusCommentId, addclient.FacilityId, Convert.ToDateTime(addclient.DateOfBirth), addclient.CalculateAge(), Convert.ToDateTime(addclient.EnrollmentDate), addclient.ClientPhoneNo, addclient.AlternativePhoneNumber1, addclient.PhoneVerifiedByAnalyst, addclient.PhoneVerifiedByFacilityStaff, addclient.Zone, addclient.Village, addclient.HouseNo, addclient.GISLocation, addclient.EnrolledByPhone, addclient.ServicePointId,
                addclient.LanguageId, addclient.EnrolledByName, addclient.GeneralComment, addclient.EnrollmentType, addclient.ClientRelationship, addclient.AccessToPhone,
                addclient.HamornizedMobilePhone, addclient.HarmonizedPhysicalAddress, addclient.DateCreated = addclient.GetDateCreated(), addclient.CreatedBy);

            string result = this._accountRepository.createclient(clientData);

            return result;
        }
        public List<ClientModel> getClient()
        {
            throw new NotImplementedException();
        }

        public int CountUsers()
        {
            return _accountRepository.CountUsers();
        }

        public int ActiveUsers()
        {
            return _accountRepository.ActiveUsers();
        }

        public string AddViralLoad(ViralLoadModel viralLoad)
        {
            ViralLoad result = new ViralLoad(viralLoad.ViralLoadId, viralLoad.ClientId, viralLoad.InitialViralLoadCount, viralLoad.CurrentViralLoadCount, viralLoad.NextVLDueDate, viralLoad.DateCreated = DateTime.Now);

            string vlResult = _accountRepository.AddVLResult(result);

            return vlResult;

        }

        public int CountUsersInFacility(long facilityId)
        {
            return _accountRepository.CountUsersInFacility(facilityId);
        }

        public List<UserModel> GetUsersByFacility(long facilityId)
        {
            List<UserModel> users = _accountRepository.GetUsersByFacility(facilityId).ToList();

            if (users == null)
                return null;
             return (users);
                    
        }
    }
}
