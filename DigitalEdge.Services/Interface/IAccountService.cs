using System.Collections.Generic;
using DigitalEdge.Domain;

namespace DigitalEdge.Services
{
    public interface IAccountService
    {
        List<UserModel> getData();
        List<ClientModel> getClient();
        List<UserBindingModel> getFacilityData();
        List<ServicePointModel> getServiceData();
        List<FacilityModel> getFacilityUserData();
        UserModel ValidateUser(string email, string password);
        ClientModel ValidateClient(RegistrationModel user);
        string GetToken(UserModel user);
        void DeleteUser(UserModel deleteuser);
        void AddUser(UserModel adduser);
        string AddAppointment(RegistrationModel addappointment);
        void AddClient(RegistrationModel addclient);
        string AddFacilityUser(UserBindingModel adduser);
        string AddServicePoint(ServicePointModel adduser);
        void UpdateUser(UserModel adduser);
        void UpdateAppointment(RegistrationModel addupdate);
        void UpdateFacilityUser(UserBindingModel adduser);
        string UpdateFacility(FacilityModel adduser);
        void UpdateServicePoint(ServicePointModel updateservicepoint);
        UserModel getData(long id);
        List<UserRolesModel> getRoles();
    }
}
