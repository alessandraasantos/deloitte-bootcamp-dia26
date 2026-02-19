
# Sistema de Monitoramento de Equipamentos Pesados (Vale)

Este projeto foi desenvolvido como desafio final do Bootcamp Deloitte/Vale.

A aplicação consiste em uma API REST para monitoramento e gestão de frotas pesadas em operações de mineração, permitindo o controle de horímetros, localização e status operacional dos equipamentos.

---

## Sobre o Projeto

A API gerencia o ciclo de vida de equipamentos como Caminhões, Escavadeiras e Perfuratrizes, garantindo que as regras de negócio da operação sejam respeitadas por meio de validações rígidas e persistência segura em banco de dados.

---

## Funcionalidades Principais

### CRUD Completo
- Criação, leitura, atualização e exclusão de equipamentos.

### Filtros Avançados
- Busca dinâmica por:
  - Tipo
  - Status operacional
  - Código (busca parcial)

### Paginação
- Controle de volume de dados via parâmetros:
  - `page`
  - `pageSize`

### Segurança e Validações
- Bloqueio de códigos duplicados (Unique Index).
- Validação de horímetro (não permite valores negativos).
- Tratamento automático de strings (Trim no campo código).
- Regra de exclusão:
  - Impede a remoção de equipamentos com histórico de manutenção concluída.
  - Retorna **409 – Conflict** caso a regra seja violada.

---

## Arquitetura e Tecnologias

- **Linguagem:** C# (.NET 10)
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core
- **Padronização:** Snake Case Naming Convention  
  Exemplo no banco: `data_aquisicao`
- **Containerização:** Docker e Docker Compose

---

## Como Executar

### 1. Requisitos

- .NET 10 SDK
- PostgreSQL
- Docker
- Insomnia (para testes da API)

### 2. Execução via Docker (Recomendado)

Para subir o banco de dados e a API simultaneamente:


docker compose up -d

### 2. Comando para rodar via terminal 

dotnet run

A API estará disponível em:

```
http://localhost:5071
```

---

## Endpoints da API

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/equipamentos` | Cadastra um novo equipamento |
| GET | `/api/equipamentos` | Lista com filtros (tipo, status, código) e paginação |
| GET | `/api/equipamentos/{id}` | Detalhes de um equipamento específico |
| PUT | `/api/equipamentos/{id}` | Atualização completa dos dados |
| DELETE | `/api/equipamentos/{id}` | Remoção (valida regras de manutenção) |

---

## Coleção de Testes (Insomnia / cURL)

### POST /api/equipamentos — Criar equipamento

```bash
curl --request POST \
  --url http://localhost:5071/api/equipamentos \
  --header 'Content-Type: application/json' \
  --data '{
  "codigo": "CAT-793F-000123",
  "tipo": "Caminhao",
  "modelo": "Caterpillar 793F",
  "horimetro": 18234.5,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2019-03-15",
  "localizacaoAtual": "Mina Carajás N4E"
}'
```

```bash
curl --request POST \
  --url http://localhost:5071/api/equipamentos \
  --header 'Content-Type: application/json' \
  --data '{
  "codigo": "ESC-HYD-2026-X1",
  "tipo": "Escavadeira",
  "modelo": "Komatsu PC8000",
  "horimetro": 4500.25,
  "statusOperacional": "EmManutencao",
  "dataAquisicao": "2023-05-20",
  "localizacaoAtual": "Oficina Central N1"
}'
```
```bash
curl --request POST \
  --url http://localhost:5071/api/equipamentos \
  --header 'Content-Type: application/json' \
  --header 'User-Agent: insomnia/12.3.1' \
  --data '{
  "codigo": "PERF-ATLAS-789",
  "tipo": "Perfuratriz",
  "modelo": "Atlas Copco Pit Viper",
  "horimetro": 890.0,
  "statusOperacional": "Parado",
  "dataAquisicao": "2025-11-02",
  "localizacaoAtual": "Pátio de Estocagem B"
}'
```
---

### GET /api/equipamentos — Listar com filtro

```bash
curl --request GET \
  --url 'http://localhost:5071/api/equipamentos?status=EmManutencao'
```

---

### GET /api/equipamentos/{id} — Detalhar equipamento

```bash
curl --request GET \
  --url http://localhost:5071/api/equipamentos/1
```
### GET /api/equipamentos/{id} - Page Size

```bash
curl --request GET \
  --url 'http://localhost:5071/api/equipamentos?page=1&pageSize=2' \
  --header 'User-Agent: insomnia/12.3.1'
  ```
---

### PUT /api/equipamentos/{id} — Atualizar equipamento

```bash
curl --request PUT \
  --url http://localhost:5071/api/equipamentos/3 \
  --header 'Content-Type: application/json' \
  --data '{
  "id": 3,
  "codigo": "PERF-ATLAS-789",
  "tipo": "Perfuratriz",
  "modelo": "Atlas Copco Pit Viper",
  "horimetro": 890.0,
  "statusOperacional": "Operacional",
  "dataAquisicao": "2025-11-02",
  "localizacaoAtual": "Pátio de Estocagem B"
}'
```

---

### DELETE /api/equipamentos/{id} — Remover equipamento

Permitido apenas se não houver manutenções concluídas associadas.  
Caso exista, a API retornará **409 – Conflict**.

```bash
curl --request DELETE \
  --url http://localhost:5071/api/equipamentos/2
```

```bash
curl --request DELETE \
  --url http://localhost:5071/api/equipamentos/3
```

