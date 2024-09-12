# Traffic Lights

Traffic Lights solution to control traffic lights, setup on the Raspberry Pi GPIO board.

## Application Design

![Traffic Lights Application](/.docs/TrafficLights.Application.png)

## Raspberry Pi Breadboard Wiring

![Traffic Lights Breadboard](/.docs/TrafficLights.Breadboard.png)

## Modules

### TrafficControl

Use to control the traffic and pedestrian lights pattern, turning different light modes.
- Traffic LEDs GPIO mapping: 26 (red), 19 (amber), 13 (green).
- Pedestrian LEDs GPIO mapping: 27 (red), 17 (green).

|Type|Description|
|-----|-----|
|Mock|Module is controlled through mock calls, no Pi required.|
|Pi|Module is executing GPIO commands on the Pi. Raspberry Pi is required for this.|
|Api|Module calls through HTTP layer to the _TrafficLights.Api_.<br />Refer to _TrafficLights.Api_ configuration and ports below.<br />Sample URL: _http://localhost:5010_|

### TrafficSensor

Use to control the motion detection sensor, turning on or off sensor mode. Configuration consist of Motion Sensor connected to Pi on GPIO port 21.

|Type|Description|
|-----|-----|
|Mock|Module is controlled through mock calls, no Pi required.|
|Pi|Module is executing GPIO commands on the Pi. Raspberry Pi is required for this.|

## Repository

Backend database details, used across all projects in the TrafficLights solution.

|Object|Name|Description|
|-----|-----|-----|
|**Database**|TrafficLights|Main database name for the TrafficLights solution.|
|**Tables**|ApiLogs|Store the API execution logs.|
||WebLogs|Store the Web App execution logs.|
||ConsoleLogs|Store the Console execution logs.|
||TrafficLogs|Store Traffic Lights execution logs.|

### Configuration

|Name|Description|
|-----|-----|
|Mock|Defaults to *empty string ("")*. Hard coded in-memory, stateless, mock calls to sample data set, no data persistance.<br /> Connection string is not required.|
|SQLite|SQLite backed database that stored on a host machine file system as _.db_ file.<br />Communication with the database through a file system.<br />Default SQLite deployment doesn't configure any user credentials and can be accessed without such permissions.<br>_NOTE: Works on AMD and ARM architectures_.|
|SQLite.Url|PATH_TO_DATABASE\DATABASE_NAME.db|
|MySql|MySql backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _3306_ over the network (IP Address) or localhost (127.0.0.1).<br />Use the default _root_ account and password of your choice that is configured during MySql database deployment.<br>_NOTE: Works on AMD and ARM architectures_.|
|MySql.Url|Server=127.0.0.1;Port=3306;User ID=root;Password=YOUR_PASSWORD;Database=DATABASE_NAME|
|MongoDb|MongoDb backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _27017_ over the network (IP Address) or localhost (127.0.0.1).<br />Default MongoDb deployment doesn't configure any user credentials and can be accessed without such permissions.<br>_NOTE: Works only on AMD architecture_.|
|MongoDb.Url|mongodb://localhost:27017|

## Projects

|Name|Description|
|-----|-----|
|[TrafficLights.Domain](/.docs/TrafficLights.Domain.md)|Domain project containing reusable models, modules and data repositories.|
|[TrafficLights.Api](/.docs/TrafficLights.Api.md)|Api (REST) project used to control the traffic lights module and access traffic lights execution logs.|
|[TrafficLights.Web](/.docs/TrafficLights.Web.md)|Web app project used to control the traffic lights module from a Razor UI HTTP web app client.|
|[TrafficLights.Console](/.docs/TrafficLights.Console.md)|Console app used to run traffic lights module or traffic sensor module.|