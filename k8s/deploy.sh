#!/bin/bash

set -e 

K8S_DIR="./"

echo "🚀 Iniciando deploy de arquivos YAML em: $K8S_DIR"

find "$K8S_DIR" -type f -name "*.yml" | sort | while read file; do
  echo "🔄 Aplicando: $file"
  kubectl apply -f "$file"
done

echo "✅ Deploy finalizado com sucesso!"
