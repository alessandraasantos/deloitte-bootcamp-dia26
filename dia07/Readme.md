# API de Gerenciamento de Lotes de Minério com Redis

## Descrição

Este projeto consiste em uma **API REST** desenvolvida em **.NET 10 (C#)** para gerenciamento de **lotes de minério**, permitindo operações completas de **CRUD** (Create, Read, Update e Delete).

Além do CRUD tradicional, a aplicação foi integrada ao **Redis** utilizando **Docker Compose**, permitindo o envio de eventos de processamento assíncrono por meio de **streams/mensageria**.

O objetivo do projeto é demonstrar:

* Arquitetura em camadas (Controller → Service → Data)
* Boas práticas com **DTOs**
* Persistência com **Entity Framework Core** e **PostgreSQL**
* Integração com **Redis** para processamento assíncrono
* Testes via **Insomnia** ou ferramentas similares

---

## Arquitetura do Projeto

A estrutura segue separação de responsabilidades:

```
MinhaApi
│
├── Controllers        → Endpoints HTTP da aplicação
├── Services           → Regras de negócio
├── Models             → Entidades do domínio
├── Dtos               → Objetos de transferência de dados
├── Data               → DbContext e configurações do EF Core
├── Fila               → Integração com Redis (Publisher)
└── Program.cs         → Configuração da aplicação
```

### Controllers

Responsáveis por receber requisições HTTP, validar dados de entrada e delegar o processamento para a camada de **Service**.

Endpoints disponíveis:

* `GET /lotes` → Lista todos os lotes
* `GET /lotes/{id}` → Busca lote por ID
* `POST /lotes` → Cria um novo lote
* `PUT /lotes/{id}` → Atualiza um lote existente
* `DELETE /lotes/{id}` → Remove um lote

### Services

Contém a **lógica de negócio** da aplicação, isolando regras do Controller e facilitando testes e manutenção.

Responsabilidades principais:

* Validação de dados
* Comunicação com o banco via **DbContext**
* Publicação de eventos no **Redis** após criação/atualização de lotes

### Models

Representam as entidades persistidas no banco de dados.

Exemplo de campos do lote:

* Código do lote
* Mina de origem
* TeorFe
* Umidade
* SiO2
* P
* Toneladas
* DataProducao
* Status
* LocalizacaoAtual

### DTOs

Utilizados para **entrada e saída de dados**, evitando exposição direta das entidades do banco.

Benefícios:

* Segurança
* Controle de versionamento
* Clareza nos contratos da API

### Data

Contém o **AppDbContext** e configurações do **Entity Framework Core** para conexão com **PostgreSQL**.

### Fila (Redis)

Responsável por publicar mensagens em um **stream do Redis**, permitindo processamento assíncrono de eventos relacionados aos lotes.

---

## Tecnologias Utilizadas

* .NET 10
* C#
* ASP.NET Core Web API
* Entity Framework Core
* Dbvear/PostgreSQL
* Redis
* Docker e Docker Compose
* Insomnia (testes de endpoints)

---

## Execução do Projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git
cd dia07
```

### 2. Subir serviços com Docker Compose

```bash
docker-compose up -d
```

Isso iniciará:

* PostgreSQL
* Redis

### 3. Executar a API

```bash
dotnet run
```

A aplicação ficará disponível em:

```
http://localhost:5035
```

---

## Testando a API

Utilize o **Insomnia** (ou Postman) para enviar requisições HTTP.

### Criar lote (POST)

```json
{
  "codigoLote": "MNA-00556-08897",
  "minaOrigem": "RS",
  "teorfe": 90.5,
  "unidade": 31.2,
  "siO2": 1.9,
  "p": 0.033,
  "toneladas": 90050.00,
  "dateProducao": "2026-02-12T14:00:00",
  "status": 1,
  "localizacaoAtual": "Gramado"
}
```

Após a criação, um **evento é publicado no Redis**, indicando processamento do lote.

---

## Fluxo com Redis

1. Um lote é criado/atualizado via API
2. A camada **Service** chama o **RedisPublisher**
3. Uma mensagem é enviada para um **stream do Redis**
4. Consumidores podem processar o evento de forma assíncrona

Esse padrão permite:

* Escalabilidade
* Desacoplamento entre serviços
* Processamento em background

---

## Boas Práticas Aplicadas

* Separação em camadas
* Uso de DTOs
* Injeção de dependência
* Código preparado para testes unitários
* Integração com mensageria

---

