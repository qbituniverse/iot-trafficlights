### Amd 64 ###
$env:DB_PWD=""
$env:DB_DIR=""

docker compose version
docker compose -f .cicd/compose/docker-compose.DockerHub-amd64.yaml up -d
docker compose -f .cicd/compose/docker-compose.DockerHub-amd64.yaml down



### Arm 64 ###
# Setup Folders
sudo mkdir -p /home/iot/code/TrafficLights
sudo mkdir -p /home/iot/data/TrafficLights
sudo mkdir -p /home/iot/data/TrafficLights/SQLite
sudo chmod 777 /home/iot/code/TrafficLights
sudo chmod 777 /home/iot/data/TrafficLights
sudo chmod 777 /home/iot/data/TrafficLights/SQLite
cd /home/iot/code/TrafficLights

export DB_PWD=""
export DB_DIR=""

sudo docker compose version
sudo docker compose -f docker-compose.DockerHub-arm64.yaml up -d
sudo docker compose -f docker-compose.DockerHub-arm64.yaml down

# Clean-up
sudo rm -rf /home/iot/code/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights
sudo rm -rf /home/iot/data/TrafficLights/SQLite
