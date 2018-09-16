using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamProject2017.Controllers;
using ExamProject2017.Models;
using Moq;
using Xunit;
using ExamProject2017.Repository;
using ExamProject2017.Models.UnitTestViewModels;
using ExamProject2017.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExamProject2017.Models.InfluencerViewModels;

namespace ExamenUnitTestProject
{
    public class UnitTest1
    {
        private Mock<IRepository<YoutubeData>>OpstartMockData() {
            var mockRepo = new Mock<IRepository<YoutubeData>>();
            mockRepo.Setup(repo => repo.GetAllAsListAsync()).Returns(Task.FromResult(GetTestSessions()));

            return mockRepo;
        }


        private Mock<IRepository<YoutubeData>> OpstartMockData()
        {
            var mockRepo = new Mock<IRepository<YoutubeData>>();
            mockRepo.Setup(repo => repo.GetAllAsListAsync()).Returns(Task.FromResult(GetTestSessions()));

            return mockRepo;
        }


        private IOptions<AppKeyConfig> OpstartAppKey()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        {"Authentication:Facebook:AppId", "326842804433660"},
                        {"Authentication:Facebook:AppSecret", "11616aa120dee0d55321a66c32fe94c0"}
                    })
                   .Build();

            var services = new ServiceCollection();
            services.Configure<AppKeyConfig>(configuration.GetSection("Authentication:Facebook"));
            services.AddOptions();
            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetService<IOptions<AppKeyConfig>>();

            return options;
        }

        [Fact]
        public async Task Edit_RetunererEtViewResult_MedModellen_HvisStateErValid()
        {
            var userManagerMock = new Mock<IUserStore<ApplicationUser>>(MockBehavior.Strict);

            // Opsætning
            var options = OpstartAppKey();
            var mockRepo = OpstartMockData();

            // Handling
            var controller = new InfluencerController(null, null, options, mockRepo.Object);

            var result = await controller.Edit() as ViewResult;
            var viewResult = Assert.IsType<ViewResult>(result);

            //// Opsætning
            //var options = OpstartAppKey();
            //var MockData = OpstartMockData();

            //var dummyModel = new EditViewModel
            //{
            //    Address ="lol"

            //};
            
            //// Handling
            //var controller = new InfluencerController(null, null, options, MockData.Object);
            //var result = await controller.Edit() as ViewResult;

            //// Test
            //var viewResult = Assert.IsType<ViewResult>(result);

            //var model = Assert.IsAssignableFrom<EditViewModel>(
            //    viewResult.ViewData.Model);
            //Assert.Equal("lol", model.Address);
            //Assert.IsType<ViewResult>(result);


        }


        private List<YoutubeData> GetTestSessions()
        {
            var sessions = new List<YoutubeData>();
            sessions.Add(new YoutubeData()
            {
                Id = "1",
                MaleViews = 10
            });
            sessions.Add(new YoutubeData()
            {
                Id = "2",
                MaleViews = 40
            });
            return sessions;
        }

        // virker
        [Fact]
        public void TestYoutubeAuthViewData()
        {
            // Opsætning
            var options = OpstartAppKey();
            var mockRepo = OpstartMockData();

            // Handling
            var controller = new InfluencerController(null, null, options, mockRepo.Object);

            var result = controller.Test() as ViewResult;
            var viewResult = Assert.IsType<ViewResult>(result);


            Assert.Equal("Test", result.ViewName);
        }

        //[Fact]
        //public void TestYoutubeRepository()
        //{
        //    // Opsætning
        //    var options = Opstart().Item1; // sætter appkey og secret fra secrets.json
        //    var mockRepo = Opstart().Item2; // sætter repository test data

        //    // Handling
        //    var controller = new InfluencerController(null, null, options, mockRepo.Object);

        //    var result = controller.Test3() as ViewResult;

        //    Assert.Equal("Test", result.ViewName);
        //}


    }
}
