# Documentação dos Testes

## Objetivo
Validar o funcionamento completo da solução, desde o consumo da fila MQ pelo microsserviço até a persistência e consulta dos pedidos no banco de dados pela API.

---

## Estrutura da Solução

- **API REST** (.NET C# 8.0)
- **Microsserviço consumidor** (ConsoleApp .NET C# 8.0)
- **Fila**: RabbitMQ - pedidos
- **Banco de dados**: MongoDB
- **Coleção**: conforme definido no arquivo de parâmetros appsettings.json

---

## Testes com Postman

Uma coleção Postman com todos os endpoints da API para facilitar a validação da solução.

- [Download da coleção Postman](https://github.com/reinaldots/desafiobtg/tree/main/docs/Desafio%20BTG%20APIs.postman_collection.json)

### Como utilizar

1. Importe o arquivo `.postman_collection.json` no Postman.
2. Crie uma variável de ambiente chamada `url_api` com o endereço da API (ex: `http://localhost:7017`).
3. Execute os endpoints conforme desejado.

---

## Testes Realizados

### 0. Confirmar que a API está em execução (pré-teste)
- **Método**: GET
- **URL**: `http://localhost:{porta}/api`

## Resultado esperado:
- API está funcionando!
- HTTP 200 OK

Verificações:
- Confirmar que o Docker esteja em execução.

### 1. Incluir pedido na fila
- **Método**: POST
- **URL**: `http://localhost:{porta}/api/pedidos`
- **Payload**:
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

## Resultado esperado:
- Pedido XXXX salvo na fila com sucesso.
- HTTP 202 Accepted
- Microsserviço deve consumir e persistir no MongoDB

Verificações:
- Console do MS exibe: [HH:mm:ss] Pedido XXXX salvo com sucesso.
- MongoDB (coleção configurada): contém documento com pedido XXXX.

### 2. Obter pedidos por cliente
- **Método**: GET
- **URL**: `http://localhost:{porta}/api/pedidos/{codigo_cliente}`

## Resultado esperado:
- 1 ou mais pedidos do cliente no formato JSON
- Status HTTP: 200 OK

Verificações:
- Nenhum pedido foi encontrado.
- Status HTTP: 404 se não encontrar pedidos para o código de cliente informado

### 3. Obter a quantidade de pedidos feitos por um cliente
- **Método**: GET
- **URL**: `http://localhost:{porta}/api/pedidos/{codigo_cliente}/quantidade`

## Resultado esperado:
- valor númerico com a quantidade total de pedidos.
- Status HTTP: 200 OK

### 4. Obter o valor total de um pedido (quantidade x preço de cada item).
- **Método**: GET
- **URL**: `http://localhost:{porta}/api/pedidos/{codigo_pedido}/valortotal`

## Resultado esperado:
- valor númerico com o total do pedido.
- Status HTTP: 200 OK

Verificações:
- Nenhum pedido foi encontrado.
- Status HTTP: 404 se não encontrar pedidos para o código do pedido informado

## Teste de Integração com RabbitMQ e MongoDB
Fluxo esperado:
1. Enviar um pedido via POST /api/pedidos ou publicar manualmente na fila pedidos
2. API publica mensagem na fila pedidos
3. Microsserviço consome a mensagem
4. Pedido salvo no MongoDB
5. Dados disponíveis via GET nos demais endpoints

Verificação no MongoDB:
- Banco: conforme appsettings.json (DesafioBTG)
- Coleção: conforme MongoDB, CollectionName (Pedidos)
- Documento esperado: documento JSON com pedido XXXX.

## Ferramentas Utilizadas
- Postman: para enviar requisições e validar respostas da API.
- MongoDB Compass: para consultar dados persistidos.
- RabbitMQ Management UI: para monitorar a fila pedidos.
- Visual Studio 2022: para implementar a API e o microsserviço.
- Firefox: para testes web com Swagger na API.

## Conclusão
Todos os testes foram realizados com sucesso. A aplicação apresenta comportamento consistente e correto no fluxo de envio, consumo e persistência dos dados. API estável, microsserviço funcional e persistência confirmada.
