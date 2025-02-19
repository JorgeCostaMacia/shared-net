JorgeCostaMacia.Shared
======================

Shared .Net solution to develop and share nuggets and libraries.<br>
It's apply DDD and CQRS architecture.<br>
All projects just have the logic of one domain concept.

Build Status
------------

| Branch        | Status                                                                                                                                                                                                | Release																																	|
|---------------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------:|
| main          | [![main](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/dotnet.yml/badge.svg?branch=main&event=push)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/dotnet.yml)    |[![Release](https://img.shields.io/github/v/release/JorgeCostaMacia/shared-net)](https://github.com/JorgeCostaMacia/shared-net/releases)	|

Supported Packages
------------------

| Package                                  | Info                                          | Nuget                                                                                                                                                                                                                                              |
|------------------------------------------|-----------------------------------------------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| **Main**                                 |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared                   | Main project with all libs                    | [![alt JorgeCostaMacia.Shared](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.svg "JorgeCostaMacia.Shared")](https://www.nuget.org/packages/JorgeCostaMacia.Shared/)                                                                        |
| **Base**                                 |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Service           | Base service                                  | [![alt JorgeCostaMacia.Shared.Service](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Service.svg "JorgeCostaMacia.Shared.Service")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Service/)                                        |
| JorgeCostaMacia.Shared.Validator         | Base validators                               | [![alt JorgeCostaMacia.Shared.Validator](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Validator.svg "JorgeCostaMacia.Shared.Validator")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Validator/)                                |
| JorgeCostaMacia.Shared.Context           | Base context                                  | [![alt JorgeCostaMacia.Shared.Context](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Context.svg "JorgeCostaMacia.Shared.Context")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Context/)                                        |
| **Error**                                |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Exception         | Exceptions and aggregates                     | [![alt JorgeCostaMacia.Shared.Exception](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Exception.svg "JorgeCostaMacia.Shared.Exception")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Exception/)                                |
| **Data**                                 |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Data.Persistence  | Data and repository                           | [![alt JorgeCostaMacia.Shared.Data.Persistence](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Data.Persistence.svg "JorgeCostaMacia.Shared.Data.Persistence")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Data.Persistence/)    |
| **DDD**                                  |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Aggregate         | Domain root and aggregates                    | [![alt JorgeCostaMacia.Shared.Aggregate](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Aggregate.svg "JorgeCostaMacia.Shared.Aggregate")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Aggregate/)                                |
| JorgeCostaMacia.Shared.Entity            | Domain entity and aggregates                                | [![alt JorgeCostaMacia.Shared.Entity](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Entity.svg "JorgeCostaMacia.Shared.Entity")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Entity/)                              |
| JorgeCostaMacia.Shared.ValueObject       | ValueObject with exceptions and validators    | [![alt JorgeCostaMacia.Shared.ValueObject](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.ValueObject.svg "JorgeCostaMacia.Shared.ValueObject")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.ValueObject/)                        |
| **Bus**                                  |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Bus.Command       | Bus command and aggregates                    | [![alt JorgeCostaMacia.Shared.Bus.Command](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Bus.Command.svg "JorgeCostaMacia.Shared.Bus.Command")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Bus.Command/)                        |
| JorgeCostaMacia.Shared.Bus.Event         | Bus event and aggregates                      | [![alt JorgeCostaMacia.Shared.Bus.Event](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Bus.Event.svg "JorgeCostaMacia.Shared.Bus.Event")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Bus.Event/)                                |
| JorgeCostaMacia.Shared.Bus.Message       | Bus message and aggregates                    | [![alt JorgeCostaMacia.Shared.Bus.Message](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Bus.Message.svg "JorgeCostaMacia.Shared.Bus.Message")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Bus.Message/)                        |
| JorgeCostaMacia.Shared.Bus.Query         | Bus query and aggregates                      | [![alt JorgeCostaMacia.Shared.Bus.Query](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Bus.Query.svg "JorgeCostaMacia.Shared.Bus.Query")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Bus.Query/)                                |
| **Other**                                |                                               |                                                                                                                                                                                                                                                    |
| JorgeCostaMacia.Shared.Util.Expression   | Utils to manage expressions                   | [![alt JorgeCostaMacia.Shared.Util.Expression](https://img.shields.io/nuget/v/JorgeCostaMacia.Shared.Util.Expression.svg "JorgeCostaMacia.Shared.Util.Expression")](https://www.nuget.org/packages/JorgeCostaMacia.Shared.Util.Expression/)        |

# REQUIREMENTS
* .NET 8 SDK
* .NET 9 SDK (Recommended) 

## Contact
* [Linkedin](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
* [Github](https://github.com/JorgeCostaMacia/)
* [Bitbucket](https://bitbucket.org/jorgecostamacia/)

No longer supported Packages
----------------------------

* JorgeCostaMacia.Shared.Aggregate.Exception
* JorgeCostaMacia.Shared.Aggregate.Exception.ValueObject
* JorgeCostaMacia.Shared.Aggregate.Exception.ValueObject.Exception
* JorgeCostaMacia.Shared.Aggregate.Exception.ValueObject.Validator
* JorgeCostaMacia.Shared.Aggregate.Message.ValueObject
* JorgeCostaMacia.Shared.Aggregate.Message.ValueObject.Exception
* JorgeCostaMacia.Shared.Aggregate.Message.ValueObject.Validator
* JorgeCostaMacia.Shared.Aggregate.Root
* JorgeCostaMacia.Shared.Application.Service
* JorgeCostaMacia.Shared.Bus.Service
* JorgeCostaMacia.Shared.Root
* JorgeCostaMacia.Shared.Util.Uuid
* JorgeCostaMacia.Shared.ValueObject.Exception
* JorgeCostaMacia.Shared.ValueObject.Validator
* Shared.Aggregate
* Shared.Application
* Shared.Bus.Command
* Shared.Bus.Event
* Shared.Bus.Message
* Shared.Bus.Query
* Shared.Data.Persistence
* Shared.Domain.Service
* Shared.Exception
* Shared.Service
* Shared.Utils.Expression
* Shared.Validator
* Shared.ValueObject