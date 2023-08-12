using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition_Task_ProjectMars.Utilities;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Competition_Task_ProjectMars.Pages
{
    public class LoginPage : CommonDriver
    {
        private IWebElement signInButton    => driver.FindElement(By.XPath("//a[text()='Sign In']"));
        private IWebElement emailField      => driver.FindElement(By.Name("email"));
        private IWebElement passwordField   => driver.FindElement(By.Name("password"));
        private IWebElement loginButton     => driver.FindElement(By.XPath("//button[text()='Login']"));

        public void LoginSteps()
        {
            driver.Navigate().GoToUrl("http://localhost:5000/");
            driver.Manage().Window.Maximize();
            // Click the "Sign In" button
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Sign In']", 5);
            signInButton.Click();
            // Enter the provided email
            Wait.WaitToBeVisible(driver, "Name", "email", 5);
            emailField.SendKeys("ammu.muru279@gmail.com");
            //Enter the provided password
            Wait.WaitToBeVisible(driver, "Name", "password", 5);
            passwordField.SendKeys("mvp123");         
            // Click the "Login" button
            Wait.WaitToBeClickable(driver, "XPath", "//button[text()='Login']", 5);
            loginButton.Click();
            Thread.Sleep(3000);       
        }
    }
}
