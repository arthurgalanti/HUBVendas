docker compose down
#!/bin/bash
rm -rf mysql_data
docker compose -f docker-compose.yml --env-file ./environments/dev.env up --build -d