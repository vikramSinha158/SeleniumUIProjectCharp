Feature: PASLocBVTAdmin

Background: User navigates to Level of care worklist page
   Given user logs into PAS Application
   And user is on PAS home page
   When user selects the facility
   And user navigates to "Worklist" and "Level of Care"
   Then worklist should open

   @LocAdminuser
	Scenario: Verify that LOC admin user should be able to create new case
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

	@LocAdminuser
	Scenario: Verify that LOC admin user is able to add document on the case	
	When user selects any case in the LOC worklist
	And user clicks on the "Documents" tab	
	And user clicks on Choosefile button to upload the document
	Then document should be successfully uploaded

	@LocAdminuser
	Scenario:Verify that LOC admin user is able to add note on the case
	When user selects any case in the LOC worklist
    And user clicks on the "Notes" tab
	Then Notes section should open for editing
    When user enters text into Notes box 
    And user clicks +Add Notes
    Then Notes should be saved

    @LocAdminuser
    Scenario:Verify that LOC admin user should be able to update existing case
    When user selects "Status" with "=" operator having value "I"
    And user clicks on search button
    And user clicks on unchecked row having "Account Number" column
    Then consult page should open in uneditable mode
    When user clicks on "Check Out" button
    Then consult page should open in editable mode
    When user clicks on the "Recommendation" tab
    And selects a PAS Recommendation and enters lastName ,PAS recommendation in PhysianNotes
    And user clicks on the "Chart Information" tab
    And selects system,daignosis
    And user clicks on "Update" button
    Then Send To DSR pop up should appear 
    When user enter the note in Send To DSR pop up
    Then "Record Updated - Review In Progress" message should appear
    When user clicks on "Release" button
    And user clicks on the Release button from popup alert

    @LocAdminuser
    Scenario: Verify that LOC adminuser request document via the portal
    When user selects any case in the LOC worklist
    And user clicks on "Check Out" button
    And user clicks on the duplicate checkout button if exist after checkout
    And user gets the accountnumber
    And user clicks on the "Chart Information" tab
    And User select value from Request Type dropdown 
    And user enter text into box below
    And user click on the Send button 
    And a link should appear below the Info request section
    Then added text should appear
    When user clicks on "Release" button
    And user clicks on the Release button from popup alert
    And user clicks on "Back to Worklist" from the worklist
    And user searches the account in worklist
    And user clicks on search button
    Then an envelope icon should appear in Flag column 
    When user hovers on envelope icon
    Then added text should appear on icon

    @LocAdminuser
    Scenario: Validate overtime checkbox functionality from LOC Admin user
    When user selects "Status" with "=" operator having value "I"
    And user clicks on search button
    And user clicks on unchecked row having "Account Number" column
    Then checkout button should appear
    When user clicks on "Check Out" button
    And user checks overtime checkbox
    And user clicks on "Update" button
    Then Record Update message should appear
    And green background should be displayed with message "This case has been marked as overtime status."
    When user clicks on "Release" button
    And user clicks on the Release button from popup alert

    @LocAdminuser
    Scenario: Verify that LOC admin user should be able to complete a case     
    When user clicks on "New" from the worklist
	Then blank consult page should open
	When user enters all required fields 
    And user clicks on Save button
    Then case should be created
	And checkout button should appear
    When user gets the accountnumber
    And user clicks on "Back to Worklist" from the worklist
    Then worklist should open
    When user searches the account in worklist
    And user clicks on search button
    And user clicks on account
    Then consult page should open in uneditable mode
    When user clicks on the duplicate checkout button if exist after checkout
    Then consult page should open in editable mode
    When user clicks on the "Recommendation" tab
    And selects a PAS Recommendation and enters lastName ,PAS recommendation in PhysianNotes
    And selects system,daignosis
    And select any value for ContactCaseManager dropdown list
    And select any value for IndicationForPhysicianToPhysician from dropdown list  
    And user clicks on "Update" button
    Then Send To DSR pop up should appear 
    When user enter the note in Send To DSR pop up
    Then "Record Updated - Review In Progress" message should appear
    When user clicks on "Release" button
    And user clicks on the Release button from popup alert
    And user navigates to "GLOBAL WORKLIST" and "LEVEL OF CARE - DSR"
    Then DSR worklist should open
    When user searches the account in worklist
    And user clicks on search button
    And user clicks on dsr worklist account
    And user clicks on the duplicate checkout button if exist after checkout
    And select a recommendation same as Initial LOC Recommendation from "Initial Consult" 
    And user go to "Secondary Recommendation" 
    And user add second recommendation
    And user clicks on "Update" button DSR Worklist
    Then "Record Updated" message should appear
    When user clicks on "Complete" button
    Then case should get "Complete"
    When user navigates to "GLOBAL WORKLIST" and "LEVEL OF CARE"
    Then worklist should open for global loc
    When user searches the account in worklist
    And user clicks on search button
    Then user clicks on global loc worklist account
    And case should get "Complete" for dsr