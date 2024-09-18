### Amd 64 ###
$env:DB_PWD=""
$env:DB_DIR=""

docker compose version
docker compose -f .cicd/compose/docker-compose.sqlite.yaml up -d
docker compose -f .cicd/compose/docker-compose.sqlite.yaml down



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

export DB_PWD=""
export DB_DIR=""

sudo docker compose version
sudo docker compose -f .cicd/compose/docker-compose.sqlite.yaml up -d
sudo docker compose -f .cicd/compose/docker-compose.sqlite.yaml down

# Clean-up
sudo rm -rf /home/iot/code/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights/SQLite
sudo rm -rf /home/iot/data/TrafficLights/MySql
sudo rm -rf /home/iot/data/TrafficLights/MongoDb