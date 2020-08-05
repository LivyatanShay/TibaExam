# TibaExam
Not so beautiful, I admit, I had to take some (ALOT!) shortcuts, especially in the "look and feel" of the app.
There's only 1 user defined, and its stored in the appsettings.json file (the task didn't specify I need to make functionality to create/delete users).
The root of the project includes the file 'CreateDbAndTableScript.sql' which is essentially a script for making the DB and table needed for the project.
After running the script, make sure you update the connection string in appsettings.json, to be according to your local setup of your SQL server.