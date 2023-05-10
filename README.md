## Technologies

* React
* MobX
* Semantic-Ui-React
* .NET 6.0
* Typescript

## Setup

To run this project, install it locally using npm:

* $ cd ../client
* $ npm install
* $ npm start

Create your first migration and update database

* $ cd /src/spm/Persistence
* $ dotnet ef --startup-project ../api migrations add "FirstMigration"
* $ cd /src/spm/api
* $ dotnet ef update database

## Source

Web tutorial by github.com/tokaym/

## Note

This application provides Turkish language support.
