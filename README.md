## Info

A web solution with dashboards and reports for spare parts order management. With authorization, uploads, e-mails and user management are provided.

<img width="1547" alt="image" src="https://github.com/tokaym/react-mobx-and-cqrs-pattern-with-dot-net-6/assets/55059335/33dbd169-6950-43bb-ba2d-e8b92450992f">

<img width="1547" alt="image" src="https://github.com/tokaym/react-mobx-and-cqrs-pattern-with-dot-net-6/assets/55059335/7c11460e-16b3-4a2b-8938-43a2557cbf6f">


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
