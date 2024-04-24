watch-api:
	dotnet watch run --project src/SynCore.Api

run-api:
	dotnet watch run --project src/SynCore.Api

build-frontend:
	rm -rf dist/
	npm --prefix src/SynCore.Vue run build

build: build-frontend
	dotnet publish -c Release -o dist/ --self-contained false src/SynCore.Api
	
build-linux: build-frontend
	dotnet publish -c Release -o dist/ --self-contained false --runtime linux-x64 src/SynCore.Api

docker-compose-up:
	docker compose up -d

docker-compose-down:
	docker compose down
	
docker-build:
	docker build --no-cache -t mayer2/syncore:latest .

docker-push: docker-build
	docker push mayer2/syncore:latest

server-deploy-docker: docker-push
	ssh mayer@143.198.51.145 "cd syncore ; ./deploy.sh"

server-deploy: build-linux
	scp -r dist mayer@143.198.51.145:/home/mayer/syncore