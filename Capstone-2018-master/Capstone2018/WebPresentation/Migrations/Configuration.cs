namespace WebPresentation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    //we added the below using statements
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebPresentation.Models.ApplicationDbContext";
        }

        /// <summary>
        /// 2018-5-1
        /// Sam Dramstad
        /// 
        /// Added all roles.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(WebPresentation.Models.ApplicationDbContext context)
        {
            //these lines will get us a user manager to create accounts
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Customer" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Employee" });

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Applicant" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Delivery" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Equipment Scheduler" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Foreman" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Inspector" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Job Scheduler" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Labor Scheduler" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Maintenance" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Mechanic" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Manager" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Prep" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Supply Clerk" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Temp" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Worker" });

            context.SaveChanges();



            // default admin and password account
            const string admin = "admin@company.com";
            const string defaultPassword = "P@ssw0rd";

            //create an admin user if there isn't one
            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    FirstName = "Default",
                    LastName = "Admin",
                    Email = admin
                };

                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges(); //save before trying to use this user

                if (result.Succeeded)
                {
                    //add roles to the admin user
                    userManager.AddToRole(user.Id, "Admin");

                    context.SaveChanges();
                }
            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
