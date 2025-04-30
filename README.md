# Aletheia

## Integrantes

Pedro Moreira de Jesus - RM553912

Natan Junior Rodrigues Lopes - RM552626

Pedro Lucca Medeiros Miranda - RM553873

## Arquitetura Utilizada

Optamos por uma arquitetura de microservices para a API, pois já estamos desenvolvendo um monolito em outra matéria, com a adaptação final necessária apenas para transformar o backend em .NET em uma API para uso no mobile. Microservices nos permite maior flexibilidade, escalabilidade e manutenção, já que cada serviço é independente. Isso facilita a implementação de novos recursos, a evolução das funcionalidades e a alocação de recursos de forma eficiente, sem impactar o sistema como um todo, além de ser mais alinhado com a necessidade de consumir a API em diferentes plataformas.

## Serviço Externo Utilizado

A aplicação utiliza o [Seq](https://datalust.co/seq), um serviço de centralização e análise de logs, para consolidar e monitorar os registros gerados pela aplicação de forma eficiente.


## Como Rodar:

1. Clone o Repositório:

```bash
git clone https://github.com/Monty-Aletheia/OdontoPrev-dotNet.git
cd OdontoPrev-dotNet
```

2. Configuração do Ambiente:

- Crie um arquivo `.env` com as variáveis necessárias (ex: string de conexão, porta etc). Um exemplo pode estar disponível como `.env.example`.
- Certifique-se de que o Docker e o Docker Compose estão instalados e rodando na sua máquina.

3. Suba a Aplicação com Docker Compose:

```bash
docker-compose up --build
```

>Isso irá construir e iniciar os containers definidos no arquivo docker-compose.yml, incluindo a aplicação e, se houver, o banco de dados.

4. Envie as Requisições com o Postman:

- Importe o arquivo `OdontoPrev.postman_collection.json` (ou equivalente) no Postman.
- Use os endpoints definidos na collection para testar as rotas da API facilmente.

5. Acessar o Seq:

- Apos rodar o docker compose acesse o endpoint `localhost:5341` para ver os logs da aplicação
