using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Remote;
using R1.Automation.UI.core.Commons;
using R1.PAS.Automation.Commons;
using R1.PAS.Automation.Pages;
using R1.PAS.Automation.StepDefinitions;
using TechTalk.SpecFlow;

namespace R1.PAS.Automation.StepDefinition
{
    [Binding]
    public class WorkListSteps 
    {
        private readonly ScenarioContext scenariocontext;
        private readonly RemoteWebDriver driver;
        LocCasePage LocCasePage => new LocCasePage(driver);
        PASCommons commons => new PASCommons(driver);
        CommonUtility commonUtil = new CommonUtility();
        private static IConfigurationRoot Configapp = CommonUtility.AppConfig;
        private static IConfigurationRoot TData = CommonUtility.TestData(Configapp["Connection:TestDataFolderName"] + "/" + Configapp["Connection:TestDataFileName"]);
        private readonly SharedBindingsData SData = new SharedBindingsData();
        public WorkListSteps(ScenarioContext scenarioContext, RemoteWebDriver _driver)
        {
            driver = _driver;
            scenariocontext = scenarioContext;
        }

        [Then(@"user should click on logout")]
        public void ThenUserShouldClickOnLogout()
        {
            commons.ClickLogout();
        }


        [Scope(Feature = "PASLocBVTAdmin")]
        [Given(@"user logs into PAS Application")]
        public void GivenUserLogsIntoPASApplicationAdmin()
        {
            driver.Navigate().GoToUrl(Configapp["Connection:PrivUrl"]);
            if (commons.VerifyUsernameTextBox())
            {
                string accountuser = TData["LocAdminUsername"];
                string passwd = TData["LocAdminPassword"];
                commons.UserLogin(accountuser, passwd);
                commons.ClickLogin();
            }
        }

        [Scope(Feature = "Automate PAS LOC BVT")]
        [Given(@"user logs into PAS Application")]
        public void GivenUserLogsIntoPASApplication()
        {
            driver.Navigate().GoToUrl(Configapp["Connection:PubUrl"]);
            if (commons.VerifyUsernameTextBox())
            {
                string accountuser = TData["LocCMUsername"];
                string passwd = TData["LocCMPassword"];
                commons.UserLogin(accountuser, passwd);
                commons.ClickLogin();
            }
        }
        [Given(@"user is on PAS home page")]
        public void GivenUserIsOnPASHomePage()
        {
            commons.VerifyHomePageVisibility();
        }

        [When(@"user selects the facility")]
        public void WhenUserSelectTheFacility()
        {
            commons.SelectFacility(commonUtil.GetTestData("Facility"));
        }

        [When(@"user navigates to ""(.*)"" and ""(.*)""")]
        public void WhenUserNavigatesToAnd(string Mmenu, string Smenu)
        {
            commons.SelectSubWorklist(Mmenu, Smenu);
        }

        [Then(@"worklist should open")]
        public void ThenWorklistShouldOpen()
        {
            commons.VerifyWorklistExists();
        }

        [When(@"user clicks on search button")]
        public void WhenUserClicksOnSearchButton()
        {
            commons.ClickSearchBtn();

        }

        [When(@"user selects any case in the LOC worklist")]
        public void WhenUserSelectsAnyCaseInTheLOCWorklist()
        {
            commons.ClickOnUnassignedAccount();
        }

        [When(@"user clicks on ""(.*)"" from the worklist")]
        public void WhenUserClicksOnFromTheWorklist(string submenu)
        {
            LocCasePage.ClickLinks(submenu);
        }

        [Then(@"consult page should open in uneditable mode")]
        public void ThenConsultPageShouldOpenInUneditableMode()
        {
            LocCasePage.VerifyUneditablePageMode();
        }
        [Then(@"consult page should open in editable mode")]
        public void ThenConsultPageShouldOpenInEditableMode()
        {
            LocCasePage.VerifyEditablePageMode(); ;
        }
        [When(@"selects a PAS Recommendation and enters lastName ,PAS recommendation in PhysianNotes")]
        public void WhenSelectsAPASRecommendationAndEntersLastNamePASRecommendationInPhysianNotes()
        {
            LocCasePage.FillPhysicianNotes();
        }
        [When(@"selects system,daignosis")]
        public void WhenSelectsSystemDaignosis()
        {
            LocCasePage.SelectSystemDiagnosis();
        }
        [Then(@"Record Update message should appear")]
        public void ThenRecordUpdateMessageShouldAppear()
        {
            LocCasePage.VerifyUpdateSuccess();
         

        }
        [When(@"user clicks on ""([^""]*)"" button")]
        public void WhenUserClicksOnButton(string ButtonName)
        {
            commons.ClickAccountRightPanel(ButtonName);
        }

        [When(@"user clicks on ""([^""]*)"" button DSR Worklist")]
        public void WhenUserClicksOnButtonDSRWorklist(string buttonname)
        {
            commons.ClickUpdateOrContactButtonsDSRWL(buttonname);
        }

        /// <summary>
        /// Print Form Functionality
        /// </summary>

        [When(@"user verify for print form in the physician advisor notes section")]
        public void WhenUserVerifyForPrintFormInThePhysicianAdvisorNotesSection()
        {
            commons.PrintformButtonExists();
        }

        [Then(@"print form button should be enabled")]
        public void ThenPrintFormButtonShouldBeEnabled()
        {
            commons.PrintformEnable();
        }

        [When(@"user click on print form  button under physician advisor notes section")]
        public void WhenUserClickOnPrintFormButtonUnderPhysicianAdvisorNotesSection()
        {
            commons.PrintFormClick();
        }

        [Then(@"displays the pdf of completed case")]
        public void ThenDisplaysThePdfOfCompletedCase()
        {
            commons.VerifyPdfOpen();
        }

        /*.........................................................*/

        [When(@"user clicks on unchecked row having ""(.*)"" column")]
        public void WhenUserClicksOnUncheckedRowHavingColumn(string column)
        {
            commons.ClickUncheckedRow(column);
        }

        [When(@"user selects ""([^""]*)"" with ""([^""]*)"" operator having value ""([^""]*)""")]
        public void WhenUserSelectsWithOperatorHavingText(string filterVal, string operatorVal,string value)
        {
            commons.WorklistFilterSearch(filterVal, operatorVal, value);
        }

        [Then(@"blank consult page should open")]
        public void ThenBlankConsultPageShouldOpen()
        {
            LocCasePage.VerifyBlanKPage();
        }

        [When(@"user without entering any data clicks on save button")]
        public void WhenUserWithoutEnteringAnyDataClicksOnSaveButton()
        {
            LocCasePage.ClickSave();
        }

        [Then(@"following errors messages are found :")]
        public void ThenFollowingErrorsMessagesAreFound(Table table)
        {
            LocCasePage.VerifyRequireMessage(table);
        }

        [Then(@"Notes section should open for editing")]
        public void ThenNotesSectionShouldOpenForEditing()
        {
            LocCasePage.VerifyEditableFunctionality();
        }
        [When(@"user enters text into Notes box")]
        public void WhenUserEntersTextIntoNotesBox()
        {
            SData.NoteText=LocCasePage.EnterNote();
        }
        
        /*[When(@"user enters text into Notes box and user click Add Note")]
        public void WhenUserEntersTextIntoNotesBoxAndUserClickAddNote()
        {
            LocCasePage.EnterNote();
            LocCasePage.ClickAdd();
        }*/
        [Then(@"Notes should be saved")]
        public void ThenNotesShouldBeSaved()
        {
            LocCasePage.VerifyAddNote(SData.NoteText);
        }

        [When(@"user enters all required fields")]
        public void WhenUserEntersAllRequiredFields()
        {
            LocCasePage.EnterAllRequiredFiled();
        }

        [When(@"user checks overtime checkbox")]
        public void WhenUserChecksOvertimeCheckbox()
        {
            LocCasePage.ClickOvertimeCheckbox();
        }
        [Then(@"green background should be displayed with message ""([^""]*)""")]
        public void ThenGreenBackgroundShouldBeDisplayedWithMessage(string message)
        {
            LocCasePage.VerifyOvertimeMessage(message);
            
        }

        [Then(@"checkout button should appear")]
        public void ThenCheckoutButtonShouldAppear()
        {
            LocCasePage.VerifyCheckoutButton();
            commons.GetAccount();
        }

        [When(@"user clicks on Save button")]
        public void WhenUserClicksOnSaveButton()
        {
            LocCasePage.ClickSave();
        }

        [When(@"user clicks on the ""(.*)"" tab")]
        public void WhenUserClicksOnTheTab(string name)
        {
            LocCasePage.ClickAccountTabs(name);
        }

       [When(@"user clicks \+Add Notes")]
        public void WhenUserClicksAddNotes()
        {
            LocCasePage.ClickAddNote();
        }

       [When(@"user clicks on Choosefile button to upload the document")]
        public void WhenUserClicksOnChoosefileButtonToUploadTheDocument()
        {
            LocCasePage.UploadDocument();
        }

        [Then(@"document should be successfully uploaded")]
        public void ThenDocumentShouldBeSuccessfullyUploaded()
        {
            LocCasePage.VerifyUploadedDocument();
        }


        [When(@"User select value from Request Type dropdown")]
        public void WhenUserSelectValueFromRequestTypeDropdown()
        {
            LocCasePage.SelectValueFromRequestTypeDD();
        }

        [When(@"user enter text into box below")]
        public void WhenUserEnterTextIntoBoxBelow()
        {
            SData.TestData=LocCasePage.AddTestData();
        }

        [When(@"user click on the Send button")]
        public void WhenUserClickOnTheSendButton()
        {
            LocCasePage.ClickSendBtn();
        }

        [When(@"a link should appear below the Info request section")]
        public void ThenALinkShouldAppearBelowTheInfoRequestSection()
        {
            LocCasePage.VerifyInfoRequestLink();
        }

        [Then(@"added text should appear")]
        public void ThenAddedTextShouldAppear()
        {
            LocCasePage.VerifyInfoRequest(SData.TestData);
        }

        [Then(@"an envelope icon should appear in Flag column")]
        public void ThenAnEnvelopeIconShouldAppearInFlagColumn()
        {
            LocCasePage.VerifyEnvelope();
        }

        [When(@"user hovers on envelope icon")]
        public void WhenUserHoversOnEnvelopeIcon()
        {
            LocCasePage.EnvelopeHover();
        }

        [When(@"user searches the account in worklist")]
        public void WhenUserSearchesTheAccountInWorklist()
        {
            commons.SearchAccountInWorklist(SData.AccountNo);
        }

        [When(@"user gets the accountnumber")]
        public void WhenUserGetsTheAccountnumber()
        {
            SData.AccountNo = commons.GetAccount();
        }


        [Then(@"added text should appear on icon")]
        public void ThenAddedTextShouldAppearOnIcon()
        {
            LocCasePage.VerifyEnvelopeIconText(SData.TestData);
        }

        [When(@"user clicks on the Release button from popup alert")]
        public void WhenUserClicksOnTheReleaseButtonFromPopupAlert()
        {
            LocCasePage.VerifyReleasePopUp();
            LocCasePage.ClickOnReleasePopUp();
        }
        
        [Then(@"case should be created")]
        public void ThenCaseShouldBeCreate()
        {
            LocCasePage.VerifyCaseCreation();
        }

        [When(@"user clicks on the duplicate checkout button if exist after checkout")]
        public void WhenUserClicksOnTheDuplicateCheckoutButtonIfExistAfterCheckout()
        {
            LocCasePage.ClickOnCheckOutButtonWithDuplicate();
        }

        [Then(@"case added above should be displayed in worklist")]
        public void ThenCaseAddedAboveShouldBeDisplayedInWorklist()
        {
            commons.ClickLocWorklistFirstRow();
            LocCasePage.VerifyNewCaseWorklist(SData.AccountNo);
        }


        [Then(@"AccountLabel should appear")]
        public void ThenAccountLabelShouldAppear()
        {
            LocCasePage.VerifyAccountLabel();
            
        }

        [Then(@"Send To DSR pop up should appear")]
        public void ThenSendToDSRPopUpShouldAppear()
        {
            LocCasePage.VerifyDSRPopUP();
        }

        [When(@"user enter the note in Send To DSR pop up")]
        public void WhenUserEnterTheNoteInSendToDSRPopUp()
        {
            LocCasePage.DSRNotes();
            LocCasePage.ClickDsrOk();
        }
        [Then(@"""(.*)"" message should appear")]
        public void ThenMessageShouldAppear(string message)
        {
            LocCasePage.CaseCurrentStatusText(message);
        }

        [When(@"on Release case warning pop show on after clicks of Release button")]
        public void WhenOnReleaseCaseWarningPopShowOnAfterClicksOfReleaseButton()
        {
            LocCasePage.VerifyReleasePopUp();
            LocCasePage.ClickOnReleasePopUp();
        }

        [When(@"user go to ""(.*)""")]
        public void WhenUserGoTo(string tabName)
        {
            LocCasePage.ClickInitialTab(tabName);
        }
        [When(@"select a recommendation same as Initial LOC Recommendation from ""(.*)"" and add a note")]
        public void WhenSelectARecommendationSameAsInitialLOCRecommendationFromAndAddANote(string tabName)
        {
            LocCasePage.ClickInitialTab(tabName);
        }

        [When(@"Select any value for INDICATION FOR PHYSICIAN TO PHYSICIAN CONTACT \(I\.E\. CHANGE IN STATUS\)\? from dropdown list")]
        public void WhenSelectAnyValueForINDICATIONFORPHYSICIANTOPHYSICIANCONTACTI_E_CHANGEINSTATUSFromDropdownList()
        {
            LocCasePage.SelectPhysicianToPhysicianContact();
        }

        [When(@"user clicks on account")]
        public void WhenUserClicksOnAccount()
        {
            commons.ClickLocWorklistFirstRow();
        }
        [Then(@"case should get ""(.*)""")]
        public void ThenCaseShouldGet(string status)
        {
            LocCasePage.VerifyCompleteReleaseStatus(status);
        }
        [When(@"user add second recommendation")]
        public void WhenUserAddSecondRecommendation()
        {
            LocCasePage.EnterSecondRecommendation();
        }
        /*[When(@"user clicks on update send confirm release update contact ""(.*)"" button")]
        public void WhenUserClicksOnUpdateSendConfirmReleaseUpdateContactButton(string button)
        {
           // commons.ClickSencondaryUpdateContact(button);
           // commons.ClickSencondaryCompleteRelease(button);
        }*/
        /*[When(@"user clicks on complete release for secondary reviewer ""(.*)"" button")]
        public void WhenUserClicksOnCompleteReleaseForSecondaryReviewerButton(string button)
        {
            commons.ClickSencondaryCompleteRelease(button);
        }*/
        /*[When(@"user clicks on update contact ""(.*)"" button")]
        public void WhenUserClicksOnUpdateContactButton(string button)
        {
            commons.ClickSencondaryUpdateContact(button);
        }*/
        [When(@"select any value for ContactCaseManager dropdown list")]
        public void WhenSelectAnyValueForContactCaseManagerDropdownList()
        {
            LocCasePage.SelectContactCaseManage();
        }
        [When(@"select any value for IndicationForPhysicianToPhysician from dropdown list")]
        public void WhenSelectAnyValueForIndicationForPhysicianToPhysicianFromDropdownList()
        {
            LocCasePage.SelectPhysicianToPhysicianContact();
        }

        [Then(@"Global Dsr worklist account in editable mode")]
        public void ThenGlobalDsrWorklistAccountInEditableMode()
        {
            LocCasePage.VerifyEditableAccountDSR();
        }

        [When(@"select a recommendation same as Initial LOC Recommendation from ""([^""]*)""")]
        public void WhenSelectARecommendationSameAsInitialLOCRecommendationFrom(string tabName)
        {
            LocCasePage.ClickInitialTab(tabName);
            LocCasePage.GetinitialRecommendation();
            LocCasePage.SelectSecondaryReview();
        }


        [Then(@"user go to ""(.*)""")]
        public void ThenUserGoTo(string tabName)
        {
            LocCasePage.ClickInitialTab(tabName);
        }
        [Then(@"user add second recommendation")]
        public void ThenUserAddSecondRecommendation()
        {
            LocCasePage.EnterSecondRecommendation();
        }
        
        [Then(@"DSR worklist should open")]
        public void ThenDSRWorklistShouldOpen()
        {
            commons.VerifyDSRWorklistExists();
        }
        [When(@"user clicks on dsr worklist account")]
        public void WhenUserClicksOnDsrWorklistAccount()
        {
            commons.ClickDSRFirstRow();
        }
        [Then(@"user clicks on global loc worklist account")]
        public void ThenUserClicksOnGlobalLocWorklistAccount()
        {
            commons.ClickGlobalLocFirstRow();
        }

        [Then(@"worklist should open for global loc")]
        public void ThenWorklistShouldOpenForGlobalLoc()
        {
            commons.VerifyGlobalLocWorklistExists();
        }

        [Then(@"case should get ""(.*)"" for dsr")]
        public void ThenCaseShouldGetForDsr(string status)
        {
            LocCasePage.VerifyDSRCompleteStatus(status);
        }

    }
}
