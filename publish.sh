docker buildx build --platform linux/amd64 -f src/SeqProxy/Dockerfile -t zlzforever/seq-proxy .
docker push zlzforever/seq-proxy