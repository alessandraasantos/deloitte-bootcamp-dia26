
---

# Bootcamp C# e .NET – Deloitte

Este repositório reúne os **projetos, exercícios e evolução prática** desenvolvidos ao longo do bootcamp de desenvolvimento em **C# e .NET**.

A branch **develop** concentra todas as atividades realizadas, incluindo:

* Desenvolvimento de **APIs REST**
* Aplicação de **Programação Orientada a Objetos (POO)**
* Implementação de **CRUD completo**
* Criação de **testes unitários**
* Uso de **Docker, PostgreSQL e Redis**
* Organização do aprendizado por **dias de evolução**

Este projeto representa a consolidação prática do aprendizado adquirido durante o bootcamp.

---

# Estrutura do repositório

A branch `develop` está organizada por **dias de estudo**, conforme descrito abaixo.

---

## dia00 – primeira-api

Primeira API criada durante o início do bootcamp.

**Objetivos:**

* Introduzir a criação de endpoints HTTP
* Compreender a estrutura básica de um projeto ASP.NET
* Realizar testes iniciais de requisições
  
**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia00](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia00)

---

## dia01 – API Piloto (Cadastro de Usuários)

API REST simples desenvolvida em **C# e .NET** para praticar:

* Organização em camadas (**Controllers, Services, DTOs**)
* Validação de dados
* Configuração do **Swagger**

Atualmente possui endpoint para validação/criação de usuários com retorno de sucesso quando os dados são válidos.

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia01](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia01)

---

## dia02 – Aplicações Console (POO)

Contém dois projetos:

### case2 – Controle de Estoque

Aplicação em C# via **Console**, com foco em:

* Orientação a objetos
* Validação de dados
* Regras de negócio

**Funcionalidades:**

* Cadastrar, editar, remover e listar produtos em memória
* Simulação de estoque simples

### case2.1 – Controle de Check-in de Visitantes

Sistema de gerenciamento de visitantes com:

* Cadastro de visitantes
* Listagem e busca por nome
* Registro de saída
* Tratamento de erros com `try/catch`

**Extras:**

* Ordenação por ID
* Filtro de primeira visita

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia02](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia02)

---

## dia03 – Projeto de Mineração (POO)

Projeto para aplicação de conceitos de **Programação Orientada a Objetos**, incluindo:

* Encapsulamento
* Regras de negócio
* Controle de acesso
* Relacionamento entre classes

O sistema simula o processo de:

* Extração de minério
* Produção
* Armazenamento em estoque

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia03](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia03)

---

## dia04 – Exercícios de POO

Implementação de duas classes:

### Lampada.cs

* Atributos e métodos de **ligar/desligar**

### ContaCorrente.cs

* Número, saldo, status especial e limite
* Métodos de:

  * Saque com validação
  * Depósito
  * Consulta de saldo
  * Verificação de uso do cheque especial

Ambas integradas ao `Program.cs`.

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia04/Exercicios3/Lista3](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia04/Exercicios3/Lista3)

---

## dia05 – API REST com CRUD

Projeto principal da API desenvolvido em **.NET**.

**Principais características:**

* Operações **CRUD completas**
* Uso de **DTOs**
* Persistência com **Entity Framework Core**
* Banco **PostgreSQL**
* Execução em **Docker**
* Arquitetura em camadas
* Testes de endpoints via **Insomnia**

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia05](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia05)

---

## dia06 – Testes Unitários

Projeto dedicado a **testes unitários** da API principal.

**Inclui:**

* Testes isolados por componente
* Validação de regras de negócio
* Garantia de funcionamento correto dos métodos

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia06](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia06)

**Execução dos testes:**

```bash
dotnet test
```

---

## dia07 – Integração com Redis e Filas

Evolução da API com:

* Integração do **Docker** com **Redis (NoSQL)**
* Criação de **filas de processamento**
* Classes de:

  * Conexão
  * Contrato
  * Implementação de envio de mensagens

Testes realizados via **Insomnia** e monitoramento com **Redis Insight**.

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia07](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia07)

---

## dia08 – Regras de Negócio e Serviços

Expansão da API com:

* Métodos **GET e POST sem body JSON**
* Classificação de qualidade do lote
* Cálculo de preço por toneladas
* Histórico de movimentações
* Avanço de status do lote
* Penalidade por umidade
* Camada **Service**
* Controller adicional (**LotesExtraController**)
* Uso contínuo de **Redis, Docker e filas**

**Link:**
[https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia08](https://github.com/alessandraasantos/deloitte-bootcamp-dia26/tree/develop/dia08)

---

# Objetivo do repositório

* Consolidar conhecimentos em **C# e .NET**
* Praticar desenvolvimento de **APIs REST**
* Implementar **CRUD completo**
* Aplicar **testes unitários**
* Organizar a evolução do aprendizado por etapas

Também funciona como **material de estudo, prática e portfólio**.

---

# Como executar o projeto

## Clonar o repositório

```bash
git clone https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git
```

## Acessar a branch develop

```bash
git checkout develop
```

## Restaurar dependências

```bash
dotnet restore
```

## Executar a API principal

```bash
dotnet run --project Minhaapi
```

## Executar os testes unitários

```bash
dotnet test
```

---

# Fluxo de branches

**main**
Versão inicial do projeto.

**develop**
Branch principal de desenvolvimento contínuo, contendo exercícios, APIs, testes e novas funcionalidades.

---


