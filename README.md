# Blackrock Hackathon Challenge — Automated Micro-Investment API

APIs that enable automated retirement savings through expense-based micro-investments. This is a part of `BlackRock Hackathon Challenge 2026` on Hackerrank. 

## **Overview**

- **Project:** Blackrock Hackathon Challenge
- **Language / Platform:** C# / .NET 10
- **Service:** HTTP APIs for transactions, investments, and performance reporting
- **Container port:** 5477 (container listens on 5477)

## **Repository layout (high level)**

- `Blackrock-Hackathon/` — main service project, Dockerfile, configuration and source code
- `Blackrock-Hackathon.Tests/` — test project with automated unit tests

## **Prerequisites**

- .NET 10 SDK (to build and run locally)
- Docker Engine (for building and running the container)

Getting started — build and run locally

1. Restore, build and run from repo root:

```bash
dotnet restore Blackrock-Hackathon/Blackrock-Hackathon.csproj
dotnet build Blackrock-Hackathon/Blackrock-Hackathon.csproj -c Release
dotnet run --project Blackrock-Hackathon/Blackrock-Hackathon.csproj --urls "http://localhost:5477"
```

The service will be available at <http://localhost:5477>

Running tests

```bash
dotnet test Blackrock-Hackathon.Tests/Blackrock-Hackathon.Tests.csproj -c Release
```

## Solution containerization and docker image hosting process

> There can be many solutions to host container image, here will be following to push our image into `github container registry`.

### Steps

- First, we need to login into our github account to create `personal access token` or `PAT` to be able to push, delete images into container registry.
- While generating we select classing token options then `Upload packages to GitHub Package Registry` and `Delete packages from GitHub Package Registry` options.
- next create PAT and store it locally, since won't be visible after we navigate.
- Next we need to login into docker using our github user name and personal access token.

```bash
docker login --username {{GITHUB_USERNAME}} --password {{PAT}} ghcr.io
```

- After successful login, we build our image using dockerfile with some tag.

```bash
docker build -t ghcr.io/{{GITHUB_USERNAME}}/{{TAG_NAME}}:latest .
```

- After successful build, we need to push it to github container registry.

```bash
docker push ghcr.io/{{GITHUB_USERNAME}}/{{TAG_NAME}}:latest
```

- Once completed, we can verify by running `docker run -d -p 5477:5477 ghcr.io/{{GITHUB_USERNAME}}/{{TAG_NAME}}` command or navigating to [https://github.com/{{GITHUB_USERNAME}}?tab=packages](https://github.com/{{GITHUB_USERNAME}}?tab=packages)

## Container / Docker instructions (required contest configuration)

- The application is configured to listen on port `5477` inside the container.
- Dockerfile already exposes port `5477` and the container is configured to serve on that port.

Run the container with required port mapping:

```bash
docker run -d -p 5477:5477 ghcr.io/anikdakua03/blk-hacking-ind-anik-dakua
```

## API Endpoints

- Transactions
  - `POST : /blackrock/challenge/v1/transactions/parse`
  - `POST : /blackrock/challenge/v1/transactions/validator`
  - `POST : /blackrock/challenge/v1/transactions/filter`

- Investment returns
  - `POST : /blackrock/challenge/v1/returns/nps`
  - `POST : /blackrock/challenge/v1/returns/index`

- Performance Report
  - `GET : /blackrock/challenge/v1/performance`

## API References

- To have sample API reference , [check here](./Blackrock-Hackathon/Blackrock-Hackathon.http)
- Or after running the image , you can navigate to [http://localhost:{PORT_NO}/scalar](http://localhost:{PORT_NO}/scalar), where we can access the `Scalar UI`.