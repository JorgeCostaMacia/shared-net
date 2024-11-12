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
|   +-- Bus
|   |   +-- Command
|   |   +-- Event
|   |   +-- Message
|   |   +-- Query
|   |   +-- Service
|   +-- Context
|   +-- Data
|   |   +-- Persistence
|   +-- Entity
|   +-- Exception
|   +-- Service
|   +-- Util
|   |   +-- Expression
|   +-- Validator
|   +-- ValueObject
```


### Shared

    Base Main Project With All Libs


### Shared.Aggregate

    Base Domain Root And Aggregates


### Shared.Bus.Command

    Base Bus Message Command And Aggregates


### Shared.Bus.Event

    Base Bus Message Event And Aggregates


### Shared.Bus.Message

    Base Bus Message And Aggregates


### Shared.Bus.Query

    Base Bus Message Query And Aggregates


### Shared.Bus.Service

    Base Bus Message Service And Infra Configurations


### Shared.Context

    Base Context


### Shared.Data.Persistence

    Base Data Persistence
    

### Shared.Entity

    Base Domain Entity And Aggregates


### Shared.Exception

    Base Domain Exception And Aggregates


### Shared.Service

    Base Service


### Shared.Util.Expression

    Utils to manage expressions


### Shared.Validator

    Base Validators


### Shared.ValueObject

    Base ValueObject With Exceptions And Validators

## Git

    This reposity applies gitflow to manage the branches.
    It has master, develop, features, hotfix, bugfix and release branches.

    Features, hotfixes and bugfixes will start by develop and end in to in develop to.
    When they are ready to go to production, a release by develep will be created and merged in to master and develop.