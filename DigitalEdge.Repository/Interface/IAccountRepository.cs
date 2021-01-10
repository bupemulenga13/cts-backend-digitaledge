
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
        void Delete(Users deleteuser);
        void updateUserFacility(UserFacility deletebuilsinguser);
        void updateServicePoint(ServicePoint updateservicepoint);
        string updateFacility(Facility updatefacility);
        void createuser(Users adduser);
        string createappointment(Appointment addappointment);
        void createclient(Client addclient);
        string facilitycreateuser(UserFacility addfacilityuser);
        string facilitywithdistrictcreateuser(Facility addfacilityuser);
        string servicecreateuser(ServicePoint addfacilityuser);
        void updateUser(Users adduser);
        void updateAppointment(Appointment adduser);
        List<UserRoles> GetRoles();
        string GetRoleName(long RoleId);
        List<UserBindingModel> getFacilityDetails();
        List<FacilityModel> getFacilityUserDetails();
        List<ServicePointModel> getServiceDetails();

    }
}
