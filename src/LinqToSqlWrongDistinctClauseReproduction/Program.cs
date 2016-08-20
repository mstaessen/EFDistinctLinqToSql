using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToSqlWrongDistinctClauseReproduction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LinqToSqlWrongDistinctClauseReproduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            var itemsPerPage = 15;
            var page = 0;
            var dbContextFactory = new ApplicationDbContextFactory();
            using (var context = dbContextFactory.Create(new DbContextFactoryOptions())) {
                var query = Filter(context.RunResults, "someUserId", null, null)
                    .Select(x => x.Property)
                    .Distinct()
                    .OrderByDescending(x => x.CreationDate);
                var nbItems = await query.CountAsync();
                var items = await query
                    .Select(x => new PropertyRowViewModel {
                        Id = x.Id,
                        Source = x.Source,
                        ForeignId = x.ForeignId,
                        CreationDate = x.CreationDate,
                        LastUpdate = x.LastUpdate,
                        RemovalDate = x.RemovalDate,
                        Details = x.Details,
                        IsRemoved = x.IsRemoved
                    })
                    .Skip(page * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToListAsync();

                Console.ReadLine();
            }
        }

        public static IQueryable<QueryRunResult> Filter(IQueryable<QueryRunResult> input, string userId, Guid? queryDefinitionId, Guid? queryRunId)
        {
            // Always filter on UserId
            var query = input.Where(x => x.QueryRun.QueryDefinition.UserId == userId);

            if (queryDefinitionId.HasValue) {
                query = query.Where(x => x.QueryRun.QueryDefinitionId == queryDefinitionId.Value);
            }

            if (queryRunId.HasValue) {
                query = query.Where(x => x.QueryRunId == queryRunId.Value);
            }

            return query;
        }

        public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext Create(DbContextFactoryOptions options)
            {
                var connectionString = "Server=(localdb)\\mssqllocaldb;Database=LinqToSqlWrongDistinctClauseReproduction;Trusted_Connection=True;MultipleActiveResultSets=true";
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddDebug((origin, ll) => { return origin == "Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory"; });
                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(connectionString)
                    .UseLoggerFactory(loggerFactory)
                    .Options;
                return new ApplicationDbContext(contextOptions);
            }
        }
    }

    public class PropertyRowViewModel
    {
        public Guid Id { get; set; }

        public DataSource Source { get; set; }

        public string ForeignId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime? RemovalDate { get; set; }

        public string Details { get; set; }

        public bool IsRemoved { get; set; }
    }
}