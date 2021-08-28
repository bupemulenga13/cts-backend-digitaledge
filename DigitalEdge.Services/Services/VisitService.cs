using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using DigitalEdge.Domain;
using DigitalEdge.Repository;
using MessageForSMS = DigitalEdge.Domain.Messages;
using Messages = DigitalEdge.Repository.Messages;

namespace DigitalEdge.Services
{
    public class VisitService : IVisitService

    {
        private readonly IVisitRepository _visitRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IConfiguration _iconfigration;
        public VisitService(IConfiguration iconfigration, IVisitRepository visitRepository, IMessageRepository messageRepository)
        {
            this._iconfigration = iconfigration;
            this._visitRepository = visitRepository;
            this._messageRepository = messageRepository;
        }

        public List<SMSRecords> CSVImportFile(DataTable csvFileDataTable)
        {
            List<SMSRecords> importCSVData = new List<SMSRecords>();
            importCSVData = ConvertDataTable<SMSRecords>(csvFileDataTable);

            return importCSVData;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            DataTable dtCloned = dr.Table.Clone();
            foreach (DataColumn column in dtCloned.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        Type type = Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType;
                        object ColumnValue = (dr[column.ColumnName] == null) ? null : Convert.ChangeType(dr[column.ColumnName], type);
                        pro.SetValue(obj, ColumnValue, null);
                        break;
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        public List<AppointmentsModel> getAppointmentsDetails()
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetAppointementsDetails().ToList();
            foreach (var user in userAppointement)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userAppointement == null)
                return null;
            return (userAppointement);
        } 
        public List<AppointmentsModel> getAppointmentCheck(AppointmentsModel data)
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetAppointmentCheck(data).ToList();           
            if (userAppointement == null)
                return null;
            return (userAppointement);
        }
        public List<AppointmentsModel> getUpcommingVisitsDetails()
        {
            List<AppointmentsModel> userVisits = _visitRepository.GetUpcommingVisitsDetails().ToList();
            foreach (var user in userVisits)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userVisits == null)
                return null;
            return (userVisits);
        }    
        public List<AppointmentsModel> getUpcommingVisitsDetails(VisitsModel filterdata)
        {
            List<AppointmentsModel> userVisits = _visitRepository.GetUpcommingVisitsDetailsfilter(filterdata).ToList();
            foreach (var user in userVisits)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userVisits == null)
                return null;
            return (userVisits);
        }
        public List<AppointmentsModel> getAppointmentsDetailsMissed()
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetAppointmentsDetailsMissed().Select(x => new AppointmentsModel(x.Id, x.ClientId, x.VisitsId, x.FirstName, x.LastName, x.MiddleName, x.PriorAppointmentDate, x.AppointmentDate, x.AppointmentTime, x.NextAppointmentDate, x.Age)).ToList();
            foreach (var user in userAppointement)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userAppointement == null)
                return null;
            return (userAppointement);
        } 
        public List<AppointmentsModel> getAppointmentsMissedFilter(VisitsModel appointmentmissed)
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetAppointmentsMissedFilter(appointmentmissed).Select(x => new AppointmentsModel(x.Id, x.ClientId, x.VisitsId, x.FirstName, x.LastName, x.MiddleName, x.PriorAppointmentDate, x.AppointmentDate, x.AppointmentTime, x.NextAppointmentDate, x.Age)).ToList();
            foreach (var user in userAppointement)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userAppointement == null)
                return null;
            return (userAppointement);
        }  
        public List<AppointmentsModel> getMissedVisitsDetails()
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetMissedVisitsDetails().ToList();
            foreach (var user in userAppointement)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userAppointement == null)
                return null;
            return (userAppointement);
        } 
        public List<AppointmentsModel> getVisitsMissedFilter(VisitsModel filtedata)
        {
            List<AppointmentsModel> userAppointement = _visitRepository.GetVisitsMissedFilter(filtedata).ToList();
            foreach (var user in userAppointement)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userAppointement == null)
                return null;
            return (userAppointement);
        } 
        public List<AppointmentsModel> getClientDetails()
        {
            List<AppointmentsModel> userClient = _visitRepository.GetClientDetails().ToList();
          
            if (userClient == null)
                return null;
            return (userClient);
        }
        public List<ClientModel> getClientDetails(string searchTerm)
        {
            List<ClientModel> userClient = _visitRepository.GetClientDetails(searchTerm).ToList();
          
            if (userClient == null)
                return null;
            return (userClient);
        }
        public List<ClientModel> getClients () {

            List<ClientModel> clients = _visitRepository.GetClients().ToList();
            if (clients == null)
                return null;
            return(clients);
            
        }

        public List<ClientModel> GetClientsByFacility(long facilityId)
        {
            List<ClientModel> clients = _visitRepository.GetClientsByFacility(facilityId).ToList();

            if (clients == null)
                return null;
            return (clients);
        }

        public List<AppointmentsModel> GetAppointments()
        {
            List<AppointmentsModel> appointments = _visitRepository.GetAppointments().ToList();
            if (appointments == null)
                return null;
            return (appointments);
        }

        public List<AppointmentsModel> getClientDetailsFilters(VisitsModel data)
        {
            List<AppointmentsModel> userClient = _visitRepository.GetClientDetailsFilters(data).ToList();
          
            if (userClient == null)
                return null;
            return (userClient);
        }
        public List<AppointmentsModel> getActiveClientFilter(VisitsModel data)
        {
            List<AppointmentsModel> userClient = _visitRepository.GetActiveClientFilter(data).ToList();
          
            if (userClient == null)
                return null;
            return (userClient);
        }   
        public List<AppointmentsModel> getUpcommingAppointment(VisitsModel data)
        {
            List<AppointmentsModel> userClient = _visitRepository.GetUpcommingAppointment(data).ToList();
          
            if (userClient == null)
                return null;
            return (userClient);
        } 
        public List<AppointmentsModel> getClientVisitPastDetails()
        {
            List<AppointmentsModel> userClientvisitpast = _visitRepository.GetClientVisitPastDetails().ToList();
          
            if (userClientvisitpast == null)
                return null;
            return (userClientvisitpast);
        }
        public List<AppointmentsModel> viewDetails(long id)
        {
            List<AppointmentsModel> viewDelatils = _visitRepository.ViewDetails(id)
                .Select(x => new AppointmentsModel(x.Id, x.FirstName, x.LastName, x.MiddleName, x.ClientPhoneNo, x.DateOfBirth, x.PriorAppointmentDate, x.NextAppointmentDate, x.VisitDate, x.VisitType,
                x.ReasonOfVisit, x.AdviseNotes)).ToList();

            if (viewDelatils == null)
                return null;
            return (viewDelatils);
        }


        public string smsRecords(long id, bool name)
        {
            List<SMSRecords> SmsRecordsById = new List<SMSRecords>();
            if (name)
            {
                SmsRecordsById = _visitRepository.SmsRecords(id).ToList();
            }
            else
            {
                SmsRecordsById = _visitRepository.SmsRecordsForVisits(id).ToList();
            }
            MessageTemplateModel messageTemplate = new MessageTemplateModel();
            messageTemplate.Language = "English";
            messageTemplate.Type = "Reminder";
            messageTemplate.Status = true;
            var Message = _visitRepository.GetMessage(messageTemplate);
            foreach (var data in SmsRecordsById)
            {
                data.Message = Message.Message;
                data.Message = ConvertMessage(data);
            }
            var result = SendSMSApi(SmsRecordsById).GetAwaiter().GetResult();
            var response = SaveSMSApi(SmsRecordsById, result);
            if (response == null)
                return null;
            return (response);
        }
        public string ConvertMessage(SMSRecords smsRecords)
        {
            var message = smsRecords.Message;
            if (message.Contains("{Facility_Name}"))
            {
                var response = (smsRecords.FacilityName == null || smsRecords.FacilityName == "") ? "" : smsRecords.FacilityName.ToString();
                message = message.Replace("{Facility_Name}", response);
            }
            if (message.Contains("{Date}"))
                message = message.Replace("{Date}", (smsRecords.NextAppointmentDate.Date == null || smsRecords.NextAppointmentDate.Date == DateTime.MinValue) ? smsRecords.AppointmenDateTime.Date.ToString("MM-dd-yyyy") : smsRecords.NextAppointmentDate.Date.ToString("MM-dd-yyyy"));
            if (message.Contains("{Time}"))
                message = message.Replace("{Time}", smsRecords.NextAppointmentDate.ToShortTimeString() ?? smsRecords.AppointmenDateTime.ToShortTimeString());
            if (message.Contains("{Source_Point_Name}"))
            {
                var response = (smsRecords.ServicePointName == null || smsRecords.ServicePointName == "") ? "" : smsRecords.ServicePointName.ToString();
                message = message.Replace("{Source_Point_Name}", response);
            }
            if (message.Contains("{Client_Name}"))
                message = message.Replace("{Client_Name}", smsRecords.FullName.ToString());
            if (message.Contains("{Facility_Contact_Number}"))
            {
                var response = (smsRecords.FacilityContactNumber == null || smsRecords.FacilityContactNumber == "") ? "" : smsRecords.FacilityContactNumber.ToString();
                message = message.Replace("{Facility_Contact_Number}", response);
            }
            return message.ToString();
        }

        public async Task<string> SendSMSApi(List<SMSRecords> SmsRecordsById)
        {
            try
            {
                List<MessageForSMS> MessagesList = new List<MessageForSMS>();
                var resultContent = "";
                string[] emptyStringArray = new string[0];
                for (var i = 0; i < SmsRecordsById.Count(); i++)
                {
                    MessagesList.Add(new MessageForSMS { phone = SmsRecordsById[i].PhoneNumber.ToString(), message = SmsRecordsById[i].Message.ToString() });
                }
                using (var client = new HttpClient())
                {
                    SMSApi sMSApi = new SMSApi();
                    Auth auth = new Auth();
                    auth.username = _iconfigration.GetValue<string>("SMSConfig:UserName");
                    auth.password = _iconfigration.GetValue<string>("SMSConfig:Password");
                    auth.sender_id = _iconfigration.GetValue<string>("SMSConfig:Sender_Id");
                    auth.short_code = _iconfigration.GetValue<string>("SMSConfig:Short_Code");
                    sMSApi.auth = auth;
                    sMSApi.messages = MessagesList.ToList();
                    var json = JsonConvert.SerializeObject(sMSApi, Formatting.Indented);
                    var stringContent = new StringContent(json);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = await client.PostAsync("https://www.smszambia.com/smsservice/jsonapi", stringContent);
                    resultContent = await result.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(resultContent);
                    resultContent = (string)jObject.SelectToken("response_description");
                }
                return resultContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SaveSMSApi(List<SMSRecords> SmsRecordsById, string resultContent)
        {
            try
            {
                var ResposeMessage = resultContent.ToString();
                foreach (var clientId in SmsRecordsById)
                {

                    Repository.Messages message = new Repository.Messages
                    {
                        DateCreated = DateTime.UtcNow,
                        DateEdited = DateTime.UtcNow,
                        FacilityId = clientId.FacilityId,
                        Message = clientId.Message,
                        MessageStatus = ResposeMessage,
                        ServicePointId = clientId.ServicePointId,
                        ClientId = clientId.ClientId,
                        ClientNumber = clientId.PhoneNumber
                    };
                    _messageRepository.CreateMessage(message);
                }

                return resultContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Facility GetFacility(long id)
        {
            return _visitRepository.GetFacilityById(id);
            //List<FacilityModel> facilityDetails = _visitRepository.GetFacility().Select(x => new FacilityModel(x.FacilityId, x.FacilityName)).ToList();

            //if (facilityDetails == null)
            //    return null;
            //return (facilityDetails);
        }
        public List<DistrictModel> getDistrict(long id)
        {
            List<DistrictModel> districtDetails = _visitRepository.GetDistrict(id).Select(x => new DistrictModel(x.DistrictId, x.DistrictName, x.ProvinceId)).ToList();

            if (districtDetails == null)
                return null;
            return (districtDetails);
        }
        public List<ServicePointModel> GetServicePoint(long id)
        {
            List<ServicePointModel> servicePointDetails = _visitRepository.GetServicePoint(id).Select(x => new ServicePointModel(x.ServicePointId, x.ServicePointName)).ToList();
            if (servicePointDetails == null)
                return null;
            return (servicePointDetails);
        }
        public List<ProvinceModel> GetProvince()
        {
            List<ProvinceModel> provinceDetails = _visitRepository.GetProvince().Select(x => new ProvinceModel(x.ProvinceId, x.ProvinceName)).ToList();
            if (provinceDetails == null)
                return null;
            return (provinceDetails);
        }
        public MessageTemplateModel GetMessage(MessageTemplateModel messageTemplateModel)
        {
            MessageTemplate messageTemplatesModel = _visitRepository.GetMessage(messageTemplateModel);
            if (messageTemplatesModel == null)
            {
                return null;
            }
            MessageTemplateModel messageModel = new MessageTemplateModel(messageTemplatesModel.MessageTemplateId, messageTemplatesModel.Message);
            return (messageModel);
        }
        public string SendBulkSMS(List<SMSRecords> SmsRecords, CSVBulkData bulkData)
        {
            var facilityNumber = bulkData.FacilityId == null ? null : _visitRepository.GetFacilityContactNumber(bulkData.FacilityId);
            facilityNumber = facilityNumber == null ? "" : facilityNumber.ToString();
            var message = this.ConvertMessage(bulkData);
            string massageWithName = message;
            foreach (var SmsRecord in SmsRecords)
            {
                massageWithName = message;
                if (message.Contains("{Client_Name}"))
                    massageWithName = massageWithName.Replace("{Client_Name}", SmsRecord.FullName.ToString());
                if (message.Contains("{Facility_Contact_Number}"))
                    massageWithName = massageWithName.Replace("{Facility_Contact_Number}", facilityNumber.ToString());
                SmsRecord.Message = massageWithName;
                SmsRecord.FacilityName = bulkData.Facility;
                SmsRecord.ServicePointName = bulkData.ServicePointName;
                SmsRecord.ServicePointId = bulkData.ServicePointId;
                SmsRecord.FacilityId = bulkData.FacilityId;
                massageWithName = null;
            }
            var result = SendSMSApi(SmsRecords).GetAwaiter().GetResult();
            return SaveBulkSMS(SmsRecords, result);
        }
        private string ConvertMessage(CSVBulkData bulkData)
        {
            var message = bulkData.Message;
            if (message.Contains("{Facility_Name}"))
                message = message.Replace("{Facility_Name}", bulkData.Facility.ToString());
            if (message.Contains("{Date}"))
                message = message.Replace("{Date}", bulkData.AppointmentDate.ToString());
            if (message.Contains("{Time}"))
            {
                string appointment = bulkData.AppointmentTime.ToString() == null ? "" : bulkData.AppointmentTime.ToString();
                message = message.Replace("{Time}", appointment);
            }
            if (message.Contains("{Source_Point_Name}"))
                message = message.Replace("{Source_Point_Name}", bulkData.ServicePointName.ToString());

            return message.ToString();
        }
        private string SaveBulkSMS(List<SMSRecords> SmsRecordsById, string resultContent)
        {
            try
            {
                var ResposeMessage = resultContent.ToString();
                foreach (var clientId in SmsRecordsById)
                {
                    BulkMessages message = new BulkMessages
                    {
                        DateCreated = DateTime.UtcNow,
                        FacilityId = clientId.FacilityId,
                        Message = clientId.Message,
                        MessageStatus = ResposeMessage.ToString(),
                        ServicePointId = clientId.ServicePointId,
                        ClientNumber = clientId.PhoneNumber
                    };
                    _messageRepository.CreateBulkMessage(message);
                }
                return resultContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFacility(FacilityModel facilityModel, string facility)
        {
            List<Appointment> appointments = _visitRepository.GetAppointment(facilityModel.FacilityId, facility).ToList();
            List<BulkMessages> bulkmessages = _visitRepository.GetBulkMessages(facilityModel.FacilityId,facility).ToList();
            List<Messages> messages = _visitRepository.GetMessages(facilityModel.FacilityId, facility).ToList();
            List<ServicePoint> servicepoints = _visitRepository.GetServicePoint(facilityModel.FacilityId).ToList();
            List<UserFacility> userfacilitys = _visitRepository.GetUserFacility(facilityModel.FacilityId, facility).ToList();
            List<Visit> visits = _visitRepository.GetVisit(facilityModel.FacilityId, facility).ToList();
            
            if (appointments != null && appointments.Count > 0)
            {
                foreach (var appointment in appointments)
                {
                    appointment.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateAppointments(appointments);
            }
            if (bulkmessages != null && bulkmessages.Count > 0)
            {
                foreach (var bulkmsg in bulkmessages)
                {
                    bulkmsg.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateBulkMessages(bulkmessages);
            }
            if (messages != null && messages.Count > 0)
            {
                foreach (var msg in messages)
                {
                    msg.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateMessages(messages);
            }
            if (servicepoints != null && servicepoints.Count > 0)
            {
                foreach (var servicepoint in servicepoints)
                {
                    servicepoint.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateServicePoints(servicepoints);
            }
            if (userfacilitys != null && userfacilitys.Count > 0)
            {
                foreach (var userfacility in userfacilitys)
                {
                    userfacility.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateUserFacility(userfacilitys);
            }
            if (visits != null && visits.Count > 0)
            {
                foreach (var visit in visits)
                {
                    visit.FacilityId = facilityModel.AssignedFacilityId;
                }
                _visitRepository.UpdateVisit(visits);
            }

            _visitRepository.DeleteFacility(facilityModel.FacilityId);
        }
        public void DeleteServicePoint(ServicePointModel servicepoint , string type)
        {
            List<Appointment> appointments = _visitRepository.GetAppointment(servicepoint.ServicePointId, type).ToList();
            List<BulkMessages> bulkmessages = _visitRepository.GetBulkMessages(servicepoint.ServicePointId, type).ToList();
            List<Messages> messages = _visitRepository.GetMessages(servicepoint.ServicePointId, type).ToList();
            List<UserFacility> userfacilitys = _visitRepository.GetUserFacility(servicepoint.ServicePointId, type).ToList();
            List<Visit> visits = _visitRepository.GetVisit(servicepoint.ServicePointId, type).ToList();

            /*if (appointments != null && appointments.Count > 0)
            {
                foreach (var appointment in appointments)
                {
                    appointment.ServicePointId = servicepoint.AssignedServicePointId;
                }
                _visitRepository.UpdateAppointments(appointments);
            }*/
            if (bulkmessages != null && bulkmessages.Count > 0)
            {
                foreach (var bulkmsg in bulkmessages)
                {
                    bulkmsg.ServicePointId = servicepoint.AssignedServicePointId;
                }
                _visitRepository.UpdateBulkMessages(bulkmessages);
            }
            if (messages != null && messages.Count > 0)
            {
                foreach (var msg in messages)
                {
                    msg.ServicePointId = servicepoint.AssignedServicePointId;
                    
                }
                _visitRepository.UpdateMessages(messages);
            }          
            if (userfacilitys != null && userfacilitys.Count > 0)
            {
                foreach (var userfacility in userfacilitys)
                {
                    userfacility.ServicePointId = servicepoint.AssignedServicePointId;

                }
                _visitRepository.UpdateUserFacility(userfacilitys);
            }
            if (visits != null && visits.Count > 0)
            {
                foreach (var visit in visits)
                {
                    visit.ServicePointId = servicepoint.AssignedServicePointId;

                }
                _visitRepository.UpdateVisit(visits);
            }

            _visitRepository.DeleteServicePoint(servicepoint.ServicePointId);
            
            }

        public ClientModel GetClient()
        {
            Client user = _visitRepository.ClientData();
            if (user == null)
                return null;
            ClientModel userModel = new ClientModel(user.ClientId, user.FirstName, user.LastName, user.ArtNo);
            return (userModel);
        }

        public List<AppointmentsModel> getVisitHistory()
        {
            List<AppointmentsModel> userVisits = _visitRepository.GetVisitHistory().Select(x => new AppointmentsModel(x.Id, x.FirstName, x.LastName, x.MiddleName, x.ClientPhoneNo, x.DateOfBirth, x.PriorAppointmentDate, x.NextAppointmentDate, x.VisitDate, x.VisitType,
               x.ReasonOfVisit, x.AdviseNotes)).ToList();           
            foreach (var user in userVisits)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userVisits == null)
                return null;
            return (userVisits);
        }

        public List<AppointmentsModel> getVisitHistoryByServicePoint(VisitsModel data)
        {
            List<AppointmentsModel> userVisits = _visitRepository.GetVisitHistoryByService(data)
                .Select(x => new AppointmentsModel(x.Id, x.FirstName, x.LastName, x.MiddleName, x.ClientPhoneNo, x.DateOfBirth, x.PriorAppointmentDate, x.NextAppointmentDate, x.VisitDate, x.VisitType,
               x.ReasonOfVisit, x.AdviseNotes)).ToList();
            foreach (var user in userVisits)
            {
                user.NextAppointmentDate = user.NextAppointmentDate == DateTime.MinValue ? null : user.NextAppointmentDate;
                user.PriorAppointmentDate = user.PriorAppointmentDate == DateTime.MinValue ? null : user.PriorAppointmentDate;

            }
            if (userVisits == null)
                return null;
            return (userVisits);
        }

        public Client GetClientById(long id)
        {
            return _visitRepository.GetClientById(id);
        }

       

        public List<FacilityModel> GetFacilities()
        {
            List<FacilityModel> facilities = _visitRepository.GetFacilities().ToList();
            if (facilities == null)
                return null;
            return (facilities);
        }
        public List<VisitsServiceModel> GetServiceTypes()
        {
            List<VisitsServiceModel> serviceTypes = _visitRepository.GetServiceTypes().ToList();
            if (serviceTypes == null)
                return null;
            return (serviceTypes);
        }

        public List<ServicePointModel> GetServicePoints()
        {
            List<ServicePointModel> servicePoints = _visitRepository.GetServicePoints().ToList();
            if (servicePoints == null)
                return null;
            return (servicePoints);
        }

        public List<VisitModel> GetVisits()
        {
            List<VisitModel> visits = _visitRepository.GetVisits().ToList();
            if (visits == null)
                return null;
            return (visits);
        }

        public Appointment GetAppointmentById(long id)
        {
            return _visitRepository.GetAppointmentById(id);
        }

        public List<FacilityTypeModel> GetFacilityTypes()
        {
            List<FacilityTypeModel> facilityTypes = _visitRepository.GetFacilityTypes().ToList();

            if (facilityTypes == null)
                return null;
            return (facilityTypes);
        }
       

        public string AddVisit(VisitModel model)
        {
            Visit visitData = new Visit(model.VisitId, model.ClientId, model.AppointmentId, model.FacilityId, model.ServiceTypeId, model.VisitDate, model.ReasonOfVisit, model.ClinicRemarks, model.Diagnosis, model.SecondDiagnosis, model.ThirdDiagnosis, model.Therapy, model.ClientPhoneNo, model.PriorAppointmentDate, model.NextAppointmentDate, model.DateOfBirth, model.VisitType, model.FirstName, model.LastName, model.AppointmentStatus, model.DateCreated, model.DateEdited, model.Age);
            string result = this._visitRepository.CreateVisit(visitData);

            return result;
        }

        public Visit GetVisitById(long id)
        {
            return _visitRepository.GetVisitById(id);
        }

        public int CountFacilities(long facilityId)
        {
            return _visitRepository.CountFacilities(facilityId);
        }

        public int CountClients()
        {
            return _visitRepository.CountClients();
        }

        public int CountAppointments()
        {
            return _visitRepository.CountAppointments();
        }

        public int AvailableFacilities()
        {
            return _visitRepository.AvailableFacilities();
        }

        public int TodaysAppointments()
        {
            return _visitRepository.TodaysAppointments();
        }

        public int TodaysClients()
        {
            return _visitRepository.TodaysClients();
        }

        public List<AppointmentsModel> GetAppointmentsByFacility(long facilityId)
        {
            List<AppointmentsModel> appointments = _visitRepository.GetAppointmentsByFacility(facilityId).ToList();
            if (appointments == null)
                return null;
            return (appointments);
        }

        public int CountClientsInFacility(long facilityId)
        {
            return _visitRepository.CountClientsInFacility(facilityId);
        }

        public int CountAppointmentsInFacility(long facilityId)
        {
            return _visitRepository.CountAppointmentsInFacility(facilityId);
        }

        public List<FacilityModel> GetFacilities(long facilityId)
        {
            List<FacilityModel> facilities = _visitRepository.GetFacilities(facilityId).ToList();
            if (facilities == null)
                return null;
            return (facilities);
        }

        public List<FacilityModel> GetFacilitiesInDistrict(long id)
        {
            List<FacilityModel> facilities = _visitRepository.GetFacilitiesInDistrict(id).ToList();

            if (facilities == null)
                return null;
            return (facilities);
            
        }

        public List<LanguageModel> GetLanguages()
        {
            List<LanguageModel> languages = _visitRepository.GetLanguages().ToList();

            if (languages == null)
                return null;
            return (languages);
        }

        public int CountFacilities()
        {
            return _visitRepository.CountFacilities();
        }
    }
}
