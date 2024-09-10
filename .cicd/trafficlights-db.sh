### MySql ###
# Network
docker network create iot-trafficlights

# Database
$DB_PWD=""
$DB_DIR=""

docker run --name iot-trafficlights-mysql --network iot-trafficlights -e MYSQL_ROOT_PASSWORD=${DB_PWD} -e MYSQL_DATABASE=TrafficLights -v ${DB_DIR}:/var/lib/mysql -d -p 3307:3306 mysql:latest
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
$DB_PWD=""
$DB_DIR=""

docker run --name iot-trafficlights-mongodb --network iot-trafficlights -v ${DB_DIR}:/data/db -d -p 27017:27017 mongo:latest
docker logs iot-trafficlights-mongodb

# Clean-up
docker rm -v -f iot-trafficlights-mongodb
docker network remove mongo-iot-trafficlights



### Mongo Express ###
docker run --name iot-trafficlights-mongoexpress --network compose_iot-trafficlights -e ME_CONFIG_MONGODB_SERVER=iot-trafficlights-mongodb -e TZ=Europe/Warsaw -d -p 8081:8081 mongo-express:latest
docker logs iot-trafficlights-mongoexpress

# admin:pass
http://localhost:8081

# Clean-up
docker rm -v -f iot-trafficlights-mongoexpress
