# IoT Traffic Lights

Traffic Lights solution to control traffic lights, setup on the Raspberry Pi GPIO board.

## Application Design

![IoT Traffic Lights Application](/.docs/IoT.TrafficLights.Application.png)

## Raspberry Pi Breadboard Wiring

![IoT Traffic Lights Breadboard](/.docs/IoT.TrafficLights.Breadboard.png)

## Projects

|Name|Description|
|-----|-----|
|[IoT.TrafficLights.Domain](/.docs/IoT.TrafficLights.Domain.md)|Domain project containing reusable models, modules and data repositories.|
|[IoT.TrafficLights.Api](/.docs/IoT.TrafficLights.Api.md)|Api (REST) project used to control the traffic lights module and access traffic lights execution logs.|
|[IoT.TrafficLights.Web](/.docs/IoT.TrafficLights.Web.md)|Web app project used to control the traffic lights module from a Razor UI HTTP web app client.|
|[IoT.TrafficLights.Console](/.docs/IoT.TrafficLights.Console.md)|Console app used to run traffic lights module or traffic sensor module.|

## Deployment

The easiest way to deploy the IoT Traffic Lights application is to run the [docker-compose.yaml](/.cicd/compose/docker-compose.yaml) Docker Compose file with the following command. This will deploy all the containers, volumes and networking on the Docker instance.

```bash
docker compose -f docker-compose.yaml --env-file vars.env up -d
```

To remove the IoT Traffic Lights application from Docker instance, use this command. This will remove all the containers, volumes and networking from Docker instance.

```bash
docker compose -f docker-compose.yaml --env-file vars.env down
```

The [vars.env](/.cicd/compose/vars.env) file provides all the environment specific configuration for the application. Use this table below as guide to configure the deployment.

|Key|Values|Description|
|-----|-----|-----|
|ENVIRONMENT|Development, Test, Production|Defines the release environment.|
|COMPOSE_PROJECT_NAME|dev, test, prod|This is used to prefix and set environmental boundaries for the application components. It allows to run Dev, Test and Prod simultaneously on the same Docker instance at the same time and not clash with each other.|
|COMPOSE_PROFILES|Mock, SQLite, MySql, MongoDb|Defines which application components are created and used as backend data storage, i.e. SQLite, or MySql or MongoDb database or Mock in-memory only hard-coded data samples.|
|DB_DIR_SQLITE|C:\Path\SQLite or /home/path/sqlite|SQLite database folder location where the database artifacts are stored.|
|DB_PORT_SQLITE|30xx|Port number to access SQLite database.|
|DB_DIR_MYSQL|C:\Path\MySql or /home/path/mysql|MySql database folder location where the database artifacts are stored.|
|DB_PORT_MYSQL|33xx|Port number to access MySql database.|
|DB_DIR_MONGODB|C:\Path\MongoDb or /home/path/mongodb|MongoDb database folder location where the database artifacts are stored.|
|DB_PORT_MONGODB|270xx|Port number to access MongoDb database.|
|DB_PWD|Password|Password to access the database, only applicable to MySql _root_ user.|
|API_PORT|80xx|Port to access the Api.|
|WEB_PORT|80xx|Port to access the Web app.|
|LOGLEVEL_CONSOLE|Verbose, Debug, Information, Warning, Error, Fatal|Log levels for the console output.|
|LOGLEVEL_DATABASE|Verbose, Debug, Information, Warning, Error, Fatal|Log levels to log in the Database.|
|GPIO|Mock, Pi|Define if the application is wired to Raspberry Pi or just in a Mock mode.|
|RUN|RunMock, RunTrafficSensor, RunTrafficTimer|Program to run in the Console app.|

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
|SQLite|SQLite backed database that is stored on a host machine file system as _.db_ file.<br />Communication with the database through a file system.<br />Default SQLite deployment doesn't configure any user credentials and can be accessed without such permissions.<br>_NOTE: Works on AMD and ARM architectures_.|
|SQLite.Url|PATH_TO_DATABASE\DATABASE_NAME.db|
|MySql|MySql backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _33xx_ over the network (IP Address) or localhost (127.0.0.1).<br />Use the default _root_ account and password of your choice that is configured during MySql database deployment.<br>_NOTE: Works on AMD and ARM architectures_.|
|MySql.Url|Server=127.0.0.1;Port=33xx;User ID=root;Password=YOUR_PASSWORD;Database=DATABASE_NAME|
|MongoDb|MongoDb backed database that can run either in Docker container or as standalone installation on the machine.<br />Communication with the database is on port _270xx_ over the network (IP Address) or localhost (127.0.0.1).<br />Default MongoDb deployment doesn't configure any user credentials and can be accessed without such permissions.<br>_NOTE: Works on AMD and ARM architectures (Pi 5 or higher)_.|
|MongoDb.Url|mongodb://localhost:270xx|