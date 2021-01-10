 using System.Collections.Generic;
using System.Data;
using System.Linq;

using DigitalEdge.Domain;

namespace DigitalEdge.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IBaseRepository<Users> _loginRepository;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<UserFacility> _facilityRepository;
        private readonly IBaseRepository<Facility> _facilityuserRepository;
        private readonly IBaseRepository<ServicePoint> _servicePointRepository;
        private readonly IBaseRepository<UserRoles> _userRolesRepository;
        private readonly DigitalEdgeContext _DigitalEdgeContext;


        public AccountRepository(IBaseRepository<Users> loginRepository,IBaseRepository<Client> clientRepository,IBaseRepository<Appointment> appointmentRepository, IBaseRepository<UserRoles> userRolesRepository,
            DigitalEdgeContext DigitalEdgeContext, IBaseRepository<UserFacility> facilityRepository,IBaseRepository<ServicePoint> servicePointRepository
            ,IBaseRepository<Facility> facilityuserRepository)
        {
            this._loginRepository = loginRepository;
            this._clientRepository = clientRepository;
            this._appointmentRepository = appointmentRepository;
            this._userRolesRepository = userRolesRepository;
            this._DigitalEdgeContext = DigitalEdgeContext;
            _facilityRepository = facilityRepository;
            _servicePointRepository = servicePointRepository;
            this._facilityuserRepository = facilityuserRepository;
        }
        public List<UserModel> GetData()
        {
            List<UserModel> users = (from p in _DigitalEdgeContext.Users
                                     from c in _DigitalEdgeContext.UserRoles
                                     where p.RoleId == c.RoleId
                                     select new UserModel
                                     {
                                         Id = p.Id,
                                         FirstName = p.FirstName,
                                         LastName = p.LastName,
                                         Email = p.Email,
                                         Gender = p.Gender,
                                         PhoneNo = p.PhoneNo,
                                         IsDeleted = p.IsDeleted,
                                         RoleName = c.RoleName,
                                         RoleId=(c.RoleId).ToString()
                                     }).ToList();
            return users;
        }
        public List<UserRoles> GetRoles()
        {
            List<UserRoles> userRoles = this._userRolesRepository.GetAll().ToList();
            return userRoles;
        }
        public Users GetData(long id)
        {
            Users users = this._loginRepository.Get().Where(x => x.Id == id).FirstOrDefault();
            return users;
        }
        public Users GetLogin(string email, string password)
        {
            var user = _loginRepository.GetAll().Where(x => x.Email == email && x.Password == password).SingleOrDefault();
            if (user == null)
                return null;
            return user;
        } 
        public Client GetClient(RegistrationModel data)
        {
            var user = _clientRepository.GetAll().Where(x => x.FirstName == data.FirstName && x.LastName == data.LastName && x.ClientPhoneNo == System.Convert.ToInt64(data.PhoneNo)).SingleOrDefault();
            if (user == null)
                return null;
            return user;
        }
        public string GetRoleName(long RoleId)
        {
            var RoleName = (from userRoles in _DigitalEdgeContext.UserRoles
                            where (userRoles.RoleId == RoleId)
                            select
                              userRoles.RoleName
                         ).SingleOrDefault();

            if (RoleName == null)
                return null;
            return RoleName;
        }
        public void createuser(Users users)
        {
            this._loginRepository.Insert(users);
        }
        public string createappointment(Appointment users)
        {
            if(_DigitalEdgeContext.Appointments.Any(o => o.ClientId.Equals(users.ClientId) && o.AppointmentDate.Date.Equals(users.AppointmentDate.Date))) return "null";

            this._appointmentRepository.Insert(users);
            return "ok";

        }
        public void createclient(Client users)
        {
            this._clientRepository.Insert(users);
        }
        public void updateUser(Users users)
        {
            this._loginRepository.Update(users);
        }
        public void updateAppointment(Appointment users)
        {
            this._appointmentRepository.Update(users);
        }
        public void updateUserFacility(UserFacility users)
        {
            this._facilityRepository.Update(users);
        }
        public void updateServicePoint(ServicePoint servicepoint)
        {
            this._servicePointRepository.Update(servicepoint);
        }  
        public string updateFacility(Facility updatefacility)
        {
            if (_DigitalEdgeContext.facility.Any(o => o.FacilityName.Equals(updatefacility.FacilityName) && o.DistrictId.Equals(updatefacility.DistrictId) && o.FacilityContactNumber.Equals(updatefacility.DistrictId))) return "null";
            this._facilityuserRepository.Update(updatefacility);
            return "ok";
        }
        public void Delete(Users deleteuser)
        {
            this._loginRepository.Delete(deleteuser);
        }
        public void DeleteFacility(UserFacility deletefacilityuser)
        {
            this._facilityRepository.Delete(deletefacilityuser);
        }
        public string facilitycreateuser(UserFacility addfacilityuser)
        {
            if (_DigitalEdgeContext.Userfacility.Any(o => o.FacilityId == addfacilityuser.FacilityId && o.ServicePointId==addfacilityuser.ServicePointId && o.IsActive==false && o.UserId.Equals(addfacilityuser.UserId))) return "null";

               this._facilityRepository.Insert(addfacilityuser);
              return "ok";
        } 
        public string facilitywithdistrictcreateuser(Facility adduser)
        {
            if (_DigitalEdgeContext.facility.Any(o => o.FacilityName.Equals(adduser.FacilityName) && o.DistrictId.Equals(adduser.DistrictId))) return "null";

            this._facilityuserRepository.Insert(adduser);
              return "ok";

        }
        public string servicecreateuser(ServicePoint addserviceuser)
        {
            if (_DigitalEdgeContext.ServicePoints.Any(o => o.ServicePointName == addserviceuser.ServicePointName && o.FacilityId.Equals(addserviceuser.FacilityId))) return "null";

            this._servicePointRepository.Insert(addserviceuser);
            return "ok";
        }
        public List<UserBindingModel> getFacilityDetails()
        {
            List<UserBindingModel> facilityuserslist = (from d in _DigitalEdgeContext.Users
                                                       join c in _DigitalEdgeContext.Userfacility on d.Id equals c.UserId
                                                       join s in _DigitalEdgeContext.facility on c.FacilityId equals s.FacilityId
                                                        join g in _DigitalEdgeContext.ServicePoints on c.ServicePointId equals g.ServicePointId

                                                        where (c.IsActive == false)
                                                        select new UserBindingModel
                                                       {
                                                           UserId = d.Id,
                                                           FacilityName = s.FacilityName,
                                                           UserName = d.Email,
                                                           UserFacilityId=c.UserFacilityId,
                                                           FacilityId = s.FacilityId,
                                                           ServicePointId=g.ServicePointId,
                                                           ServicePointName = g.ServicePointName,                                                           
                                                       }).ToList();
            return facilityuserslist;
        }
          public List<FacilityModel> getFacilityUserDetails()
        {
            List<FacilityModel> facilityuserslist = (from d in _DigitalEdgeContext.facility
                                                       join c in _DigitalEdgeContext.Districts on d.DistrictId equals c.DistrictId
                                                       join f in _DigitalEdgeContext.Provinces on c.ProvinceId equals f.ProvinceId
                                                     select new FacilityModel
                                                        {
                                                           FacilityName = d.FacilityName,
                                                           FacilityId = d.FacilityId,
                                                           DistrictName = c.DistrictName,
                                                            DistrictId = c.DistrictId.ToString(),
                                                            FacilityContactNumber=d.FacilityContactNumber,
                                                            ProvinceId=f.ProvinceId.ToString(),
                                                            ProvinceName=f.ProvinceName
                                                       }).ToList();
            return facilityuserslist;
        }        
        public List<ServicePointModel> getServiceDetails()
        {
            List<ServicePointModel> serviceuserslist = (from d in _DigitalEdgeContext.facility
                                                        join c in _DigitalEdgeContext.ServicePoints on d.FacilityId equals c.FacilityId
                                                        select new ServicePointModel
                                                      {                                                            
                                                            FacilityId = d.FacilityId,
                                                            ServicePointId = c.ServicePointId,
                                                            ServicePointName = c.ServicePointName,
                                                            FacilityName = d.FacilityName,
                                                        }).ToList();
            return serviceuserslist;
        }

           
    }
}

