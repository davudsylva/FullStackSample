using Xunit;
using System.Linq;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VehicleMicroservice.Core.Services;
using VehicleMicroservice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using VehicleMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleMicroservice.Contracts.Models;
using System;

namespace VehicleMicroservice.IntegrationTests
{
    public class TestBase
    {
        protected VehiclesController EstablishEnvironment()
        {
            // Establish the environment
            var baseDir = System.IO.Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
               .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("ConnectionString", "Data Source=../../../TestDatabase/vehicles.db") })
              .Build();

            VehicleMicroservice.API.Mappers.DapperMappers.Config();

            var productRepository = new VehicleRepository(configuration);
            var vehicleService = new VehicleService(new VehicleValidator(), 
                                                    productRepository);

            var logger = new Mock<ILogger<VehiclesController>>();

            var controller = new VehiclesController(vehicleService, logger.Object);
            return controller;
        }
    }
}
