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

## Testes Realizados
