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
+-- shared-net/
|   +-- Shared/
|   +-- Shared.Aggregate/
|   +-- Shared.Application/
|   +-- Shared.Context/
|   +-- Shared.Data.Persistence/
|   +-- Shared.Service/
```


### Shared

    Empty main project


### Shared.Aggregate

    Base aggregate


### Shared.Application

    Base application service


### Shared.Context

    Base Context


### Shared.Data.Persistence

    Base Data Persistence


### Shared.Service

    Base service


## Git

    This reposity applies gitflow to manage the branches.
    It has master, develop, features, hotfix, bugfix and release branches.

    Features, hotfixes and bugfixes will start by develop and end in to in develop to.
    When they are ready to go to production, a release by develep will be created and merged in to master and develop.