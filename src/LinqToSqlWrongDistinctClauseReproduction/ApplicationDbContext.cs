using LinqToSqlWrongDistinctClauseReproduction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LinqToSqlWrongDistinctClauseReproduction
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<QueryDefinition> QueryDefinitions { get; set; }

        public DbSet<QueryRun> Runs { get; set; }

        public DbSet<QueryRunResult> RunResults { get; set; }

        public DbSet<Property> Properties { get; set; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relationship  User <> QueryDefinition
            builder.Entity<QueryDefinition>(b => {
                b.HasOne(x => x.User)
                    .WithMany(u => u.Definitions)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Relationship QueryDefinition <> QueryRun
            builder.Entity<QueryRun>(b => {
                b.HasOne(x => x.QueryDefinition)
                    .WithMany(x => x.Runs)
                    .HasForeignKey(x => x.QueryDefinitionId)
                    .HasPrincipalKey(x => x.Id)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Relationship QueryRun <> QueryRunResult
            // Relationship  QueryRunResult <> Property
            builder.Entity<QueryRunResult>(b => {
                b.HasOne(qr => qr.QueryRun)
                    .WithMany(x => x.Results)
                    .HasForeignKey(x => x.QueryRunId)
                    .HasPrincipalKey(x => x.Id)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(qr => qr.Property)
                    .WithMany()
                    .HasForeignKey(x => x.PropertyId)
                    .HasPrincipalKey(x => x.Id)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Property>(b => {
                b.HasIndex(p => new {p.Source, p.ForeignId}).IsUnique();
                b.HasIndex(p => p.ForeignId).IsUnique(false);
            });
        }
    }
}