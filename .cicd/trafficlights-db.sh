### SQLite Browser ###
# Network
docker network create iot-trafficlights

# Database
$DB_DIR=""
export DB_DIR=""

docker run -d --name iot-trafficlights-sqlitebrowser --security-opt seccomp=unconfined -e PUID=1000 -e PGID=1000 -e TZ=Europe/Warsaw -p 3019:3000 -v ${DB_DIR}:/data/db --restart unless-stopped lscr.io/linuxserver/sqlitebrowser:latest
docker logs iot-trafficlights-sqlitebrowser

# Browse
http://localhost:3000

# Queries
SELECT * FROM ApiLogs ORDER BY TimeStamp DESC
SELECT * FROM WebLogs ORDER BY TimeStamp DESC
SELECT * FROM ConsoleLogs ORDER BY TimeStamp DESC
SELECT * FROM TrafficLogs ORDER BY TimeStamp DESC

# Clean-up
docker rm -v -f iot-trafficlights-sqlitebrowser
docker network remove iot-trafficlights



### MySql ###
# Network
docker network create iot-trafficlights

# Database
$DB_PWD=""
$DB_DIR=""

docker run --name iot-trafficlights-mysql --network iot-trafficlights -e TZ=Europe/Warsaw -e MYSQL_ROOT_PASSWORD=${DB_PWD} -e MYSQL_DATABASE=TrafficLights -v ${DB_DIR}:/var/lib/mysql -d -p 3319:3306 mysql:latest
docker logs iot-trafficlights-mysql

# Queries
SELECT * FROM TrafficLights.TrafficLogs ORDER BY TimeStamp DESC;
SELECT * FROM TrafficLights.ApiLogs ORDER BY TimeStamp DESC;
SELECT * FROM TrafficLights.WebLogs ORDER BY TimeStamp DESC;
SELECT * FROM TrafficLights.ConsoleLogs ORDER BY TimeStamp DESC;

# Clean-up
docker rm -v -f iot-trafficlights-mysql
docker network remove iot-trafficlights



### Mongo DB ###
# Network
docker network create iot-trafficlights

# Database
$DB_DIR=""

docker run --name iot-trafficlights-mongodb --network iot-trafficlights -e TZ=Europe/Warsaw -v ${DB_DIR}:/data/db -d -p 27019:27017 mongo:latest
docker logs iot-trafficlights-mongodb

# Clean-up
docker rm -v -f iot-trafficlights-mongodb
docker network remove mongo-iot-trafficlights



### Mongo Express ###
docker run --name iot-trafficlights-mongoexpress --network compose_iot-trafficlights -e ME_CONFIG_MONGODB_SERVER=iot-trafficlights-mongodb -e TZ=Europe/Warsaw -d -p 8019:8081 mongo-express:latest
docker logs iot-trafficlights-mongoexpress

# admin:pass
http://localhost:8181

# Clean-up
docker rm -v -f iot-trafficlights-mongoexpress
