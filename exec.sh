docker compose down
docker compose -f docker-compose.yml --env-file ./environments/dev.env up --build -d