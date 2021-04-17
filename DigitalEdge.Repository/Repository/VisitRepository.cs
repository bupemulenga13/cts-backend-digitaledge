using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalEdge.Domain;

namespace DigitalEdge.Repository
{
    public class VisitRepository : IVisitRepository
    {
        private readonly DigitalEdgeContext _DigitalEdgeContext;
        private readonly IBaseRepository<Facility> _facilityRepository;
        private readonly IBaseRepository<District> _districtRepository;
        private readonly IBaseRepository<ServicePoint> _servicePointRepository;
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<BulkMessages> _bulkMessagesRepository;
        private readonly IBaseRepository<UserFacility> _userFacilityRepository;
        private readonly IBaseRepository<Visit> _visitRepository;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly IBaseRepository<Messages> _messagesRepository;
        private readonly IBaseRepository<Province> _provinceRepository;
        private readonly IBaseRepository<MessageTemplate> _messageTemplateRepository;
        public VisitRepository(
            IBaseRepository<Facility> facilityRepository,
            IBaseRepository<ServicePoint> servicePointRepository,
            IBaseRepository<District> districtRepository,
            IBaseRepository<Province> provinceRepository,
            DigitalEdgeContext DigitalEdgeContext,
            IBaseRepository<MessageTemplate> messageTemplateRepository,
            IBaseRepository<Appointment> appointmentRepository,
            IBaseRepository<BulkMessages> bulkMessagesRepository,
            IBaseRepository<Messages> messagesRepository,
            IBaseRepository<UserFacility> userFacilityRepository,
            IBaseRepository<Visit> visitRepository,
            IBaseRepository<Client> clientRepository
            )
        {
            this._messageTemplateRepository = messageTemplateRepository;
            this._servicePointRepository = servicePointRepository;
            this._facilityRepository = facilityRepository;
            this._DigitalEdgeContext = DigitalEdgeContext;
            this._districtRepository = districtRepository;
            this._provinceRepository = provinceRepository;
            this._appointmentRepository = appointmentRepository;
            this._bulkMessagesRepository = bulkMessagesRepository;
            this._messagesRepository = messagesRepository;
            this._userFacilityRepository = userFacilityRepository;
            this._visitRepository = visitRepository;
            this._clientRepository = clientRepository;
        }
        public List<AppointmentsModel> GetAppointementsDetails()
        {

            List<AppointmentsModel> appointmentsdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                           from visits in appointments.DefaultIfEmpty()
                                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                           from clients in list.DefaultIfEmpty()
                                                           where (appointment.AppointmentDate >= DateTime.UtcNow)
                                                           select new AppointmentsModel
                                                           {
                                                               Id = appointment.AppointmentId,
                                                               ClientId = clients.ClientId,
                                                               FirstName = clients.FirstName,
                                                               LastName = clients.LastName,
                                                               Age = clients.Age,
                                                               PriorAppointmentDate = visits.PriorAppointmentDate,
                                                               AppointmentDate = appointment.AppointmentDate,
                                                               AppointmentTime = appointment.AppointmentDate,
                                                               NextAppointmentDate = visits.NextAppointmentDate,
                                                               VisitsId = visits.VisitId
                                                           }).ToList();
            return appointmentsdetails;
        }
        public List<AppointmentsModel> GetAppointmentCheck(AppointmentsModel model)
        {

            List<AppointmentsModel> appointmentsdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                           from clients in list.DefaultIfEmpty()
                                                           where clients.FirstName == model.FirstName &&  appointment.AppointmentDate >= DateTime.UtcNow
                                                           select new AppointmentsModel
                                                           {
                                                               Id = appointment.AppointmentId,
                                                               ClientId = clients.ClientId,
                                                               FirstName = clients.FirstName,
                                                               LastName = clients.LastName,
                                                               MiddleName = clients.MiddleName,
                                                               Age = clients.Age,
                                                               ClientPhoneNo = clients.ClientPhoneNo,
                                                               DateOfBirth = clients.DateOfBirth,
                                                               AppointmentDate = appointment.AppointmentDate,
                                                               AppointmentTime = appointment.AppointmentDate,
                                                           }).ToList();
            return appointmentsdetails;
        }

        public List<AppointmentsModel> GetUpcommingAppointment(VisitsModel upcommingfilterdata)
        {

            List<AppointmentsModel> upcommingappointmentsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                           from visits in appointments.DefaultIfEmpty()
                                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                           from clients in list.DefaultIfEmpty()                                                          
                                                           where appointment.ServicePointId == upcommingfilterdata.ServicePointId && appointment.AppointmentDate >= DateTime.UtcNow
                                                           select new AppointmentsModel
                                                           {
                                                               Id = appointment.AppointmentId,
                                                               ClientId = clients.ClientId,
                                                               FirstName = clients.FirstName,
                                                               LastName = clients.LastName,
                                                               MiddleName = clients.MiddleName,
                                                               PriorAppointmentDate = visits.PriorAppointmentDate,
                                                               AppointmentDate = appointment.AppointmentDate,
                                                               AppointmentTime = appointment.AppointmentDate,
                                                               NextAppointmentDate = visits.NextAppointmentDate,
                                                               VisitsId = visits.VisitId,
                                                               VisitDate = visits.VisitDate,
                                                               Age = clients.Age,
                                                               ServicePointId = visits.ServicePointId,                                                               
                                                           }).ToList();

            return upcommingappointmentsfilter;
        }
        public List<AppointmentsModel> GetAppointmentsDetailsMissed()
        {

            List<AppointmentsModel> appointmentsmisseddetails = (from appointment in _DigitalEdgeContext.Appointments
                                                                 join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                                 from visits in appointments.DefaultIfEmpty()
                                                                 join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                                 from clients in list.DefaultIfEmpty()
                                                                 where (appointment.AppointmentDate < DateTime.UtcNow)
                                                                 select new AppointmentsModel
                                                                 {
                                                                     Id = appointment.AppointmentId,
                                                                     ClientId = clients.ClientId,
                                                                     FirstName = clients.FirstName,
                                                                     LastName = clients.LastName,
                                                                     MiddleName = clients.MiddleName,
                                                                     Age = clients.Age,
                                                                     PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                     AppointmentDate = appointment.AppointmentDate,
                                                                     AppointmentTime = appointment.AppointmentDate,
                                                                     NextAppointmentDate = visits.NextAppointmentDate,
                                                                     VisitsId = visits.VisitId
                                                                 }).ToList();

            return appointmentsmisseddetails;
        }
        public List<AppointmentsModel> GetAppointmentsMissedFilter(VisitsModel missedfilter)
        {

            List<AppointmentsModel> appointmentsmissedfilter = (from appointment in _DigitalEdgeContext.Appointments
                                                                 join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                                 from visits in appointments.DefaultIfEmpty()
                                                                 join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                                 from clients in list.DefaultIfEmpty()
                                                                 where appointment.ServicePointId == missedfilter.ServicePointId && appointment.AppointmentDate < DateTime.UtcNow
                                                                 select new AppointmentsModel
                                                                 {
                                                                     Id = appointment.AppointmentId,
                                                                     ClientId = clients.ClientId,
                                                                     FirstName = clients.FirstName,
                                                                     LastName = clients.LastName,
                                                                     MiddleName = clients.MiddleName,
                                                                     PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                     AppointmentDate = appointment.AppointmentDate,
                                                                     AppointmentTime = appointment.AppointmentDate,
                                                                     NextAppointmentDate = visits.NextAppointmentDate,
                                                                     VisitsId = visits.VisitId,
                                                                     VisitDate = visits.VisitDate,
                                                                     Age = clients.Age,
                                                                     //FacilityId = visits.FacilityId,
                                                                     ServicePointId = visits.ServicePointId,
                                                                 }).ToList();

            return appointmentsmissedfilter;
        }
        public List<AppointmentsModel> GetUpcommingVisitsDetailsfilter(VisitsModel filterdata)
        {
            List<AppointmentsModel> upcommingvisitsdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                                              join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                              from visits in appointments.DefaultIfEmpty()
                                                              join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                              from clients in list.DefaultIfEmpty()
                                                              where  visits.ServicePointId==filterdata.ServicePointId && visits.VisitDate >= DateTime.UtcNow
                                                              select new AppointmentsModel
                                                              {
                                                                  Id = appointment.AppointmentId,
                                                                  ClientId = clients.ClientId,
                                                                  FirstName = clients.FirstName,
                                                                  LastName = clients.LastName,
                                                                  ArtNo = clients.ArtNo,
                                                                  Age = clients.Age,
                                                                  PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                  AppointmentDate = appointment.AppointmentDate,
                                                                  AppointmentTime = appointment.AppointmentDate,
                                                                  NextAppointmentDate = visits.NextAppointmentDate,
                                                                  VisitsId = visits.VisitId,
                                                                  VisitDate = visits.VisitDate
                                                              }).ToList();


            return upcommingvisitsdetailsfilter;
        }
        public List<AppointmentsModel> GetUpcommingVisitsDetails()
        {
            List<AppointmentsModel> upcommingvisitsdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                                    join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                                    from visits in appointments.DefaultIfEmpty()
                                                                    join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                                    from clients in list.DefaultIfEmpty()
                                                                    where visits.VisitDate >= DateTime.UtcNow
                                                                    select new AppointmentsModel
                                                                    {
                                                                        Id = appointment.AppointmentId,
                                                                        ClientId = clients.ClientId,
                                                                        FirstName = clients.FirstName,
                                                                        LastName = clients.LastName,
                                                                        MiddleName = clients.MiddleName,
                                                                        Age = clients.Age,
                                                                        PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                        AppointmentDate = appointment.AppointmentDate,
                                                                        AppointmentTime = appointment.AppointmentDate,
                                                                        NextAppointmentDate = visits.NextAppointmentDate,
                                                                        VisitDate = visits.VisitDate,
                                                                        VisitsId = visits.VisitId
                                                                    }).ToList();


            return upcommingvisitsdetails;
        }
        public List<AppointmentsModel> GetMissedVisitsDetails()
        {

            List<AppointmentsModel> missedvisitdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                          join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                          from visits in appointments.DefaultIfEmpty()
                                                          join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                          from clients in list.DefaultIfEmpty()
                                                          where (visits.VisitDate < DateTime.UtcNow)
                                                          select new AppointmentsModel
                                                          {
                                                              Id = appointment.AppointmentId,
                                                              ClientId = clients.ClientId,
                                                              FirstName = clients.FirstName,
                                                              LastName = clients.LastName,
                                                              MiddleName = clients.MiddleName,
                                                              Age = clients.Age,
                                                              PriorAppointmentDate = visits.PriorAppointmentDate,
                                                              AppointmentDate = appointment.AppointmentDate,
                                                              AppointmentTime = appointment.AppointmentDate,
                                                              NextAppointmentDate = visits.NextAppointmentDate,
                                                              VisitDate = visits.VisitDate,
                                                              VisitsId = visits.VisitId,
                                                              ArtNo = clients.ArtNo
                                                          }).ToList();

            return missedvisitdetails;
        } 
        public List<AppointmentsModel> GetVisitsMissedFilter(VisitsModel fileterdata)
        {

            List<AppointmentsModel> missedvisitfilterdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                          join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                          from visits in appointments.DefaultIfEmpty()
                                                          join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                          from clients in list.DefaultIfEmpty()
                                                          where visits.ServicePointId == fileterdata.ServicePointId && visits.VisitDate < DateTime.UtcNow
                                                          select new AppointmentsModel
                                                          {
                                                              Id = appointment.AppointmentId,
                                                              ClientId = clients.ClientId,
                                                              FirstName = clients.FirstName,
                                                              LastName = clients.LastName,
                                                              MiddleName = clients.MiddleName,
                                                              Age = clients.Age,
                                                              PriorAppointmentDate = visits.PriorAppointmentDate,
                                                              AppointmentDate = appointment.AppointmentDate,
                                                              AppointmentTime = appointment.AppointmentDate,
                                                              NextAppointmentDate = visits.NextAppointmentDate,
                                                              VisitsId = visits.VisitId,
                                                              VisitDate = visits.VisitDate
                                                          }).ToList();

            return missedvisitfilterdetails;
        }
        public List<AppointmentsModel> GetClientDetails()
        {

            List<AppointmentsModel> viewClientdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                          join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                          from visits in appointments.DefaultIfEmpty()
                                                          join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                          from clients in list.DefaultIfEmpty()
                                                          select new AppointmentsModel
                                                          {
                                                              Id = appointment.AppointmentId,
                                                              ClientId = clients.ClientId,
                                                              FirstName = clients.FirstName,
                                                              LastName = clients.LastName,
                                                              MiddleName = clients.MiddleName,
                                                              PriorAppointmentDate = visits.PriorAppointmentDate,
                                                              AppointmentDate = appointment.AppointmentDate,
                                                              AppointmentTime = appointment.AppointmentDate,
                                                              NextAppointmentDate = visits.NextAppointmentDate,
                                                              VisitsId = visits.VisitId,                                                            
                                                              VisitDate = visits.VisitDate,                                                            
                                                              Age = clients.Age,                                                            
                                                          }).ToList();
            return viewClientdetails;
        }
        public List<ClientModel> GetClientDetails(string searchTerm)
        { 
            
            List<ClientModel> viewClientdetails = (from client in _DigitalEdgeContext.Clients
                                                          where client.FirstName.Contains(searchTerm) || client.LastName.Contains(searchTerm) || client.ArtNo.Contains(searchTerm)
                                                   select new ClientModel
                                                          {
                                                              ClientId = client.ClientId,
                                                              FirstName = client.FirstName,
                                                              LastName = client.LastName,
                                                              MiddleName = client.MiddleName,
                                                              Age = client.Age,
                                                              ClientPhoneNo = client.ClientPhoneNo,
                                                              DateOfBirth = client.DateOfBirth,
                                                              ArtNo = client.ArtNo,
                                                              EnrollmentDate = client.EnrollmentDate,
                                                              Facility = client.Facilities.FacilityName
                                                          }).ToList();
            return viewClientdetails;
        }
        public List<ClientModel> GetClients()
        {
            

            List<ClientModel> clients = (from client in _DigitalEdgeContext.Clients
                                         join facility in _DigitalEdgeContext.Facilities on client.FacilityId equals facility.FacilityId
                                         join status in _DigitalEdgeContext.ClientStatuses on client.ClientStatusId equals status.ClientStatusId
                                         join sex in _DigitalEdgeContext.Sexes on client.SexId equals sex.SexId
                                         select new ClientModel
                                         {
                                             ClientId = client.ClientId,
                                             FirstName = client.FirstName,
                                             LastName = client.LastName,
                                             ArtNo = client.ArtNo,
                                             EnrollmentDate = client.EnrollmentDate,
                                             ClientPhoneNo = client.ClientPhoneNo,
                                             Facility = facility.FacilityName,
                                             Address = client.Address,
                                             ClientStatusId = client.ClientStatusId,
                                             ClientStatus = status.ClientStatusName,
                                             ClientTypeId = client.ClientTypeId,
                                             ClientType = client.ClientTypes.ClientTypeName,
                                             DateOfBirth = client.DateOfBirth,
                                             StatusCommentId = client.StatusCommentId,
                                             StatusComment = client.StatusComments.StatusCommentName,
                                             SexId = client.SexId,
                                             Sex= sex.SexName,
                                             ServicePointId = client.ServicePointId,
                                             LanguageId = client.LanguageId,
                                             FacilityId = client.FacilityId,
                                             PhoneVerifiedByAnalyst = client.PhoneVerifiedByAnalyst,
                                             PhoneVerifiedByFacilityStaff = client.PhoneVerifiedByFacilityStaff
                                                                                         
 
                                         }
                ).ToList();
            return clients;
            
        }
        public List<AppointmentsModel> GetAppointments()
        {
            List<AppointmentsModel> appointments = (from appointment in _DigitalEdgeContext.Appointments
                                                    join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId
                                                    join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId
                                                    join department in _DigitalEdgeContext.ServicePoints on appointment.ServicePointId equals department.ServicePointId
                                                    select new AppointmentsModel
                                                    {
                                                        Id = appointment.AppointmentId,
                                                        ClientId = appointment.ClientId,
                                                        FacilityId = appointment.FacilityId,
                                                        ServicePointId = department.ServicePointId,
                                                        AppointmentDate = appointment.AppointmentDate,
                                                        AppointmentStatus = appointment.AppointmentStatus,
                                                        InteractionDate = appointment.InteractionDate,
                                                        PriorAppointmentDate = appointment.PriorAppointmentDate,
                                                        Detail = appointment.Detail,
                                                        FirstName = client.FirstName,
                                                        LastName = client.LastName,
                                                        ArtNo = client.ArtNo,
                                                        FacilityName = appointment.FacilityModel.FacilityName,
                                                        AppointmentTime = appointment.AppointmentDate,
                                                        ClientPhoneNo = appointment.ClientModel.ClientPhoneNo,
                                                        ClientModel = new ClientModel { FirstName = client.FirstName, LastName = client.LastName, ArtNo = client.ArtNo, ClientPhoneNo = client.ClientPhoneNo },
                                                        FacilityModel = new FacilityModel { FacilityName = facility.FacilityName },
                                                        ServicePointModel = new ServicePointModel { ServicePointName = department.ServicePointName }
                                                        

                                                    }
                                                    ).ToList();
            return appointments;
        }

        public List<AppointmentsModel> GetClientDetailsFilters(VisitsModel filterdata)
        {
            List<AppointmentsModel> viewClientdetailsfilter = new List<AppointmentsModel>();

            if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId 
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;

            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId && provinces.ProvinceId == filterdata.ProvinceId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;

            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId
                                            && provinces.ProvinceId == filterdata.ProvinceId && districts.DistrictId==filterdata.DistrictId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;

            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            where visits.ServicePointId==filterdata.ServicePointId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where provinces.ProvinceId == filterdata.ProvinceId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId > 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where districts.DistrictId == filterdata.DistrictId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && provinces.ProvinceId==filterdata.ProvinceId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && provinces.ProvinceId == filterdata.ProvinceId 
                                            && districts.DistrictId==filterdata.DistrictId                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                viewClientdetailsfilter = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where provinces.ProvinceId == filterdata.ProvinceId && districts.DistrictId == filterdata.DistrictId
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return viewClientdetailsfilter;
            }

            return this.GetClientDetails();
        }
        public List<AppointmentsModel> GetClientVisitPastDetails()
        {
            List<AppointmentsModel> clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                                          join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                          from visits in appointments.DefaultIfEmpty()
                                                          join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                          from clients in list.DefaultIfEmpty()
                                                          where visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                                          select new AppointmentsModel
                                                          {
                                                              Id = appointment.AppointmentId,
                                                              ClientId = clients.ClientId,
                                                              FirstName = clients.FirstName,
                                                              LastName = clients.LastName,
                                                              MiddleName = clients.MiddleName,
                                                              PriorAppointmentDate = visits.PriorAppointmentDate,
                                                              AppointmentDate = appointment.AppointmentDate,
                                                              AppointmentTime = appointment.AppointmentDate,
                                                              NextAppointmentDate = visits.NextAppointmentDate,
                                                              VisitsId = visits.VisitId,
                                                              VisitDate = visits.VisitDate,
                                                              Age = clients.Age,
                                                          }).ToList();
            return clientVisitPastDetails;
        }
        public List<AppointmentsModel> GetActiveClientFilter(VisitsModel filterdata)
        {
            List<AppointmentsModel> clientVisitPastDetails = new List<AppointmentsModel>();

            if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                           from visits in appointments.DefaultIfEmpty()
                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                           from clients in list.DefaultIfEmpty()
                                           join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                           from facilitys in facilitylist.DefaultIfEmpty()
                                           join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                           from districts in districtlist.DefaultIfEmpty()
                                           join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                           from provinces in provincelist.DefaultIfEmpty()
                                           where visits.FacilityId == filterdata.FacilityId && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                           {
                                               Id = appointment.AppointmentId,
                                               ClientId = clients.ClientId,
                                               FirstName = clients.FirstName,
                                               LastName = clients.LastName,
                                               MiddleName = clients.MiddleName,
                                               PriorAppointmentDate = visits.PriorAppointmentDate,
                                               AppointmentDate = appointment.AppointmentDate,
                                               AppointmentTime = appointment.AppointmentDate,
                                               NextAppointmentDate = visits.NextAppointmentDate,
                                               VisitsId = visits.VisitId,
                                               VisitDate = visits.VisitDate,
                                               Age = clients.Age,
                                               FacilityId = visits.FacilityId,
                                               ServicePointId = visits.ServicePointId,
                                               ProvinceId = provinces.ProvinceId,
                                               DistrictId = districts.DistrictId,
                                           }).ToList();
                return clientVisitPastDetails;


            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;

            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId && provinces.ProvinceId == filterdata.ProvinceId
                                             && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                            select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && visits.ServicePointId == filterdata.ServicePointId
                                            && provinces.ProvinceId == filterdata.ProvinceId && districts.DistrictId == filterdata.DistrictId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;

            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId > 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            where visits.ServicePointId == filterdata.ServicePointId
                                             && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where provinces.ProvinceId == filterdata.ProvinceId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId == 0 && filterdata.DistrictId > 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where districts.DistrictId == filterdata.DistrictId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId == 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && provinces.ProvinceId == filterdata.ProvinceId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId > 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where visits.FacilityId == filterdata.FacilityId && provinces.ProvinceId == filterdata.ProvinceId
                                            && districts.DistrictId == filterdata.DistrictId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }
            else if (filterdata.FacilityId == 0 && filterdata.ServicePointId == 0 && filterdata.ProvinceId > 0 && filterdata.DistrictId > 0)
            {
                clientVisitPastDetails = (from appointment in _DigitalEdgeContext.Appointments
                                            join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                            from visits in appointments.DefaultIfEmpty()
                                            join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                            from clients in list.DefaultIfEmpty()
                                            join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into facilitylist
                                            from facilitys in facilitylist.DefaultIfEmpty()
                                            join district in _DigitalEdgeContext.Districts on facilitys.DistrictId equals district.DistrictId into districtlist
                                            from districts in districtlist.DefaultIfEmpty()
                                            join province in _DigitalEdgeContext.Provinces on districts.ProvinceId equals province.ProvinceId into provincelist
                                            from provinces in provincelist.DefaultIfEmpty()
                                            where provinces.ProvinceId == filterdata.ProvinceId && districts.DistrictId == filterdata.DistrictId
                                            && visits.VisitDate >= DateTime.UtcNow.AddMonths(-1)
                                           select new AppointmentsModel
                                            {
                                                Id = appointment.AppointmentId,
                                                ClientId = clients.ClientId,
                                                FirstName = clients.FirstName,
                                                LastName = clients.LastName,
                                                MiddleName = clients.MiddleName,
                                                PriorAppointmentDate = visits.PriorAppointmentDate,
                                                AppointmentDate = appointment.AppointmentDate,
                                                AppointmentTime = appointment.AppointmentDate,
                                                NextAppointmentDate = visits.NextAppointmentDate,
                                                VisitsId = visits.VisitId,
                                                VisitDate = visits.VisitDate,
                                                Age = clients.Age,
                                                FacilityId = visits.FacilityId,
                                                ServicePointId = visits.ServicePointId,
                                            }).ToList();
                return clientVisitPastDetails;
            }     
            
            return GetClientVisitPastDetails();
        }
        public List<SMSRecords> GetAppointementsDetailsForSMS()
        {
            List<SMSRecords> smsRecords = (from appointment in _DigitalEdgeContext.Appointments
                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                           from visits in appointments.DefaultIfEmpty()
                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                           from clients in list.DefaultIfEmpty()
                                           join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into facilityDetais
                                           from facilitys in facilityDetais.DefaultIfEmpty()
                                           join servicepoint in _DigitalEdgeContext.ServicePoints on appointment.ServicePointId equals servicepoint.ServicePointId into servicepointDetails
                                           from servicepoints in servicepointDetails.DefaultIfEmpty()
                                           where (appointment.AppointmentDate >= DateTime.UtcNow && appointment.AppointmentDate < DateTime.UtcNow.Date.AddDays(8)) ||
                                           (visits.NextAppointmentDate >= DateTime.UtcNow && visits.NextAppointmentDate < DateTime.UtcNow.Date.AddDays(8))
                                           select new SMSRecords
                                           {
                                               ClientId = clients.ClientId,
                                               FirstName = clients.FirstName,
                                               LastName = clients.LastName,
                                               MiddleName = clients.MiddleName,
                                               PhoneNumber = clients.ClientPhoneNo,
                                               AppointmenDateTime = appointment.AppointmentDate,
                                               NextAppointmentDate = visits.NextAppointmentDate,
                                               FacilityId = facilitys.FacilityId,
                                               FacilityName = facilitys.FacilityName,
                                               FacilityContactNumber = facilitys.FacilityContactNumber,
                                               ServicePointId = servicepoints.ServicePointId,
                                               ServicePointName = servicepoints.ServicePointName
                                           }).ToList();
            return smsRecords;
        }
        public List<AppointmentsModel> ViewDetails(long id)
        {
            List<AppointmentsModel> viewdetails = (from visits in _DigitalEdgeContext.Visits
                                                   join client in _DigitalEdgeContext.Clients on visits.ClientId equals client.ClientId into list
                                                   from clients in list.DefaultIfEmpty()
                                                   where (visits.AppointmentId == id)
                                                   select new AppointmentsModel
                                                   {
                                                       Id = visits.VisitId,
                                                       FirstName = clients.FirstName,
                                                       LastName = clients.LastName,
                                                       MiddleName = clients.MiddleName,
                                                       ClientPhoneNo = clients.ClientPhoneNo,
                                                       DateOfBirth = clients.DateOfBirth,
                                                       VisitDate = visits.VisitDate,
                                                       VisitType = visits.VisitType,
                                                       PriorAppointmentDate = visits.PriorAppointmentDate,
                                                       NextAppointmentDate = visits.NextAppointmentDate,
                                                       ReasonOfVisit = visits.ReasonOfVisit,
                                                       AdviseNotes = visits.AdviseNotes,
                                                   }).ToList();
            return viewdetails;
        }
        public List<SMSRecords> SmsRecords(long id)
        {
            List<SMSRecords> smsRecords = (from appointment in _DigitalEdgeContext.Appointments
                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                           from visits in appointments.DefaultIfEmpty()
                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                           from clients in list.DefaultIfEmpty()
                                           join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into facilityDetais
                                           from facilitys in facilityDetais.DefaultIfEmpty()
                                           join servicepoint in _DigitalEdgeContext.ServicePoints on appointment.ServicePointId equals servicepoint.ServicePointId into servicepointDetails
                                           from servicepoints in servicepointDetails.DefaultIfEmpty()
                                           where ((appointment.AppointmentDate >= DateTime.UtcNow || visits.NextAppointmentDate >= DateTime.UtcNow) && appointment.AppointmentId == id)
                                           select new SMSRecords
                                           {
                                               ClientId = clients.ClientId,
                                               FirstName = clients.FirstName,
                                               LastName = clients.LastName,
                                               MiddleName = clients.MiddleName,
                                               PhoneNumber = clients.ClientPhoneNo,
                                               AppointmenDateTime = appointment.AppointmentDate,
                                               NextAppointmentDate = visits.NextAppointmentDate,
                                               FacilityId = facilitys.FacilityId,
                                               FacilityName = facilitys.FacilityName,
                                               FacilityContactNumber = facilitys.FacilityContactNumber,
                                               ServicePointId = servicepoints.ServicePointId,
                                               ServicePointName = servicepoints.ServicePointName
                                           }).ToList();
            return smsRecords;
        }
        public List<SMSRecords> SmsRecordsForVisits(long id)
        {
            List<SMSRecords> smsRecords = (from appointment in _DigitalEdgeContext.Appointments
                                           join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                           from visits in appointments.DefaultIfEmpty()
                                           join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                           from clients in list.DefaultIfEmpty()
                                           join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into facilityDetais
                                           from facilitys in facilityDetais.DefaultIfEmpty()
                                           join servicepoint in _DigitalEdgeContext.ServicePoints on appointment.ServicePointId equals servicepoint.ServicePointId into servicepointDetails
                                           from servicepoints in servicepointDetails.DefaultIfEmpty()
                                           where ((appointment.AppointmentDate >= DateTime.UtcNow || visits.NextAppointmentDate >= DateTime.UtcNow) && visits.VisitId == id)
                                           select new SMSRecords
                                           {
                                               ClientId = clients.ClientId,
                                               FirstName = clients.FirstName,
                                               LastName = clients.LastName,
                                               MiddleName = clients.MiddleName,
                                               PhoneNumber = clients.ClientPhoneNo,
                                               AppointmenDateTime = appointment.AppointmentDate,
                                               NextAppointmentDate = visits.NextAppointmentDate,
                                               FacilityId = facilitys.FacilityId,
                                               FacilityName = facilitys.FacilityName,
                                               FacilityContactNumber = facilitys.FacilityContactNumber,
                                               ServicePointId = servicepoints.ServicePointId,
                                               ServicePointName = servicepoints.ServicePointName
                                           }).ToList();
            return smsRecords;
        }
        public List<AppointmentsModel> GetAppointmentsByVistorID()
        {

            try
            {

                List<AppointmentsModel> appointmentsdetails = (from appointment in _DigitalEdgeContext.Appointments
                                                               join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                               from visits in appointments.DefaultIfEmpty()
                                                               join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                               from clients in list.DefaultIfEmpty()
                                                               where (appointment.AppointmentDate >= DateTime.UtcNow || visits.NextAppointmentDate >= DateTime.UtcNow)
                                                               select new AppointmentsModel
                                                               {
                                                                   Id = appointment.AppointmentId,
                                                                   ClientId = clients.ClientId,
                                                                   FirstName = clients.FirstName,
                                                                   LastName = clients.LastName,
                                                                   MiddleName = clients.MiddleName,
                                                                   PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                   AppointmentDate = appointment.AppointmentDate,
                                                                   AppointmentTime = appointment.AppointmentDate,
                                                                   NextAppointmentDate = visits.NextAppointmentDate
                                                               }).ToList();



                return appointmentsdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<AppointmentsModel>> GetAllData()
        {
            List<Object> returnData = new List<object>();
            IList<AppointmentsModel> appointmentsdetails = await (from appointment in _DigitalEdgeContext.Appointments
                                                                  join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                                  from visits in appointments.DefaultIfEmpty()
                                                                  join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                                  from clients in list.DefaultIfEmpty()
                                                                  where (appointment.AppointmentDate >= DateTime.UtcNow || visits.NextAppointmentDate >= DateTime.UtcNow)
                                                                  select new AppointmentsModel
                                                                  {
                                                                      Id = appointment.AppointmentId,
                                                                      ClientId = clients.ClientId,
                                                                      FirstName = clients.FirstName,
                                                                      LastName = clients.LastName,
                                                                      MiddleName = clients.MiddleName,
                                                                      PriorAppointmentDate = visits.PriorAppointmentDate,
                                                                      AppointmentDate = appointment.AppointmentDate,
                                                                      AppointmentTime = appointment.AppointmentDate,
                                                                      NextAppointmentDate = visits.NextAppointmentDate,
                                                                      FacilityId = appointment.FacilityId,
                                                                      ServicePointId = appointment.ServicePointId,
                                                                      ClientPhoneNo = appointment.ClientModel.ClientPhoneNo
                                                                  }).ToListAsync();

            return appointmentsdetails;
        }
        public List<Facility> GetFacility()
        {
            List<Facility> buldingDetails = this._facilityRepository.GetAll().ToList();
            return buldingDetails;
        }
        public List<District> GetDistrict(long id)
        {
            List<District> districtDetails = this._districtRepository.Get().Where(x => x.ProvinceId == id).ToList();
            return districtDetails;
        }
        public List<ServicePoint> GetServicePoint(long id)
        {
            List<ServicePoint> servicePointDetails = this._servicePointRepository.Get().Where(x => x.FacilityId == id).ToList();

            return servicePointDetails;
        }
        public List<Appointment> GetAppointment(long id, string type)
        {
            List<Appointment> appointmentDetails = new List<Appointment>();
            if (type == "facility")
            {
                appointmentDetails = this._appointmentRepository.Get().Where(x => x.FacilityId == id).ToList();
            }
            else if (type == "servicepoint")
            {
                appointmentDetails = this._appointmentRepository.Get().Where(x => x.ServicePointId == id).ToList();
            }
            return appointmentDetails;

        }
        public List<BulkMessages> GetBulkMessages(long id, string type)
        {
            List<BulkMessages> bulkMessagesDetails = new List<BulkMessages>();
            if (type == "facility")
            {
                bulkMessagesDetails = this._bulkMessagesRepository.Get().Where(x => x.FacilityId == id).ToList();
            }
            else if (type == "servicepoint")
            {
                bulkMessagesDetails = this._bulkMessagesRepository.Get().Where(x => x.ServicePointId == id).ToList();
            }
            return bulkMessagesDetails;
        }
        public List<Messages> GetMessages(long id, string type)
        {
            List<Messages> messagesDetails = new List<Messages>();
            if (type == "facility")
            {
                messagesDetails = this._messagesRepository.Get().Where(x => x.FacilityId == id).ToList();
            }
            else if (type == "servicepoint")
            {
                messagesDetails = this._messagesRepository.Get().Where(x => x.ServicePointId == id).ToList();
            }
            return messagesDetails;
        }
        public List<UserFacility> GetUserFacility(long id, string type)
        {
            List<UserFacility> userfacilityDetails = new List<UserFacility>();
            if (type == "facility")
            {
                userfacilityDetails = this._userFacilityRepository.Get().Where(x => x.FacilityId == id).ToList();
            }
            else if (type == "servicepoint")
            {
                userfacilityDetails = this._userFacilityRepository.Get().Where(x => x.ServicePointId == id).ToList();
            }
            return userfacilityDetails;
        }
        public List<Visit> GetVisit(long id, string type)
        {
            List<Visit> visitDetails = new List<Visit>();
            if (type == "facility")
            {
                visitDetails = this._visitRepository.Get().Where(x => x.FacilityId == id).ToList();
            }
            else if (type == "servicepoint")
            {
                visitDetails = this._visitRepository.Get().Where(x => x.ServicePointId == id).ToList();
            }
            return visitDetails;
        }
        public List<Province> GetProvince()
        {
            List<Province> proviceDetails = this._provinceRepository.GetAll().ToList();
            return proviceDetails;
        }
        public MessageTemplate GetMessage(MessageTemplateModel messageTemplate)
        {
            MessageTemplate messageTemplateDetails = this._messageTemplateRepository.Get().Where(x => x.Type == messageTemplate.Type && x.Language == messageTemplate.Language && x.Status == true).FirstOrDefault();
            return messageTemplateDetails;
        }
        public async Task<long> GetFacilityId(string name)
        {
            var Details = await (from facility in _DigitalEdgeContext.Facilities
                                 where (facility.FacilityName == name)
                                 select new Facility
                                 {
                                     FacilityId = facility.FacilityId
                                 }).ToArrayAsync();
            return Details[0].FacilityId;
        }
        public async Task<long> GetServicePointId(string name)
        {
            var Details = await (from servicePoint in _DigitalEdgeContext.ServicePoints
                                 where (servicePoint.ServicePointName == name)
                                 select new ServicePoint
                                 {
                                     ServicePointId = servicePoint.ServicePointId
                                 }).ToArrayAsync();
            return Details[0].ServicePointId;
        }
        public string GetFacilityContactNumber(long? id)
        {
            var Details = (from facility in _DigitalEdgeContext.Facilities
                           where (facility.FacilityId == id)
                           select new Facility
                           {
                               FacilityContactNumber = facility.FacilityContactNumber
                           }).ToArray();
            return Details[0].FacilityContactNumber;
        }
        public void UpdateServicePoints(List<ServicePoint> servicepoints)
        {
            _servicePointRepository.UpdateAll(servicepoints);
        }
        public void UpdateAppointments(List<Appointment> appointments)
        {
            _appointmentRepository.UpdateAll(appointments);
        }
        public void UpdateBulkMessages(List<BulkMessages> bulkmessages)
        {
            _bulkMessagesRepository.UpdateAll(bulkmessages);
        }
        public void UpdateMessages(List<Messages> messages)
        {
            _messagesRepository.UpdateAll(messages);
        }
        public void UpdateUserFacility(List<UserFacility> userfacility)
        {
            _userFacilityRepository.UpdateAll(userfacility);
        }
        public void UpdateVisit(List<Visit> visits)
        {
            _visitRepository.UpdateAll(visits);
        }
        public void DeleteFacility(long facilityId)
        {
            _DigitalEdgeContext.Remove(_DigitalEdgeContext.Facilities.Single(a => a.FacilityId == facilityId));
            _DigitalEdgeContext.SaveChanges();
        }
        public void DeleteServicePoint(long servicePointId)
        {
            _DigitalEdgeContext.Remove(_DigitalEdgeContext.ServicePoints.Single(a => a.ServicePointId == servicePointId));
            _DigitalEdgeContext.SaveChanges();
        }
        public Client ClientData()
        {
            Client users = this._clientRepository.Get().LastOrDefault();
            return users;
        }
        public List<AppointmentsModel> GetVisitHistory()
        {
            List<AppointmentsModel> visitHistorydetails = (from visit in _DigitalEdgeContext.Visits
                                                                    join client in _DigitalEdgeContext.Clients on visit.ClientId equals client.ClientId into list
                                                                    from clients in list.DefaultIfEmpty()
                                                                    select new AppointmentsModel
                                                                    {
                                                                        ServicePointId = visit.ServicePointId,
                                                                        ClientId = clients.ClientId,
                                                                        FirstName = clients.FirstName,
                                                                        LastName = clients.LastName,
                                                                        MiddleName = clients.MiddleName,
                                                                        PriorAppointmentDate = visit.PriorAppointmentDate,
                                                                        NextAppointmentDate = visit.NextAppointmentDate,
                                                                        VisitsId = visit.VisitId,
                                                                        VisitDate = visit.VisitDate,
                                                                        VisitType = visit.VisitType,
                                                                        ReasonOfVisit = visit.ReasonOfVisit,
                                                                        AdviseNotes = visit.AdviseNotes,
                                                                        ClientPhoneNo = clients.ClientPhoneNo
                                                                    }).ToList();
            return visitHistorydetails;
        }
        public List<AppointmentsModel> GetVisitHistoryByService(VisitsModel filterdata)
        {
            List<AppointmentsModel> visitHistorydetails = (from visit in _DigitalEdgeContext.Visits
                                                                    join client in _DigitalEdgeContext.Clients on visit.ClientId equals client.ClientId into list
                                                                    from clients in list.DefaultIfEmpty()
                                                                    where visit.ServicePointId == filterdata.ServicePointId
                                                                    select new AppointmentsModel
                                                                    {
                                                                        ServicePointId = visit.ServicePointId,
                                                                        ClientId = clients.ClientId,
                                                                        FirstName = clients.FirstName,
                                                                        LastName = clients.LastName,
                                                                        MiddleName = clients.MiddleName,
                                                                        PriorAppointmentDate = visit.PriorAppointmentDate,
                                                                        NextAppointmentDate = visit.NextAppointmentDate,
                                                                        VisitsId = visit.VisitId,
                                                                        VisitDate = visit.VisitDate,
                                                                        VisitType = visit.VisitType,
                                                                        ReasonOfVisit = visit.ReasonOfVisit,
                                                                        AdviseNotes = visit.AdviseNotes,
                                                                        ClientPhoneNo = clients.ClientPhoneNo
                                                                    }).ToList();

            return visitHistorydetails;
        }

        public Client GetClientById(long id)
        {
            var client = (from singleClient in _DigitalEdgeContext.Clients
                          join sex in _DigitalEdgeContext.Sexes on singleClient.SexId equals sex.SexId into clients
                          from sexes in clients.DefaultIfEmpty()
                          join clientStatus in _DigitalEdgeContext.ClientStatuses on singleClient.ClientStatusId equals clientStatus.ClientStatusId into statuses    
                          from clientStatuses in statuses.DefaultIfEmpty()
                          join facility in _DigitalEdgeContext.Facilities on singleClient.FacilityId equals facility.FacilityId into facilities
                          from clientFacility in facilities.DefaultIfEmpty()
                          where (singleClient.ClientId == id)
                          select new Client
                          {
                              ClientId = id,
                              FirstName = singleClient.FirstName,
                              LastName = singleClient.LastName,
                              ArtNo = singleClient.ArtNo,
                              DateOfBirth = singleClient.DateOfBirth,
                              EnrollmentDate = singleClient.EnrollmentDate,
                              ClientPhoneNo = singleClient.ClientPhoneNo,
                              PhoneVerifiedByAnalyst = singleClient.PhoneVerifiedByAnalyst,
                              PhoneVerifiedByFacilityStaff = singleClient.PhoneVerifiedByFacilityStaff,
                              Sex = sexes,
                              ClientStatuses = clientStatuses,
                              Facilities = clientFacility,
                              SexId = singleClient.SexId,
                              ClientTypeId = singleClient.ClientTypeId,
                              FacilityId = singleClient.FacilityId,
                              StatusCommentId = singleClient.StatusCommentId,
                              LanguageId = singleClient.LanguageId,
                              ServicePointId = singleClient.ServicePointId,
                              Address = singleClient.Address,
                              EnrolledBy = singleClient.EnrolledBy,
                              EnrolledByPhone = singleClient.EnrolledByPhone,
                              GeneralComment = singleClient.GeneralComment
                              
                              
                          })         
                          .SingleOrDefault();

            return client;
        }

        public List<FacilityModel> GetFacilities()
        {
            List<FacilityModel> facilities = (from facility in _DigitalEdgeContext.Facilities
                                              join facilityType in _DigitalEdgeContext.FacilityTypes on facility.FacilityTypeId equals facilityType.FacilityTypeId
                                              
                                                    select new FacilityModel
                                                    {
                                                        FacilityId = facility.FacilityId,
                                                        FacilityName = facility.FacilityName,
                                                        FacilityTypeId = facility.FacilityTypeId,
                                                        FacilityContactNumber = facility.FacilityContactNumber,
                                                        IsAvailable = facility.IsAvailable,
                                                        FacilityTypeName = facility.FacilityTypeModel.FacilityTypeName,
                                                        Address = facility.Address
                                                        

                                                    }
                                              ).ToList();
            return facilities;
        }

        public List<ServicePointModel> GetServicePoints()
        {
            List<ServicePointModel> servicePoints = (from departments in _DigitalEdgeContext.ServicePoints
                                                     select new ServicePointModel
                                                     {
                                                        ServicePointId = departments.ServicePointId,
                                                        ServicePointName = departments.ServicePointName,
                                                     }
            ).ToList();

            return servicePoints;
        }

        public List<VisitModel> GetVisits()
        {
            List<VisitModel> visits = (from attendances in _DigitalEdgeContext.Visits
                                        select new VisitModel
                                        {
                                            VisitId = attendances.VisitId,
                                            VisitDate = attendances.VisitDate,
                                            VisitType = attendances.VisitType,
                                            AppointmentId = attendances.AppointmentId,
                                            ServicePointId = attendances.ServicePointId,
                                            AppointmentStatus = attendances.AppointmentStatus,
                                            PriorAppointmentDate = attendances.PriorAppointmentDate,
                                            NextAppointmentDate = attendances.NextAppointmentDate,
                                            ClinicRemarks = attendances.ClinicRemarks,
                                            Diagnosis = attendances.Diagnosis,
                                            SecondDiagnosis = attendances.SecondDiagnosis,
                                            ThirdDiagnosis = attendances.ThirdDiagnosis,
                                            Therapy = attendances.Therapy,
                                            FacilityId = attendances.FacilityId,


                                        }).ToList();
            return visits;
        }

        public Appointment GetAppointmentById(long id)
        {
            var appointment = (from appointments in _DigitalEdgeContext.Appointments
                               join client in _DigitalEdgeContext.Clients on appointments.ClientId equals client.ClientId into clients
                               from clientAppointments in clients.DefaultIfEmpty()
                               join facility in _DigitalEdgeContext.Facilities on appointments.FacilityId equals facility.FacilityId into facilities
                               from appointmentInFacility in facilities.DefaultIfEmpty()
                               join servicePoint in _DigitalEdgeContext.ServicePoints on appointments.ServicePointId equals servicePoint.ServicePointId into servicePoint
                               from clientServicePoint in servicePoint.DefaultIfEmpty()
                               where (appointments.AppointmentId == id)
                               select new Appointment
                               {
                                   AppointmentId = id,
                                   ClientId = clientAppointments.ClientId,
                                   ServicePointId = clientAppointments.ServicePointId,
                                   FacilityId = appointmentInFacility.FacilityId,
                                   ClientModel = clientAppointments,
                                   FacilityModel = appointmentInFacility,
                                   ServicePointModel = clientServicePoint,
                                   AppointmentStatus = appointments.AppointmentStatus,
                                   AppointmentDate = appointments.AppointmentDate,
                                   Detail = appointments.Detail                             
                               }

                               ).SingleOrDefault();

            return appointment;
        }

        public Facility GetFacilityById(long id)
        {
            var facility = (from facilities in _DigitalEdgeContext.Facilities
                            join facilityTypes in _DigitalEdgeContext.FacilityTypes on facilities.FacilityTypeId equals facilityTypes.FacilityTypeId into buildings
                            from building in buildings.DefaultIfEmpty()
                            where (facilities.FacilityId == id)
                            select new Facility
                            {
                                FacilityId = facilities.FacilityId,
                                FacilityName = facilities.FacilityName,
                                FacilityTypeId = facilities.FacilityTypeId,
                                IsAvailable = facilities.IsAvailable,
                                FacilityContactNumber = facilities.FacilityContactNumber,
                                Address = facilities.Address,
                                FacilityTypeModel = building
                            }
                            ).SingleOrDefault();
            return facility;

        }

        public List<FacilityTypeModel> GetFacilityTypes()
        {
            List<FacilityTypeModel> facilityTypes = (from facilityType in _DigitalEdgeContext.FacilityTypes
                                                     select new FacilityTypeModel
                                                     {
                                                         FacilityTypeId = facilityType.FacilityTypeId,
                                                         FacilityTypeName = facilityType.FacilityTypeName

                                                     }).ToList();

            return facilityTypes;
        }

        public string CreateVisit(Visit visitData)
        {
            if (_DigitalEdgeContext.Visits.Any(v => v.VisitId.Equals(visitData.VisitId))) return "null";
            this._visitRepository.Insert(visitData);
            return "ok";
        }

        public Visit GetVisitById(long id)
        {
            var visit = (from visits in _DigitalEdgeContext.Visits
                         join client in _DigitalEdgeContext.Clients on visits.ClientId equals client.ClientId into dbClients
                         from clientVisits in dbClients.DefaultIfEmpty()
                         join facility in _DigitalEdgeContext.Facilities on visits.FacilityId equals facility.FacilityId into dbFacilitites
                         from clientFaciltiy in dbFacilitites.DefaultIfEmpty()
                         join appointment in _DigitalEdgeContext.Appointments on visits.AppointmentId equals appointment.AppointmentId into dbAppointments
                         from clientAppointment in dbAppointments.DefaultIfEmpty()
                         join servicePoint in _DigitalEdgeContext.ServicePoints on visits.ServicePointId equals servicePoint.ServicePointId into dbServicePoints
                         from visitServicePoint in dbServicePoints.DefaultIfEmpty()
                         where (visits.VisitId == id)
                         select new Visit
                         {
                             VisitId = visits.VisitId,
                             VisitDate = visits.VisitDate,
                             ClientId = clientVisits.ClientId,
                             AppointmentId = clientAppointment.AppointmentId,
                             FacilityId = clientFaciltiy.FacilityId,
                             ServicePointId = visitServicePoint.ServicePointId,
                             ReasonOfVisit = visits.ReasonOfVisit,
                             Diagnosis = visits.Diagnosis,
                             SecondDiagnosis = visits.SecondDiagnosis,
                             ThirdDiagnosis = visits.ThirdDiagnosis,
                             ClinicRemarks = visits.ClinicRemarks                         

                         }
                         ).SingleOrDefault();

            return visit;
        }
    }
}
