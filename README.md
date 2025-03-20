# Aletheia

## Integrantes

Pedro Moreira de Jesus - RM553912

Natan Junior Rodrigues Lopes - RM552626

Pedro Lucca Medeiros Miranda - RM553873

## Arquitetura Utilizada

Optamos por uma arquitetura de microservices para a API, pois já estamos desenvolvendo um monolito em outra matéria, com a adaptação final necessária apenas para transformar o backend em .NET em uma API para uso no mobile. Microservices nos permite maior flexibilidade, escalabilidade e manutenção, já que cada serviço é independente. Isso facilita a implementação de novos recursos, a evolução das funcionalidades e a alocação de recursos de forma eficiente, sem impactar o sistema como um todo, além de ser mais alinhado com a necessidade de consumir a API em diferentes plataformas.

## Design Patterns Utilizados

### 1. Singleton

- Garantiu que apenas uma instância de HttpClient seja criada e reutilizada ao longo da aplicação, otimizando conexões HTTP.

### 2. Object Mapper (AutoMapper)

- Facilitou a conversão de objetos entre tipos (como entidades de domínio e DTOs) utilizando o AutoMapper, centralizando a lógica de mapeamento e melhorando a manutenibilidade.

### 3. Repository

- Encapsulou a lógica de acesso a dados com a interface IConsultationRepository e a implementação ConsultationRepository, desacoplando a lógica de negócios da persistência e tornando o código mais testável e manutenível.

## Como Rodar:

1. Clone o Repositório:

```bash
git clone https://github.com/Monty-Aletheia/OdontoPrev-dotNet.git
cd OdontoPrev-dotNet
```

2. Configuração do Ambiente:

- Certifique-se de ter o .NET SDK instalado.
- Configure as variáveis de ambiente necessárias (como strings de conexão).

3. Instale as Dependências: Execute o seguinte comando para instalar as dependências do projeto:

```bash
dotnet restore
```

4. Execute o Projeto: Inicie o projeto utilizando o comando:

```bash
dotnet run
```

5. Acesse a Documentação da API: A documentação Swagger estará disponível em:

```bash
http://localhost:{porta}/swagger
```

## Exemplos Entidade

### Dentist

```json
{
  "name": "Dr. Ana Silva",
  "specialty": "Cardiologia",
  "registrationNumber": "CRM123456",
  "claimsRate": 2.5,
  "riskStatus": 1
}
```

### Pacient

```json
{
  "name": "João Pereira",
  "birthday": "1985-07-12T00:00:00.000Z",
  "gender": 1,
  "riskStatus": 2,
  "consultationFrequency": 3,
  "associatedClaims": "Hipertensão, Diabetes"
}
```

### Consultation

```json
{
  "consultationDate": "2025-03-20T14:30:00.000Z",
  "consultationValue": 150,
  "riskStatus": 1,
  "patientId": "846c79da-4960-4dd1-9776-b1b67bed345e",
  "dentistIds": [
    "7cf455ad-9829-4a9e-8962-c261fd9a62b1",
    "33c73423-e5cf-4830-ae78-5ad8ac49d8d7"
  ],
  "description": "Consulta de rotina e limpeza."
}
```

### Claim

```json
{
  "occurrenceDate": "2025-03-20T10:45:00.000Z",
  "value": 500.75,
  "claimType": 1,
  "suggestedPreventiveAction": "Revisão periódica a cada 3 meses.",
  "consultationId": "1ca5c580-3220-4a84-96ab-2ce5d1e3cc82"
}
```