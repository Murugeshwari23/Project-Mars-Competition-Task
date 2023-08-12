using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Competition_Task_ProjectMars.TestModel;
using Competition_Task_ProjectMars.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Competition_Task_ProjectMars.Pages
{
    public class Education : CommonDriver
    {
        private IWebElement educationTab => driver.FindElement(By.XPath("//a[text() = 'Education']"));
        private IWebElement addNewButton => driver.FindElement(By.XPath("//div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div"));
        private IWebElement collegName => driver.FindElement(By.Name("instituteName"));
        private IWebElement countryOfCollege => driver.FindElement(By.Name("country"));
        private IWebElement title => driver.FindElement(By.Name("title"));
        private IWebElement degree => driver.FindElement(By.Name("degree"));
        private IWebElement yearOfGraduation => driver.FindElement(By.Name("yearOfGraduation"));
        private IWebElement addbutton => driver.FindElement(By.XPath("//input [contains (@class, 'ui teal button')]"));
        private IWebElement messageBox => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement newRecordInstituteName => driver.FindElement(By.XPath("(//*[@class= 'ui fixed table'])[3]/tbody[last()]/tr/td[2]"));
        private IWebElement cancelIcon => driver.FindElement(By.XPath("//input[@value= 'Cancel']"));

        public void AddNewEducation(EducationTestModel data)
        {
            //Click on Education tab
            Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Education']", 3);
            educationTab.Click();

            //Click AddNew button on Education tab
            Wait.WaitToBeClickable(driver, "XPath", "//div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div", 3);
            addNewButton.Click();

            //Enter University/Institute Name
            Wait.WaitToBeVisible(driver, "Name", "instituteName", 3);
            collegName.Clear();
            collegName.SendKeys(Keys.Control + "A");
            collegName.SendKeys(Keys.Backspace);
            collegName.SendKeys(data.InstituteName);

            //Select the name of the Country
            Wait.WaitToBeVisible(driver, "Name", "country", 3);
            countryOfCollege.Click();
            countryOfCollege.SendKeys(data.CountryOfCollege);

            //Select the Title
            Wait.WaitToBeVisible(driver, "Name", "title", 3);
            title.Click();
            title.SendKeys(data.Title);

            //Enter the Degree
            Wait.WaitToBeVisible(driver, "Name", "degree", 3);
            degree.SendKeys(data.Degree);

            //Select the year of graduation
            Wait.WaitToBeClickable(driver, "Name", "yearOfGraduation", 3);
            yearOfGraduation.Click();
            yearOfGraduation.SendKeys(data.YearOfGraduation);

            //Click on Add button
            Wait.WaitToBeClickable(driver, "XPath", "//input [contains (@class, 'ui teal button')]", 3);
            addbutton.Click();
            Thread.Sleep(2000);

            //Wait for the popup message window to display
            Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            Thread.Sleep(3000);

            string popupMessage = messageBox.Text;
            Console.WriteLine(popupMessage);

            //verify the expected message text
            string expectedMessage2 = "Education information was invalid";
            string expectedMessage3 = "Please enter all the fields";
            string expectedMessage4 = "Duplicated data";
            string expectedMessage5 = "This information is already exist.";


            if (popupMessage == expectedMessage2 || popupMessage == expectedMessage3 || popupMessage == expectedMessage4 || popupMessage == expectedMessage5)
            {
                Thread.Sleep(2000);
                cancelIcon.Click();
            }

        }
        public string getNewRecordInstituteName()
        {

            return newRecordInstituteName.Text;
        }

        public void UpdateEducation(EducationTestModel updateData)

        {
            //Click on Education Icon
            Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Education']", 3);
            educationTab.Click();
            Thread.Sleep(2000);

            IWebElement pencilIcon = driver.FindElement(By.XPath($"//tbody[tr[td[text()= '{updateData.InstituteName}']]]//span[1]"));
            pencilIcon.Click();
            Thread.Sleep(2000);

            Wait.WaitToBeVisible(driver, "Name", "instituteName", 3);
            collegName.Clear();
            collegName.SendKeys(Keys.Control + "A");
            collegName.SendKeys(Keys.Backspace);
            collegName.SendKeys(updateData.InstituteName);

            Wait.WaitToBeVisible(driver, "Name", "country", 3);
            countryOfCollege.Click();
            countryOfCollege.SendKeys(updateData.CountryOfCollege);

            Wait.WaitToBeVisible(driver, "Name", "title", 3);
            title.Click();
            title.SendKeys(updateData.Title);

            Wait.WaitToBeVisible(driver, "Name", "degree", 3);
            degree.Clear();
            degree.SendKeys(Keys.Control + "A");
            degree.SendKeys(Keys.Backspace);
            degree.SendKeys(updateData.Degree);

            Wait.WaitToBeClickable(driver, "Name", "yearOfGraduation", 3);
            yearOfGraduation.Click();
            yearOfGraduation.SendKeys(updateData.YearOfGraduation);

            //click on update button
            Wait.WaitToBeClickable(driver, "XPath", "//input[contains(@value, 'Update')]", 3);
            IWebElement updateButton = driver.FindElement(By.XPath("//input[contains(@value, 'Update')]"));
            updateButton.Click();
            Thread.Sleep(3000);

            //Wait for the popup message window to display
            Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
            Thread.Sleep(3000);

            //Get the POPup Message text
            string popupMessage = messageBox.Text;
            Console.WriteLine(popupMessage);

            string expectedMessage2 = "This information is already exist.";
            string expectedMessage3 = "Please enter all the fields";
            string expectedMessage4 = "Education information was invalid";
            if (popupMessage == expectedMessage2 || popupMessage == expectedMessage3 || popupMessage == expectedMessage4)
            {
                Thread.Sleep(2000);
                cancelIcon.Click();
            }
            Thread.Sleep(3000);
        }
        public string getUpdatedRecordInstituteName(EducationTestModel updateData)
        {
            IWebElement UpdatedRecordInstituteName = driver.FindElement(By.XPath($"//tbody[tr[td[text()='{updateData.InstituteName}']]]/tr/td[2]"));
            return UpdatedRecordInstituteName.Text;
        }

        public void DeleteEducation(EducationTestModel deleteData)
        {
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text() = 'Education']", 3);
                educationTab.Click();
                //delete education
                IWebElement deleteIcon = driver.FindElement(By.XPath($"//tbody[tr[td[text()='{deleteData.InstituteName}']]]//span[2]"));
                // Find and click the delete icon in the row
                deleteIcon.Click();
                Thread.Sleep(2000);

                //Wait for the popup message window to display
                Wait.WaitToBeVisible(driver, "XPath", "//div[@class='ns-box-inner']", 3);
                Thread.Sleep(2000);

                //Get the POPup Message text
                string popupMessage = messageBox.Text;
                Console.WriteLine(popupMessage);
            }
        }
    }
}

