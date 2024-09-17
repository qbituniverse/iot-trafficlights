# IoT.TrafficLights.Console

Console app used to run traffic lights module or traffic sensor module.

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
|Modules.TrafficSensor||Traffic Sensor module details.|
|Modules.TrafficSensor.Type|Mock|Defaults to *empty string ("")*. Hard-coded module not requiring Pi.|
||Pi|Running on Pi and making GPIO calls.|
|**Run**||Execute run process.|
||RunMock|Defaults to *empty string ("")*. Run mock command prompt.|
||RunTrafficTimer|Run traffic lights in alternating mode executing at predefined time intervals.|
||RunTrafficSensor|Use infrared sensor to control the traffic lights.|

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
      "Type": "SQLite",
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
      },
      "TrafficSensor": {
        "Type": "string"
      }
    },
    "Run": "string"
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
      },
      "TrafficSensor": {
        "Type": "Mock"
      }
    },
    "Run": "RunTrafficTimer"
  }
}
```