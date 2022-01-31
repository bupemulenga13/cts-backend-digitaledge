﻿

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
                                                           where clients.FirstName == model.FirstName && appointment.AppointmentDate >= DateTime.UtcNow
                                                           select new AppointmentsModel
                                                           {
                                                               Id = appointment.AppointmentId,
                                                               ClientId = clients.ClientId,
                                                               ServiceTypeId = appointment.ServiceTypeId,
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
                                                                   where appointment.ServiceTypeId == upcommingfilterdata.ServiceTypeId && appointment.AppointmentDate >= DateTime.UtcNow
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
                                                                       ServiceTypeId = visits.ServicePointId,
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
                                                                     VisitsId = visits.VisitId,
                                                                     ServiceTypeId = appointment.ServiceTypeId
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
                                                                where appointment.ServiceTypeId == missedfilter.ServiceTypeId && appointment.AppointmentDate < DateTime.UtcNow
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
                                                                    ServiceTypeId = visits.ServiceTypeId
                                                                }).ToList();

            return appointmentsmissedfilter;
        }

        public List<AppointmentsModel> GetAppointmentsByClientId(RegistrationModel missedfilter)
        {

            List<AppointmentsModel> appointmentsmissedfilter = (from appointment in _DigitalEdgeContext.Appointments
                                                                join visit in _DigitalEdgeContext.Visits on appointment.AppointmentId equals visit.AppointmentId into appointments
                                                                from visits in appointments.DefaultIfEmpty()
                                                                join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into list
                                                                from clients in list.DefaultIfEmpty()
                                                                where appointment.ClientId == missedfilter.ClientId
                                                                select new AppointmentsModel
                                                                {
                                                                    Id = appointment.AppointmentId,
                                                                    ClientId = clients.ClientId,
                                                                    AppointmentDate = appointment.AppointmentDate,
                                                                    AppointmentTime = appointment.AppointmentDate,
                                                                    ServiceTypeId = appointment.ServiceTypeId,
                                                                    AppointmentStatus = appointment.AppointmentStatus
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
                                                                    where visits.ServicePointId == filterdata.ServicePointId && visits.VisitDate >= DateTime.UtcNow
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
                                             ClientStatusId = client.ClientStatusId,
                                             ClientStatus = status.ClientStatusName,
                                             ClientTypeId = client.ClientTypeId,
                                             ClientType = client.ClientTypes.ClientTypeName,
                                             DateOfBirth = client.DateOfBirth,
                                             StatusCommentId = client.StatusCommentId,
                                             StatusComment = client.StatusComments.StatusCommentName,
                                             SexId = client.SexId,
                                             Sex = sex.SexName,
                                             ServicePointId = client.ServicePointId,
                                             LanguageId = client.LanguageId,
                                             FacilityId = client.FacilityId,
                                             PhoneVerifiedByAnalyst = client.PhoneVerifiedByAnalyst,
                                             PhoneVerifiedByFacilityStaff = client.PhoneVerifiedByFacilityStaff,
                                             AlternativePhoneNumber1 = client.AlternativePhoneNumber1,
                                             GeneralComment = client.GeneralComment,
                                             EnrolledByName = client.EnrolledByName,
                                             EnrolledByPhone = client.EnrolledByPhone,
                                             EnrollmentType = client.EnrollmentType,
                                             AccessToPhone = client.AccessToPhone,
                                             HamornizedMobilePhone = client.HamornizedMobilePhone,
                                             HarmonizedPhysicalAddress = client.HarmonizedPhysicalAddress,
                                             ClientRelationship = client.ClientRelationship,
                                             Zone = client.Zone,
                                             Village = client.Village,
                                             HouseNo = client.HouseNo,
                                             GISLocation = client.GISLocation,
                                             DateCreated = client.DateCreated,
                                             DateEdit = client.DateEdit,
                                             CreatedBy = client.CreatedBy,
                                             EditBy = client.EditBy
                                         }
                ).OrderByDescending(c => c.DateCreated).ToList();
            return clients;

        }
        public List<ClientModel> GetClientsByFacility(long facilityId)
        {
            List<ClientModel> clientsInFacility = (from clients in _DigitalEdgeContext.Clients
                                                   join facility in _DigitalEdgeContext.Facilities on clients.FacilityId equals facility.FacilityId
                                                   join status in _DigitalEdgeContext.ClientStatuses on clients.ClientStatusId equals status.ClientStatusId
                                                   join sex in _DigitalEdgeContext.Sexes on clients.SexId equals sex.SexId
                                                   where clients.FacilityId == facilityId
                                                   select new ClientModel
                                                   {
                                                       ClientId = clients.ClientId,
                                                       FirstName = clients.FirstName,
                                                       LastName = clients.LastName,
                                                       ArtNo = clients.ArtNo,
                                                       ClientPhoneNo = clients.ClientPhoneNo,
                                                       DateOfBirth = clients.DateOfBirth,
                                                       Age = clients.Age,
                                                       Zone = clients.Zone,
                                                       Village = clients.Village,
                                                       HouseNo = clients.HouseNo, 
                                                       GISLocation = clients.GISLocation,
                                                       GeneralComment = clients.GeneralComment,
                                                       EnrolledByName = clients.EnrolledByName,
                                                       AlternativePhoneNumber1 = clients.AlternativePhoneNumber1,
                                                       PhoneVerifiedByAnalyst = clients.PhoneVerifiedByAnalyst,
                                                       PhoneVerifiedByFacilityStaff = clients.PhoneVerifiedByFacilityStaff,
                                                       EnrollmentDate = clients.EnrollmentDate,
                                                       EnrolledByPhone = clients.EnrolledByPhone,
                                                       FacilityId = clients.FacilityId,
                                                       Facility = facility.FacilityName,
                                                       ClientTypeId = clients.ClientTypeId,
                                                       ServicePointId = clients.ServicePointId,
                                                       LanguageId = clients.LanguageId,
                                                       ClientStatusId = clients.ClientStatusId,                                                      
                                                       StatusCommentId = clients.StatusCommentId,
                                                       SexId = clients.SexId,
                                                       ClientRelationship = clients.ClientRelationship,
                                                       EnrollmentType = clients.EnrollmentType,
                                                       AccessToPhone = clients.AccessToPhone,
                                                       HamornizedMobilePhone = clients.HamornizedMobilePhone,
                                                       HarmonizedPhysicalAddress = clients.HarmonizedPhysicalAddress,
                                                       ClientStatus = status.ClientStatusName,
                                                       ClientType = clients.ClientTypes.ClientTypeName,
                                                       StatusComment = clients.StatusComments.StatusCommentName,
                                                       Sex = sex.SexName,                                                       
                                                       DateCreated = clients.DateCreated,
                                                       DateEdit = clients.DateEdit,
                                                       CreatedBy = clients.CreatedBy,
                                                       EditBy = clients.EditBy
                                                   }

                ).OrderByDescending(c => c.DateCreated).ToList();

            return clientsInFacility;

        }

        public List<AppointmentsModel> GetAppointments()
        {
            List<AppointmentsModel> appointments = (from appointment in _DigitalEdgeContext.Appointments
                                                    join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into clientAppt
                                                    from clientAppts in clientAppt.DefaultIfEmpty()
                                                    join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into apptFacility
                                                    from appointmentFacility in apptFacility.DefaultIfEmpty()
                                                    join department in _DigitalEdgeContext.ServiceTypes on appointment.ServiceTypeId equals department.ServiceTypeId into apptDepartment
                                                    from apptsServicePoint in apptDepartment.DefaultIfEmpty()
                                                    select new AppointmentsModel
                                                    {
                                                        Id = appointment.AppointmentId,
                                                        ClientId = appointment.ClientId,
                                                        FacilityId = appointment.FacilityId,
                                                        ServiceTypeId = appointment.ServiceTypeId,
                                                        AppointmentDate = appointment.AppointmentDate,
                                                        AppointmentStatus = appointment.AppointmentStatus,
                                                        InteractionDate = appointment.InteractionDate,
                                                        Comment = appointment.Comment,
                                                        FirstName = clientAppts.FirstName,
                                                        LastName = clientAppts.LastName,
                                                        ArtNo = clientAppts.ArtNo,
                                                        FacilityName = appointment.FacilityModel.FacilityName,
                                                        AppointmentTime = appointment.AppointmentDate,
                                                        ClientPhoneNo = appointment.ClientModel.ClientPhoneNo,
                                                        ClientModel = new ClientModel { FirstName = clientAppts.FirstName, LastName = clientAppts.LastName, ArtNo = clientAppts.ArtNo, ClientPhoneNo = clientAppts.ClientPhoneNo },
                                                        FacilityModel = new FacilityModel { FacilityName = appointmentFacility.FacilityName },
                                                        ServiceTypeModel = new ServiceTypeModel { ServiceTypeName = apptsServicePoint.ServiceTypeName },
                                                        DateCreated = appointment.DateCreated,
                                                        DateEdited = appointment.DateEdited,
                                                        CreatedBy = appointment.CreatedBy,
                                                        EditedBy = appointment.EditedBy
                                                    }
                                                    ).OrderByDescending(c => c.DateCreated).ToList();
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
                                           && provinces.ProvinceId == filterdata.ProvinceId && districts.DistrictId == filterdata.DistrictId
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
                                           where visits.ServicePointId == filterdata.ServicePointId
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
                                           where visits.FacilityId == filterdata.FacilityId && provinces.ProvinceId == filterdata.ProvinceId
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
                                           && districts.DistrictId == filterdata.DistrictId
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
                                           join serviceType in _DigitalEdgeContext.ServiceTypes on appointment.ServiceTypeId equals serviceType.ServiceTypeId into serviceTypesDetails
                                           from serviceTypes in serviceTypesDetails.DefaultIfEmpty()
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
                                               ServiceTypeId = serviceTypes.ServiceTypeId,
                                               ServiceTypeName = serviceTypes.ServiceTypeName
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
                                               /*//join serviceType in _DigitalEdgeContext.ServiceTypes on appointment.ServiceTypeId equals serviceType.ServiceTypeId into serviceTypesDetails  
                                               from serviceTypes in serviceTypesDetails.DefaultIfEmpty()*/
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
                                               FacilityContactNumber = facilitys.FacilityContactNumber,/*
                                               ServiceTypeId = serviceTypes.ServiceTypeId,
                                               ServiceTypeName = serviceTypes.ServicePointName*/
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
                                           from facilitys in facilityDetais.DefaultIfEmpty()/*
                                           join servicepoint in _DigitalEdgeContext.ServicePoints on appointment.ServicePointId equals servicepoint.ServicePointId into servicepointDetails
                                           from servicepoints in servicepointDetails.DefaultIfEmpty()*/
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
                                               FacilityContactNumber = facilitys.FacilityContactNumber,/*
                                               ServicePointId = servicepoints.ServicePointId,
                                               ServicePointName = servicepoints.ServicePointName*/
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
                                                                   NextAppointmentDate = visits.NextAppointmentDate,
                                                                   ServiceTypeId = appointment.ServiceTypeId
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
                                                                      ServiceTypeId = appointment.ServiceTypeId,
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
            else if (type == "serviceType")
            {
                appointmentDetails = this._appointmentRepository.Get().Where(x => x.ServiceTypeId == id).ToList();
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
                              Zone = singleClient.Zone,
                              HouseNo = singleClient.HouseNo,
                              Village = singleClient.Village,
                              GISLocation = singleClient.GISLocation,
                              EnrolledByName = singleClient.EnrolledByName,
                              EnrolledByPhone = singleClient.EnrolledByPhone,
                              GeneralComment = singleClient.GeneralComment,
                              HamornizedMobilePhone = singleClient.HamornizedMobilePhone,
                              HarmonizedPhysicalAddress = singleClient.HarmonizedPhysicalAddress,
                              DateCreated = singleClient.DateCreated,
                              DateEdit = singleClient.DateEdit,
                              CreatedBy = singleClient.CreatedBy,
                              EditBy = singleClient.EditBy
                          })
                          .SingleOrDefault();

            return client;
        }

        public List<FacilityModel> GetFacilities()
        {
            List<FacilityModel> facilities = (from facility in _DigitalEdgeContext.Facilities
                                              join facilityType in _DigitalEdgeContext.FacilityTypes on facility.FacilityTypeId equals facilityType.FacilityTypeId
                                              join districtName in _DigitalEdgeContext.Districts on facility.DistrictId equals districtName.DistrictId
                                              
                                              select new FacilityModel
                                              {
                                                  FacilityId = facility.FacilityId,
                                                  FacilityName = facility.FacilityName,
                                                  FacilityTypeId = facility.FacilityTypeId,
                                                  FacilityContactNumber = facility.FacilityContactNumber,
                                                  IsAvailable = facility.IsAvailable,
                                                  FacilityTypeName = facility.FacilityTypeModel.FacilityTypeName,
                                                  Address = facility.Address,
                                                  DistrictId = facility.DistrictId,
                                                  DistrictName = facility.Districts.DistrictName,
                                                  DateCreated = facility.DateCreated,
                                                  DateEdited = facility.DateEdited,
                                                  CreatedBy = facility.CreatedBy,
                                                  EditedBy = facility.EditedBy
                                              }
                                              ).ToList();
            return facilities;
        }

        public List<VisitsServiceModel> GetServiceTypes()
        {
            List<VisitsServiceModel> serviceTypes = (from serviceType in _DigitalEdgeContext.ServiceTypes
                                                     select new VisitsServiceModel
                                                     {
                                                         ServiceTypeId = serviceType.ServiceTypeId,
                                                         ServiceTypeName = serviceType.ServiceTypeName
                                                     }).ToList();

            return serviceTypes;
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
                                           ServiceTypeId = attendances.ServiceTypeId,
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
                               join serviceType in _DigitalEdgeContext.ServiceTypes on appointments.ServiceTypeId equals serviceType.ServiceTypeId into serviceType
                               from clientServiceType in serviceType.DefaultIfEmpty()
                               where (appointments.AppointmentId == id)
                               select new Appointment
                               {
                                   AppointmentId = id,
                                   ClientId = clientAppointments.ClientId,
                                   ServiceTypeId = appointments.ServiceTypeId,
                                   FacilityId = appointmentInFacility.FacilityId,
                                   ClientModel = clientAppointments,
                                   FacilityModel = appointmentInFacility,
                                   ServiceTypeModel = clientServiceType,
                                   AppointmentStatus = appointments.AppointmentStatus,
                                   AppointmentDate = appointments.AppointmentDate,
                                   Comment = appointments.Comment,
                                   DateCreated = appointments.DateCreated,
                                   DateEdited = appointments.DateEdited,
                                   CreatedBy = appointments.CreatedBy,
                                   EditedBy = appointments.EditedBy
                               }

                               ).SingleOrDefault();

            return appointment;
        }

        public Facility GetFacilityById(long id)
        {
            var facility = (from facilities in _DigitalEdgeContext.Facilities
                            join facilityTypes in _DigitalEdgeContext.FacilityTypes on facilities.FacilityTypeId equals facilityTypes.FacilityTypeId into buildings
                            from building in buildings.DefaultIfEmpty()
                            join distirct in _DigitalEdgeContext.Districts on facilities.DistrictId equals distirct.DistrictId into facilityDistrict
                            from faccilityInDistrict in facilityDistrict.DefaultIfEmpty()
                            where (facilities.FacilityId == id)
                            select new Facility
                            {
                                FacilityId = facilities.FacilityId,
                                FacilityName = facilities.FacilityName,
                                FacilityTypeId = facilities.FacilityTypeId,
                                DistrictId = facilities.DistrictId,
                                IsAvailable = facilities.IsAvailable,
                                FacilityContactNumber = facilities.FacilityContactNumber,
                                Address = facilities.Address,
                                FacilityTypeModel = building,
                                DateCreated = facilities.DateCreated,
                                DateEdited = facilities.DateEdited,
                                CreatedBy = facilities.CreatedBy,
                                EditedBy = facilities.EditedBy,
                                Districts = faccilityInDistrict
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

        public int CountFacilities(long facilityId)
        {
            var count = _DigitalEdgeContext.Facilities.Count(facility => facility.FacilityId == facilityId);

            return count;
        }

        public int CountClients()
        {
            var count = _DigitalEdgeContext.Clients.Count();

            return count;
        }

        public int CountAppointments()
        {
            var count = _DigitalEdgeContext.Appointments.Count();

            return count;
        }

        public int AvailableFacilities()
        {
            var facilities = _DigitalEdgeContext.Facilities.Where(f => f.IsAvailable == true).Count();

            return facilities;
        }

        public int TodaysAppointments()
        {
            DateTime today = DateTime.Now.Date;

            var appointments = _DigitalEdgeContext.Appointments.Where(a => a.AppointmentDate == today).Count();

            return appointments;
        }

        public int TodaysClients()
        {
            DateTime today = DateTime.Now.Date;

            var clients = _DigitalEdgeContext.Clients.Where(c => c.EnrollmentDate == today).Count();

            return clients;
        }

        public List<AppointmentsModel> GetAppointmentsByFacility(long facilityId)
        {
            List<AppointmentsModel> appointments = (from appointment in _DigitalEdgeContext.Appointments
                                                    join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into clientAppt
                                                    from clientAppts in clientAppt.DefaultIfEmpty()
                                                    join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into apptFacility
                                                    from appointmentFacility in apptFacility.DefaultIfEmpty()
                                                    join department in _DigitalEdgeContext.ServiceTypes on appointment.ServiceTypeId equals department.ServiceTypeId into apptDepartment
                                                    from apptsServicePoint in apptDepartment.DefaultIfEmpty()
                                                    where appointment.FacilityId == facilityId
                                                    select new AppointmentsModel
                                                    {
                                                        Id = appointment.AppointmentId,
                                                        ClientId = appointment.ClientId,
                                                        FacilityId = appointment.FacilityId,
                                                        ServiceTypeId = appointment.ServiceTypeId,
                                                        AppointmentDate = appointment.AppointmentDate,
                                                        AppointmentStatus = appointment.AppointmentStatus,
                                                        InteractionDate = appointment.InteractionDate,
                                                        Comment = appointment.Comment,
                                                        FirstName = clientAppts.FirstName,
                                                        LastName = clientAppts.LastName,
                                                        ArtNo = clientAppts.ArtNo,
                                                        FacilityName = appointment.FacilityModel.FacilityName,
                                                        AppointmentTime = appointment.AppointmentDate,
                                                        ClientPhoneNo = appointment.ClientModel.ClientPhoneNo,
                                                        ClientModel = new ClientModel { FirstName = clientAppts.FirstName, LastName = clientAppts.LastName, ArtNo = clientAppts.ArtNo, ClientPhoneNo = clientAppts.ClientPhoneNo },
                                                        FacilityModel = new FacilityModel { FacilityName = appointmentFacility.FacilityName },
                                                        ServiceTypeModel = new ServiceTypeModel { ServiceTypeName = apptsServicePoint.ServiceTypeName },
                                                        DateCreated = appointment.DateCreated,
                                                        DateEdited = appointment.DateEdited,
                                                        CreatedBy = appointment.CreatedBy,
                                                        EditedBy = appointment.EditedBy
                                                    }
                                                    ).OrderByDescending(c => c.DateCreated).ThenByDescending(c => c.DateEdited).ToList();
            return appointments;
        }

        public long CountAppointmentsFacility(long facilityId)
        {
            var count = _DigitalEdgeContext.Facilities.Count(facility => facility.FacilityId == facilityId);

            return count;
        }

        public int CountClientsInFacility(long facilityId)
        {
            var count = _DigitalEdgeContext.Clients.Count(client => client.FacilityId == facilityId);

            return count;
        }

        public int CountAppointmentsInFacility(long facilityId)
        {
            var count = _DigitalEdgeContext.Appointments.Count(appointment => appointment.FacilityId == facilityId);

            return count;
        }

        public List<FacilityModel> GetFacilities(long facilityId)
        {
            List<FacilityModel> facilities = (from facility in _DigitalEdgeContext.Facilities
                                              join facilityType in _DigitalEdgeContext.FacilityTypes on facility.FacilityTypeId equals facilityType.FacilityTypeId
                                              where (facility.FacilityId == facilityId)
                                              select new FacilityModel
                                              {
                                                  FacilityId = facility.FacilityId,
                                                  FacilityName = facility.FacilityName,
                                                  FacilityTypeId = facility.FacilityTypeId,
                                                  FacilityContactNumber = facility.FacilityContactNumber,
                                                  IsAvailable = facility.IsAvailable,
                                                  FacilityTypeName = facility.FacilityTypeModel.FacilityTypeName,
                                                  Address = facility.Address,
                                                  DistrictName = facility.Districts.DistrictName,
                                                  DateCreated = facility.DateCreated,
                                                  DateEdited = facility.DateEdited,
                                                  CreatedBy = facility.CreatedBy,
                                                  EditedBy = facility.EditedBy
                                              }
                                              ).ToList();
            return facilities;
        }

        public List<FacilityModel> GetFacilitiesInDistrict(long id)
        {
            List<FacilityModel> facilitiesInDistrict    = (from facility in _DigitalEdgeContext.Facilities
                                                           join district in _DigitalEdgeContext.Districts on facility.DistrictId equals district.DistrictId
                                                           join facilityType in _DigitalEdgeContext.FacilityTypes on facility.FacilityTypeId equals facilityType.FacilityTypeId
                                                           where (facility.DistrictId == id)
                                                            select new FacilityModel
                                                            {
                                                                FacilityId = facility.FacilityId,
                                                                FacilityName = facility.FacilityName,
                                                                FacilityTypeId = facility.FacilityTypeId,
                                                                FacilityContactNumber = facility.FacilityContactNumber,
                                                                IsAvailable = facility.IsAvailable,
                                                                FacilityTypeName = facility.FacilityTypeModel.FacilityTypeName,
                                                                Address = facility.Address,
                                                                DistrictName = facility.Districts.DistrictName,
                                                                DateCreated = facility.DateCreated,
                                                                DateEdited = facility.DateEdited,
                                                                CreatedBy = facility.CreatedBy,
                                                                EditedBy = facility.EditedBy

                                                            }).ToList();

            return facilitiesInDistrict;
        }

        public List<LanguageModel> GetLanguages()
        {
            List<LanguageModel> languages = (from languageModel in _DigitalEdgeContext.Languages
                                             select new LanguageModel
                                             {
                                                 LanguageId = languageModel.LanguageId,
                                                 LanguageName = languageModel.LanguageName
                                             }).ToList();

            return languages;
        }

        public int CountFacilities()
        {
            var count = _DigitalEdgeContext.Facilities.Count();

            return count;
        }

        public int CountFacilitiesInDisitrct(long districtId)
        {
            var count = _DigitalEdgeContext.Facilities.Count(f => f.DistrictId == districtId);

            return count;
        }

        public RegistrationModel GetClientAppointemnt(long id)
        {
            var appointment = (from appointments in _DigitalEdgeContext.Appointments
                               join client in _DigitalEdgeContext.Clients on appointments.ClientId equals client.ClientId into clients
                               from clientAppointments in clients.DefaultIfEmpty()
                               join facility in _DigitalEdgeContext.Facilities on appointments.FacilityId equals facility.FacilityId into facilities
                               from appointmentInFacility in facilities.DefaultIfEmpty()
                               join serviceType in _DigitalEdgeContext.ServiceTypes on appointments.ServiceTypeId equals serviceType.ServiceTypeId into serviceType
                               from clientServiceType in serviceType.DefaultIfEmpty()
                               where (appointments.AppointmentId == id)
                               select new RegistrationModel
                               {
                                   AppointmentId = id,
                                   ClientId = clientAppointments.ClientId,
                                   ServiceTypeId = appointments.ServiceTypeId,
                                   FacilityId = appointmentInFacility.FacilityId,
                                   AppointmentStatus = appointments.AppointmentStatus,
                                   AppointmentDate = appointments.AppointmentDate.ToShortDateString(),
                                   AppointmentTime = appointments.AppointmentDate.ToShortTimeString(),
                                   Comment = appointments.Comment,
                                   DateCreated = appointments.DateCreated,
                                   DateEdited = appointments.DateEdited,
                                   CreatedBy = appointments.CreatedBy,
                                   EditedBy = appointments.EditedBy
                               }

                              ).SingleOrDefault();

            return appointment;
        }

        public IEnumerable<SearchModel> SearchClient(string searchTerm)
        {
            IEnumerable<SearchModel> query = (from client in _DigitalEdgeContext.Clients
                                                join facility in _DigitalEdgeContext.Facilities on client.FacilityId equals facility.FacilityId into appointmentFacility
                                                from f in appointmentFacility.DefaultIfEmpty()
                                                where (client.FirstName.Contains(searchTerm) || client.LastName.Contains(searchTerm) || client.ArtNo.Contains(searchTerm) || client.ClientPhoneNo.Contains(searchTerm))
                                                select new SearchModel
                                                    {
                                                        ClientId = client.ClientId,
                                                        FirstName = client.FirstName,
                                                        LastName = client.LastName,
                                                        ArtNo = client.ArtNo,
                                                        ClientPhoneNo = client.ClientPhoneNo,
                                                        EnrollmentDate = client.EnrollmentDate,
                                                        FacilityId = client.FacilityId,
                                                        FacilityName = f.FacilityName

                                                    });

            

            return query.ToList();
        }

        public IEnumerable<SearchModel> SearchAppointment(string searchTerm)
        {
            IEnumerable<SearchModel> query = (from appointment in _DigitalEdgeContext.Appointments
                                                    join client in _DigitalEdgeContext.Clients on appointment.ClientId equals client.ClientId into clients
                                                    from clientAppointment in clients.DefaultIfEmpty()
                                                    join facility in _DigitalEdgeContext.Facilities on appointment.FacilityId equals facility.FacilityId into appointmentFacility
                                                    from f in appointmentFacility.DefaultIfEmpty()
                                                    join serviceType in _DigitalEdgeContext.ServiceTypes on appointment.ServiceTypeId equals serviceType.ServiceTypeId into appointmentServiceType
                                                    from st in appointmentServiceType.DefaultIfEmpty()
                                                    where (clientAppointment.FirstName.Contains(searchTerm) || clientAppointment.LastName.Contains(searchTerm) || clientAppointment.ArtNo.Contains(searchTerm) || clientAppointment.ClientPhoneNo.Contains(searchTerm))
                                                    select new SearchModel
                                                    {
                                                        AppointmentId = appointment.AppointmentId,
                                                        AppointmentDate = appointment.AppointmentDate.ToString("dd/MM/yyyy"),
                                                        AppointmentTime = appointment.AppointmentDate.ToString("HH:mm"),
                                                        AppointmentStatus = appointment.AppointmentStatus,
                                                        ServiceTypeId = appointment.ServiceTypeId,
                                                        FacilityId = appointment.FacilityId,
                                                        Comment = appointment.Comment,
                                                        InteractionDate = appointment.InteractionDate.Value.ToShortDateString(),
                                                        InteractionTime = appointment.InteractionDate.Value.ToShortTimeString(),                                                        
                                                        FirstName = clientAppointment.FirstName,
                                                        LastName = clientAppointment.LastName,
                                                        ArtNo = clientAppointment.ArtNo,
                                                        ClientPhoneNo = clientAppointment.ClientPhoneNo                                                       


                                                    });



            return query.ToList();
        }

        public ViralLoadModel GetClientVlResultDetails(long id)
        {
            var query = (from viralload in _DigitalEdgeContext.ViralLoadResults
                         join client in _DigitalEdgeContext.Clients on viralload.ClientId equals client.ClientId into ClientViralLoad
                         from clientVl in ClientViralLoad.DefaultIfEmpty()
                         where (viralload.ViralLoadId == id)
                         select new ViralLoadModel
                         {

                             ViralLoadId = viralload.ViralLoadId,
                             ClientId = clientVl.ClientId,
                             FirstName = clientVl.FirstName,
                             LastName = clientVl.LastName,
                             ArtNo = clientVl.ArtNo,
                             InitialViralLoadCount = viralload.InitialViralLoadCount.ToString(),
                             CurrentViralLoadCount = viralload.CurrentViralLoadCount.ToString(),
                             NextVLDueDate = viralload.NextVLDueDate.Value.ToString("dd/mm/yyyy")

                         }).SingleOrDefault();


            return query;
        }

        public List<ViralLoadModel> GetClientVLList(long id)
        {
            List<ViralLoadModel> query = (from viralload in _DigitalEdgeContext.ViralLoadResults
                                          join client in _DigitalEdgeContext.Clients on viralload.ClientId equals client.ClientId into ClientViralLoad
                                          from clientVl in ClientViralLoad.DefaultIfEmpty()
                                          where (viralload.ClientId == id)
                                          select new ViralLoadModel
                                          {

                                              ViralLoadId = viralload.ViralLoadId,
                                              ClientId = clientVl.ClientId,
                                              FirstName = clientVl.FirstName,
                                              LastName = clientVl.LastName,
                                              ArtNo = clientVl.ArtNo,
                                              InitialViralLoadCount = viralload.InitialViralLoadCount.ToString(),
                                              CurrentViralLoadCount = viralload.CurrentViralLoadCount.ToString(),
                                              NextVLDueDate = viralload.NextVLDueDate.Value.ToString("dd/MM/yyyy"),
                                              DateCreated = viralload.DateCreated

                                          }).OrderByDescending(vl => vl.DateCreated).ToList();

            return query;
        }
    }
}
