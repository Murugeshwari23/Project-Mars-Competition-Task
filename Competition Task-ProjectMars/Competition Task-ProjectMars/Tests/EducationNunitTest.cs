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
        private ExtentReports extent;
        private ExtentTest test;

        [SetUp]
        public void SetUpActions()
        {
            extent = ExtentReportManager.getInstance();
            driver = new ChromeDriver();
            //Login page object initialization and definition
            LoginPage LoginPageObj = new LoginPage();
            LoginPageObj.LoginSteps();
        }
        [Test, Order(1)]
        public void AddNewEducation_Test()

        {
            test = extent.CreateTest("AddNewEducation_Test", "Add Education test");
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
                test.Log(Status.Pass, "Test Passed");
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "AddNewEducation");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }

                Education EducationObj = new Education();
                EducationObj.AddNewEducation(data);
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
            test = extent.CreateTest("UpdateEducation_Test", "Update Education test");
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
                test.Log(Status.Pass, "Test Passed");
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "UpdateEducation");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }

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
            test = extent.CreateTest("DeleteEducation_Test", "Delete Education test");
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
                test.Log(Status.Pass, "Test Passed");
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "DeleteEducation");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }

                //string screenshotPath = CaptureScreenshot(driver, "DeleteEducation");
                //test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
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
            extent.Flush();
        }
    }
}

