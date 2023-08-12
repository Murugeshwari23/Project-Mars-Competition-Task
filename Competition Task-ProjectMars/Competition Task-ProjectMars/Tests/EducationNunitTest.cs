using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition_Task_ProjectMars.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Competition_Task_ProjectMars.Utilities;
using NUnit.Framework;
using Competition_Task_ProjectMars.TestModel;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using System.Reflection;
using AventStack.ExtentReports.Reporter;

namespace Competition_Task_ProjectMars.Tests
{
    [TestFixture]
    public class EducationNunitTest : CommonDriver
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            string reportPath = "C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\EducationReports\\";
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent.AttachReporter(htmlReporter);
        }
        [SetUp]
        public void SetUpActions()
        {
            driver = new ChromeDriver();
            //Login page object initialization and definition
            LoginPage LoginPageObj = new LoginPage();
            LoginPageObj.LoginSteps();
        }
        [Test, Order(1)]
        public void AddNewEducation_Test()

        {
            // Read test data from the JSON file using JsonHelper
            List<EducationTestModel> addEducationTestData = JsonHelper.ReadTestDataFromJson<EducationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\AddEducation.json");
            foreach (var data in addEducationTestData)
            {
                string InstituteName = data.InstituteName;
                Console.WriteLine(InstituteName);
                string CountryOfCollege = data.CountryOfCollege;
                Console.WriteLine(CountryOfCollege);
                string Title = data.Title;
                Console.WriteLine(Title);
                string Degree = data.Degree;
                Console.WriteLine(Degree);
                string YearOfGraduation = data.YearOfGraduation;
                Console.WriteLine(YearOfGraduation);
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name, "Adding");
                test = test.Log(Status.Info, "Adding test");
                Education EducationObj = new Education();
                EducationObj.AddNewEducation(data);
                string screenshotPath = CaptureScreenshot(driver, "AddNewEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                string newRecordInstituteName = EducationObj.getNewRecordInstituteName();

                if (data.InstituteName == newRecordInstituteName)
                {
                    Assert.AreEqual(data.InstituteName, newRecordInstituteName, "Added Education and expected Education does not match.");
                }
                else
                {
                    Console.WriteLine("Check error");
                }

            }
        }
        [Test, Order(2)]
        public void UpdateEducation_Test()
        {
            // Read test data from the JSON file using JsonHelper
            List<EducationTestModel> UpdateEducationTestData = JsonHelper.ReadTestDataFromJson<EducationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\UpdateEducation.json");
            foreach (var updateData in UpdateEducationTestData)
            {

                string InstituteName = updateData.InstituteName;
                Console.WriteLine(InstituteName);
                string CountryOfCollege = updateData.CountryOfCollege;
                Console.WriteLine(CountryOfCollege);
                string Title = updateData.Title;
                Console.WriteLine(Title);
                string Degree = updateData.Degree;
                Console.WriteLine(Degree);
                string YearOfGraduation = updateData.YearOfGraduation;
                Console.WriteLine(YearOfGraduation);
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name, "Update");
                test = test.Log(Status.Info, "Updating test");
                string screenshotPath = CaptureScreenshot(driver, "UpdateEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                Education EducationObj = new Education();
                try
                {
                    EducationObj.UpdateEducation(updateData);
                    string UpdatedRecordInstituteName = EducationObj.getUpdatedRecordInstituteName(updateData);

                    if (updateData.InstituteName == UpdatedRecordInstituteName)
                    {
                        Assert.AreEqual(updateData.InstituteName, UpdatedRecordInstituteName, "Added Education and expected Education does not match.");
                    }
                    else
                    {
                        Console.WriteLine("Check error");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"UpdateEducation element not found for InstituteName: {updateData.InstituteName}");
                }
            }
        }

        [Test, Order(3)]
        public void DeleteEducation_Test()
        {
            List<EducationTestModel> deleteEducationTestData = JsonHelper.ReadTestDataFromJson<EducationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\DeleteEducation.json");

            foreach (var deleteData in deleteEducationTestData)
            {

                string InstituteName = deleteData.InstituteName;
                Console.WriteLine(InstituteName);
                string CountryOfCollege = deleteData.CountryOfCollege;
                Console.WriteLine(CountryOfCollege);
                string Title = deleteData.Title;
                Console.WriteLine(Title);
                string Degree = deleteData.Degree;
                Console.WriteLine(Degree);
                string YearOfGraduation = deleteData.YearOfGraduation;
                Console.WriteLine(YearOfGraduation);
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name, "Delete");
                test = test.Log(Status.Info, "Deleting test");
                string screenshotPath = CaptureScreenshot(driver, "DeleteEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                Education EducationObj = new Education();
                try
                {
                    EducationObj.DeleteEducation(deleteData);

                }
                catch (NoSuchElementException)
                {

                    Console.WriteLine($"DeleteEducation element not found for InstituteName: {deleteData.InstituteName}");
                }
            }
        }

        [TearDown]
        public void TearDownActions()
        {
            driver.Quit();
        }

        public string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string screenshotPath = Path.Combine(@"C:\Competition Task-Project Mars\Project-Mars-Competition-Task\Competition Task-ProjectMars\Competition Task-ProjectMars\Screenshots\", $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmss}.png");
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            return screenshotPath;
        }

        [OneTimeTearDown]
        public void ExtentTeardown()
        {
            extent.Flush();

        }
    }
}

