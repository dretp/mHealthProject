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
