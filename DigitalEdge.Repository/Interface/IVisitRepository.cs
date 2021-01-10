
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalEdge.Domain;

namespace DigitalEdge.Repository
{
    public interface IVisitRepository
    {
        List<SMSRecords> GetAppointementsDetailsForSMS();
        List<AppointmentsModel> GetAppointementsDetails();
        List<AppointmentsModel> GetAppointmentCheck(AppointmentsModel model);
        List<AppointmentsModel> GetUpcommingAppointment(VisitsModel filterdata);
        List<AppointmentsModel> GetUpcommingVisitsDetails();
        List<AppointmentsModel> GetUpcommingVisitsDetailsfilter(VisitsModel filterdata);
        List<AppointmentsModel> GetMissedVisitsDetails();
        List<AppointmentsModel> GetVisitsMissedFilter(VisitsModel data);
        List<AppointmentsModel> GetAppointmentsDetailsMissed();
        List<AppointmentsModel> GetAppointmentsMissedFilter(VisitsModel filterdata);
        List<AppointmentsModel> GetClientDetails();
        List<ClientModel> GetClientDetails(string searchTerm);
        List<AppointmentsModel> GetClientDetailsFilters(VisitsModel filterdata);
        List<AppointmentsModel> GetActiveClientFilter(VisitsModel filterdata);
        List<AppointmentsModel> GetClientVisitPastDetails();
        List<AppointmentsModel> ViewDetails(long id);
        List<SMSRecords> SmsRecords(long id);
        List<AppointmentsModel> GetAppointmentsByVistorID();
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
        void UpdateServicePoints(List<ServicePoint>  servicepoints);
        void UpdateAppointments(List<Appointment> appointments);
        void UpdateBulkMessages(List<BulkMessages> bulkmessages);
        void UpdateMessages(List<Messages> messages);
        void UpdateUserFacility(List<UserFacility> userfacility);
        void UpdateVisit(List<Visit> visits);
        void DeleteFacility(long facilityId);
        void DeleteServicePoint(long servicePointId);
        Client ClientData();
        List<AppointmentsModel> GetVisitHistory();
        List<AppointmentsModel> GetVisitHistoryByService(VisitsModel filterdata);
    }

}
