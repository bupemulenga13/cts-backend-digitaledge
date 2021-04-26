using DigitalEdge.Repository.Enitity;
using System.Linq;

namespace DigitalEdge.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(DigitalEdgeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            if (context.Sexes.Any())
            {
                return;   // DB has been seeded
            }
            if (context.ClientTypes.Any())
            {
                return;   // DB has been seeded
            }
            if (context.ClientStatuses.Any())
            {
                return;   // DB has been seeded
            }
            if (context.StatusComments.Any())
            {
                return;   // DB has been seeded
            }       
            if (context.FacilityTypes.Any())
            {
                return;   // DB has been seeded
            }
            if (context.ServiceTypes.Any())
            {
                return;   // DB has been seeded
            }


            //User Roles
            var roles = new UserRoles[]
            {
                new UserRoles{RoleName = "Administrator",Description="Admin",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Health Analyst",Description="Analyst",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Facility Staff",Description="Staff",IsDeleted=false,CreatedBy=1,ModifiedBy=0},                              
                new UserRoles{RoleName = "Supervisor",Description="Supervisor",IsDeleted=false,CreatedBy=1,ModifiedBy=0},                              
            };

            context.UserRoles.AddRange(roles);
            context.SaveChanges();

            //Application Users
            var users = new Users[]
            {
                new Users{FirstName = "Admin",Password="Admin",Email="admin@admin.com",IsSuperAdmin=true,RoleId=1},
                new Users{FirstName = "Analyst",Password="Pass@analyst1",Email="cts@analyst.com",IsSuperAdmin=false,RoleId=2},
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            //Client Sex
            var sex = new Sex[]
            {
                new Sex{SexName = "Male"},
                new Sex{SexName = "Female"},

            };
            context.Sexes.AddRange(sex);
            context.SaveChanges();

            //Client Type
            var clientType = new ClientType[]
            {
                new ClientType{ClientTypeName = "Old"},
                new ClientType{ClientTypeName = "New"},

            };
            context.ClientTypes.AddRange(clientType);
            context.SaveChanges();

            //Client Status
            var clientStatus = new ClientStatus[]
            {
                new ClientStatus{ClientStatusName = "Inactive"},
                new ClientStatus{ClientStatusName = "Active"},

            };
            context.ClientStatuses.AddRange(clientStatus);
            context.SaveChanges();

            //Facility Type
            var facilityType = new FacilityType[]
            {
                new FacilityType{FacilityTypeName = "Urban Health Centre"},
                new FacilityType{FacilityTypeName = "First Level Hospital"},
                new FacilityType{FacilityTypeName = "Health Post"}
            };
            context.FacilityTypes.AddRange(facilityType);
            context.SaveChanges();

            //Client Status Comment
            var statusComment = new StatusComments[]
            {
                new StatusComments{StatusCommentName = "Trans-in", ClientStatusId = 1},
                new StatusComments{StatusCommentName = "Local", ClientStatusId = 1},
                new StatusComments{StatusCommentName = "Deceased", ClientStatusId = 2},
                new StatusComments{StatusCommentName = "Declared inactive by facility", ClientStatusId = 2},
                new StatusComments{StatusCommentName = "Trans-out", ClientStatusId = 2},
                new StatusComments{StatusCommentName = "Withdrawn from treatment", ClientStatusId = 2},
            };
            context.StatusComments.AddRange(statusComment);
            context.SaveChanges();

            //Service Types

            var serviceType = new VisitServices[]
            {
                new VisitServices{ServiceTypeName = "Clinical"},
                new VisitServices{ServiceTypeName = "Pharmacy"},
                new VisitServices{ServiceTypeName = "Lab CD4"},
                new VisitServices{ServiceTypeName = "Lab Viral Load"},
                new VisitServices{ServiceTypeName = "Cervical Cancer Screening"},
                new VisitServices{ServiceTypeName = "Cervical Cancer Treatment"},
                new VisitServices{ServiceTypeName = "PREP"},
                new VisitServices{ServiceTypeName = "Referral"},
                new VisitServices{ServiceTypeName = "Other"},
            };
            context.ServiceTypes.AddRange(serviceType);
            context.SaveChanges();



        }
    }
}