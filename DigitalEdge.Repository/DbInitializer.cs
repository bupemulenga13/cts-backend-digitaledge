using System;
using DigitalEdge.Repository.Enitity;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using CsvHelper;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            /*if (!context.FacilityTypes.Any())
            {
                var facilityTypes = new List<FacilityType>();
                using (StreamReader reader = new StreamReader(@"C:\Users\bupej\facilitytypes.json"))
                {
                    string json = reader.ReadToEnd();
                    facilityTypes = JsonConvert.DeserializeObject<List<FacilityType>>(json);
                }

                foreach (var facilityType in facilityTypes)
                    context.FacilityTypes.AddRange(facilityType);
                context.SaveChanges();
            }*/
            if (context.ServiceTypes.Any())
            {
                return;   // DB has been seeded
            }
            
            if (!context.Languages.Any())
            {
                var languages = new List<Language>();
                using (StreamReader reader = new StreamReader(@"C:\Users\bupej\languages.json"))
                {
                    string json = reader.ReadToEnd();
                    languages = JsonConvert.DeserializeObject<List<Language>>(json);
                }

                foreach (var language in languages)
                    context.Languages.AddRange(language);
                context.SaveChanges();
            }

            if (!context.ServicePoints.Any())
            {
                var servicePoints = new List<ServicePoint>();
                using (StreamReader reader = new StreamReader(@"C:\Users\bupej\servicepoints.json"))
                {
                    string json = reader.ReadToEnd();
                    servicePoints = JsonConvert.DeserializeObject<List<ServicePoint>>(json);
                }

                foreach (var servicePoint in servicePoints)
                    context.ServicePoints.AddRange(servicePoint);
                context.SaveChanges();
            }
           


            //User Roles
            var roles = new UserRoles[]
            {
                
                new UserRoles{RoleName = "National Admin",Description="National Administrator",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Provincial Admin",Description="Provincial Administrator",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "District Admin",Description="District Administrator",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Facility Admin",Description="Facility Adminstrator",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Health Analyst",Description="Facility Analyst",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
                new UserRoles{RoleName = "Facility Staff",Description="Facility Staff",IsDeleted=false,CreatedBy=1,ModifiedBy=0},                              
                new UserRoles{RoleName = "Supervisor",Description="Facility Supervisor",IsDeleted=false,CreatedBy=1,ModifiedBy=0},                              
                new UserRoles{RoleName = "Technical Administrator",Description="Technical Administrator",IsDeleted=false,CreatedBy=1,ModifiedBy=0},                              
            };

            context.UserRoles.AddRange(roles);
            context.SaveChanges();

            //Application Users
            var users = new Users[]
            {
                new Users{FirstName = "National",LastName="Administrator", Password="NationalAdmin@cts21",Email="admin@national.cts",IsSuperAdmin=true,RoleId=1, DateCreated= DateTime.Now, CreatedBy = 1},
                new Users{FirstName = "Facility",LastName="Administrator", Password="FacilityAdmin@cts21",Email="admin@facility.cts",IsSuperAdmin=false,RoleId=4, DateCreated= DateTime.Now,CreatedBy = 1},
                new Users{FirstName = "Technical",LastName="Administrator", Password="TechAdmin@cts11",Email="bupe@digiprophets.com",IsSuperAdmin=true,RoleId=8, DateCreated= DateTime.Now, CreatedBy = 1},
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
                new FacilityType{FacilityTypeName = "First Level Hospital"},
                new FacilityType{FacilityTypeName = "Second Level Hospital"},
                new FacilityType{FacilityTypeName = "Third Level Hospital"},
                new FacilityType{FacilityTypeName = "Health Post (Rural)"},
                new FacilityType{FacilityTypeName = "Health Post (Urban)"},
                new FacilityType{FacilityTypeName = "Rural Health Center"},
                new FacilityType{FacilityTypeName = "Urban Health Center"}
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

            var provinces = new Province[]
            {
                new Province{ProvinceName ="Western"},
                new Province{ProvinceName ="Southern"},
                new Province{ProvinceName ="Northern"},
                new Province{ProvinceName ="North-Western"},
                new Province{ProvinceName ="Muchinga"},
                new Province{ProvinceName ="Lusaka"},
                new Province{ProvinceName ="Luapula"},
                new Province{ProvinceName ="Eastern"},
                new Province{ProvinceName ="Copperbelt"},
                new Province{ProvinceName ="Central"}
            };
            context.Provinces.AddRange(provinces);
            context.SaveChanges();




        }
    }
}