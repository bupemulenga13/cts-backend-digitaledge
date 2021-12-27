using System;
using System.Collections.Generic;
using DigitalEdge.Domain;
using DigitalEdge.Repository;

namespace DigitalEdge.Services
{
    public class SMSSchedulerService: ISMSSchedulerService
    {

        private readonly IVisitRepository _visitRepository;
        private readonly IVisitService _visitService;
        public SMSSchedulerService(IVisitRepository visitRepository,IVisitService visitService)
        {
            this._visitRepository = visitRepository;
            _visitService = visitService;
        }
        public List<SMSRecords> ClientsList()
        {
            var Records = _visitRepository.GetAppointementsDetailsForSMS();
            MessageTemplateModel messageTemplate = new MessageTemplateModel();
            messageTemplate.Language = "English";
            messageTemplate.Type = "Reminder";
            messageTemplate.Status = true;
            var Message = _visitRepository.GetMessage(messageTemplate);
            var SevenDaysBeforeAppointment = new List<SMSRecords>();
            var ThreeDaysBeforeAppointment = new List<SMSRecords>();
            var OneDayBeforeAppointment = new List<SMSRecords>();
            var AppointmentDay = new List<SMSRecords>();
            

            foreach (var data in Records)
            {
                data.Message = Message.Message;
                data.Message = _visitService.ConvertMessage(data);
                if (data.NextAppointmentDate.Date != DateTime.MinValue)
                {
                    if (data.NextAppointmentDate.Date == DateTime.UtcNow.Date.AddDays(7))
                    {
                        SevenDaysBeforeAppointment.Add(data);
                        continue;
                    }
                       
                    if (data.NextAppointmentDate.Date == DateTime.UtcNow.Date.AddDays(3))
                    {
                        ThreeDaysBeforeAppointment.Add(data);
                        continue;
                    }
                    
                    if (data.NextAppointmentDate.Date == DateTime.UtcNow.Date.AddDays(1))
                    {
                        OneDayBeforeAppointment.Add(data);
                        continue;
                    }
                   
                    if (data.NextAppointmentDate.Date == DateTime.UtcNow.Date)
                        AppointmentDay.Add(data);
                    
                }
                if ((data.NextAppointmentDate.Date == DateTime.MinValue))
                {
                    if (data.AppointmenDateTime.Date == DateTime.UtcNow.Date.AddDays(7))
                    {
                        SevenDaysBeforeAppointment.Add(data);
                        continue;
                    }
                    if (data.AppointmenDateTime.Date == DateTime.UtcNow.Date.AddDays(3))
                    {
                        ThreeDaysBeforeAppointment.Add(data);
                        continue;
                    }
                    if (data.AppointmenDateTime.Date == DateTime.UtcNow.Date.AddDays(1))
                    {
                        OneDayBeforeAppointment.Add(data);
                        continue;
                    }
                    if (data.AppointmenDateTime.Date == DateTime.UtcNow.Date)
                        AppointmentDay.Add(data);
                }

            }
            List<List<SMSRecords>> AllAppointmentList = new List<List<SMSRecords>>();
            AllAppointmentList.Add(SevenDaysBeforeAppointment);
            AllAppointmentList.Add(ThreeDaysBeforeAppointment);
            AllAppointmentList.Add(OneDayBeforeAppointment);
            AllAppointmentList.Add(AppointmentDay);
            foreach (var data in AllAppointmentList)
            {
                if (data.Count > 0) {
                   var result= _visitService.SendSMSApi(data).GetAwaiter().GetResult();
                    _visitService.SaveSMSApi(data, result);
                }
            }

            return Records;
        }
        
    }
}







