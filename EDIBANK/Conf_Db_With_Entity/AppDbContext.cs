using EDIBANK.Models.Monitor;
using EDIBANK.Models.Users_EdiWeb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* This class "AppDbContext" extends "Entity Framework" represents a session with the database
// and can be used to our custom in the context so that in the migration the Framework user knows to make changes to the schema.*/

namespace EDIBANK.Conf_Db_With_Entity;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    // Using dbcontext to create roles
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        IdentityRole admin = new("Admin")
        {
            NormalizedName = "Admin"
        };

        IdentityRole user = new("User")
        {
            NormalizedName = "User"
        };

        modelBuilder.Entity<IdentityRole>().HasData(admin, user);
        modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");
        modelBuilder
            .Entity<Intercambio>()
            .ToTable(static void (TableBuilder<Intercambio> b) =>
            {
                b.HasCheckConstraint("CK_tip_intercambio", "[tip_intercambio] IN (0, 1, 2)");
                b.HasCheckConstraint("CK_status", "[status] IN (0, 1, 2)");
            });
        modelBuilder
            .Entity<HistoricoIntercambio>()
            .ToTable(static void (TableBuilder<HistoricoIntercambio> b) =>
            {
                b.HasCheckConstraint("CK_tip_intercambio", "[tip_intercambio] IN (0, 1, 2)");
            });
    }

    public async Task<SelectList> EDISelectorAsync(string? selectedValue = null) => new(
        items: await (from e in EDIs
                      orderby e.Descripcion
                      select new { e.Id, e.Descripcion }).ToListAsync(),
        dataValueField: nameof(EDI.Id),
        dataTextField: nameof(EDI.Descripcion),
        selectedValue: selectedValue);

    public required DbSet<Intercambio> Intercambios { get; set; }
    public required DbSet<HistoricoIntercambio> HistoricoIntercambios { get; set; }
    public required DbSet<EDI> EDIs { get; set; }
    public required DbSet<Auditoria> Auditorias { get; set; }
}