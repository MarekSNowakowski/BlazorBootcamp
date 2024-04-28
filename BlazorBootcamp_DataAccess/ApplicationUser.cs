using Microsoft.AspNetCore.Identity;

namespace BlazorBootcamp_DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
