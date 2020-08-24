using System;
using OpenQA.Selenium;
using R1.Automation.UI.core.Selenium.Extensions;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using SeleniumExtras.PageObjects;
using R1.Automation.UI.core.Commons;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using R1.PAS.Automation.Commons;
using OpenQA.Selenium.Remote;

namespace R1.PAS.Automation.Pages
{
    public class LocCasePage
    {
        public static IConfigurationRoot Configapp = CommonUtility.AppConfig;
        public RemoteWebDriver driver;
        string DSRNote = null;
        string initalRecommendation = null;
        IList<IWebElement> elementsList;
        Random objRandom = new Random();
        string noteText = null;
        string UploadedFileDetails, Note, RequestInfoNote, AccountDtls;
        string fileName= Configapp["Connection:TestDataFile"];
        int RandomNumber;
        long NewAccountDtls;
        CommonUtility CommonUtil = new CommonUtility();
        string spiner = "//h3[@text='loading']";
        readonly string AuthObtainedfiled = "//select[contains(@id,'_ucConsultInfo_ddlAuthorizationObtained_ddl')]";
        readonly string AccountNoLabel = "//span[contains(@id,'lblAccountNumber')]";
        readonly string DiagnosisAstric = "//b[@id='ddlPrimaryDiagnosisChildAstrix']";
        readonly string Phisiciancontact = "//select[contains(@id,'_ddlPhysicianContactedInfo_ddl')]";
        readonly string ContactCasemanager = "//select[contains(@id,'_ddlCMContactedInfo_ddl')]";

        public LocCasePage(RemoteWebDriver Driver)
        {
            PageFactory.InitElements(Driver, this);
            driver = Driver;
        }
        [FindsBy(How = How.CssSelector, Using = "a[class='rtbWrap']")]
        private IList<IWebElement> MenuList;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'_vsPhysicianAdvisoryServices')]")]
        private IWebElement LOCMandatoryErrorMsg;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_btnAdd')]/span")]
        private IWebElement SaveButton;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ucEpisodeInfo_ddlLocation_ddl')]")]
        private IWebElement Location;

        [FindsBy(How = How.XPath, Using = "//input[contains(@name,'LastName')]")]
        private IWebElement LastName;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'_txtPatFirstName_txt')]")]
        private IWebElement FirstName;

        [FindsBy(How = How.CssSelector, Using = "input[id*='txtMRN_txt']")]
        private IWebElement MrnNo;

        [FindsBy(How = How.CssSelector, Using = "input[id*='txtAccountNumber_txt']")]
        private IWebElement AccountNo;

        [FindsBy(How = How.XPath, Using = "//*[@id='notes']/div[1]")]
        private IWebElement NoteSection;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'_chkOvertime')]")]
        private IWebElement OvertimeCheckbox;

        [FindsBy(How = How.Id, Using = "overtime-status")]
        private IWebElement OvertimeMessage;

        [FindsBy(How = How.XPath, Using = "//Textarea[contains(@id,'_txtNotes')]")]
        private IWebElement NoteText;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id,'_btnAdd')]")]
        public IWebElement AddButton;

        [FindsBy(How = How.XPath, Using = "//input[contains(@name,'DOB')]")]
        private IWebElement Dob;

        [FindsBy(How = How.XPath, Using = "//span[contains(@id,'_lblResultLabel')]")]
        private IWebElement UpdateSuccessMessage;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ucConsultInfo_ddlAuthorizationObtained_ddl')]")]
        private IWebElement AuthObtained;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'_vsPhysicianAdvisoryServices')]")]
        private IWebElement ErrorMessageonSave;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'txtRequestSponsorPhone')]")]
        private IWebElement CmPhonenumber;

        [FindsBy(How = How.XPath, Using = "//select[contains(@name,'ddlRequestSponsor')]")]
        private IWebElement RequestSponser;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'ucEpisodeInfo_rdoDischargeStatus_0')]")]
        private IWebElement RadiobuttonInhouse;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ddlPayor_ddl')]")]
        private IWebElement Payor;

        [FindsBy(How = How.XPath, Using = "//span[text()='Check Out']")]
        private IWebElement Checkoutbutton;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ucConsultInfo_ddlAuthorizationFor_ddl')]")]
        private IWebElement AuthFor;

        [FindsBy(How = How.XPath, Using = "//input[contains(@name,'AdmissionDate')]")]
        private IWebElement PresentationDate;

        [FindsBy(How = How.XPath, Using = "//select[contains(@name,'ddlPatientType')]")]
        private IWebElement Currentorder;

        [FindsBy(How = How.XPath, Using = "//div[@class='timeline-top-info border-bottom']")]
        private IWebElement AddNoteSection;

        [FindsBy(How = How.XPath, Using = "//input[@id='fUploadDocument1']")]
        private IWebElement BrowseBtn;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id, 'ViewTask157_btnAdd')]")]
        private IWebElement UploadBtn;

        [FindsBy(How = How.XPath, Using = "//tr[@class='LOCGridTD']/td[contains(text(), 'Sample.pdf')]")]
        private IWebElement UploadedDocument;

        [FindsBy(How = How.XPath, Using = "//ul[@id='prm-menu']//li/a")]
        private IList<IWebElement> TabsList;

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@id, 'txtNotes')]")]
        private IWebElement NotesTextArea;

        [FindsBy(How = How.XPath, Using = "//a//span[contains(text(), 'Add Note')]")]
        private IWebElement AddNoteBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='timeline-top-info border-bottom']/span")]
        private IList<IWebElement> NotesData;

        [FindsBy(How = How.XPath, Using = "//iframe[contains(@id,'contentIframe')]")]
        private IWebElement PhysicianNotes;

       [FindsBy(How = How.XPath, Using = "//input[contains(@id,'rblPatientClassification_')][not(@checked='checked')][not(contains(@style,'display'))]/following-sibling::label")]
        private IList<IWebElement> RecomandationRadioButton;

        [FindsBy(How = How.XPath, Using = "//body")]
        private IWebElement PhysicianNotesTextBox;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'_rblReasonForNotInpatient_')]/following-sibling::label")]
        private IList<IWebElement> OutPatientReasons;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ucDignosisInfo_ddlPrimaryDiagnosis_ddl')]")]
        private IWebElement SystemDropDown;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ucDignosisInfo_ddlPrimaryDiagnosisChild_ddl')]")]
        private IWebElement DiagnosisDropDown;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id, 'InformationRequestType_primary_ddl')]")]
        private IWebElement RequestDD;

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@id,'ucInfoRequest_txtInfoText_txt')]")]
        private IWebElement RequestInfoTB;

        [FindsBy(How = How.XPath, Using = "//a[contains(@id,'ucInfoRequest_btnSend')]")]
        private IWebElement RequestInfoSendBtn;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'ucInfoRequest_divHistory')]")]
        private IWebElement RequestInfoHistoryBtn;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'divHistoryDetail')]/h4")]
        private IList<IWebElement> RequestInfoHistoryBtnDetails;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'divHistoryDetail')]/h4/p")]
         private IList<IWebElement> RequestInfoHistoryData;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id, 'btnCheckOutDuplicateUpdate')]")]
         private IWebElement DupContinueBtn;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id, 'WorklistPanel_grid_txtSearch')]")]
        private IWebElement SearchBox;

        [FindsBy(How = How.XPath, Using = "//span[contains(@id,'lblAccountNumber')]")]
        private IWebElement AccountNoLbl;

        [FindsBy(How = How.XPath, Using = "//span[@class='fa-stack fa-lg loc-notes']")]
        private IWebElement EnevelopeIcon;

        [FindsBy(How = How.XPath, Using = "//ul[@class= 'primary_structure']//span[contains(text(), 'Home')]")]
        private IWebElement HomeLink;

        [FindsBy(How = How.XPath, Using = "//span[text()='Send To PPTP']")]
        private IWebElement SendToPPTPbutton;

        [FindsBy(How = How.XPath, Using = "//table[contains(@id, 'grdWorklist')]//tr[2]")]
        private IWebElement firstAccountWL;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_dvComplete')]/div")]
        private IWebElement CompleteReleaseStatus;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ddlCMContactedInfo_ddl')]")]
        private IWebElement ContactCaseManager;

        [FindsBy(How = How.XPath, Using = "//select[contains(@id,'_ddlPhysicianContactedInfo_ddl')]")]
        private IWebElement PhysicianToPhysicianContact;

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@id,'_txtSecondaryDiscussionNote_txt')]")]
        private IWebElement SecondaryRecommendation;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_dvalert')]/div")]
        private IWebElement StatusSection;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_updTaskAction')]//a//span//span")]
        private IWebElement AccountCheckout;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_btnCheckOutDuplicateUpdate')]")]
        private IList<IWebElement> CheckoutWarning;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_lblLOCRecommendation')]")]
        private IWebElement InitialRecommendation;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_rblSecondaryClassification')]//tr//td")]
        private IList<IWebElement> SecondaryRecommendationRadioButton;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_lblAccountNumber')]")]
        public IWebElement NewAccount;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'btnRereviewOk')]")]
        private IWebElement NewWaringOK;

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@id,'txtReReviewNote')]")]
        private IWebElement NewWaringComments;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_dvRiskLevel')]/div")]
        private IWebElement CompleteDSRStatus;

        [FindsBy(How = How.XPath, Using = "//textarea[contains(@id,'txtLOCAuthorNotes')]")]
        private IWebElement DsrNotesText;

        [FindsBy(How = How.XPath, Using = "//div[contains(@id,'dvResult')]//span[contains(@id,'lblResultLabel')]")]
        private IWebElement CaseStatus;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_btnAddDSRNotesOk')]")]
        private IWebElement DSRok;

        [FindsBy(How = How.XPath, Using = "//ul[@id='prm-menu']/li/a")]
        private IList<IWebElement> AccountLevel;

        [FindsBy(How = How.XPath, Using = "//*[contains(@id,'_btnReleaseCaseOk')]")]
        private IWebElement RelesePopUP;

        public void FillPhysicianNotes()
        {

            string UserLastName = LastName.GetAttribute("value");
            string SelectedLabel = SelectRandomRadioButton(RecomandationRadioButton);
            if (SelectedLabel.Equals("INPATIENT"))
            {
                driver.SwitchTo().Frame(PhysicianNotes);
                PhysicianNotesTextBox.Clear();
                PhysicianNotesTextBox.SendKeys(UserLastName + " " + SelectedLabel);
                driver.SwitchTo().DefaultContent();
            }
            if (SelectedLabel.Equals("OUTPATIENT"))
            {
                string SelectedReason = SelectRandomRadioButton(OutPatientReasons);
                driver.SwitchTo().Frame(PhysicianNotes);
                PhysicianNotesTextBox.Clear();
                PhysicianNotesTextBox.SendKeys(UserLastName + " " + SelectedLabel + " " + SelectedReason);
                driver.SwitchTo().DefaultContent();
            }
            if (SelectedLabel.Equals("OBSERVATION"))
            {
                string SelectedReason = SelectRandomRadioButton(OutPatientReasons);
                driver.SwitchTo().Frame(PhysicianNotes);
                PhysicianNotesTextBox.Clear();
                PhysicianNotesTextBox.SendKeys(UserLastName + " " + SelectedLabel + " " + SelectedReason);
                driver.SwitchTo().DefaultContent();
            }

        }

        public void VerifyCaseCreation()
        {
            if (ErrorMessageonSave.VerifyElementDisplay())
            {
                Assert.False(ErrorMessageonSave.Text.Contains("Save failed. The following errors were found:"), "Case is not created");
            }
        }
        public void VerifyUpdateSuccess()
        {
            Assert.True(UpdateSuccessMessage.Text.Equals("Record Updated"), "Record is not updated");
        }
        /// <summary>Clicks the RadioButton And gets the label of selected radio button.</summary>
        /// <param name="ListofElements">The list of elements.</param>
        /// <returns>label of selected radio button</returns>
        public string SelectRandomRadioButton(IList<IWebElement> ListofElements)
        {
            Random r = new Random();
            int Index = r.Next(ListofElements.Count);
            driver.ClickOnElement(ListofElements[Index]);
            string LabelSelected = ListofElements[Index].Text;
            return LabelSelected;
        }
        public void SelectSystemDiagnosis()
        {
            SystemDropDown.SelectRandomValuefromDropdown();
            driver.WaitForVisibility(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), By.XPath(DiagnosisAstric));
            DiagnosisDropDown.SelectRandomValuefromDropdown();
        }

        public void ClickOvertimeCheckbox()
        {
            OvertimeCheckbox.Click();
        }


        public void VerifyOvertimeMessage(string Message)
        {
            Assert.True(OvertimeMessage.Text.Equals(Message), "Actual message is " + OvertimeMessage.Text + " Expected message is " + Message);
            Assert.True(PASCommons.GetColor(OvertimeMessage, "color").Equals("#6e9038"), "Message color is other than green");
        }
            

        
        public void VerifyEditablePageMode()
        {
            Assert.True(PhysicianNotes.VerifyElementDisplay(), "Page is uneditable");
        }

        public void VerifyUneditablePageMode()
        {
            Assert.False(PhysicianNotes.VerifyElementDisplay(), "Page is editable");
        }
        /// <summary>Enters all required field for creating  New case.</summary>
        public void EnterAllRequiredFiled()
        {
            LastName.SendKeys("TestLastName" + CommonUtility.GetRandomNumber(1, 1000));
            FirstName.SendKeys("TestFirstName" + CommonUtility.GetRandomNumber(1, 1000));
            Dob.SendKeys(PASCommons.AddYearstoCurrentDate(-18) + Keys.Enter);
            MrnNo.SendKeys("" + CommonUtility.GetRandomNumber(90000, 91000));
            List<string> PayorNOAuthRequired = new List<string>();
            PayorNOAuthRequired.Add("Medicaid");
            PayorNOAuthRequired.Add("Medicare");
            PayorNOAuthRequired.Add("Self Pay");
            string Payorname = Payor.SelectRandomValuefromDropdown();
            if (!PayorNOAuthRequired.Contains(Payorname))
            {
                driver.WaitForVisibility(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), By.XPath(AuthObtainedfiled));
                string so = AuthObtained.SelectRandomValuefromDropdown();
                if (so.Equals("Yes"))
                {
                    AuthFor.SelectRandomValuefromDropdown();
                }
            }
            driver.ScrollInView(RadiobuttonInhouse);
            driver.ClickOnElement(RadiobuttonInhouse);
            NewAccountDtls = CommonUtility.GetRandomNumber(999999, 1000500);
            AccountNo.SendKeys("" + NewAccountDtls);
            PresentationDate.SendKeys(DateTime.Now.ToShortDateString() + Keys.Enter);
            Location.SelectRandomValuefromDropdown();
            Currentorder.SelectRandomValuefromDropdown();
            RequestSponser.SelectRandomValuefromDropdown();
            CmPhonenumber.Click();
            CmPhonenumber.SendKeys("" + 999999999000);
        }


        public void VerifyCheckoutButton()
        {
            Assert.True(Checkoutbutton.VerifyElementDisplay(), "Checkout button does not exist");
        }

        /// <summary>
        /// This method is use for click  on menu's link like New, Export. 
        /// </summary>
        public void ClickLinks(string linkText)
        {
            Assert.True(MenuList.Count > 0, linkText + "MenuList does not exist ");
            bool found = false;
            for (int i = 0; i < MenuList.Count; i++)
            {
                if (MenuList[i].Text.Trim() == linkText.Trim())
                {
                    MenuList[i].Click();
                    found = true;
                    break;
                }
            }
            Assert.True(found, "LinkButton : " + linkText + " is not found");
        }
        /// <summary>
        /// This method is verify require messages when user click save button without entering any data in require fields.
        /// </summary>
        public void VerifyRequireMessage(Table table)
        {
            string requiredMesage = null;
            foreach (TableRow tr in table.Rows)
            {
                requiredMesage = tr[0].Trim();
                Assert.True(LOCMandatoryErrorMsg.Text.Contains(requiredMesage), requiredMesage + " is not found");
            }
        }
        /// <summary>
        ///  This method is verify the blank page after the click on New Button.
        /// </summary>
        public void VerifyBlanKPage()
        {
            string firstName = FirstName.GetAttribute("value");
            Assert.True(string.IsNullOrEmpty(firstName), "First Name Text Box is not blank");
            string MRN = MrnNo.GetAttribute("value");
            Assert.True(string.IsNullOrEmpty(MRN), "MRN Text Box is not blank");
            string AccountNumber = AccountNo.GetAttribute("value");
            Assert.True(string.IsNullOrEmpty(AccountNumber), "AccountNumber Text Box is not blank");
            Assert.True(SaveButton.VerifyElementDisplay(), "Page is not found");
        }
        /// <summary>
        /// This method is use for click on Save button.
        /// </summary>
        public void ClickSave()
        {
            driver.WaitForClickable(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), SaveButton);
            driver.ClickOnElement(SaveButton);
        }

        /// <summary>
        /// This method is verify the Note section
        /// </summary>
        public void VerifyNoteSection()
        {
            Assert.True(NoteSection.VerifyElementDisplay(), "Note section does not exist");
            Assert.True(NoteText.VerifyElementDisplay(), "Note Text does not exist");
        }
        /// <summary>
        /// This method is verify the enable property for edit functionality 
        /// </summary>
        public void VerifyEditableFunctionality()
        {
            Assert.True(NoteSection.Enabled, "Note section does not exist");
        }
        /// <summary>This method is user for Enter the Note</summary>
        public string EnterNote()
        {
            noteText = "TestNote" + objRandom.Next(100, 9999).ToString();
            NoteText.Clear();
            NoteText.SendKeys(noteText);
            return noteText;
        }

        /// <summary>This method is use for Add Note</summary>
        public void ClickAdd()
        {
            driver.WaitForClickable(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), AddButton);
            AddButton.Click();
            driver.ClickOnElement(AddButton);
        }
        /// <summary>This method is use for verify Added Note</summary>
        public void VerifyAddNote(string text)
        {
            PASCommons.ExplicitWaitConditions(spiner, driver);
            string recentNote = AddNoteSection.Text.Trim();
            Assert.True(recentNote.Contains(text), "Last entered note does not match");
        }
        public void ClickAccountTabs(string TabName)
        {
            Assert.True(TabsList.Count > 0, TabsList + "Tabs doesn't exist on page.");
            bool found = false;
            for (int i = 0; i < TabsList.Count; i++)
            {
                if (TabsList[i].Text.Trim().Contains(TabName.Trim()))
                {
                    driver.PageScrollUp();
                    TabsList[i].Click();
                    found = true;
                    break;
                }
            }
            Assert.True(found, "Tab Name : " + TabName + " is not found on page.");
        }
        /// <summary>
        /// Method is used to upload Test Document, path is being fetched from Appsettings.json file and file reside under TestData folder.
        /// </summary>
        public void UploadDocument()
        {
            string TestDataPath = CommonUtil.GetFolderPath("TestData") + fileName;
            BrowseBtn.SendKeys(TestDataPath);
            UploadBtn.Click();
        }

        /// <summary>
        /// This methods verifies if the document is uploaded properly or not by comparing the file names.
        /// </summary>
        public void VerifyUploadedDocument()
        {
            UploadedFileDetails = UploadedDocument.Text;
            Assert.Equal(fileName, UploadedFileDetails);
        }

        public void ClickAddNote()
        {
            AddNoteBtn.Click();
        }
        
        public void SelectValueFromRequestTypeDD()
        {
            RequestDD.SelectRandomValuefromDropdown();
            AccountDtls = AccountNoLbl.Text;
        }

        public string AddTestData()
        {
            RandomNumber = CommonUtility.GetRandomNumber(1, 99);
            RequestInfoNote = "Test note" + RandomNumber;
            RequestInfoTB.Clear();
            RequestInfoTB.SendKeys(RequestInfoNote);
            return RequestInfoNote;
        }

        public void ClickSendBtn()
        {
            RequestInfoSendBtn.Click();
            PASCommons.ExplicitWaitConditions(spiner, driver);
        }

        public void VerifyInfoRequestLink()
        {
            Assert.True(RequestInfoHistoryBtn.VerifyElementDisplay(), "Info Request link is not availble.");
            driver.ScrollInView(RequestDD);
        }

        public void VerifyInfoRequest(string text)
        {
            Assert.True(RequestInfoHistoryBtnDetails.Count > 0, RequestInfoHistoryBtnDetails + "Notes are not appearing.");
            bool RequestInfoDataFound = false;

            for (int i = 0; i < RequestInfoHistoryBtnDetails.Count; i++)
            {
                if (RequestInfoHistoryBtnDetails[i].VerifyElementDisplay())
                {
                    RequestInfoHistoryBtnDetails[i].Click();
                    if (RequestInfoHistoryData[i].Text.Contains(text))
                    {
                        RequestInfoDataFound = true;
                        break;
                    }
                }
            }
            Assert.True(RequestInfoDataFound, "RequestInfoNote : " + RequestInfoNote + " added by user wasn't found on page.");
        }

        public void WorklistSearchAccountManual()
        {
            SearchBox.SendKeys(AccountDtls);
            driver.PageScrollUp();
        }

        public void VerifyEnvelope()
        {
            PASCommons.ExplicitWaitConditions(spiner, driver);
            Assert.True(EnevelopeIcon.VerifyElementDisplay(), "Envelope icon is not appearing on account.");
        }

        public void EnvelopeHover()
        {
            driver.MouseHover(EnevelopeIcon);
        }

        public void VerifyEnvelopeIconText(string text)
        {
            string RequestNote = EnevelopeIcon.GetAttribute("data-original-title");
            Assert.True(RequestNote.Equals(text), "No details found when user hover on Envelope Icon.");
        }

        
        public void WorklistSearchNewCase()
        {
            SearchBox.SendKeys(NewAccountDtls.ToString());
        }

        public void VerifyNewCaseWorklist(string text)
        {
            long ActualAccntDtls= long.Parse(AccountNoLbl.Text);
            long LatestAccntDtls = long.Parse(text);
            Assert.True(ActualAccntDtls == LatestAccntDtls, "Account detail doesn't matched");
        }

        public void VerifyAccountLabel()
        {
            driver.WaitForVisibility(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), By.XPath(AccountNoLabel));
            driver.PageScrollUp();
            Assert.True(AccountNoLbl.VerifyElementDisplay(), "Account lable after Save operation does not exist");
            
        }

        /// <summary>
        /// This method is use to get the initial recodmmendation
        /// </summary>
        public string GetinitialRecommendation()
        {
            initalRecommendation = InitialRecommendation.Text;
            return initalRecommendation;
        }
        /// <summary>
        /// This method is use for select second recommendation
        /// </summary>
        public void SelectSecondaryReview()
        {
            elementsList = SecondaryRecommendationRadioButton;
            foreach (IWebElement element in elementsList)
            {
                if (element.Text.ToUpper().Trim().Equals(initalRecommendation.ToUpper().Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    element.Click();
                    break;
                }
            }
        }
        /// <summary>
        /// This method is use for enter second recommendation
        /// </summary>
        public void EnterSecondRecommendation()
        {
            SecondaryRecommendation.Clear();
            SecondaryRecommendation.SendKeys("Test " + initalRecommendation);
        }
        /// <summary>
        ///  This method is use for verify release button popup
        /// </summary>

        public void VerifyReleasePopUp()
        {
            Assert.True(RelesePopUP.VerifyElementDisplay(), "Release PopUp does not exist");
            
        }
        /// <summary>
        ///  This method is use for click on release button popup
        /// </summary>
        public void ClickOnReleasePopUp()
        {
            if (RelesePopUP.VerifyElementDisplay())
            {
                RelesePopUP.Click();
                PASCommons.ExplicitWaitConditions(spiner, driver);
            }
        }
        /// <summary>
        ///  This method is use for verify Dsr popup
        /// </summary>
        public void VerifyDSRPopUP()
        {
            Assert.True(DsrNotesText.VerifyElementDisplay(), "DSR Note pop up does not exist");
        }
        /// <summary>
        ///  This method is use for enter note in dsr popup
        /// </summary>
        public void DSRNotes()
        {
            if (DsrNotesText.VerifyElementDisplay())
            {
                RandomNumber = CommonUtility.GetRandomNumber(1, 9999);
                DSRNote = "DSR Notes By Automation" + RandomNumber;
                DsrNotesText.Clear();
                DsrNotesText.SendKeys(DSRNote);
            }
        }
        /// <summary>
        /// This method is use for click on on dsr ok button 
        /// </summary>
        public void ClickDsrOk()
        {
            if (DSRok.VerifyElementDisplay())
            {
                DSRok.Click();
                PASCommons.ExplicitWaitConditions(spiner, driver);
            }
        }
        /// <summary>
        ///  verify the currenet status 
        /// </summary>
        public void CaseCurrentStatusText(string message)
        {
            PASCommons.ExplicitWaitConditions(spiner, driver);
            if (CaseStatus.VerifyElementDisplay())
            {
                Assert.True(CaseStatus.Text.Contains(message), "Message does not exist");
            }
        }
        /// <summary>
        /// This method is use for click on tab like recommendation , initial 
        /// </summary>
        public void ClickInitialTab(string tabname)
        {
            elementsList = AccountLevel.ToList();
            foreach (IWebElement element in elementsList)
            {
                if (element.Text.Trim().Equals(tabname.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    element.Click();
                    break;
                }
            }
        }
        /// <summary>
        /// This method is use for verify complete relase status
        /// </summary>
        public void VerifyCompleteReleaseStatus(string status)
        {
            string content = CompleteReleaseStatus.Text.ToUpper().Trim();
            Assert.True(content.Contains(status.ToUpper().Trim()), status + " does not exist");
        }
        /// <summary>
        /// This method is use for click on dupliate warning popup of checkout button
        /// </summary>
        public void ClickOnCheckOutButtonWithDuplicate()
        {
            AccountCheckout.Click();
            if (CheckoutWarning[0].VerifyElementDisplay())
            {
                CheckoutWarning[0].Click();
                PASCommons.ExplicitWaitConditions(spiner, driver);
            }
        }
        /// <summary>
        /// This method is use select any value from contact dropdown
        /// </summary>
        public void SelectContactCaseManage()
        {
            driver.WaitForVisibility(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), By.XPath(ContactCasemanager));
            ContactCaseManager.SelectRandomValuefromDropdown();
        }
        /// <summary>
        /// This method is use select any value from physicianContact dropdown
        /// </summary>
        public void SelectPhysicianToPhysicianContact()
        {
            driver.WaitForVisibility(int.Parse(Configapp["Connection:ExplicitlyWaitTime"]), By.XPath(Phisiciancontact));
            PhysicianToPhysicianContact.SelectRandomValuefromDropdown();
        }
        /// <summary>
        /// This method is use for editable mode of dsrnote text box
        /// </summary>
        public void VerifyEditableAccountDSR()
        {
            Assert.True(DsrNotesText.Enabled, "DSR account is not editable");
        }
        /// <summary>
        /// This method is use for verify the complete status of dsr account
        /// </summary>
        public void VerifyDSRCompleteStatus(string status)
        {
            string content = CompleteDSRStatus.Text.ToUpper().Trim();
            Assert.True(content.Contains(status.ToUpper().Trim()), status + " does not exist");
        }
        /// <summary>
        /// This method is use for handle popup on new case
        /// </summary>
        public void NoteReviewComments()
        {
            if (NewWaringComments.VerifyElementDisplay())
            {
                RandomNumber = CommonUtility.GetRandomNumber(1, 9999);
                string newCaseReview = "New case review Notes By Automation" + RandomNumber;
                NewWaringComments.Clear();
                NewWaringComments.SendKeys(newCaseReview);
                if (NewWaringOK.VerifyElementDisplay())
                {
                    NewWaringOK.Click();
                    PASCommons.ExplicitWaitConditions(spiner, driver);
                }
            }
        }
    }
}