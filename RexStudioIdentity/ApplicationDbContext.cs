using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudioIdentity
{
    /// <summary>
    /// Envuelve todos los objetos fuertemente tipados que nos permitiran personalizar la informacion de los usuarios , el mapa de  tablas de la base de datos 
    /// y las entidades relacionadas.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<Models.ApplicationUser>
    {
        /// <summary>
        /// Constructor de la entidad ApplicationDbContext
        /// </summary>
        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }

        /// <summary>
        /// Metodo estatico para llamar desde nuestra clase Owin Startup
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        /// <summary>
        /// Sobreescribimos la propiedad que mapea el nombre de las tablas y las relaciones entre las diferentes identidades.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.Id).HasColumnName("Id").HasMaxLength(36);
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.UserName).HasColumnName("Usuario").HasMaxLength(50);
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.PhoneNumber).HasColumnName("Telefono").HasMaxLength(50);
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmado");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.Email).HasColumnName("Email").HasMaxLength(100);
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.EmailConfirmed).HasColumnName("EmailConfirmado");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.TwoFactorEnabled).HasColumnName("DobleAutenticacion");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.AccessFailedCount).HasColumnName("TotalIntentosFallidos");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.LockoutEnabled).HasColumnName("BloqueoPermitido");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.LockoutEndDateUtc).HasColumnName("UltimoBloqueo");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.SecurityStamp).HasColumnName("TokenSeguridad");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.FirstName).HasColumnName("Nombres");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.LastName).HasColumnName("Apellidos");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.Level).HasColumnName("Nivel");
            modelBuilder.Entity<Models.ApplicationUser>().ToTable("Usuarios", "dbo").Property(p => p.JoinDate).HasColumnName("FechaRegistro");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("Id").HasMaxLength(36);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo").Property(p => p.Name).HasColumnName("Nombre").HasMaxLength(50);
            modelBuilder.Entity<Models.ApplicationRole>().ToTable("Roles", "dbo").Property(p => p.Description).HasColumnName("Descripcion").HasMaxLength(200);

            modelBuilder.Entity<IdentityUserRole>().ToTable("UsuariosRoles", "dbo").Property(p => p.RoleId).HasColumnName("RolId").HasMaxLength(36);
            modelBuilder.Entity<IdentityUserRole>().ToTable("UsuariosRoles", "dbo").Property(p => p.UserId).HasColumnName("UsuarioId").HasMaxLength(35);

            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuariosLogines", "dbo").Property(p => p.UserId).HasColumnName("UsuarioId").HasMaxLength(36);
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuariosLogines", "dbo").Property(p => p.LoginProvider).HasColumnName("Proveedor");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuariosLogines", "dbo").Property(p => p.ProviderKey).HasColumnName("ProveedorToken");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuariosNotificaciones", "dbo").Property(p => p.ClaimType).HasColumnName("Tipo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuariosNotificaciones", "dbo").Property(p => p.UserId).HasColumnName("UsuarioId").HasMaxLength(36);
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuariosNotificaciones", "dbo").Property(p => p.ClaimValue).HasColumnName("Valor");
        }
    }
}
