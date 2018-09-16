using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebPresentation.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? employeeID { get; set; }
        public int? customerID { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("crlandscapingUsers", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DataObjects.ServicePackage> ServicePackages { get; set; }

        public System.Data.Entity.DbSet<DataObjects.ServiceOffering> ServiceOfferings { get; set; }

        public System.Data.Entity.DbSet<DataObjects.ServiceItem> ServiceItems { get; set; }
		



		//public System.Data.Entity.DbSet<WebPresentation.Models.ApplicationUser> ApplicationUsers { get; set; }
	}
}