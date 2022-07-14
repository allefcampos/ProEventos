using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Context
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<UserRole>(userRole => 
            //     {
            //         userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

            //         userRole.HasOne(ur => ur.Role)
            //             .WithMany(r => r.UserRoles)
            //             .HasForeignKey(ur => ur.RoleId)
            //             .IsRequired();

            //         userRole.HasOne(ur => ur.User)
            //             .WithMany(r => r.UserRoles)
            //             .HasForeignKey(ur => ur.UserId)
            //             .IsRequired();
            //     }
            // );

            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            // Configuração para quando deletar evento deletar tbm as redes sociais.
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração para quando deletar um palestrante deletar tbm as redes sociais.
            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}