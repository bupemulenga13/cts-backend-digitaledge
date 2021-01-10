using System.Linq;

namespace DigitalEdge.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(DigitalEdgeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            var roles = new UserRoles[]
            {
                new UserRoles{RoleName = "Admin",Description="Admin",IsDeleted=false,CreatedBy=1,ModifiedBy=0},
            };

            context.UserRoles.AddRange(roles);
            context.SaveChanges();

            var users = new Users[]
            {
                new Users{FirstName = "Admin",Password="Admin",Email="admin@admin.com",IsSuperAdmin=true,RoleId=1},
            };

            context.Users.AddRange(users);
            context.SaveChanges();

        }
    }
}
