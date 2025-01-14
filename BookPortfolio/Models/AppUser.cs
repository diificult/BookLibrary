using Microsoft.AspNetCore.Identity;

namespace BookPortfolio.Models
{
    public class AppUser : IdentityUser
    {
    
    public List<Porfolio> Porfolios { get; set; } = new List<Porfolio>();
    }
}
