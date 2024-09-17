### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/iot/code/TrafficLights/IoT.TrafficLights.Web
sudo chmod 777 /home/iot/code/TrafficLights/IoT.TrafficLights.Web
sudo chmod 777 /home/iot/code/TrafficLights

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/iot/code/TrafficLights/IoT.TrafficLights.Web
export ASPNETCORE_ENVIRONMENT=Test
#dotnet IoT.TrafficLights.Web.dll --no-launch-profile
dotnet IoT.TrafficLights.Web.dll --launch-profile "Web-Test"

# Clean-up
sudo rm -rf /home/iot/code/TrafficLights/IoT.TrafficLights.Web



### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-trafficlights-web:latest -f .cicd/docker/Dockerfile-iot-trafficlights-web .
docker buildx rm -f iot-trafficlights-buildx



### Run Single Conrainer ###
sudo docker network create iot-trafficlights
sudo docker run -it --rm --name iot-trafficlights-web --network iot-trafficlights -e TZ=Europe/Warsaw -p 8020:8080 qbituniverse/iot-trafficlights-web:latest
sudo docker rm -fv iot-trafficlights-web
sudo docker rmi -f qbituniverse/iot-trafficlights-web:latest
