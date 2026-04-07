.PHONY: up down start migrate

up:
	docker compose up -d

down:
	docker compose down

start:
	docker compose exec airportapi dotnet run --project AirportAPI.csproj --urls http://0.0.0.0:5000

migrate:
	docker compose exec airportapi sh -c "dotnet tool install --global dotnet-ef 2>/dev/null; export PATH=\"$$PATH:/root/.dotnet/tools\"; dotnet ef database update --project AirportAPI.csproj"
