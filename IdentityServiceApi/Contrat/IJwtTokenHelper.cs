using IdentityServiceApi.Entities;

namespace IdentityServiceApi.Contrat
{
    public interface IJwtTokenHelper
    {
        string GeneratToken(ApplicationUser applicationUser);
    }
}
