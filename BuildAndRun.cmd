 docker container stop Items-container
 docker build -t items:latest .
 docker run --rm -p 80:80 --name Items-container items