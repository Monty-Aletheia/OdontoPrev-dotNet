# Aletheia

## Objetivo do Projeto

A Aletheia trata-se de um sistema que tem o objetivo de colher informações de clientes,
médicos e consultas, para posteriormente utilizando Inteligência Artificial e Ciência de
Dados, prever classificar padrões de consultas que indicam fraudes ou golpes que
possam prejudicar uma empresa

## Visão Geral do Projeto
O sistema é composto por uma API desenvolvida em .NET, que possui endpoints documentados via Swagger, 
facilitando o acesso e testes de funcionalidades. As principais entidades do sistema incluem clientes, 
médicos, consultas e sinistros, e a API permite a gestão desses dados, garantindo a escalabilidade e o suporte necessário 
para análises futuras de padrões suspeitos.

## Funcionalidades
- Cadastro e Gestão de Clientes, Médicos, Consultas e Sinistros: Endpoints para inserir, atualizar e consultar informações de clientes, médicos, consultas e sinistros.
- Documentação via Swagger: Todos os endpoints da API são documentados e acessíveis através de uma interface gráfica do Swagger.

## Como Rodar:
1. Clone o Repositório:

  ```bash
  git clone https://github.com/seu-usuario/seu-repositorio.git
  cd seu-repositorio
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
