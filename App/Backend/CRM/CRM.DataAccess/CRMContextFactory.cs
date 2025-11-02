using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.DataAccess;

public class CRMContextFactory :  IDesignTimeDbContextFactory<CRMContext>
{
    public CRMContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CRMContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=School_CRM_DB;Username=postgres;Password=postgres");

        return new CRMContext(optionsBuilder.Options);
    }
}