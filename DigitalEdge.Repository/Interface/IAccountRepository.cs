
using System.Collections.Generic;
using DigitalEdge.Domain;

namespace DigitalEdge.Repository
{
   public interface IAccountRepository     
    {
        List<UserModel> GetData();
        Users GetData(long id);
        Users GetLogin(string email, string password);
        Client GetClient(RegistrationModel data);
        Appointment GetAppointment(long id);
        void Delete(Users deleteuser);
        void updateUserFacility(UserFacility deletebuilsinguser);
        void updateServicePoint(ServicePoint updateservicepoint);
        void updateFacility(Facility updatefacility);
        string CreateUser(Users adduser);
        string createappointment(Appointment addappointment);
        string createclient(Client addclient);
        string facilitycreateuser(UserFacility addfacilityuser);
        string CreateFacility(Facility addfacility);
        string servicecreateuser(ServicePoint addfacilityuser);
        void updateUser(Users adduser);
        void UpdateAppointment(Appointment adduser);
        void UpdateClient(Client addclient);
        List<UserRoles> GetRoles();
        string GetRoleName(long RoleId);
        List<UserBindingModel> getFacilityDetails();
        List<FacilityModel> getFacilityUserDetails();
        List<ServicePointModel> getServiceDetails();

        int CountUsers();

        int ActiveUsers();
    }
}
