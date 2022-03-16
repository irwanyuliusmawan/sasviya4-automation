# Selenium Test Automation
Selenium Automation enables you to perform end-to-end automation tests on a secure, reliable, and scalable SAS Viya 4 deployments. You can perform automated UI testing for validating the different features deployed with SAS viya 4. This post will help you to quickly get started with running your automation test scripts on SAS Viya 4.

## Objective
Objective is to validate different below test cases to be executed for SAS Viya 4 Deployment.
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
1. <b>For Windows 10 or above </b>
   - Install Latest version of Chrome Browser. Once installation done then check the Chrome Version as below -
     ![sasviya4-automation](../../assets/Selenium12.png)

   - Chrome Driver Download. Download the chrome driver based on the version above.
     https://chromedriver.chromium.org/downloads

     Note - Once chrome driver is downloaded, It need to unzip and path of the same need to be noted for further use.

   - Selenium IDE for Chrome. This is optional if you want to get help for recording.
     https://www.selenium.dev/selenium-ide/

   - Visual Studio 2022 Community Edition. During instllation just select .NET desktop development if you don't want to get everything installed.
     https://visualstudio.microsoft.com/vs/community/

     ![sasviya4-automation](../../assets/Selenium13.png)

2. <b> For Linux Ubuntu 20.04 </b>
   - Chromium / Chrome Browser instllation on Ubuntu 20.04 LTS
   ```
   sudo apt-get install -y libappindicator1 fonts-liberation
   sudo apt-get install -f
   sudo apt install -y unzip xvfb libxi6 libgconf-2-4
   wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
   sudo dpkg -i google-chrome*.deb
   sudo curl -sS -o - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add
   sudo bash -c "echo 'deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main' >> /etc/apt/sources.list.d/google-chrome.list"
   sudo apt -y update
   sudo apt -y install google-chrome-stable
   sudo apt --fix-broken install
   sudo apt -y install google-chrome-stable
   google-chrome --version
   ```
   - Chrome Driver Setup. Install the chrome driver based on the version of chrome browser. As per this example the version is 99.0.4844.51
   ```
   wget https://chromedriver.storage.googleapis.com/99.0.4844.51/chromedriver_linux64.zip
   unzip chromedriver_linux64.zip
   sudo mv chromedriver /usr/bin/chromedriver
   sudo chown root:root /usr/bin/chromedriver
   sudo chmod +x /usr/bin/chromedriver
   ```
   - Dotnet Package installation to run Test Cases on Linux
   ```
   wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   sudo apt-get update;   
   sudo apt-get install -y apt-transport-https 
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-6.0
   dotnet --version
   ```
    <br />

3. Setting up kube config file on Test Machine / Server
   - Copy config file to access Kubernetes cluster.
     - For Windows config file need to be copied in path C:\Users\<<UserName>>\.kube
     - For Linux config file need to be copied in path \home\<<UserName>>\.kube

4. Accessibilty
   - SAS Viya 4 deployment and Application URL must be accessible from the machine / server were test is to be performed. 
   - SAS Viya 4 should be deployed with all the latest features. 
   - Attached test cases are validated with SAS viya 4 Version 2021.2.3 Stable.
   <br />

5. Others
   - Test case are writtent in C# so basic understanding of C# language is required.
   - HTML DOM Understanding is required to write selenium automation.

## Executing the Test Case on Windows 10 Environment <br />
1. Logon or RDP to Windows VM were you had done installation.
2. Download the code from the gitlab from main branch - https://gitlab.sas.com/sinbrv/sasviya4-automation
   Note - GitLab can be cloned used either with ssh or https. Please check below screenshot for example
   ![sasviya4-automation](../../assets/Selenium2.png)

3. Create the folder Test on C drive and add below 2 input test files. You can get input files from assets folders.
   ![sasviya4-automation](../../assets/Selenium1.png)

4. Open the SASViya4Test solution and Restore the Package. Once solution is ready you can Run Build.
   - Open the Solution
     ![sasviya4-automation](../../assets/Selenium3.png)

   - Build the Soluton
     ![sasviya4-automation](../../assets/Selenium5.png)

   - Validate if Build was success
     ![sasviya4-automation](../../assets/Selenium6.png)

5. Modify RunSettings file as below
   | S. No  | Parameters                 | Description                         
   | -------|:---------------------------| :--------------------------------
   | 1.     | SASEnvMgrUrl               | URL for SAS Environment Manager e.g. 
   | 2.     | SASDriveUrl                | URL for SAS Drive
   | 3.     | SASStudioUrl               | URL for SAS Studio
   | 4.     | SASVisualAnalyticsUrl      | URL for SAS Viual Analytics
   | 5.     | SASThemeDesigner           | URL for Theme Designer
   | 6.     | SASDataExplorerUrl         | URL for SAS Data Explorer
   | 7.     | SASLineageUrl              | URL for SAS Lineage 
   | 8.     | SASDataStudioUrl           | URL for SAS Data Studio
   | 9.     | SASConversationDesignerUrl | URL for SAS Conversational Design for BOTS
   | 10.    | headless                   | Value could be true or false. e.g. false if you need to see UI flow
   | 11.    | DriverPath                 | Value will be Chrome Driver Path only when used with Windows.
   | 12.    | environment                | Value will be either Linux or Windows, Depending on which environment used
   | 13.    | testFilePath               | CSV Test File Path to be imported during test
   | 14.    | demographicfilepath        | Demographic Test File Path to be imported during test
   | 15.    | screenshotFilepath         | Screenshot File Path
   | 15.    | username                   | SAS User name
   | 15.    | password                   | SAS Password

Example of .runsettings for Windows
```
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
	<RunConfiguration>
		<!-- TestSessionTimeout was introduced in Visual Studio 2017 version 15.5 -->
		<!-- Specify timeout in milliseconds. A valid value should be greater than 0 -->
		<TestSessionTimeout>900000</TestSessionTimeout>
	</RunConfiguration>
	<TestRunParameters>
		<Parameter name="SASEnvMgrUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASEnvironmentManager" />
		<Parameter name="SASDriveUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDrive" />
		<Parameter name="SASStudioUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASStudio" />
		<Parameter name="SASVisualAnalyticsUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASVisualAnalytics" />
		<Parameter name="SASThemeDesigner" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASThemeDesigner" />
		<Parameter name="SASDataExplorerUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDataExplorer" />
		<Parameter name="SASLineageUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASLineage" />
		<Parameter name="SASDataStudioUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDataStudio" />
		<Parameter name="SASConversationDesignerUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASConversationDesigner" />
		<Parameter name="headless" value="false" />
		<Parameter name="DriverPath" value="C:\\Users\\sinbrv\\Downloads\\chromedriver_win32_new" />
		<Parameter name="environment" value="Windows" />
		<Parameter name="testFilePath" value="C:\\Test\\csv_file.csv" />
		<Parameter name="demographicfilepath" value="C:\\Test\\demographics.csv" />
		<Parameter name="screenshotFilepath" value="C:\\Test\\" />
		<Parameter name="username" value="viya_admin" />
		<Parameter name="password" value="Password123" />
		<Parameter name="InitialPageLoadSleep" value="25000" />
		<Parameter name="IntermediatePageLoadSleep" value="2000" />
		<!-- C:\Users\sinbrv\csv_file.csv -->
	</TestRunParameters>
</RunSettings>
```

6. Set the RunSettings in Visual. This is required for the first time during setup.
   - In Visual Studio, Select Test -> Configure Run Settings -> Auto Detect runsettings file

   - In Visual Studio, Select Test -> Configure Run Settings -> Select Solution Wide runsettings file -> Select the .runsettings file from your solution.
     ![sasviya4-automation](../../assets/Selenium8.png)

   
7. Open the Test Explorer and Run the Playlists for running actual test case
   - In Visual Studio, Select Test -> Test Explorer -> Select the Playlist and Run Test
     ![sasviya4-automation](../../assets/Selenium9.png)
     ![sasviya4-automation](../../assets/Selenium10.png)

8. After Successful test you can see the Test Result
   ![sasviya4-automation](../../assets/Selenium10.png)
   ![sasviya4-automation](../../assets/Selenium14.png)


## Executing the Test Case on Linux Ubuntu Environment <br />
1. SSH to Ubuntu VM were you had done instllation.

2. Clone the code from the gitlab from main branch - https://gitlab.sas.com/sinbrv/sasviya4-automation
   Note - GitLab can be cloned used either with ssh or https. Please check below screenshot for example
   
3. Create the folder sasviya4test on home directory of the Ubuntu environment. Copy the pubish version code into that directory.
```
cd ~
mkdir sasviya4test
```

4. Copy 2 input test files into sasviya4test\test\ directory. 2 csv files you can find in test folder, if not there then copy from assets/csv_file.csv and assets/demographics.csv.

5. Create .runsettings file into sasviya4test directory.
```
sudo nano .runsettings
```

Example of .runsettings for Windows
```
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
	<RunConfiguration>
		<!-- TestSessionTimeout was introduced in Visual Studio 2017 version 15.5 -->
		<!-- Specify timeout in milliseconds. A valid value should be greater than 0 -->
		<TestSessionTimeout>900000</TestSessionTimeout>
	</RunConfiguration>
	<TestRunParameters>
		<Parameter name="SASEnvMgrUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASEnvironmentManager" />
		<Parameter name="SASDriveUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDrive" />
		<Parameter name="SASStudioUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASStudio" />
		<Parameter name="SASVisualAnalyticsUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASVisualAnalytics" />
		<Parameter name="SASThemeDesigner" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASThemeDesigner" />
		<Parameter name="SASDataExplorerUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDataExplorer" />
		<Parameter name="SASLineageUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASLineage" />
		<Parameter name="SASDataStudioUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASDataStudio" />
		<Parameter name="SASConversationDesignerUrl" value="https://sinbrv.gelsandbox.aws.unx.sas.com/SASConversationDesigner" />
		<Parameter name="headless" value="true" />
		<Parameter name="DriverPath" value="NA" />
		<Parameter name="environment" value="Linux" />
		<Parameter name="testFilePath" value="/home/ubuntu/sasviya4test/test/csv_file.csv" />
		<Parameter name="demographicfilepath" value="/home/ubuntu/sasviya4test/test/demographics.csv" />
		<Parameter name="screenshotFilepath" value="/home/ubuntu/sasviya4test/test/" />
		<Parameter name="username" value="viya_admin" />
		<Parameter name="password" value="Password123" />
		<Parameter name="InitialPageLoadSleep" value="25000" />
		<Parameter name="IntermediatePageLoadSleep" value="2000" />
		<!-- C:\Users\sinbrv\csv_file.csv -->
	</TestRunParameters>
</RunSettings>
```

6. Run below command to execute the test cases
```
dotnet vstest SASViya4Test.dll --Settings:.runsettings --filter TestCategory=Playlist1
dotnet vstest SASViya4Test.dll --Settings:.runsettings --filter TestCategory=Playlist2
dotnet vstest SASViya4Test.dll --Settings:.runsettings --filter TestCategory=Playlist3
```

7. After Successful test you can see the Test Result under test folder.
   
