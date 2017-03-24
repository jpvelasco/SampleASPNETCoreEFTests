using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SampleApi.Controllers;
using SampleApi.Data;
using SampleApi.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SampleApi.Tests
{
    public class ClientsControllerTests
    {
        [Fact]
        public void GetClients_WhenCalledWithInMemoryDataContext_ReturnsExpectedResult()
        {
            var inMemoryDataContextOptions = new DbContextOptionsBuilder<EventDataContext>()
              .UseInMemoryDatabase(databaseName: "Test_With_In_Memory_Database")
              .Options;

            // NOTE: Because we will need to assert against known data, 
            // we need to seed the in-memory test database
            // with the same context options as the unit test
            CreateTestClient(inMemoryDataContextOptions);

            var eventDataContext = new EventDataContext(inMemoryDataContextOptions);
            
            var controller = new ClientsController(eventDataContext);

            // NOTE: As a side note, in a real world test you will ideally have a specific
            // Assert per test, but for sample purposes multiple asserts are used here.
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(controller.GetClients());
            List<Client> returnSession = Assert.IsType<List<Client>>(okResult.Value);

            Assert.True(returnSession.FirstOrDefault().FirstName == "first");
        }

        [Fact]
        public void GetClients_WhenCalledWithSqlLiteInMemoryDataContext_ReturnsExpectedResult()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var sqlLiteDataContextOptions = new DbContextOptionsBuilder<EventDataContext>()
                    .UseSqlite(connection)
                    .Options;

                // This will update the database with the current schema for the test
                using (var context = new EventDataContext(sqlLiteDataContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                // Need to add a record with the given data context configuration, 
                // in this case, SQL Lite.
                CreateTestClient(sqlLiteDataContextOptions);

                using (var eventDataContext = new EventDataContext(sqlLiteDataContextOptions))
                {
                    // Need to inject a separate data context into the controller, 
                    // then the results will be asserted against the current records
                    var controller = new ClientsController(eventDataContext);

                    OkObjectResult okResult = Assert.IsType<OkObjectResult>(controller.GetClients());
                    List<Client> returnSession = Assert.IsType<List<Client>>(okResult.Value);

                    Assert.True(returnSession.FirstOrDefault().LastName == "last");
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private static void CreateTestClient(DbContextOptions<EventDataContext> contextOptions)
        {
            using (var context = new EventDataContext(contextOptions))
            {
                var fakeClient = new Client();
                fakeClient.FirstName = "first";
                fakeClient.LastName = "last";
                
                context.Add(fakeClient);
                context.SaveChanges();
            }
        }



    }
}
