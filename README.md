# Shared .Net

	Jorge Costa Macia

	https://github.com/JorgeCostaMacia/
	https://bitbucket.org/jorgecostamacia/

    https://www.linkedin.com/in/jorge-costa-macia-842817164/



## Introduction

    Shared .Net solution to develop and share nuggets and libraries.
    
    Its apply DDD and CQRS architecture.
    All projects just have the logic of one domain concept.


## Domains

```
+-- Shared
|   +-- Aggregate
|   |   +-- Exception
|   |   |   +-- ValueObject
|   |   |   |   +-- Exception
|   |   |   |   +-- Validator
|   |   +-- Message
|   |   |   +-- ValueObject
|   |   |   |   +-- Exception
|   |   |   |   +-- Validator
|   |   +-- Root
|   +-- Application
|   +-- Bus
|   |   +-- Command
|   |   +-- Event
|   |   +-- Message
|   |   +-- Query
|   +-- Context
|   +-- Data
|   |   +-- Persistence
|   +-- Exception
|   +-- Service
|   +-- Util
|   |   +-- Expression
|   +-- Validator
|   +-- ValueObject
|   |   +-- Exception
|   |   +-- Validator
```


### Shared

    Base Main Project With All Libs


### Shared.Aggregate

    Base Aggregate


### Shared.Aggregate.Exception

    Base Aggregate Exception

    
### Shared.Aggregate.Exception.ValueObject

    Base Exception Aggregate ValueObject


### Shared.Aggregate.Exception.ValueObject.Exception

    Base Exception Aggregate ValueObject Exceptions


### Shared.Aggregate.Exception.ValueObject.Validator

    Base Exception Aggregate ValueObject Validators

### Shared.Aggregate.Message

    Base Aggregate Message


### Shared.Aggregate.Message.ValueObject

    Base Message Aggregate ValueObject


### Shared.Aggregate.Message.ValueObject.Exception

    Base Message Aggregate ValueObject Exceptions


### Shared.Aggregate.Message.ValueObject.Validator

    Base Message Aggregate ValueObject Validators


### Shared.Aggregate.Root

    Base Aggregate Root


### Shared.Application

    Base Application Service


### Shared.Bus.Command

    Base Bus Message Command And Aggregates


### Shared.Bus.Event

    Base Bus Message Event And Aggregates


### Shared.Bus.Message

    Base Bus Message And Aggregates


### Shared.Bus.Query

    Base Bus Message Query And Aggregates


### Shared.Context

    Base Context


### Shared.Data.Persistence

    Base Data Persistence


### Shared.Exception

    Base Domain Exception And Aggregates


### Shared.Service

    Base Service


### Shared.Util.Expression

    Utils to manage expressions


### Shared.Validator

    Base Validators


### Shared.ValueObject

    Base ValueObject


### Shared.ValueObject.Exception

    Base ValueObject Exceptions


### Shared.ValueObject.Validator

    Base ValueObject Validators


## Git

    This reposity applies gitflow to manage the branches.
    It has master, develop, features, hotfix, bugfix and release branches.

    Features, hotfixes and bugfixes will start by develop and end in to in develop to.
    When they are ready to go to production, a release by develep will be created and merged in to master and develop.