# Contributing

This repository follows a **GitFlow** branching model. The conventions below are
the single source of truth for how branches, merges, versions and tags work here.

## Branches

| Branch    | Role                                   |
| --------- | -------------------------------------- |
| `main`    | Production. Only release/hotfix land here, always tagged. |
| `develop` | Integration. Day-to-day work merges here. |

Remote is always `origin`. The main branch is `main` (legacy repos may use `master`).

## Branch types

| Type      | Prefix / name                        | Starts from | On finish                              |
| --------- | ------------------------------------ | ----------- | -------------------------------------- |
| Feature   | `feature/<name>-<YYYYMMDDHHMM>`      | `develop`   | merge → `develop` (no tag)             |
| Bugfix    | `bugfix/<name>-<YYYYMMDDHHMM>`       | `develop`   | merge → `develop` (no tag)             |
| Release   | `release/<version>`                  | `develop`   | merge → `develop` + `main`, tag `v<version>` |
| Hotfix    | `hotfix/<version>`                   | `main`      | merge → `develop` + `main`, tag `v<version>` |

- **Feature/bugfix** names get a `-YYYYMMDDHHMM` timestamp suffix to avoid collisions.
- **Release/hotfix** names are a strict SemVer version (`MAJOR.MINOR.PATCH`, optional `-prerelease`).
- Branch names use lowercase letters, digits and `. _ - /` (must start with a letter or digit).

## Conventions

- **Merges are always `--no-ff`** (every feature/release/hotfix keeps its own merge commit). On conflict, abort and resolve manually.
- **Tags** are annotated, prefixed with `v` (e.g. `v1.0.0`), created on `main`. Messages: `Release v<version>` / `Hotfix v<version>`.
- The working tree must be **clean** before starting or finishing any branch.

## Working on a change (feature)

```bash
# start: branch off develop and publish
git checkout develop && git pull
git checkout -b feature/my-change-202606281530
git push -u origin feature/my-change-202606281530

# ... commit your work ...

# finish: merge into develop (--no-ff), publish, delete branch
git checkout develop && git pull
git merge --no-ff feature/my-change-202606281530
git push origin develop
git push origin --delete feature/my-change-202606281530
git branch -d feature/my-change-202606281530
```

## Releasing (release / hotfix)

A release/hotfix finish is **transactional**: merge into `develop` first, then `main`,
tag, and push atomically.

```bash
# start a release off develop (hotfix starts off main instead)
git checkout develop && git pull
git checkout -b release/1.2.0
git push -u origin release/1.2.0

# ... final adjustments / version bumps ...

# finish
git checkout develop && git pull
git merge --no-ff release/1.2.0
git checkout main && git pull
git merge --no-ff release/1.2.0
git tag -a v1.2.0 -m "Release v1.2.0"
git push --atomic origin main develop --tags
git push origin --delete release/1.2.0
git branch -d release/1.2.0
git checkout develop
```

## Building & testing

Packages multi-target **.NET 8 / 9 / 10** (10 recommended).

```bash
dotnet build -c Release
dotnet test
```

Shared build settings (target frameworks, package metadata, SourceLink, symbols)
live in `Directory.Build.props`; each project's `.csproj` only declares what is
unique to it (`VersionPrefix`, `Description`, `PackageTags`).
