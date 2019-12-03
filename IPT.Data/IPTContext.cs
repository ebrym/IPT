using IPT.Data.Entity;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IPT.Data
{
    public class IPTContext : IdentityDbContext<User,Role,int>
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public IPTContext(DbContextOptions<IPTContext> options, IHttpContextAccessor contextAccessor) :base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public IPTContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailSentLogs { get; set; }
        public DbSet<TaxPayer> TaxPayers { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<MessageOut> MessageOuts { get; set; }
        public DbSet<MessageIn> MessageIns { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Role>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int saved = 0;
            var currentUser = "";// _contextAccessor.HttpContext.User.Identity.Name == null ? _contextAccessor.HttpContext.User.Identity.Name : "";
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity.Entity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.LastDateUpdated = currentDate;
                        entry.Entity.UpdatedBy = currentUser;
                    }
                }
                saved = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception Ex)
            {
                //Log.Error(Ex, "An error has occured in SaveChanges");
                //Log.Error(Ex.InnerException, "An error has occured in SaveChanges InnerException");
                //Log.Error(Ex.StackTrace, "An error has occured in SaveChanges StackTrace");
            }

            return saved;
            //   return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            int saved = 0;
            //var currentUser = _contextAccessor.HttpContext.User.Identity.Name == null? _contextAccessor.HttpContext.User.Identity.Name : "";
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<BaseEntity.Entity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = currentDate;
                    //    entry.Entity.CreatedBy = currentUser;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.LastDateUpdated = currentDate;
                      //  entry.Entity.UpdatedBy = currentUser;
                    }
                }
                saved = base.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return saved;
        }

    }


    public class ContextDesignFactory : IDesignTimeDbContextFactory<IPTContext>
    {
        public IPTContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IPTContext>()
                .UseSqlServer("Server=.;Initial Catalog=IPT;Persist Security Info=False;User ID=sa;Password=pa$$word123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

            return new IPTContext(optionsBuilder.Options);
        }
    }
}
  