
# Docker config

<!-- docker run -d -p 27017:27017 --name mongodb mongo:latest -->

<!-- docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/docker/volumes/postgres:/var/lib/postgresql/data postgres -->

```
sudo docker run --name postgres-alpine -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres:alpine
```

```
sudo docker -it exec 'bash'
```


Connect to the running container

```
sudo docker exec -it some-postgres bash
```

In the container run:

```
psql -U postgres
```
