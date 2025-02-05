# mHealthProject Dotnet core 9.0 API for MpathHealth
HTTPS and Issues in macOS
Cleaning and trusing the dev certs
If you run into issues run the following command in the terminal within CrashPadAPI directory:

dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust
If you are using macOS Sequioa
If you are using macOs you may need to follow the instructions found here: https://dev.to/michaelcharles/fixing-the-https-developer-certificate-error-in-net-on-macos-sequoia-516h

Setting environment to HTTPS within Rider
Upon opening the solution in Rider. Ensure that you are using the CrashPad.https configuration fromt he dropdown to the left of the run/debug button

Labeling Endpoints to bypass Auth
In the event you need to add an endpoint that does not need a JWT token, then it needs to be whitelabled. To do this navigate to to the PipelineMiddleware.cs file. Within this file look for the _noAuthNeededList array. Add the endpoint there start from the controller. For example, /user/private would allow you to retrieve the private id of a user without having to authenticate.

For all other endpoints make sure you used the Auth Admin endpoint FIRST to grab the JWT Token from the Header  so you can bypass the security layer


###PostMan collections endpoint

To test the backend API alone please download and import mHealth.postman_collection.json from the  repo once you have the file please follow the steps below 

Open Postman and click on the "Import" button located in the top left corner.
In the import modal, select "Import File" and choose the JSON collection file from your local file system.
Click on the "Import" button to load the collection. The collection will now be available in Postman under "Collections" on the left sidebar.
If you encounter issues importing a JSON file, ensure the file is in the correct format (Collection v2.1 or later). If the JSON file was exported from an older version (v1), you may need to convert it to v2 format using the Postman Collection Transformer tool.

For more detailed instructions, you can refer to the official Postman documentation on importing and exporting data: https://learning.postman.com/docs/getting-started/importing-and-exporting/importing-data/




### Running the project In Rider
Running Dotnet Core Project in Rider
To run a .NET Core project in JetBrains Rider, follow these steps:

Open the Project: Open your .NET Core project in JetBrains Rider.
Locate launchSettings.json: Ensure that your project has a launchSettings.json file in the Properties folder. This file contains configurations for running and debugging your application.
Generate Run/Debug Configurations: If you haven't already, JetBrains Rider will automatically generate run/debug configurations from the launchSettings.json file when you first open the project. If not, you can manually import the profiles by right-clicking on launchSettings.json in the Solution Explorer and selecting "Generate Configurations."
Run the Project: You can run the project by clicking on the run button in the toolbar and selecting the appropriate configuration from the dropdown menu. Alternatively, you can right-click the project in the Solution Explorer and choose "Run '...'". This will create a temporary run configuration for the first profile in launchSettings.json and execute it.
If you encounter issues, make sure your environment is correctly set up with the appropriate .NET SDK versions and that JetBrains Rider is configured to use the correct SDK and runtime.
You can also Open the SLN file inside of the folder and it should open all of the resources that you need to start the API


###Running .NET Core in Visual Studio
To run a .NET Core application in Visual Studio on Windows, follow these steps:

Open the Project: Open your .NET Core project in Visual Studio.
Build the Project: Ensure that your project is built successfully. You can do this by clicking on the Build menu and selecting Build Solution, or by pressing Ctrl+Shift+B.
Run the Project: To run the project, you can use one of the following methods:
Click on the Start button in the toolbar, which looks like a green play button. This will run the project without debugging.
Alternatively, you can use the dotnet run command from the Developer Command Prompt for Visual Studio. To open the Developer Command Prompt, go to Tools > Command Line > Developer Command Prompt for VS 2019 (or your version of Visual Studio). Navigate to your project directory and run the command dotnet run.
If you encounter any issues, make sure that the .NET SDK is installed on your system. You can download and install it from the official .NET website: https://dotnet.microsoft.com/download/dotnet

For more detailed instructions on running and debugging .NET Core applications in Visual Studio, you can refer to the Microsoft documentation: https://learn.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio
