# Plano de Trabalho - Desafio Engenheiro de Software BTG Pactual

**Nome:** Reinaldo Tenório dos Santos

**Data:** 23/07/2025

Este documento detalha o plano de trabalho para a execução das atividades solicitadas, incluindo a criação de tarefas e a estimativa de horas para cada uma.

| Item | Atividade                  | Tarefas                                                                                                                                                           | Estimativa (horas) |
| :--: | :------------------------: | :---------------------------------------------------------------------------------------------------------------------------------------------------------------: | :----------------: |
| 1    | Setup e Configuração       | - Criar repositório público no GitHub.<br>- Configurar o docker-compose inicial com RabbitMQ e MongoDB.<br>- Estruturar as soluções (.sln) no .NET para a API e o Micro Serviço | 6                  |
| 2    | Modelagem e Banco de Dados | - Modelar as entidades/documentos (Cliente, Pedido, Item) para o banco de dados.<br>- Implementar a camada de acesso a dados.           | 3                  |
| 3    | Desenvolvimento do MS      | - Criar o serviço de escuta para a fila do RabbitMQ.<br>- Implementar a lógica para processar a mensagem do pedido.<br>- Implementar a gravação dos dados no banco de dados | 10                 |
| 4    | Desenvolvimento da API REST | - Criar os endpoints da API REST para as consultas solicitadas.<br>- Implementar a lógica de leitura dos dados gravados no banco.<br>- Validar retorno dos dados das consultas. | 5                  |
| 5    | Integração e Docker        | - Integrar a API com os containers (DB e RabbitMQ).<br>- Finalizar e testar os endpoints. | 10                 |
| 6    | Testes Funcionais          | - Criar testes para validar o processamento da mensagem e os endpoints da API.<br>- Gerar evidências (prints, scripts de teste) para o relatório. | 4                  |
| 7    | Elaboração do Relatório Técnico | - Documentar todos os itens solicitados (diagramas, tecnologias, previsto vs realizado, etc). | 4                  |
| 8    | Entrega                    | - Preparar a entrega final do projeto.<br>- Verificar checklist de itens solicitados: código no github, relatório técnico, imagem docker, instruções para execução.| 3                  |

## Total Estimado

O projeto tem um **total de 45 horas estimadas** para ser concluído.
