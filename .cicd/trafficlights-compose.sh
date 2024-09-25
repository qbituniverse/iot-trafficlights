### Amd 64 ###
$environment = "development"

docker compose version
docker compose -f .cicd/compose/docker-compose.yaml --env-file .cicd/compose/vars-$environment.env up -d
docker compose -f .cicd/compose/docker-compose.yaml --env-file .cicd/compose/vars-$environment.env down



### Arm 64 ###
# Setup Folders
# Code
sudo mkdir -p /home/iot/code/TrafficLights
sudo chmod 777 /home/iot/code/TrafficLights
# Data
sudo mkdir -p /home/iot/data/TrafficLights
sudo mkdir -p /home/iot/data/TrafficLights/SQLite
sudo mkdir -p /home/iot/data/TrafficLights/MySql
sudo mkdir -p /home/iot/data/TrafficLights/MongoDb
sudo chmod 777 /home/iot/data/TrafficLights
sudo chmod 777 /home/iot/data/TrafficLights/SQLite
sudo chmod 777 /home/iot/data/TrafficLights/MySql
sudo chmod 777 /home/iot/data/TrafficLights/MongoDb

cd /home/iot/code/TrafficLights

environment=test

sudo docker compose version
sudo docker compose -f docker-compose.yaml --env-file vars-$environment.env up -d
sudo docker compose -f docker-compose.yaml --env-file vars-$environment.env down

# Clean-up
sudo rm -rf /home/iot/code/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights/SQLite
sudo rm -rf /home/iot/data/TrafficLights/MySql
sudo rm -rf /home/iot/data/TrafficLights/MongoDb