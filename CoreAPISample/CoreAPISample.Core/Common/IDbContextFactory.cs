using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPISample.Core.Common
{
    public interface IDbContextFactory
    {
        /// <summary>
        /// Creates and returns a new IDBContext
        /// </summary>
        /// <returns>IDBContext</returns>
        IDbContext CreateDbContext();
    }
}
