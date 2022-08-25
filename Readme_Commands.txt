docker commands

docker ps - to list the running docker images
docker ps -a to list the all docker images

catalog db - Mongo DB
docker run -d -p 27017:27017 --name shopping-mongo mongo

Basket db - Redis:alpine
docker run -d -p 6379:6379 --name aspnetrun-redis redis

portainer
localhost:9000
username: admin
password: admin123456789