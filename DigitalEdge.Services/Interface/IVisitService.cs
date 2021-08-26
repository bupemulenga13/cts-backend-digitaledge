using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DigitalEdge.Domain;
using DigitalEdge.Repository;

namespace DigitalEdge.Services
{
    public interface IVisitService
    {
        List<SMSRecords> CSVImportFile(DataTable csvFileDataTable);
        List<AppointmentsModel> getAppointmentsDetails();
        List<AppointmentsModel> getAppointmentCheck(AppointmentsModel model);
        List<AppointmentsModel> getUpcommingAppointment(VisitsModel data);
        List<AppointmentsModel> getAppointmentsDetailsMissed();
        List<AppointmentsModel> getAppointmentsMissedFilter(VisitsModel data);
        List<AppointmentsModel> getUpcommingVisitsDetails();
        List<AppointmentsModel> getUpcommingVisitsDetails(VisitsModel data);
        List<AppointmentsModel> getMissedVisitsDetails();
        List<AppointmentsModel> getVisitsMissedFilter(VisitsModel data);
        List<AppointmentsModel> getClientDetails();
        List<ClientModel> getClientDetails(string searchTerm);
        List<ClientModel> getClients();
        List<AppointmentsModel> GetAppointments();
        List<AppointmentsModel> getClientDetailsFilters(VisitsModel data);
        List<AppointmentsModel> getActiveClientFilter(VisitsModel data);
        List<AppointmentsModel> getClientVisitPastDetails();
        List<AppointmentsModel> viewDetails(long id);
        string smsRecords(long id, bool name);
        Task<string> SendSMSApi(List<SMSRecords> SmsRecordsById);
        string SaveSMSApi(List<SMSRecords> SmsRecordsById, string resultContent);
        Facility GetFacility(long id);
        List<DistrictModel> getDistrict(long id);
        List<ServicePointModel> GetServicePoint(long id);
        List<ProvinceModel> GetProvince();
        MessageTemplateModel GetMessage(MessageTemplateModel messageTemplateModel);
        string SendBulkSMS(List<SMSRecords> SmsRecords, CSVBulkData bulkData);
        string ConvertMessage(SMSRecords smsRecords);
        void DeleteFacility(FacilityModel facilityModel, string isfacility);
        void DeleteServicePoint(ServicePointModel servicepoint , string isservicePoint);
    
        string AddVisit(VisitModel model);
        ClientModel GetClient();
        Client GetClientById(long id);
        List<AppointmentsModel> getVisitHistory();
        List<AppointmentsModel> getVisitHistoryByServicePoint(VisitsModel data);
        List<FacilityModel> GetFacilities();
        List<ServicePointModel> GetServicePoints();
        List<VisitModel> GetVisits();
        Appointment GetAppointmentById(long id);
        List<FacilityTypeModel> GetFacilityTypes();
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
        int CountClientsInFacility(long facilityId);
        int CountAppointmentsInFacility(long facilityId);
        List<FacilityModel> GetFacilities(long facilityId);
        
        List<FacilityModel> GetFacilitiesInDistrict(long id);
        List<LanguageModel> GetLanguages();
    }
}
