using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using R1.Automation.UI.core.Selenium.Extensions;
using System;
using System.Collections.Generic;
using Xunit;
using SeleniumExtras.PageObjects;
using R1.Automation.UI.core.Commons;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf;
using OpenQA.Selenium.Remote;
using System.IO;

namespace R1.PAS.Automation.Commons
{
    public class PASCommons
    {
        private RemoteWebDriver driver;
        Actions action;
        public static IConfigurationRoot Configapp = CommonUtility.AppConfig;
        IWebElement element, table;
        List<string> actionText = null;
        public string tableHeader = "tr[class='wlHeader']";
        public string tableRow = "//tr[@class='wlItem']";
        public string spiner = "//h3[@text='loading']";
        readonly string PreLoader = "//div[@id='jquery-loader-background']";
        int count, index;
        string returntext, rightPanelAccountLabel = null;
        string newAccount = null;
        IList<IWebElement> elementsList;
        DataTable WorklistDataTable;
        public PASCommons(RemoteWebDriver Driver)
        {
            PageFactory.InitElements(Driver, this);
            driver = Driver;

        }


        /*------------------------- LOGIN PAGE XPath ---------------------------------------------------*/

        [FindsBy(How = How.XPath, Using = "//input[contains(@id, 'DNN_txtUsername')]")]
        private IWebElement username;

        [FindsBy(How = How.CssSelector, Using = "table[class='wlTable']")]
        private IWebElement WorklistTableData;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id, 'DNN_txtPassword')]")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, 'DNN_cmdLogin')]")]
        private IWebElement loginBtn;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, 'enhancedLoginLink')]")]
        private IWebElement logoutBtn;

        /*------------------------- HOME PAGE USEFUL LINKS XPath ---------------------------------------------------*/

        [FindsBy(How = How.XPath, Using = "//ul[@class= 'primary_structure']//span[contains(text(), 'Home')]")]
        private IWebElement HomeLink;

        [FindsBy(How = How.XPath, Using = "//ul[@class= 'primary_structure']//span[contains(text(), 'Our Solutions')]")]
        private IWebElement OurSolutionLink;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Contact Us')]")]
        private IWebElement ContactUs;

        /*------------------------- SELECT FACILITY AND CLICK   XPath ---------------------------------------------------*/

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, '_lnkChangeLoc')]")]
        private IWebElement facilityList;

        [FindsBy(How = How.XPath, Using = "//input[contains(@type,'input')]")]
        private IWebElement facility;

        /*------------------------- SELECT WORKLIST AND CLICK SUB WORKLIST ---------------------------------------------------*/

        [FindsBy(How = How.XPath, Using = "//ul[@class='primary_structure']/li")]
        private IList<IWebElement> MainMenu;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_lblAccountNumber')]")]
        private IWebElement RightPanelAccountLabel;

        [FindsBy(How = How.XPath, Using = "//ul[@class='primary_structure']/li//li/a")]
        private IList<IWebElement> SubMenu;

        [FindsBy(How = How.XPath, Using = "//li[contains(@class, 'item-3')]")]
        private IWebElement worklist;

        [FindsBy(How = How.XPath, Using = "//li[contains(@class, 'item-3')]//span[contains(text(), 'Level of Care')]")]
        private IWebElement worklistSelect;

        /*------------------------- WORKLIST LEVEL XPath ---------------------------------------------------*/

        [FindsBy(How = How.XPath, Using = "//table[contains(@id, 'grdWorklist')]//tr[2]")]
        private IWebElement firstAccountWL;

        [FindsBy(How = How.XPath, Using = "//tr[contains(@class, 'wlItem')]")]
        private IList<IWebElement> workListData;

        [FindsBy(How = How.XPath, Using = "//table[contains(@id,'grdWorklist')]//tr[@class='wlItem']")]
        private IList<IWebElement> workListDataCount;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id, '_ddlColumns')]")]
        private IWebElement SearchFilter1;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id, '_ddlClauses')]")]
        private IWebElement SearchFilter2;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id, 'WorklistPanel_grid_txtSearch')]")]
        private IWebElement SearchBox;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, 'WorklistPanel_grid_btnFilter')]")]
        private IWebElement SearchBtn;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, 'WorklistPanel_grid_lnkClear')]")]
        private IWebElement clearSearchBtn;

        /// <summary>
        ///  ACCOUNT LEVEL XPath
        /// </summary>

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'_task_updTaskAction')]//*[(text()='Check Out')]")]
        private IList<IWebElement> VerifyCheckOutButton;

        [FindsBy(How = How.XPath, Using = "//span[contains(@id,'lblAccountNumber')]")]
        private IWebElement AccountNoLbl;

        [FindsBy(How = How.XPath, Using = "//div[@id='btn-update']//span")]
        private IList<IWebElement> UpdateDelete;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'chkOvertime')]")]
        private IWebElement CheckOverTime;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'pnlCheckOutDuplicate')]//div[@class='modal-footer']//input[contains(@id,'_btnCheckOutDuplicateUpdate')]")]
        private IList<IWebElement> DuplicateCheckoutContinue;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'dvResult')]//span[contains(@id,'lblAddUpdate')]")]
        private IWebElement UpdateStatus;

        [FindsBy(How = How.XPath, Using = "//div[@id='overtime-status']")]
        private IWebElement OverTime;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id,'cmdPrintPASForm')]")]
        private IWebElement PrintForm;

        
        [FindsBy(How = How.XPath, Using = "//table[contains(@id,'grdWorklist')]//tr[contains(@class,'commercialCase')]")]
        private IList<IWebElement> workListDsrCount;

      [FindsBy(How = How.XPath, Using = "//a[contains(@class,'btn rghtpane')]/descendant::i/following-sibling::span")]
       private IList<IWebElement> Buttons;

        [FindsBy(How = How.XPath, Using = "//a[contains(@class,'btn turnon')]/descendant::i/following-sibling::span")]
        private IList<IWebElement> UpdateContactButtononDSRWL;

        [FindsBy(How = How.XPath, Using = "//table[contains(@id,'grdWorklist')]//tr[@class='wlItem']//td[@class='number']//span")]
        private IWebElement workListFirstRow;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_lblNumber')]")]
        private IWebElement DsrworkListFirstRow;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'lblNumber')]")]
        private IWebElement GlobalLocFirstRow;

        [FindsBy(How = How.XPath, Using = "//table[contains(@id,'grdWorklist')]//tr[contains(@class,' commercialCase')]")]
        private IList<IWebElement> GlobalLocWorklist;


        /// <summary>Gets the color of the cssvalues like background.</summary>
        /// <param name="iwe">WebElement.</param>
        /// <param name="cssValue">The CSS value whose color is to be fetched.</param>
        /// <returns>Color in Hex format</returns>
        public static string GetColor(IWebElement iwe, string cssValue)
        {
            var value = iwe.GetCssValue(cssValue);
            string[] colours = value.Replace("rgba(", "").Replace(")", "").Split(",");
            int r = int.Parse(colours[0]);
            int g = int.Parse(colours[1]);
            int b = int.Parse(colours[2]);
            int a = int.Parse(colours[3]);
            Color myColor = Color.FromArgb(a, r, g, b);
            string hex = "#" + myColor.R.ToString("X2")+ myColor.G.ToString("X2") + myColor.B.ToString("X2");
            string hexlower = hex.ToLower();
            return hexlower;
        }
        public string ClickUncheckedRow(string ColumnName)
        {
            WorklistDataTable = null;
            returntext = null;
            index = -1;
            table = null;
            WorklistDataTable = new DataTable();
            table = WorklistTableData;
            IList<IWebElement> TH = table.FindElements(By.CssSelector(tableHeader));
            IList<IWebElement> TDlist = TH[0].FindElements(By.TagName("td"));
            List<string> Headings = TDlist.Select(e => e.Text).ToList();
            index = Headings.IndexOf(ColumnName);
            Assert.True(index > -1, "Column Name(" + ColumnName + ") not found");
            table = null;
            table = WorklistTableData;
            ExplicitWaitConditions(spiner, driver);
            IList<IWebElement> Rows = table.FindElements(By.XPath(tableRow));
            IList<IWebElement> Cols = Rows[0].FindElements(By.TagName("td"));
            returntext = Cols[index].Text;            
            Rows[0].Click();
            return returntext;
        }

       
        public void WorklistFilterSearch(string dropDownValue, string operatorVal,string value)
        {
            SelectFilterValueHavingEqualValue(dropDownValue);
            SearchFilter2.ClickDropDownValuebyContainingText(operatorVal);
            SearchBox.SendKeys(value);
        }
        public void SelectFilterValueHavingEqualValue(string dropDownValue)
        {
            SelectElement element = new SelectElement(SearchFilter1);
            foreach (var option in element.Options)
            {
                if (option.Text.Trim().Equals(dropDownValue))
                {
                    option.Click();
                }
            }
        }
        public bool VerifyUsernameTextBox()
        {
            return username.VerifyElementDisplay();
        }
        public void UserLogin(string Username, string Pass)
        {
            username.SendKeys(Username);
            password.SendKeys(Pass);
        }
        public void ClickLogin()
        {
            loginBtn.Click();
        }

        /// <summary>
        /// verify Print form funtionality
        /// </summary>
        public void PrintformButtonExists()
        {
            Assert.True(PrintForm.VerifyElementDisplay(), "Print Form Button Is Not Displayed");
        }

        
        public void PrintformEnable()
        {
            Assert.True(PrintForm.Enabled, "Print Form Button Is Not Enabled");
        }
        public void PrintFormClick()
        {
            driver.ClickOnElement(PrintForm);
        }


        /// <summary>Extracts the text from PDF.</summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The PDF contents</returns>
        public static string ExtractTextFromPDF(string filePath)
        {
            PdfReader pdfReader = new PdfReader(filePath);
            PdfDocument pdfDoc = new PdfDocument(pdfReader);
            string pageContent = null;
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                pageContent += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), new SimpleTextExtractionStrategy());
            }
            pdfDoc.Close();
            pdfReader.Close();
            return pageContent;
        }
                
        public void VerifyPdfOpen()
        {
            var browerTabs = driver.WindowHandles;
            Assert.True(browerTabs.Count > 0, "Form not open in new Window");
            driver.SwitchTo().Window(browerTabs[1]);
            string PdfUrl = driver.Url;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Assert.True(PdfUrl.Contains(".pdf"), "PDF is not open");
            driver.Close();
            driver.SwitchTo().Window(browerTabs[0]);
        }
       
        public bool VerifyLogoutVisibility()
        {
            return logoutBtn.VerifyElementDisplay(); ;
        }
        public void ClickLogout()
        {
            logoutBtn.Click();
        }
        public void ClickR1Home()
        {
            HomeLink.Click();
        }
        public void ClickOurSolution()
        {
            OurSolutionLink.Click();
        }
        public bool VerifyHomePageVisibility()
        {
            return HomeLink.VerifyElementDisplay();
        }
        public void SelectFacility(string facilityName)
        {
            action = new Actions(driver);
            driver.WaitForClickable(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), facilityList);
            action.DoubleClick(facilityList).Perform();
            facility.SendKeys(facilityName + Keys.Enter);
           
        }
        public void SelectSubWorklist(string Mmenu, string Smenu)
        {
            action = new Actions(driver);
            for (int i = 1; i < MainMenu.Count - 1; i++)
            {
                if (MainMenu[i].Text.Equals(Mmenu, StringComparison.OrdinalIgnoreCase))
                {
                    action.MoveToElement(MainMenu[i]).Perform();

                    for (int j = 0; j < SubMenu.Count; j++)
                    {
                        if (SubMenu[j].Text.Equals(Smenu, StringComparison.OrdinalIgnoreCase))
                        {
                            SubMenu[j].Click();
                            break;
                        }
                    }
                }
            }
        }
        
        public void VerifyWorklistExists()
        {
            count = workListDataCount.Count;
            Assert.True(count > 0, "Worklist doesn't exists");
        }
        public void VerifyWorklistDoesNotExists()
        {
            count = workListDataCount.Count;
            Assert.True(count == 0, "Worklist exists");
        }
        
        /// <summary>Adds the years to current date.</summary>
        /// <param name="years">The years.takes signed int</param>
        /// <returns>New Date</returns>
        public static string AddYearstoCurrentDate(int years)
        {
            string Datewithyears = DateTime.Now.AddYears(years).ToShortDateString();
            return Datewithyears;
        }
        public void ClickSearchBtn()
        {
            SearchBtn.Click();
            PASCommons.ExplicitWaitConditions(spiner, driver);


        }
        public void ClickFirstAccount()
        {
            firstAccountWL.Click();
        }
        public void ClickOnUnassignedAccount()
        {
            foreach (IWebElement unassignedAccount in workListData)
            {
                unassignedAccount.Click();
                break;
            }
        }
        /// <summary>
        /// Explict Wait Logic
        /// </summary>
        /// <param name="elementPath"></param>
        /// <param name="driver"></param>
        public static void ExplicitWaitConditions(String elementPath, RemoteWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(int.Parse(Configapp["Connection:ExplicitlyWaitTime"])));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(elementPath)));
        }

        
        public string GetAccount()
        {
            rightPanelAccountLabel = RightPanelAccountLabel.Text.Trim();
            return rightPanelAccountLabel;
        }
        public void SearchAccountInWorklist(string text)
        {
            SelectFilterValueHavingEqualValue("Account Number");
            SearchFilter2.ClickDropDownValuebyContainingText("=");
            SearchBox.SendKeys(text);
        }
        /// <summary>
        /// This method is use for internally calling click on complete, release ,update and contact button
        /// </summary>
        public void InternallyCallingAction(IList<IWebElement> listofActions, string buttonText)
        {
            Boolean check = false;
            int index = 0;
            actionText = listofActions.Select(details => details.Text.Trim()).ToList<string>();
            
            foreach (string lstValue in actionText)
            {
                if (lstValue.Trim().Equals(buttonText.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    listofActions[index].Click();
                    check = true;
                    break;
                }
                index++;
            }
            Assert.True(check, "Unable to find : " + buttonText);
        }

        public void ClickAccountRightPanel(string buttonText)
        {
            elementsList = Buttons.ToList();
            InternallyCallingAction(elementsList, buttonText);
            ExplicitWaitConditions(spiner, driver);
        }
        

        public void ClickUpdateOrContactButtonsDSRWL(string buttonname)
        {
            InternallyCallingAction(UpdateContactButtononDSRWL, buttonname);
            ExplicitWaitConditions(spiner, driver);
        }
        
        /// <summary>
        /// This method is use for verify global dsr worklist
        /// </summary>
        public void VerifyDSRWorklistExists()
        {
            count = workListDsrCount.Count;
            Assert.True(count > 0, "Worklist doesn't exists");
        }
        /// <summary>
        /// This method is use for verify global loc worklist
        /// </summary>
        public void VerifyGlobalLocWorklistExists()
        {
            count = GlobalLocWorklist.Count;
            Assert.True(count > 0, "Worklist doesn't exists");
        }
        
        
        /// <summary>
        /// This method is use click on first account of loc 
        /// </summary>
        public void ClickLocWorklistFirstRow()
        {
           
            workListFirstRow.Click();
            PASCommons.ExplicitWaitConditions(spiner, driver);

        }
        /// <summary>
        /// This method is use click on first account of dsr of global worklist 
        /// </summary>
        public void ClickDSRFirstRow()
        {
            DsrworkListFirstRow.Click();
        }
        /// This method is use click on first account of loc of global worklist 
        /// </summary>
        public void ClickGlobalLocFirstRow()
        {
            GlobalLocFirstRow.Click();
        }

    }
}
