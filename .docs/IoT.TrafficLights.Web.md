# IoT.TrafficLights.Web

Web app project used to control the traffic lights module from a Razor UI HTTP web app client.

## Pages

|Controller|Endpoint|Details|
|-----|-----|-----|
|**TrafficControl**|/traffic-control|Send traffic control signals to control the traffic lights.|
|**TrafficLog**|/traffic-log-list|View traffic log execution history.|
||/traffic-log-details/{id}<br />{id}: string|View traffic log details by Id.|
|**DatabaseBrowser**|/database-browser|Access database browser client.|

## Ports

|Host|Environment|Port|
|-----|-----|-----|
|**Localhost**|Development|5015|
||Test|5016|
||Production|5017|
|**Docker**|Development|8015|
||Test|8016|
||Production|8017|

## Configuration

|Key|Value|Description|
|-----|-----|-----|
|**Environment**|Development|Use for local development where Raspberry Pi is not connected.|
||Test|Use for testing the deployment where Raspberry Pi is connected.|
||Production|Use this for the final production deployment of the running application on your network with Raspberry Pi connected.|
|**LogLevel**||Log levels to capture.|
|LogLevel.Console|See options|Log level to record in the database repository.<br />_Options: Verbose, Debug, Information, Warning, Error, Fatal_|
|LogLevel.Database|See options|Log level to record in the database repository.<br />_Options: Verbose, Debug, Information, Warning, Error, Fatal_|
|**Repository**||Repository configuration.|
|Repository.Type|Mock|Defaults to *empty string ("")*. Hard-coded in-memory data, no persistent storage.|
||SQLite|SQLite database backend.<br>_NOTE: Works on AMD and ARM architectures_.|
||MySql|MySql database backend.<br>_NOTE: Works on AMD and ARM architectures_.|
||MongoDb|MongoDb database backend.<br>_NOTE: Not available for ARM architecture_.|
|Repository.SQLite||SQLite details.|
|Repository.SQLite.Url|Connection string|Connection details to SQLite.|
|Repository.MySql||MySql details.|
|Repository.MySql.Url|Connection string|Connection details to MySql.|
|Repository.MongoDb||MongoDb details.|
|Repository.MongoDb.Url|Connection string|Connection details to MongoDb.|
|**Api**||Api configuration.|
|Api.Url|Connection string|Connection string to the Api.|

### Schema

```json
{
  "Configuration": {
    "Environment": "string",
    "LogLevel": {
      "Console": "string",
      "Database": "string"
    },
    "Repository": {
      "Type": "string",
      "SQLite": {
        "Url": "string"
      },
      "MySql": {
        "Url": "string"
      },
      "MongoDb": {
        "Url": "string"
      }
    },
    "Api": {
      "Url": "string"
    }
  }
}
```

### Example

```json
{
  "Configuration": {
    "Environment": "Development",
    "LogLevel": {
      "Console": "Information",
      "Database": "Error"
    },
    "Repository": {
      "Type": "SQLite",
      "SQLite": {
        "Url": "PATH_TO_DATABASE\\DATABASE_NAME"
      },
      "MySql": {
        "Url": "Server=127.0.0.1;Port=3319;User ID=root;Password=YOUR_PASSWORD;Database=DATABASE_NAME"
      },
      "MongoDb": {
        "Url": "mongodb://localhost:27019"
      }
    },
    "Api": {
      "Url": "http://localhost:5010"
    }
  }
}
```