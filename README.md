## Project Summary Description
The *Cycle for Survival Event Planning App* will automate many aspects of planning a cycling event. It will allow users to create events, event maps and imports teams into events.
The process of assigning teams of riders to bikes will be automated based on defined criteria with the ability to override assignments.
At events, printable team packets and searchable team info help on-site staff direct teams to their assigned bikes.

***********************************************

architecture
-----------
This is the back end component of: *Cycle for Survival Event Planning App*

It's based on [SmartAdmin](https://wrapbootstrap.com/theme/smartadmin-responsive-webapp-WB0573SK0) template with heavy modification and updates.
###### Notable Technologies:
* .NET Core
* MSSQL 2015
* Code First Migrations
* JWT Authentication

***********************************************

development
-----------
To run the code in your development environment make sure you have installed the lates version of .NET Core:

1. Clone this repo: git@bitbucket.org:surgeforward/cycleforsurvivaleventmanagerbackend.git
2. Switch to development branch. All development will occur in this branch.
3. If needed **Restore NuGet Packages** when right-clicking on your solution inside the Solution Explorer]
4. In MSSQL Database Manager create a database called: CFS. (make sure you have SQL alias localhost)
4. Build the project and Run.

##### Remedy:

1. When project will be run for the first time, it will complain about trying to connect to db and that it doesen;t have users/roles tables etc.

To fix this in Visual Studio:
1. Tools -> Nuget Package Manager -> Package Manager Console
2. Select CFS.Web as default project.
3. Type: database-update (you should be good running the project after that)

##### Handle Database Migration

* Open Nuget Managemanet console in Visual studio: Tools-> Nuget Package Manager -> Package Manager Console.
* Change default project to: CFS.DataAccess.Impl
* run -> update-database -Verbose
* Do some changes in the model

* run-> add-migration
* run -> update-database -Verbose
* Update Property name ex: update-database -Verbose -Target modifiedaapplicantentity

Recommended Ide's:
* Visual Studio Community Edition
* Visual Studio Code


###### User Logins
| username       | pwd           | role  |
| ------------- |:-------------:| -----|
| admin@cfs-test.com | fall2016! | Admin |
| marketmanager@cfs-test.com | fall2016! | MarketManager |
| participant@cfs-test.com  | fall2016! | Participant |

***********************************************

qa
----------
The project will be setup on Surge CI integration environment.

###### User Logins
| username       | pwd           | role  |
| ------------- |:-------------:| -----|
| admin@cfs-test.com | fall2016! | Admin |
| marketmanager@cfs-test.com | fall2016! | MarketManager |
| participant@cfs-test.com  | fall2016! | Participant |

***********************************************

production
----------

***********************************************

resources
---------
* [wireframes](http://13ao1t.axshare.com)

******************************************************************
