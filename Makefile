.PHONY: up down restart logs status build reset-db infra dev

up:
	docker compose up -d --build    # ← добавить --build

down:
	docker compose down

restart:
	docker compose down
	docker compose up -d --build    # ← добавить --build

logs:
	docker compose logs -f

status:
	docker compose ps

build:
	docker compose build --no-cache

reset-db:
	docker compose down -v
	docker compose up -d --build    # ← добавить --build

infra:
	docker compose -f docker-compose.dev.yml up -d

dev:
	dotnet run --project src/Services/TradingLab.Journal/TradingLab.Journal

status:
	docker compose ps