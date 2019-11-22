using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPISample.Core.Common
{
    public class DbContextFactory : IDbContextFactory
    {
        /// <summary>
        /// Creates and returns a new IDBContext
        /// </summary>
        /// <returns>IDBContext</returns>
        public IDbContext CreateDbContext()
        {
            return new DbContext();
        }
    }
}
