watch-api:
	dotnet watch run --project src/SynCore.Api

run-api:
	dotnet watch run --project src/SynCore.Api

build:
	rm -rf dist/
	dotnet publish -c Release -o dist/ src/SynCore.Api

docker-compose-up:
	docker compose up -d

docker-compose-down:
	docker compose down
	
docker-build:
	docker build -t 460032777393.dkr.ecr.us-east-1.amazonaws.com/syncore:latest .

docker-push: docker-build
	docker push 460032777393.dkr.ecr.us-east-1.amazonaws.com/syncore:latest

server-deploy: docker-push
	ssh ubuntu@23.20.253.166 "cd syncore ; ./deploy.sh"