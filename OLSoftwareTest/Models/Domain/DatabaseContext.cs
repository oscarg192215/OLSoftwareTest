using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OLSoftwareTest.Models.DTO;
using OLSoftwareTest.Models;

namespace OLSoftwareTest.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<OLSoftwareTest.Models.DTO.LenguajesProgramacion>? LenguajeProgramacion { get; set; }
        public DbSet<OLSoftwareTest.Models.DTO.TiposPruebas>? TiposPruebas { get; set; }
        public DbSet<OLSoftwareTest.Models.DTO.NivelesConocimiento>? NivelesConocimiento { get; set; }
        public DbSet<OLSoftwareTest.Models.DTO.EstadosPruebaAspirante>? EstadosPruebaAspirante { get; set; }
        public DbSet<OLSoftwareTest.Models.PruebasViewModel>? PruebasViewModel { get; set; }
        public DbSet<OLSoftwareTest.Models.DTO.Preguntas>? Preguntas { get; set; }
        public DbSet<OLSoftwareTest.Models.PreguntasViewModel>? PreguntasViewModel { get; set; }
        public DbSet<OLSoftwareTest.Models.AspirantesViewModel>? AspirantesViewModel { get; set; }
        public DbSet<OLSoftwareTest.Models.DTO.Aspirantes>? Aspirantes { get; set; }
       


    }
}
