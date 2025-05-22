# Aletheia

## Integrantes

- Pedro Moreira de Jesus - RM553912
- Natan Junior Rodrigues Lopes - RM552626
- Pedro Lucca Medeiros Miranda - RM553873

## Descrição do Projeto
A Aletheia trata-se de um sistema que tem o objetivo de colher informações de clientes, médicos e consultas, para posteriormente utilizando Inteligência Artificial e Ciência de Dados, prever e classificar padrões de consultas que indicam fraudes ou golpes que possam prejudicar uma empresa.

## Arquitetura Utilizada

O projeto adota arquitetura de microserviços, cada um responsável por um domínio específico:

- **AuthService**: Autenticação e autorização
- **PatientService**: Gestão de pacientes
- **DentistService**: Gestão de dentistas
- **ConsultationService**: Consultas
- **ClaimService**: Sinistros
- **MlNetWorker**: Serviço de IA para avaliação de risco ([MlNetWorker/AI/ModelBuilder.cs](MlNetWorker/AI/ModelBuilder.cs))
- **OcelotApiGateway**: API Gateway para roteamento

> Veja a estrutura detalhada nas pastas do repositório.

## Diagrama de Arquitetura

![Diagrama de Arquitetura](https://i.imgur.com/Kfu5wl0.png)

## Tecnologias Utilizadas

- .NET 8
- Docker & Docker Compose
- ML.NET
- Seq (centralização de logs)
- Ocelot (API Gateway)
- Azure Pipelines (CI/CD)

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

## Exemplo de Requisição

Você pode testar os endpoints utilizando o Postman (veja instruções acima) ou realizar uma requisição manual, por exemplo, para cadastrar um paciente:

```http
POST http://localhost:5000/api/patient
Content-Type: application/json

{
  "name": "João Pereira",
  "birthday": "1985-07-12T00:00:00.000Z",
  "gender": "Male",
  "riskStatus": "Low",
  "consultationFrequency": 3,
  "associatedClaims": "Hipertensão, Diabetes"
}
```

**Dica:**  
Utilize o arquivo `OdontoPrev.postman_collection.json` disponível no repositório para importar no Postman e testar todos os endpoints de forma prática e rápida.

## Como funciona a IA

O serviço [`MlNetWorker`](MlNetWorker/) utiliza o arquivo de dados [MlNetWorker/Data/dados_sinistro.csv](MlNetWorker/Data/dados_sinistro.csv) para treinar um modelo de regressão com ML.NET. O modelo é salvo em [`MlNetWorker/AI/AletheIA.zip`](MlNetWorker/AI/AletheIA.zip) e utilizado para prever a probabilidade de fraude em sinistros odontológicos.

## Gerando o Modelo de IA

> ⚠️ **Importante:**  
> Só é necessário rodar este passo caso tenha ocorrido alguma atualização no código da IA ou nos dados de treinamento.

Antes de utilizar o sistema, é necessário treinar e gerar o modelo de Inteligência Artificial utilizado para previsão de fraudes.  
Para isso, execute o serviço **MlNetWorker** com o argumento `generate-ai`:

```bash
dotnet run --project MlNetWorker -- generate-ai
```

Ou, se estiver usando Docker Compose, adapte o comando para passar o argumento ao container:

```bash
docker compose run mlnetworker generate-ai
```

Esse comando irá processar os dados em `MlNetWorker/Data/dados_sinistro.csv` e gerar o modelo em `MlNetWorker/AI/AletheIA.zip`.  
Após a geração, o serviço MlNetWorker pode ser iniciado normalmente e estará pronto para realizar previsões.

## Licença

Este projeto está licenciado sob a licença [MIT](LICENSE).