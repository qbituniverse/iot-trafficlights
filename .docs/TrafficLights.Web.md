# TrafficLights.Web

Web app project used to control the traffic lights module from a Razor UI HTTP web app client.

## Pages

|Controller|Endpoint|Details|
|-----|-----|-----|
|**TrafficControl**|/traffic-control|Send traffic control signals to control the traffic lights.|
|**TrafficLog**|/traffic-log-list|View traffic log execution history.|
||/traffic-log-details/{id}<br />{id}: string|View traffic log details by Id.|

## Ports

|Host|Environment|Port|
|-----|-----|-----|
|**Localhost**|Development|5020|
||Test|5021|
||Production|5022|
|**Docker**|Development|8020|
||Test|8021|
||Production|8022|

## Configuration

|Key|Value|Description|
|-----|-----|-----|
|**Environment**|Development|Use for local development where Raspberry Pi is not connected.|
||Test|Use for testing the deployment where Raspberry Pi is connected.|
||Production|Use this for the final production deployment of the running application on your network with Raspberry Pi connected.|
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
||Api|Traffic Control module running through Traffic Lights Api.|
|Modules.TrafficControl.Api||Traffic Control details through an API.|
|Modules.TrafficControl.Api.Url|Connection string|Url to the Traffic Lights Api.<br>_NOTE: Used when Modules.TrafficControl.Type = Api_.|
|**TrafficLog**||Traffic Log access details.|
|TrafficLog.Type|Repository|Defaults to *empty string ("")*. Access to Traffic Log through database repository.|
||Api|Access to Traffic Log through Traffic Lights Api.|
|TrafficLog.Api||Traffic Log details through an API.|
|TrafficLog.Api.Url|Connection string|Url to the Traffic Lights Api.<br>_NOTE: Used when TrafficLog.Type = Api_.|

### Schema

```json
{
  "Configuration": {
    "Environment": "string",
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
        "Type": "Api",
        "Api": {
          "Url": "string"
        }
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
        "Type": "Api",
        "Api": {
          "Url": "http://localhost:5010"
        }
      }
    }
  }
}
```