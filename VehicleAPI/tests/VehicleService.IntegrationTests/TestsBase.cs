using Xunit;
using System.Linq;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductMicroservice.Core.Services;
using ProductMicroservice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using ProductMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductMicroservice.Contracts.Models;
using System;

namespace ProductMicroservice.IntegrationTests
{
    public class TestBase
    {
        protected VehiclesController EstablishEnvironment()
        {
            // Establish the environment
            var baseDir = System.IO.Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
               .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("ConnectionString", "Data Source=../../../TestDatabase/products.db") })
              .Build();

            ProductMicroservice.API.Mappers.DapperMappers.Config();

            var productRepository = new ProductRepository(configuration);
            var productService = new VehicleService(new VehicleValidator(), 
                                                    productRepository);

            var logger = new Mock<ILogger<VehiclesController>>();

            var controller = new VehiclesController(productService, logger.Object);
            return controller;
        }
    }
}
