using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using WebApp.Controllers;
using Xunit;

namespace WebApp.Tests
{
    public class MySettingsControllerTest
    {
        [Fact]
        public void Get_ReturnsOkResult()
        {
            var settings = new MySettings()
            {
                SqlConnection = "SQL Connection for Unit Test."
            };
            IOptions<MySettings> options = Options.Create(settings);
            var controller = new MySettingsController(options);

            // Act
            var actionResult = controller.Get();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(settings.SqlConnection, (result.Value as string[])[0]);
        }
    }
}
