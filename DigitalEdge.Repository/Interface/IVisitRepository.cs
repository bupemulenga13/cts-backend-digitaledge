
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalEdge.Domain;

namespace DigitalEdge.Repository
{
    public interface IVisitRepository
    {
        List<SMSRecords> GetAppointementsDetailsForSMS();
        List<AppointmentsModel> GetAppointmentsDetails();
        List<AppointmentsModel> GetAppointmentCheck(AppointmentsModel model);
        List<AppointmentsModel> GetUpcomingAppointment(VisitsModel filterData);
        List<AppointmentsModel> GetUpcomingVisitsDetails();
        List<AppointmentsModel> GetUpcomingVisitsDetailsFilter(VisitsModel filterData);
        List<AppointmentsModel> GetMissedVisitsDetails();
        List<AppointmentsModel> GetVisitsMissedFilter(VisitsModel data);
        List<AppointmentsModel> GetAppointmentsDetailsMissed();
        List<AppointmentsModel> GetAppointmentsMissedFilter(VisitsModel filterData);
        List<AppointmentsModel> GetClientDetails();
        List<ClientModel> GetClientDetails(string searchTerm);
        List<ClientModel> GetClients();
        List<AppointmentsModel> GetAppointments();
        List<AppointmentsModel> GetClientDetailsFilters(VisitsModel filterData);
        List<AppointmentsModel> GetActiveClientFilter(VisitsModel filterData);
        List<AppointmentsModel> GetClientVisitPastDetails();
        List<AppointmentsModel> ViewDetails(long id);
        List<SMSRecords> SmsRecords(long id);
        List<AppointmentsModel> GetAppointmentsByVisitorID();
        Task<IList<AppointmentsModel>> GetAllData();
        List<Facility> GetFacility();
        List<District> GetDistrict(long id);
        List<ServicePoint> GetServicePoint(long id);
        List<Appointment> GetAppointment(long id, string type);
        List<BulkMessages> GetBulkMessages(long id , string type);
        List<Messages> GetMessages(long id , string type);
        List<UserFacility> GetUserFacility(long id, string type);
        List<Visit> GetVisit(long id, string type);
        List<Province> GetProvince();
        MessageTemplate GetMessage(MessageTemplateModel messageTemplate);
        Task<long> GetFacilityId(string name);
        Task<long> GetServicePointId(string name);
        string GetFacilityContactNumber(long? id);
        List<SMSRecords> SmsRecordsForVisits(long id);
        void UpdateServicePoints(List<ServicePoint>  servicePoints);
        void UpdateAppointments(List<Appointment> appointments);
        void UpdateBulkMessages(List<BulkMessages> bulkMessages);
        void UpdateMessages(List<Messages> messages);
        void UpdateUserFacility(List<UserFacility> userFacility);
        void UpdateVisit(List<Visit> visits);
        void DeleteFacility(long facilityId);
        void DeleteServicePoint(long servicePointId);
        Client ClientData();
        Client GetClientById(long id);

        List<AppointmentsModel> GetVisitHistory();
        List<AppointmentsModel> GetVisitHistoryByService(VisitsModel filterdata);
        List<FacilityModel> GetFacilities();
        List<ServicePointModel> GetServicePoints();
        List<VisitModel> GetVisits();
        Appointment GetAppointmentById(long id);
        Facility GetFacilityById(long id);
        List<FacilityTypeModel> GetFacilityTypes();
        string CreateVisit(Visit visitData);
        Visit GetVisitById(long id);
        List<VisitsServiceModel> GetServiceTypes();

        int CountFacilities(long facilityId);
        int CountClients();
        int CountAppointments();      

        int AvailableFacilities();
        int TodaysAppointments();
        int TodaysClients();
        List<ClientModel> GetClientsByFacility(long facilityId);
        List<AppointmentsModel> GetAppointmentsByFacility(long facilityId);
        long CountAppointmentsFacility(long facilityId);
        int CountClientsInFacility(long facilityId);
        int CountAppointmentsInFacility(long facilityId);
        List<FacilityModel> GetFacilities(long facilityId);
        List<FacilityModel> GetFacilitiesInDistrict(long id);
        List<LanguageModel> GetLanguages();
        int CountFacilities();
        int CountFacilitiesInDistrict(long districtId);
        RegistrationModel GetClientAppointment(long id);
        IEnumerable<SearchModel> SearchClient(string searchTerm);
        IEnumerable<SearchModel> SearchAppointment(string searchTerm);

        Boolean UpdateAppointmentStatus(AppointmentsModel appointment);
    }

}
