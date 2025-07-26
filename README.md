# Desafio BTG - Engenheiro de Software

**Status:** Em desenvolvimento

## Descrição do Projeto

Este projeto consiste em uma solução para processar pedidos de clientes em um microsserviço e listar em uma API. A arquitetura é baseada em microserviços e utiliza uma fila para comunicação assíncrona.

O sistema é composto por:
* Um **microserviço consumidor** que lê os pedidos de uma fila do RabbitMQ e os persiste em um banco de dados.
* Uma **API REST** que permite consultar informações consolidadas sobre os pedidos.

## Tecnologias Utilizadas

- **Visual Studio 2022 / .NET C# 8.0** — IDE / framework o desenvolvimento da API e do microsserviço.
- **RabbitMQ** — Fila de mensagens para comunicação assíncrona entre sistemas.
- **MongoDB** — Banco de dados NoSQL para persistência dos pedidos.
- **Docker** — Containerização dos serviços.
- **Docker Compose** — Orquestração local dos containers.
- **Git** — Controle de versão do código-fonte
- **GitHub** — Hospedagem e colaboração do repositóri
- **Swagger/OpenAPI** — Documentação dos endpoints da API.

## Como Executar o Projeto

Preencher no desenvolvimento...

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
