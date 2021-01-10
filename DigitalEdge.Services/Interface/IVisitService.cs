using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DigitalEdge.Domain;

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
        List<AppointmentsModel> getClientDetailsFilters(VisitsModel data);
        List<AppointmentsModel> getActiveClientFilter(VisitsModel data);
        List<AppointmentsModel> getClientVisitPastDetails();
        List<AppointmentsModel> viewDetails(long id);
        string smsRecords(long id, bool name);
        Task<string> SendSMSApi(List<SMSRecords> SmsRecordsById);
        string SaveSMSApi(List<SMSRecords> SmsRecordsById, string resultContent);
        List<FacilityModel> getFacility();
        List<DistrictModel> getDistrict(long id);
        List<ServicePointModel> GetServicePoint(long id);
        List<ProvinceModel> GetProvince();
        MessageTemplateModel GetMessage(MessageTemplateModel messageTemplateModel);
        string SendBulkSMS(List<SMSRecords> SmsRecords, CSVBulkData bulkData);
        string ConvertMessage(SMSRecords smsRecords);
        void DeleteFacility(FacilityModel facilityModel, string isfacility);
        void DeleteServicePoint(ServicePointModel servicepoint , string isservicePoint);
        ClientModel GetClient();
        List<AppointmentsModel> getVisitHistory();
        List<AppointmentsModel> getVisitHistoryByServicePoint(VisitsModel data);
    }
}
