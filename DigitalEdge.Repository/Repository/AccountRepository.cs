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
        private readonly IBaseRepository<ViralLoad> _viralLoadRepository;
        private readonly DigitalEdgeContext _DigitalEdgeContext;


        public AccountRepository(IBaseRepository<Users> loginRepository, IBaseRepository<Client> clientRepository, IBaseRepository<Appointment> appointmentRepository, IBaseRepository<UserRoles> userRolesRepository,
            DigitalEdgeContext DigitalEdgeContext, IBaseRepository<UserFacility> facilityRepository, IBaseRepository<ServicePoint> servicePointRepository
            , IBaseRepository<Facility> facilityuserRepository, IBaseRepository<ViralLoad> viralLoadRepository)
        {
            this._loginRepository = loginRepository;
            this._clientRepository = clientRepository;
            this._appointmentRepository = appointmentRepository;
            this._userRolesRepository = userRolesRepository;
            this._DigitalEdgeContext = DigitalEdgeContext;
            _facilityRepository = facilityRepository;
            _servicePointRepository = servicePointRepository;
            this._facilityuserRepository = facilityuserRepository;
            this._viralLoadRepository = viralLoadRepository;
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
                                         Password = p.Password,
                                         Email = p.Email,
                                         FacilityId = p.FacilityId,
                                         PhoneNo = p.PhoneNo,
                                         IsDeleted = p.IsDeleted,
                                         RoleName = c.RoleName,
                                         RoleId = c.RoleId,
                                         ProvinceId = p.ProvinceId,
                                         DistrictId = p.DistrictId
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
            var user = _clientRepository.GetAll().Where(x => x.FirstName == data.FirstName && x.LastName == data.LastName && x.ArtNo == data.ArtNo).SingleOrDefault();
            if (user == null)
                return null;
            return user;
        }
        public Appointment GetAppointment(long id)
        {
            var appointment = _DigitalEdgeContext.Appointments.Find(id);
            if (appointment == null)
                return null;
            return appointment;
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
        public string CreateUser(Users users)
        {
            if (_DigitalEdgeContext.Users.Any(u => u.FirstName.Equals(users.FirstName) && u.Email.Equals(users.Email))) return "null";
            this._loginRepository.Insert(users);
            return "Ok";
        }
        public string createappointment(Appointment users)
        {
            if (_DigitalEdgeContext.Appointments.Any(o => o.AppointmentId.Equals(users.AppointmentId))) return "null";

            this._appointmentRepository.Insert(users);
            return "ok";

        }
        public string createclient(Client users)
        {
            if (_DigitalEdgeContext.Clients.Any(c => c.ArtNo.Equals(users.ArtNo))) return "null";
            this._clientRepository.Insert(users);
            return "Ok";
        }
        public void updateUser(Users users)
        {
            this._loginRepository.Update(users);
        }

        public void UpdateAppointment(Appointment users)
        {
            this._appointmentRepository.Update(users);
        }

        public void UpdateClient(Client client)
        {
            this._clientRepository.Update(client);
        }

        public void updateUserFacility(UserFacility users)
        {
            this._facilityRepository.Update(users);
        }

        public void updateServicePoint(ServicePoint servicepoint)
        {
            this._servicePointRepository.Update(servicepoint);
        }
        public void updateFacility(Facility updatefacility)
        {
            this._facilityuserRepository.Update(updatefacility);

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
            if (_DigitalEdgeContext.Userfacility.Any(o => o.FacilityId == addfacilityuser.FacilityId && o.ServicePointId == addfacilityuser.ServicePointId && o.IsActive == false && o.UserId.Equals(addfacilityuser.UserId))) return "null";

            this._facilityRepository.Insert(addfacilityuser);
            return "ok";
        }
        public string CreateFacility(Facility adduser)
        {
            if (_DigitalEdgeContext.Facilities.Any(o => o.FacilityName.Equals(adduser.FacilityName))) return "null";

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
                                                        join s in _DigitalEdgeContext.Facilities on c.FacilityId equals s.FacilityId
                                                        join g in _DigitalEdgeContext.ServicePoints on c.ServicePointId equals g.ServicePointId

                                                        where (c.IsActive == false)
                                                        select new UserBindingModel
                                                        {
                                                            UserId = d.Id,
                                                            FacilityName = s.FacilityName,
                                                            UserName = d.Email,
                                                            UserFacilityId = c.UserFacilityId,
                                                            FacilityId = s.FacilityId,
                                                            ServicePointId = g.ServicePointId,
                                                            ServicePointName = g.ServicePointName,
                                                        }).ToList();
            return facilityuserslist;
        }
        public List<FacilityModel> getFacilityUserDetails()
        {
            List<FacilityModel> facilityuserslist = (from d in _DigitalEdgeContext.Facilities
                                                     join c in _DigitalEdgeContext.Districts on d.DistrictId equals c.DistrictId
                                                     join f in _DigitalEdgeContext.Provinces on c.ProvinceId equals f.ProvinceId
                                                     select new FacilityModel
                                                     {
                                                         FacilityName = d.FacilityName,
                                                         FacilityId = d.FacilityId,
                                                         DistrictName = c.DistrictName,
                                                         DistrictId = c.DistrictId,
                                                         FacilityContactNumber = d.FacilityContactNumber,
                                                         ProvinceId = f.ProvinceId.ToString(),
                                                         ProvinceName = f.ProvinceName
                                                     }).ToList();
            return facilityuserslist;
        }
        public List<ServicePointModel> getServiceDetails()
        {
            List<ServicePointModel> serviceuserslist = (from d in _DigitalEdgeContext.Facilities
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

        public int CountUsers()
        {
            var users = _DigitalEdgeContext.Users.Count();

            return users;
        }

        public int ActiveUsers()
        {
            var users = _DigitalEdgeContext.Users.Where(u => u.IsActive == true).Count();

            return users;
        }

        public string AddVLResult(ViralLoad result)
        {
            if (_DigitalEdgeContext.ViralLoadResults.Any(o => o.ViralLoadId.Equals(result.ViralLoadId))) return "null";

            this._viralLoadRepository.Insert(result);
            return "ok";
        }

        public int CountUsersInFacility(long facilityId)
        {
            var count = _DigitalEdgeContext.Users.Count(user => user.FacilityId == facilityId);

            return count;
        }

        

        public List<UserModel> GetUsersByFacility(long facilityId)
        {
            List<UserModel> users = (from p in _DigitalEdgeContext.Users
                                     from c in _DigitalEdgeContext.UserRoles
                                     where (p.FacilityId == facilityId &&  p.RoleId == c.RoleId)
                                     select new UserModel
                                     {
                                         Id = p.Id,
                                         FirstName = p.FirstName,
                                         LastName = p.LastName,
                                         Password = p.Password,
                                         Email = p.Email,
                                         FacilityId = p.FacilityId,
                                         PhoneNo = p.PhoneNo,
                                         IsDeleted = p.IsDeleted,
                                         RoleName = c.RoleName,
                                         RoleId = c.RoleId
                                     }).ToList();
            return users;
        }
    }
}

