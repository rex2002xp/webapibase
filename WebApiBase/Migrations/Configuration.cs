namespace WebApiBase.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using WebApiBase.Models;
    using WebApiBase.Models.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiBase.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApiBase.Models.ApplicationDbContext context)
        {
            /* Roles */
            var rolesManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));

            var roleSuperAdmin = rolesManager.RoleExists("SuperAdmin");
            if (!roleSuperAdmin)
            {
                rolesManager.Create(new ApplicationRole()
                {
                    Name = "SuperAdmin",
                    Description = "Administradores de la Aplicacion con el nivel mas alto de acceso."
                });
            }

            var roleAdmin = rolesManager.RoleExists("Admin");
            if (!roleAdmin)
            {
                rolesManager.Create(new ApplicationRole()
                {
                    Name = "Admin",
                    Description = "Administradores de la Aplicacion."
                });
            }

            var roleRegisted = rolesManager.RoleExists("Registrado");
            if (!roleRegisted)
            {
                rolesManager.Create(new ApplicationRole()
                {
                    Name = "Registrado",
                    Description = "Usuario registrado, como el acceso tipico."
                });
            };

            /* Usuarios iniciales */
            var usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var userSuperAdmin = usersManager.FindByName("superadmin");
            if (userSuperAdmin == null)
            {
                userSuperAdmin = new ApplicationUser()
                {
                    UserName = "superadmin",
                    Email = "superadmin@dominio.local",
                    EmailConfirmed = true,
                    FirstName = "Super Administrador",
                    LastName = "Aplicacion",
                    Level = 1,
                    JoinDate = DateTime.Now.AddYears(-3),
                    PhoneNumber = "(999) 999-9999",
                    PhoneNumberConfirmed = true
                };
                usersManager.Create(userSuperAdmin, "Demo123$!");
            }

            var userAdmin = usersManager.FindByName("admin");
            if (userAdmin == null)
            {
                userAdmin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@dominio.local",
                    EmailConfirmed = true,
                    FirstName = "Administrador",
                    LastName = "Aplicacion",
                    Level = 1,
                    JoinDate = DateTime.Now.AddYears(-3),
                    PhoneNumber = "(999) 999-9999",
                    PhoneNumberConfirmed = true
                };
                usersManager.Create(userAdmin, "Demo123$!");
            }

            var userRegisted = usersManager.FindByName("user");
            if (userRegisted == null)
            {
                userRegisted = new ApplicationUser()
                {
                    UserName = "user",
                    Email = "user@dominio.local",
                    EmailConfirmed = true,
                    FirstName = "Usuario",
                    LastName = "Aplicacion",
                    Level = 1,
                    JoinDate = DateTime.Now.AddYears(-3),
                    PhoneNumber = "(999) 999-9999",
                    PhoneNumberConfirmed = true
                };
                usersManager.Create(userRegisted, "Demo123$!");
            }
            context.SaveChanges();

            usersManager.AddToRoles(userSuperAdmin.Id, new string[] { "SuperAdmin", "Admin" });
            usersManager.AddToRoles(userAdmin.Id, new string[] { "Admin" });
            usersManager.AddToRoles(userRegisted.Id, new string[] { "Registrado" });
        }
    }
}
