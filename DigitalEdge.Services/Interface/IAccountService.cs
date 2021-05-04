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
        string AddUser(UserModel adduser);
        string AddAppointment(RegistrationModel addappointment);
        string AddClient(RegistrationModel addclient);
        string AddFacilityUser(UserBindingModel adduser);
        string AddServicePoint(ServicePointModel adduser);
        void UpdateUser(UserModel adduser);
        void UpdateAppointment(RegistrationModel addupdate);
        void UpdateClient(RegistrationModel updateClient);
        void UpdateFacilityUser(UserBindingModel adduser);
        void UpdateFacility(FacilityModel updateFacility);
        void UpdateServicePoint(ServicePointModel updateservicepoint);
        UserModel GetData(long id);
        List<UserRolesModel> getRoles();
        AppointmentsModel GetAppointment(long id);

        int CountUsers();

        int ActiveUsers();
        
    }
}
