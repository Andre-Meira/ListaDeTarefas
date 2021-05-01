using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ListaDeTarefas.Models
{
    public partial class ListaTarefaDBContext : DbContext
    {
        public ListaTarefaDBContext()
        {
        }

        public ListaTarefaDBContext(DbContextOptions<ListaTarefaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarefa> Tarefas { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;DataBase=ListaTarefaDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(e => e.TarefasId)
                    .HasName("PK__Tarefa__E6C7CAC2DF280AD7");

                entity.ToTable("Tarefa");

                entity.Property(e => e.TarefasId).HasColumnName("Tarefas_ID");

                entity.Property(e => e.FkUserTarefa).HasColumnName("Fk_UserTarefa");

                entity.Property(e => e.NomeTarefa).HasColumnName("Nome_Tarefa");

                entity.HasOne(d => d.FkUserTarefaNavigation)
                    .WithMany(p => p.Tarefas)
                    .HasForeignKey(d => d.FkUserTarefa)
                    .HasConstraintName("FK__Tarefa__Fk_UserT__42E1EEFE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C92638C8DE3226");

                entity.ToTable("User");

                entity.Property(e => e.Senha).HasColumnName("Senha ");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
