using Microsoft.EntityFrameworkCore;
using SharedKernel.Claims;
using SharedKernel.Constants;
using SharedKernel.Entity;
using SharedKernel.Interfaces;

namespace Persistence.Interceptors
{
    public class AuditOnSavingChanges
    {
        private readonly IClaims _qClaims;
        public AuditOnSavingChanges(IClaims qClaims)
        {
            _qClaims = qClaims;
        }

        private void AddToken(DbContext? context)
        {
            foreach (var entry in context?.ChangeTracker.Entries<IMustHaveToken>()!)
            {
                var postFix = Constants.GetToken_PostFix(entry.Metadata.ClrType.Name);
                var guid = Guid.NewGuid();

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(_ => _.Token).CurrentValue = guid.ToString("D") + "_" + postFix;
                        break;
                    case EntityState.Modified:
                        entry.Property(_ => _.Token).IsModified = false;
                        break;
                }
            }
        }

        private void AddTimestamp(DbContext? context)
        {
            foreach (var entry in context?.ChangeTracker.Entries<Entity>()!)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(_ => _.CreatedDate).CurrentValue = DateTime.UtcNow;
                        entry.Property(_ => _.LastModifiedDate).IsModified = false;
                        break;
                    case EntityState.Modified:
                        entry.Property(_ => _.LastModifiedDate).CurrentValue = DateTime.UtcNow;
                        entry.Property(_ => _.CreatedDate).IsModified = false;
                        break;
                }
            }
        }

        private void AddOrganizationId(DbContext? context)
        {
            foreach (var entry in context?.ChangeTracker.Entries<IMustHaveOrganization>()!)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(_ => _.OrganizationId).CurrentValue = _qClaims.OrganizationId;
                        break;
                    case EntityState.Modified:
                        entry.Property(_ => _.OrganizationId).IsModified = false;
                        break;
                }
            }
        }
    }
}
