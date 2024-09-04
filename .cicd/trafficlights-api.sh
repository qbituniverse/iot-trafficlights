### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/iot/code/TrafficLights/TrafficLights.Api
sudo chmod 777 /home/iot/code/TrafficLights/TrafficLights.Api
sudo chmod 777 /home/iot/code/TrafficLights

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/iot/code/TrafficLights/TrafficLights.Api
export ASPNETCORE_ENVIRONMENT=Test
#dotnet TrafficLights.Api.dll --no-launch-profile
dotnet TrafficLights.Api.dll --launch-profile "Api-Test"

# Clean-up
sudo rm -rf /home/iot/code/TrafficLights/TrafficLights.Api



### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-trafficlights-api:latest -f .cicd/docker/Dockerfile-iot-trafficlights-api .
docker buildx rm -f iot-trafficlights-buildx



### Run Single Conrainer ###
sudo docker network create iot-trafficlights
sudo docker run -it --rm --name iot-trafficlights-api --network iot-trafficlights -e TZ=Europe/Warsaw -p 8010:8080 qbituniverse/iot-trafficlights-api:latest
sudo docker rm -fv iot-trafficlights-api
sudo docker rmi -f qbituniverse/iot-trafficlights-api:dev
