using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition_Task_ProjectMars.TestModel;
using Competition_Task_ProjectMars.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MongoDB.Driver.Core.WireProtocol.Messages;
using OpenQA.Selenium;

namespace Competition_Task_ProjectMars.Pages
{
    public class Certifications : CommonDriver
    {
        private IWebElement certificationTab => driver.FindElement(By.XPath("//a[text() = 'Certifications']"));
        private IWebElement addNew => driver.FindElement(By.XPath("//div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div"));
        private IWebElement certifiedFrom => driver.FindElement(By.Name("certificationFrom"));
        private IWebElement certifiedYear => driver.FindElement(By.Name("certificationYear"));
        private IWebElement AddButton => driver.FindElement(By.XPath("//div[3]/form/div[5]/div[1]/div[2]/div/div/div[3]/input[1]"));
        private IWebElement MessageWindow => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement newRecordCertificateName => driver.FindElement(By.XPath("(//*[@class= 'ui fixed table'])[4]/tbody[last()]/tr/td[1]"));
        private IWebElement updateButton => driver.FindElement(By.XPath("//input[contains(@value, 'Update')]"));
        private IWebElement cancelIcon => driver.FindElement(By.XPath("//input[@value= 'Cancel']"));
        public void AddNewCertification(CertificationTestModel input)

        {
            Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Certifications']", 5);
            certificationTab.Click();

            Wait.WaitToBeClickable(driver, "XPath", "//div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div", 5);
            addNew.Click();
            Thread.Sleep(3000);

            IWebElement certificateAwardName = driver.FindElement(By.XPath("//div[3]/form/div[5]/div[1]/div[2]/div/div/div[1]/div/input"));
            certificateAwardName.SendKeys(input.certificateAwardName);

            Wait.WaitToBeVisible(driver, "Name", "certificationFrom", 3);
            certifiedFrom.SendKeys(input.certifiedFrom);

            Wait.WaitToBeVisible(driver, "Name", "certificationYear", 3);
            certifiedYear.Click();
            certifiedYear.SendKeys(input.certifiedYear);

            Wait.WaitToBeClickable(driver, "XPath", "//div[3]/form/div[5]/div[1]/div[2]/div/div/div[3]/input[1]", 3);
            AddButton.Click();
            Thread.Sleep(5000);

            //Wait for the popup message window to display
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
            var popupMessageText = MessageWindow.Text;
            Console.WriteLine(popupMessageText);

            //verify the expected message text
            string expectedMessage1 = certificateAwardName + " has been added to your certification";
            string expectedMessage2 = "Duplicated data";
            string expectedMessage3 = "Please enter Certification Name, Certification From and Certification Year";
            string expectedMessage4 = "This information is already exist.";

            if (popupMessageText == expectedMessage1)
            {
                Thread.Sleep(2000);
                Console.WriteLine(expectedMessage1);
            }
            else if (popupMessageText == expectedMessage2 || popupMessageText == expectedMessage3 || popupMessageText == expectedMessage4)
            {
                Thread.Sleep(2000);
                IWebElement cancelIcon = driver.FindElement(By.XPath("//input[@value= 'Cancel']"));
                cancelIcon.Click();
            }
        }
        public string getNewRecordCertificateName()
        {
            return newRecordCertificateName.Text;
        }

        public void UpdateCertifications(CertificationTestModel updateInput)

        {
            Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Certifications']", 5);
            certificationTab.Click();

            IWebElement CertificationpencilIcon = driver.FindElement(By.XPath($"//tbody[tr[td[text()= '{updateInput.certificateAwardName}']]]//span[1]"));
            CertificationpencilIcon.Click();
            Thread.Sleep(1000);

            IWebElement certificateAwardName = driver.FindElement(By.Name("certificationName"));
            certificateAwardName.Clear();
            certificateAwardName.SendKeys(Keys.Control + "A");
            certificateAwardName.SendKeys(Keys.Backspace);
            certificateAwardName.SendKeys(updateInput.certificateAwardName);
            Thread.Sleep(2000);

            Wait.WaitToBeVisible(driver, "Name", "certificationFrom", 3);
            certifiedFrom.Clear();
            certifiedFrom.SendKeys(Keys.Control + "A");
            certifiedFrom.SendKeys(Keys.Backspace);
            certifiedFrom.SendKeys(updateInput.certifiedFrom);
            Thread.Sleep(2000);

            Wait.WaitToBeVisible(driver, "Name", "certificationYear", 3);
            certifiedYear.Click();
            certifiedYear.SendKeys(updateInput.certifiedYear);
            Thread.Sleep(2000);

            Wait.WaitToBeClickable(driver, "XPath", "//input[contains(@value, 'Update')]", 3);
            updateButton.Click();
            Thread.Sleep(2000);

            //Wait for the popup message window to display
            Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            string popupMessageText = MessageWindow.Text;
            Console.WriteLine(popupMessageText);

            //verify the expected message text
            string expectedMessage1 = certificateAwardName + " has been updated to your certification";
            string expectedMessage2 = "Duplicated data";
            string expectedMessage3 = "Please enter Certification Name, Certification From and Certification Year";
            string expectedMessage4 = "This information is already exist.";

            if (popupMessageText == expectedMessage1)
            {
                Thread.Sleep(2000);
                Console.WriteLine(expectedMessage1);
            }
            else if (popupMessageText == expectedMessage2 || popupMessageText == expectedMessage3 || popupMessageText == expectedMessage4)
            {
                Thread.Sleep(2000);
                cancelIcon.Click();
            }
        }

        public string getUpdatedRecordCertificationName(CertificationTestModel updateInput)
        {
            IWebElement UpdatedRecordCertificationName = driver.FindElement(By.XPath($"//tbody[tr[td[text()='{updateInput.certificateAwardName}']]]/tr/td[2]"));
            return UpdatedRecordCertificationName.Text;
        }

        public void DeleteCertification(CertificationTestModel deleteinput)
        {
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Certifications']", 5);
                certificationTab.Click();

                //delete education
                IWebElement deleteIcon = driver.FindElement(By.XPath($"//tbody[tr[td[text()='{deleteinput.certificateAwardName}']]]//span[2]"));
                // Find and click the delete icon in the row
                deleteIcon.Click();
                Thread.Sleep(2000);

                //Wait for the popup message window to display
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
                Thread.Sleep(2000);

                //Get the POPup Message text
                string popupMessage = MessageWindow.Text;
                Console.WriteLine(popupMessage);
            }
        }
    }
}
