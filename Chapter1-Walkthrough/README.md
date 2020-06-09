# Chapter 1 - Walkthrough: Implement the sample Azure Functions and call the QRCode Function from a Microsoft Healthbot Service Scenario needing QRCode support

#### This chapter gives and overview of the Azure Functions and how to consume the function(s) to include QRCode functionality.  Also I will show you how to add a few variable assignments and an adaptive card to display the QRCode in the Healthbot Scenario.

## Prerequisites
* Visual Studio
* Microsoft Healthcare bot service instance


<center><img src="images//csdQRCode Detail Level.jpg" width="850"></center>

#### * Perform a pull request to get the solution and associated projects for Covidtoken (validator) and csdQRCode (QRCode Generator)

#### * Open the csdQRCode.sln file with Visual Studio to get started

#### * Edit the local.settings.json files in each project and ensure your "FUNCTIONS_WORKER_RUNTIME" is set to "dotnet"

#### * Edit the local.settings.json file for the csdQRCode project and ensure you add the QRCodeValidationURL and QRCodeValidationKEY for the Covidtoken endpoint (when deploying your functions to Azure these should be entered into the configuration settings "application settings" for safe storage)

<center><img src="images//VS QRCode Project settings and Dependencies.png" width="850"></center>

#### * Ensure you have the Nuget Packages required for the csdQRCode project (System.Runtime.Extensions, QRCoder, Functions SDK)

<center><img src="images//VS QRCode Project settings and Dependencies.png" width="850"></center>

#### * example assign variable elements and the adaptive card json to include in your Healthbot scenario
<center><img src="images//assignkeyvisual.png" width="850"></center>
<center><img src="images//assignurlvisual.png" width="850"></center>

#### * adaptive card template for displaying the QRCode during chat
<center><img src="images//QRCode Statement for Adaptive Card.png" width="850"></center>
<center><img src="images//QRCode Statement for Adaptive Card - Select Card.png" width="850"></center>
<center><img src="images//QRCode Statement for Adaptive Card - View Card.png" width="850"></center>




