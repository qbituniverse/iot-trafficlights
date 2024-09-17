# IoT.TrafficLights.Api

Api (REST) project used to control the traffic lights module and access traffic lights execution logs.

## Endpoints

For a complete OpenAPI specification please refer to [IoT.TrafficLights.Api.Spec.json](IoT.TrafficLights.Api.Spec.json) file. Use [Swagger](https://swagger.io/) tools to browse this OpenAPI spec.

|Controller|Endpoint|Details|
|-----|-----|-----|
|**TrafficControl**|POST /api/trafficcontrol/start|Set the traffic lights to start traffic condition (green light).|
||POST /api/trafficcontrol/stop|Set the traffic lights to stop traffic condition (red light).|
||POST /api/trafficcontrol/standby|Set the traffic lights to standby traffic condition (amber light).|
||POST /api/trafficcontrol/shut|Shut down the traffic (no light).|
|**TrafficLog**|GET /api/trafficlog/id/{id}<br />{id}: string|Get traffic log record by Id.|
||GET /api/trafficlog/date/{date}<br />{date}: string [dd-MM-yyyy]|Get traffic log records by date.|
||GET /api/trafficlog/all|Get all traffic log records.|
||POST /api/trafficlog/create<br />Body: TrafficLog JSON object|Create new traffic log record.|
||PUT /api/trafficlog/update<br />{id}: string<br />Body: TrafficLog JSON object|Update traffic log record.|
||DELETE /api/trafficlog/id/{id}<br />{id}: string|Delete traffic log record by Id.|
||DELETE /api/trafficlog/date/{date}<br />{date}: string [dd-MM-yyyy]|Delete traffic log records by date.|
||DELETE /api/trafficlog/all|Delete all traffic log records.|
|**Admin**|GET /api/admin/ping|Prints out Ping OK healthy response.|
||GET /api/admin/config|Prints out current Api configuration.|

## Ports

|Host|Environment|Port|
|-----|-----|-----|
|**Localhost**|Development|5100|
||Test|5101|
||Production|5102|
|**Docker**|Development|8100|
||Test|8101|
||Production|8102|

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
|**Modules**||Modules configuration.|
|Modules.TrafficControl||Traffic Control module details.|
|Modules.TrafficControl.Type|Mock|Defaults to *empty string ("")*. Hard-coded module not requiring Pi.|
||Pi|Running on Pi and making GPIO calls.|

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
    "Modules": {
      "TrafficControl": {
        "Type": "string"
      }
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
        "Url": "Server=127.0.0.1;Port=3306;User ID=root;Password=YOUR_PASSWORD;Database=DATABASE_NAME"
      },
      "MongoDb": {
        "Url": "mongodb://localhost:27017"
      }
    },
    "Modules": {
      "TrafficControl": {
        "Type": "Mock"
      }
    }
  }
}
```