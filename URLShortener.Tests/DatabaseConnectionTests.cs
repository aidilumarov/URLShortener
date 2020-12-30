using System;
using MongoDB.Driver;
using URLShortener.Tests.Common;
using Xunit;

namespace URLShortener.Tests
{
    public class DatabaseConnectionTests
    {
        [Fact]
        public void ConnectionIsEstablished()
        {
            var dbConfig = ApplicationConfigGetter.ParseDbConfigSection();
            new MongoClient(dbConfig.ConnectionString);
        }
    }
}