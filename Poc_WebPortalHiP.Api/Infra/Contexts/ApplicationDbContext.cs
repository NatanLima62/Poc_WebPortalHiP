using Microsoft.EntityFrameworkCore;

namespace Poc_WebPortalHiP.Api.Infra.Contexts;

public sealed class ApplicationDbContext : BaseApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}