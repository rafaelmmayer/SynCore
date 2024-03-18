run:
	dotnet watch run --project src/SynCore.Api

build:
	rm -rf dist/
	dotnet publish -c Release -o dist/ src/SynCore.Api

docker:
	docker compose up -d

docker-down:
	docker compose down