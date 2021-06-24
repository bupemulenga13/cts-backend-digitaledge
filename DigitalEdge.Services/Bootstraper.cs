using DigitalEdge.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DigitalEdge.Service;

namespace DigitalEdge.Services
{
    public static class Bootstraper
    {
        public static void InitializeRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DigitalEdgeContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DigitalEdgeConnection")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IBaseRepository<Users>, BaseRepository<Users>>();
            services.AddScoped<IBaseRepository<UserRoles>, BaseRepository<UserRoles>>();
            services.AddScoped<IBaseRepository<MessageTemplate>, BaseRepository<MessageTemplate>>();
            services.AddScoped<IBaseRepository<Messages>, BaseRepository<Messages>>();
            services.AddScoped<IBaseRepository<Facility>, BaseRepository<Facility>>();
            services.AddScoped<IBaseRepository<ServicePoint>, BaseRepository<ServicePoint>>();
            services.AddScoped<IBaseRepository<BulkMessages>, BaseRepository<BulkMessages>>();
            services.AddScoped<IBaseRepository<UserFacility>, BaseRepository<UserFacility>>();
            services.AddScoped<IBaseRepository<Appointment>, BaseRepository<Appointment>>();
            services.AddScoped<IBaseRepository<Province>, BaseRepository<Province>>();
            services.AddScoped<IBaseRepository<District>, BaseRepository<District>>();
            services.AddScoped<IBaseRepository<Visit>, BaseRepository<Visit>>();
            services.AddScoped<IBaseRepository<Client>, BaseRepository<Client>>();
            services.AddScoped<IBaseRepository<ViralLoad>, BaseRepository<ViralLoad>>();
        }
        public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            InitializeRepository(services, configuration);
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<ISMSSchedulerService, SMSSchedulerService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
