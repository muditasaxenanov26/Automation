using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Configuration;

namespace FillSeleniumForm
{
    [TestClass]
    public class UnitTest1
    {
        private static By txtFirstName = By.Name("firstname");
        private static By txtLastName = By.Name("lastname");
        private static String radSex = "//*[@name='sex']/..";
        private static String radTeaYears = "//*[@name='exp']/..";
        private static By txtDateStopped = By.Id("datepicker");
        private static By chkBlackTea = By.Id("tea1");
        private static By chkBreak = By.Id("tool-0");
        private static By chkOneOfThose = By.Id("tool-2");
        private static By drpContinents = By.Id("continents");
        private static By selBackToWork = By.Id("selenium_commands");
        private static By btnOK = By.Id("submit");
        

        [TestMethod]
        public void FormFilling()
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://www.practiceselenium.com/practice-form.html");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                IWebElement targetTextBoxFName = driver.FindElement(txtFirstName);
                targetTextBoxFName.SendKeys(ConfigurationManager.AppSettings["firstName"]);
                IWebElement targetTextBoxLName = driver.FindElement(txtLastName);

                targetTextBoxLName.SendKeys(ConfigurationManager.AppSettings["lastName"]);
                IWebElement sex = driver.FindElement(By.XPath(radSex));
                By targetButtonBy = By.XPath(radSex + "/*[@value='" + ConfigurationManager.AppSettings["gender"] + "']");
                IWebElement targetButton = driver.FindElement(targetButtonBy);
                targetButton.Click();

                IWebElement TeaYears = driver.FindElement(By.XPath(radTeaYears));
                By targetButtonBy2 = By.XPath(radTeaYears + "/*[@value='" + ConfigurationManager.AppSettings["yearsDrinking"] + "']");
                IWebElement targetButton2 = driver.FindElement(targetButtonBy);
                targetButton2.Click();


                IWebElement DateStopped = driver.FindElement(txtDateStopped);
                DateStopped.SendKeys(ConfigurationManager.AppSettings["dateStopped"]);

                IWebElement blackTea = driver.FindElement(chkBlackTea);
                blackTea.Click();

                IWebElement exciteBreak = driver.FindElement(chkBreak);
                exciteBreak.Click();

                IWebElement continentList = driver.FindElement(drpContinents);
                var selectElement = new SelectElement(continentList);

                selectElement.SelectByText(ConfigurationManager.AppSettings["continent"]);
                IWebElement topicList = driver.FindElement(selBackToWork);
                topicList.SendKeys(ConfigurationManager.AppSettings["topic"]);
                //Thread.Sleep(5000);
                IWebElement okButton = driver.FindElement(btnOK);
                okButton.Click();
                driver.Close();
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        [TestMethod]
        public void LinksinInmar()
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://www.inmar.com");
                IList<IWebElement> links = driver.FindElements(By.TagName("a"));
                StreamWriter log;
                if (!File.Exists("logfile.txt"))
                {
                    log = new StreamWriter("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                else
                {
                    log = File.AppendText("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                log.WriteLine("Number of links present is" + links.Count);
                int i;
                String[] allText = new String[links.Count];
                for (i = 1; i <= links.Count; i = i + 1)
                {
                    log.WriteLine(links[i].Text);
                }
                //  log.WriteLine("Link texts are:-" + allText);

                log.Close();
                driver.Quit();
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        [TestMethod]
        public void TitlePrint()
        {
			IWebDriver driver=new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.inmar.com");
            Thread.Sleep(9000);
			String Title = driver.Title;
                StreamWriter log;
                if (!File.Exists("logfile.txt"))
                {
                    log = new StreamWriter("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                else
                {
                    log = File.AppendText("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                log.WriteLine("Title of the page is:-" + Title);
            log.Close();
			driver.Quit();
        }
        [TestMethod]
        public void Handles()
        {
            try
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                String winHandleBefore = driver.CurrentWindowHandle;
                driver.FindElement(By.XPath("//button[text()='New Browser Window']")).Click();
                Thread.Sleep(2000);
                // List<String> handles = driver.WindowHandles;
                ReadOnlyCollection<string> handles = driver.WindowHandles;
                string popupHandle = string.Empty;
                ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

                foreach (string handle in windowHandles)
                {
                    if (handle != winHandleBefore)
                    {
                        popupHandle = handle; break;
                    }
                }

                //switch to new window 
                driver.SwitchTo().Window(popupHandle);
                StreamWriter log;
                if (!File.Exists("logfile.txt"))
                {
                    log = new StreamWriter("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                else
                {
                    log = File.AppendText("C:\\Users\\lmadmin\\Desktop\\log.txt");
                }
                log.WriteLine("Title of the child window is:-" + driver.Title);
                driver.Close();

                //switch back to original window 
                driver.SwitchTo().Window(winHandleBefore);

                log.WriteLine("Title of the child window is:-" + driver.Title);

                log.Close();
                driver.Quit();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
    }

