# Desafio BTG - Engenheiro de Software

**Status:** Concluído

## Descrição do Projeto

Este projeto consiste em uma solução para processar pedidos de clientes em um microsserviço e listar em uma API. A arquitetura é baseada em microserviços e utiliza uma fila para comunicação assíncrona.

O sistema é composto por:
* Um **microserviço consumidor** que lê os pedidos de uma fila do RabbitMQ e os persiste em um banco de dados.
* Uma **API REST** que permite consultar informações consolidadas sobre os pedidos.

## Tecnologias Utilizadas

- **Visual Studio 2022 / .NET C# 8.0** — IDE / framework para o desenvolvimento da API e do microsserviço.
- **RabbitMQ** — Fila de mensagens para comunicação assíncrona.
- **MongoDB** — Banco de dados NoSQL para persistência dos pedidos.
- **Docker** — Containerização dos serviços.
- **Docker Compose** — Orquestração local dos containers.
- **Git** — Controle de versão do código-fonte.
- **GitHub** — Repositório de código-fonte.
- **Swagger/OpenAPI** — Documentação dos endpoints da API.

## Como Executar o Projeto
Requisitos
- Docker, Docker Compose, Git, MongoDB Compass, Visual Studio 2022

1. Clone o repositório
git clone https://github.com/reinaldots/desafiobtg
2. Acesse a pasta da solution
cd ~\desafiobtg
3. Suba todos os containers
docker-compose up --build
4. Abra e execute a solução no Visual Studio
cd ~\desafiobtg\solution\DesafioBTG.sln
5. A API REST e o MS serão abertos
MS - Será executado em janela de console
API REST - https://localhost:7017/swagger
RabbitMQ - http://localhost:15672 (acesso padrão: guest / guest)
MongoDB - mongodb://localhost:27017

Parar todos os containers
docker-compose down

## Endpoints da API

### `GET /api`
Confirmar que a API está em execução.
Resposta: "API está funcionando!"

### `POST /api/pedidos`
Incluir pedido na fila.
Payload de exemplo:
```json
{
  "codigoPedido": 1001,
  "codigoCliente": 1,
  "itens": [
    {
      "produto": "lápis",
      "quantidade": 100,
      "preco": 1.10
    },
    {
      "produto": "caderno",
      "quantidade": 10,
      "preco": 1.00
    }
  ]
}
```
Resposta: Pedido XXXX salvo na fila com sucesso.

### `GET /api/pedidos/{codigo_cliente}`
Obter pedidos por cliente
Parâmetro {codigo_cliente} (int): Código do cliente.
Resposta: 1 ou mais pedidos do cliente no formato JSON.

### `GET /api/pedidos/{codigo_cliente}/quantidade`
Obter a quantidade de pedidos feitos por um cliente.
Parâmetro {codigo_cliente} (int): Código do cliente.
Resposta: valor númerico com a quantidade total de pedidos do cliente

### `GET /api/pedidos/{codigo_pedido}/valortotal`
Obter o valor total de um pedido (quantidade x preço de cada item).
Parâmetro {codigo_pedido} (int): Código do pedido.
Resposta: valor númerico com o total do pedido.
