using DigitalEdge.Repository.Enitity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalEdge.Repository
{
    public class DigitalEdgeContext : IdentityDbContext
    {
        public DigitalEdgeContext(DbContextOptions<DigitalEdgeContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<ServicePoint> ServicePoints { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientIdentity> ClientIdentitys { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<BulkMessages> BulkMessages { get; set; }
        public DbSet<UserFacility> Userfacility { get; set; }
        public DbSet<ClientStatus> ClientStatuses { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<StatusComments> StatusComments { get; set; }
        public DbSet<VisitServices> ServiceTypes { get; set; }








        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Public methods
        public override int SaveChanges()
        {
            return base.SaveChanges(true);
        }
        #endregion
    }
}