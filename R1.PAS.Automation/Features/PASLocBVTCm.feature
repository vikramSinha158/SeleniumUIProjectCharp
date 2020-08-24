Feature: Automate PAS LOC BVT

   Background: User navigates to Level of care worklist page
   Given user logs into PAS Application
   And user is on PAS home page
   When user selects the facility
   And user navigates to "Worklist" and "Level of Care"
   Then worklist should open

	@LocCMuser
	Scenario:Verify required fields validation from Case Manager user
	When user clicks on "New" from the worklist
	Then blank consult page should open
    When user without entering any data clicks on save button
    Then following errors messages are found :
    | Messages                       |
    | Save failed.                   |
    | Patient Last Name is required  |
    | Patient First Name is required |
    | MRN is required                |
    | DOB is required                |
    | Payer is required              |
    | Account Number is required     |
    | Presentation Date is required  |
    | Discharge Status is required   |
    | Location is required           |
    | Current Order is required      |
   
	@LocCMuser
	Scenario:Verify that Case manager user is able to add note on the case
	When user selects any case in the LOC worklist
    And user clicks on the "Notes" tab
	Then Notes section should open for editing
    When user enters text into Notes box 
    And user clicks +Add Notes
    Then Notes should be saved

@LocCMuser
    Scenario: Verify that Case Manager should be able to view Print form details
    When user selects "Status" with "=" operator having value "C"
    And user clicks on search button
    And user clicks on unchecked row having "Account Number" column
    And user verify for print form in the physician advisor notes section
    Then print form button should be enabled
    When user click on print form  button under physician advisor notes section
    Then displays the pdf of completed case

 @LocCMuser
	Scenario: Verify that Case Manager user is able to add document on the case	
	When user selects any case in the LOC worklist
    And user clicks on the "Documents" tab	
    And user clicks on Choosefile button to upload the document
    Then document should be successfully uploaded

    @LocCMuser
	Scenario: Verify that Case Manager user should be able to create new case
	When user clicks on "New" from the worklist
	Then blank consult page should open
	When user enters all required fields 
    And user clicks on Save button
    Then AccountLabel should appear
    When user gets the accountnumber
    And user clicks on "Back to Worklist" from the worklist
    And user searches the account in worklist
    And user clicks on search button
    Then case added above should be displayed in worklist