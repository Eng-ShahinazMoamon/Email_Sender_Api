To Test Sending mail 
-Go to appsettings.json (change (From, userName , Email) to Your Mail and password)

--------------------------------------------------------------------------------
Note:maybe face problem (Username and Password not accepted) that because google security  
please follow The Next steps to solve problem 

First:

enable two factor authentication for your Gmail account by going to Google Support:
Protect your account with 2-Step Verification and enabling it.
Then:

generate a 16-symbols password by going to Gmail Security Settings
use the generated code instead of your password for authentication.
---------------------------------------------------------------------------------
You can change connection string (data source) to your local data source  
---------------------------------------------------------------------------------
-If you facing problem during building app because (EmailServic.dll)
->remove library from Email_Sender_Api and added it again 
---------------------------------------------------------------------------------
ToAdd database 
just using (update-database) in package manager console 