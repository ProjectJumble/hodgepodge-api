# About

Jumble was a news source reliability-checking initiative.

This repository is hosting the RESTful API. Web browser extension is available in the separate [repository](https://github.com/ProjectJumble/hodgepodge-web-extension).

## Why Jumble?

Disinformation represents risks for democratic processes and social fabric, undermines the trust in the information society and confidence in digital news sources. We believed everyone had a responsibility to combat the scourge of disinformation and "fake news."

## Was Jumble reliable?

We were committed to limit the spread of disinformation and improve the visibility of trusted news sources. Data used for news source quality indicators was compiled by well-respected fact-checkers, crowdsourced from people like yourself, and analyzed using machine learning algorithms.

## Could I contribute to Jumble?

Crowdsourced data was an integral part of our news source audit process. We valued everyone's opinion and encouraged anyone to contribute.

## Was Jumble secure?

We didn't track browsing behavior, neither we asked for, nor stored personal information.

## Who was behind Jumble?

Jumble was developed by Permanent Holiday OÜ. Permanent Holiday OÜ was committed to freedom of expression and the right to receive and impart information and ideas without interference. We upheld liberty, independence and transparency as our core principles.

# Dependencies

Jumble RESTful API requires [Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction) for persisting data and [Machine Box Fakebox](https://machinebox.io/docs/fakebox) for news source analysis. After setting up both, make sure you enter appropriate service endpoints in the `appsettings[.*].json` file.

After lunching the API, and before making any requests, please initialize the database. To do so, make a POST request to `/api/v1/service/initialize`, for example:

```
POST /api/v1/service/initialize HTTP/1.1
Host: localhost:53135
Content-Type: application/json
{
  "token": "00000000-0000-0000-0000-000000000000"
}
```

After the database has been successfully initialized, you should get the HTTP 204 response; otherwise, HTTP 400 will be returned.

# Build Instructions

Before proceeding, make sure [.NET Core 2 SDK](https://dotnet.microsoft.com/download) is installed on the build machine. To build the Jumble RESTful API, navigate to the `./Hodgepodge.Api` project directory and execute `dotnet [build|publish]`.
