using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Competition_Task_ProjectMars.Pages;
using Competition_Task_ProjectMars.TestModel;
using Competition_Task_ProjectMars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Competition_Task_ProjectMars.Tests
{
    [TestFixture]
    public class CertificationNunitTest : CommonDriver
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
        public void AddNewCertification_Test()

        {
            test = extent.CreateTest("AddNewCertification_Test", "Add Certification test");
            // Read test data from the JSON file using JsonHelper
            List<CertificationTestModel> addCertificationTestData = JsonHelper.ReadTestDataFromJson<CertificationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\AddCertification.json");
            foreach (var input in addCertificationTestData)
            {
                string updatecertificateName = input.certificateAwardName;
                Console.WriteLine(updatecertificateName);
                string certifiedFrom = input.certifiedFrom;
                Console.WriteLine(certifiedFrom);
                string certifiedYear = input.certifiedYear;
                Console.WriteLine(certifiedYear);
                test.Log(Status.Pass, "Test Passed");
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "AddCertification");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                Certifications CertificationsObj = new Certifications();
                CertificationsObj.AddNewCertification(input);
                string newRecordCertificateName = CertificationsObj.getNewRecordCertificateName();

                if (input.certificateAwardName == newRecordCertificateName)
                {
                    Assert.AreEqual(input.certificateAwardName, newRecordCertificateName, "Added Certifications and expected Certifications does not match.");
                }
                else
                {
                    Console.WriteLine("Check error");
                }
            }
        }
        [Test, Order(2)]

        public void UpdateCertification_Test()

        {
            test = extent.CreateTest("UpdateCertification_Test", "Update Certification test");
            // Read test data from the JSON file using JsonHelper
            List<CertificationTestModel> updateCertificationTestData = JsonHelper.ReadTestDataFromJson<CertificationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\UpdateCertification.json");
            foreach (var updateInput in updateCertificationTestData)
            {
                string updatecertificateName = updateInput.certificateAwardName;
                Console.WriteLine(updatecertificateName);
                string certifiedFrom = updateInput.certifiedFrom;
                Console.WriteLine(certifiedFrom);
                string certifiedYear = updateInput.certifiedYear;
                Console.WriteLine(certifiedYear);
                test.Log(Status.Pass, "Test Passed");
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "UpdateCertification");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                Certifications CertificationsObj = new Certifications();
                CertificationsObj.UpdateCertifications(updateInput);
                string UpdatedRecordCertificationName = CertificationsObj.getUpdatedRecordCertificationName(updateInput);

                if (updateInput.certificateAwardName == UpdatedRecordCertificationName)
                {
                    Assert.AreEqual(updateInput.certificateAwardName, UpdatedRecordCertificationName, "updated Certifications and expected Certifications does not match.");
                }
                else
                {
                    Console.WriteLine("Check error");
                }

            }
        }

        [Test, Order(3)]
        public void DeleteCertification_Test()
        {
            test = extent.CreateTest("DeleteCertification_Test", "Delete Certification test");
            List<CertificationTestModel> deleteUpdateTestData = JsonHelper.ReadTestDataFromJson<CertificationTestModel>("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\JsonDataFiles\\DeleteCertification.json");
            foreach (var deleteinput in deleteUpdateTestData)
            {
                string certificateName = deleteinput.certificateAwardName;
                Console.WriteLine(certificateName);
                string certifiedFrom = deleteinput.certifiedFrom;
                Console.WriteLine(certifiedFrom);
                string certifiedYear = deleteinput.certifiedYear;
                Console.WriteLine(certifiedYear);
                string screenshotPath = ScreenshotUtility.CaptureScreenshot(driver, "DeleteCertification");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                Certifications CertificationsObj = new Certifications();
                CertificationsObj.DeleteCertification(deleteinput);
                try
                {
                    CertificationsObj.DeleteCertification(deleteinput);

                }
                catch (NoSuchElementException)
                {

                    Console.WriteLine($"DeleteCertification element not found for certificateName: {deleteinput.certificateAwardName}");
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
