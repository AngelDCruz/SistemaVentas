using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura
{
    public partial class AppDbContext
    {

        public override int SaveChanges()
        {
            AuditarDatos();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {

            AuditarDatos();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public void AuditarDatos()
        {

            if (Autenticado())
            {

                Guid id = UsuarioAutenticado();

                CrearDatos(id);
                ActualizarDatos(id);
                EliminarDatos(id);

            }

        }

        private void CrearDatos(Guid usuarioAutenticado)
        {

            foreach(var valor in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added &&  x.Entity is IAuditoria))
            {

                var entidad = valor.Entity as IAuditoria;

                entidad.UsuarioCreacion = usuarioAutenticado;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estatus = "Act";

            }

        }

        private void ActualizarDatos(Guid usuarioAutenticado)
        {

            foreach (var valor in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified && x.Entity is IAuditoria))
            {

                var entidad = valor.Entity as IAuditoria;

                entidad.UsuarioModificacion = usuarioAutenticado;
                entidad.FechaModificacion = DateTime.Now;

            }

        }

        private void EliminarDatos(Guid usuarioAutenticado)
        {

            foreach (var valor in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted &&
                    x.Metadata.GetProperties().Any(e => e.Name == "Estatus")
                )
             )
            {

                valor.State = EntityState.Unchanged;

                var entidad = valor.Entity as IAuditoria;
                entidad.UsuarioModificacion = usuarioAutenticado;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estatus = "Baj";

            }

        }

        public bool Autenticado()
        {

            if (_httpContextAccessor.HttpContext == null) return false;

            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? true : false;

        }

        public Guid UsuarioAutenticado()
        {

            if (!Autenticado()) return Guid.NewGuid();

            return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        }

    }
}
