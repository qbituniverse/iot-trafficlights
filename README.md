# IoT Traffic Lights

Traffic Lights solution to control traffic lights, setup on the Raspberry Pi GPIO board.

## Application Design

![IoT Traffic Lights Application](/.docs/IoT.TrafficLights.Application.png)

## Raspberry Pi Breadboard Wiring

![IoT Traffic Lights Breadboard](/.docs/IoT.TrafficLights.Breadboard.png)

## Modules

### TrafficControl

Use to control the traffic and pedestrian lights pattern, turning different light modes through GPIO pins.

|LED Light|GPIO Pin|
|-----|-----|
|Traffic Red|26|
|Traffic Amber|19|
|Traffic Green|13|
|Pedestrian Red|27|
|Pedestrian Green|17|

|Type|Description|
|-----|-----|
|Mock|Module is controlled through mock calls, no Pi required.|
|Pi|Module is executing GPIO commands on the Pi. Raspberry Pi is required for this.|
|Api|Module calls through HTTP layer to the _IoT.TrafficLights.Api_.<br />Refer to _IoT.TrafficLights.Api_ configuration and ports below.<br />Sample URL: _http://localhost:8011_|

### TrafficSensor

Use to control the motion detection sensor, turning on or off sensor mode through GPIO pins.

|Sensor|GPIO Pin|
|-----|-----|
|Sensor On|21|

|Type|Description|
|-----|-----|
|Mock|Module is controlled through mock calls, no Pi required.|
|Pi|Module is executing GPIO commands on the Pi. Raspberry Pi is required for this.|

## Repository

Backend database details, used across all projects in the IoT TrafficLights solution.

|Object|Name|Description|
|-----|-----|-----|
|**Database**|IotTrafficLights|Main database name for the IoT TrafficLights solution.|
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
|MySql|MySql backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _33xx_ over the network (IP Address) or localhost (127.0.0.1).<br />Use the default _root_ account and password of your choice that is configured during MySql database deployment.<br>_NOTE: Works on AMD and ARM architectures_.|
|MySql.Url|Server=127.0.0.1;Port=33xx;User ID=root;Password=YOUR_PASSWORD;Database=DATABASE_NAME|
|MongoDb|MongoDb backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _270xx_ over the network (IP Address) or localhost (127.0.0.1).<br />Default MongoDb deployment doesn't configure any user credentials and can be accessed without such permissions.<br>_NOTE: Works on AMD and ARM architectures (Pi 5 or higher)_.|
|MongoDb.Url|mongodb://localhost:270xx|

## Projects

|Name|Description|
|-----|-----|
|[IoT.TrafficLights.Domain](/.docs/IoT.TrafficLights.Domain.md)|Domain project containing reusable models, modules and data repositories.|
|[IoT.TrafficLights.Api](/.docs/IoT.TrafficLights.Api.md)|Api (REST) project used to control the traffic lights module and access traffic lights execution logs.|
|[IoT.TrafficLights.Web](/.docs/IoT.TrafficLights.Web.md)|Web app project used to control the traffic lights module from a Razor UI HTTP web app client.|
|[IoT.TrafficLights.Console](/.docs/IoT.TrafficLights.Console.md)|Console app used to run traffic lights module or traffic sensor module.|