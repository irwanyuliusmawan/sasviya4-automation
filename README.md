# Selenium Test Automation
Selenium Automation enables you to perform end-to-end automation tests on a secure, reliable, and scalable SAS Viya 4 deployments. You can perform automated UI testing for validating the different features deployed with SAS viya 4. This post will help you to quickly get started with running your automation test scripts on SAS Viya 4.

## Objective
Objective is to validate different below test cases to be executed for SAS Viya 4.
| S. No  | Test Cases                         
| -------|:-------------------------------------
| 1.     | <B>Kuberenetes Deployment Check</B>         
| 1.1    | Check status of Kebernetes Worker Nodes             
| 1.2    | Check Pods are Running Successfully     
| 2.     | <B>Environment Manager</B>         
| 2.1    | Login to Environment Manager             
| 2.2    | Create a validation folder
| 2.3    | Access Servers page
| 2.4    | Access Data page         
| 3.     | <B>SAS Drive</B>         
| 3.1    | Access SAS Drive  
| 4.     | <B>SAS Studio</B>         
| 4.1    | Submit a SAS code            
| 4.2    | Connect to CAS
| 4.3    | Load data to CAS
| 4.4    | Explore data from CAS
| 4.5    | Promote sample data to CAS
| 5.     | <B>Theme Designer</B>         
| 5.1    | Access Theme Designer
| 6.     | <B>Data Explorer</B>         
| 6.1    | Access Data Explorer
| 7.     | <B>Data Studio</B>         
| 7.1    | Update permission
| 7.2    | Add new calculated column to a dataset
| 8.     | <B>Data Profiling</B>         
| 8.1    | Run a profile report
| 9.     | <B>Lineage Viewer</B>         
| 9.1    | Display the relationship of a simple component
| 10.    | <B>Data Quality</B>         
| 10.1   | Display the relationship of a simple component
| 11.    | <B>Visual Analytics</B>         
| 11.1   | Create a Report
| 11.2  | Create a map Report
| 12.    | <B>Conversation Designer</B>         
| 12.1   | Login to Conversation Designer
| 12.2   | Create a Bot
| 12.3   | Welcome a User
| 12.4   | Open a Web Page
| 12.5   | Test Your Bot
| 13.    | <B>Job Flow Scheduler</B>         
| 13.1   | Create a Decision Tree report


## Prerequisite
To prepare or excute the test case below is necessary
1. For Windows 10 or above
   - Install Latest version of Chrome Browser. Once installation done then check the Chrome Version as below -

   - Chrome Driver Download. Download the chrome driver based on the version above.
     https://chromedriver.chromium.org/downloads

     Note - Once chrome driver is downloaded, It need to unzip and path of the same need to be noted for further use.

   - Selenium IDE for Chrome. This is optional if you want to get help for recording.
     https://www.selenium.dev/selenium-ide/

   - Visual Studio 2022 Community Edition. During instllation just select .NET desktop development if you don't want to get everything installed.
     https://visualstudio.microsoft.com/vs/community/

2. For Ubuntu 20.04
   - Chromium
   - Chrome Driver Setup
   - Dotnet

3. Accessibilty
   - SAS Viya 4 deployment and Application URL must be accessible from the machine / server were test is to be performed.
   - SAS Viya 4 should be deployed with all the latest features. 
   - Attached test cases are validated with SAS viya 4 Version 2021.2.3 Stable.

4. Others
   - Test case are writtent in C# so basic understanding of C# language is required.
   - HTML DOM Understanding is required to write selenium automation.

## Executing the Test Case oon Windows 10  Environment <br />
1. Download the code from the gitlab from main branch - https://gitlab.sas.com/sinbrv/sasviya4-automation
2. Create the folder Test on C drive and add below 2 input test files
3. Open the SASViya4Test solution and Restore the Package and Run Build
5. Modify RunSettings file as below
4. Provide the RunSettings in Visual Studio
5. Open the Test Explorer and Run the Playlists for running actual test case



