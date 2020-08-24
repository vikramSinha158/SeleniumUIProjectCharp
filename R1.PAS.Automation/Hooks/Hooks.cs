using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Extensions.Configuration;
using R1.Automation.UI.core.Commons;
using R1.Automation.UI.core.Selenium.Base;
using System;
using TechTalk.SpecFlow;
using BoDi;
using R1.Automation.Reporting.Core;
using OpenQA.Selenium.Remote;

namespace R1.PAS.Automation.Hook

{
    [Binding]
    class Hooks
    {
        //Global Variable for Extend report

        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        private readonly ScenarioContext scenariocontext;
        private static string path;
        private static bool ExtentReportReq;
        public static IConfigurationRoot Configapp = CommonUtility.AppConfig;
        public static IConfigurationRoot TData = CommonUtility.TestData(Configapp["Connection:TestDataFolderName"] + "/" + Configapp["Connection:TestDataFileName"]);
        private static string Browser = Configapp["Connection:Browser"];
        private readonly IObjectContainer objectContainer;
        private RemoteWebDriver _driver;
        private DriverFactory _driverFactory = new DriverFactory();
        public CommonUtility commonUtility = new CommonUtility();
        public ExtentReport Report = new ExtentReport();

        public Hooks(ScenarioContext scenarioContext, IObjectContainer objectContainer)
        {
            scenariocontext = scenarioContext;
            this.objectContainer = objectContainer;
        }


        [BeforeScenario]
        public void TestSetup()
        {
            DriverController(Browser);
            if (ExtentReportReq)
            {
                scenario = featureName.CreateNode<Scenario>(scenariocontext.ScenarioInfo.Title);
            }
        }

        [AfterScenario]
        public void closeBrowser()
        {
            _driver.Quit();
        }

        [BeforeTestRun]
        public static void Initialize()
        {
            ExtentReportReq = bool.Parse(Configapp["Connection:ExtentReport"]);
            if (ExtentReportReq)
            {
                extent = ExtentReport.InitReport(Configapp["Connection:ExtentReportFolderName"]);
                path = CommonUtility.DeleteOldFolders(Configapp["Connection:ScreenShotsFolderName"], Configapp["Connection:NumberOfDaysToKeepScreenShots"]);
                path = CommonUtility.CreateFolder(path);
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            if (ExtentReportReq)
            {
                //Flush report once test completes
                extent.Flush();
            }
        }


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if (ExtentReportReq)
            {
                //Create dynamic feature name
                featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            }
        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            if (ExtentReportReq)
            {
                object TestResult = Report.ConfigSteps(scenariocontext);
                bool pass = bool.Parse(TData["ScreenShotsWithPassTestCases"]);
                bool fail = bool.Parse(TData["ScreenShotsWithFailTestCases"]);
                string Spath = commonUtility.TakeScreenshot(_driver, path);
                Report.InsertStepsInReport(scenariocontext, TestResult, Spath, scenario, pass, fail);
            }
        }

        private void DriverController(string BrowserType)
        {
            _driver = _driverFactory.InitDriver(BrowserType);
            _driver.Manage().Window.Maximize();
            int ip = int.Parse(Configapp["Connection:ImplicitWait"]);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ip);
            objectContainer.RegisterInstanceAs<RemoteWebDriver>(_driver);
        }
    }
}