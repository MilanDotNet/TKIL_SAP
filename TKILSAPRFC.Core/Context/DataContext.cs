using Microsoft.EntityFrameworkCore;
using TKILSAPRFC.Core.Helpers.Interface;

namespace TKILSAPRFC.Core.Context
{
    public partial class DataContext : DbContext
    {
        private readonly ICurrentUser _currentUser;

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options, ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
