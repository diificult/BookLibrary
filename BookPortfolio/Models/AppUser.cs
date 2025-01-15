using Microsoft.AspNetCore.Identity;

namespace BookPortfolio.Models
{
    public class AppUser : IdentityUser
    {
    
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
