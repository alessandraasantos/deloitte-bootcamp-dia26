
# Bootcamp C# e .NET - Deloitte







Este repositório reúne os projetos, exercícios e evoluções desenvolvidos durante o bootcamp de desenvolvimento .NET.

A branch **develop** concentra todas as atividades desenvolvidas, incluindo APIs, POO, testes unitários, listas de exercícios e a organização do aprendizado dividido por dias.

---

## Estrutura do repositório

A branch `develop` está organizada da seguinte forma:

### Exercicios

Contém listas de exercícios de lógica e programação em C#, utilizadas para reforçar conceitos fundamentais da linguagem e da resolução de problemas.

* **Exercicios3/Lista3**
  Exercícios práticos com foco Programação Orientada a Objetos

---

### Minhaapi

Projeto principal de API REST desenvolvido em .NET.

Principais características:

* Implementação de operações CRUD
* Uso de DTOs para transferência de dados
* Persistência com Entity Framework Core
* Integração com banco de dados PostgreSQL no ambiente do Dbvear
* Utlização do Docker via contâiner
* Organização em camadas (Controllers, Models, DTOs, DbContext)
* Insomnia para realizar os métodos: Post, Get, Put e Delete
* 

Este projeto representa a evolução prática do aprendizado de desenvolvimento de APIs durante o bootcamp.

---

### dia00/primeira-api

Primeira API criada durante o início do bootcamp.

Objetivo:

* Introduzir a criação de endpoints HTTP
* Compreender a estrutura básica de um projeto ASP.NET
* Realizar testes iniciais de requisições

---

### dia01, dia02, dia03, dia05

Pastas que representam a evolução diária do aprendizado incluindo cases para colocar em prática o que foi aprendido.

Cada dia contém:

* Exemplos de código
* Organização de Pastas
* Exercícios práticos relacionados ao conteúdo estudado no dia
* Cases

---

### dia06/MinhaApi.Tests

Projeto de **testes unitários** da API principal.

Características:

* Testes unitários testando cada parte individual do projeto
* Validação de regras de negócio e comportamentos da API
* Garantia de funcionamento correto dos métodos implementados

Execução dos testes:

```bash
dotnet test
```

---

---

## Objetivo do repositório

Este repositório tem como finalidade:

* Consolidar conhecimentos em C# e .NET
* Praticar o desenvolvimento de APIs REST
* Aprender sobre os métodos: Put, Get, Put e Delete por meio de um Crud
* Aplicar testes unitários para validação de funcionalidades
* Organizar a evolução do aprendizado por etapas do bootcamp

Também serve como material de estudo, prática e portfólio de desenvolvimento.

---

## Como executar o projeto

### Clonar o repositório

```bash
git clone https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git
```

### Acessar a branch de desenvolvimento

```bash
git checkout develop
```

### Restaurar dependências

```bash
dotnet restore
```

### Executar a API principal

```bash
dotnet run --project Minhaapi
```

### Executar os testes unitários

```bash
dotnet test
```

---

## Fluxo de branches

* **main**
  Contém a versão inicial do projeto.

* **develop**
  Contém o desenvolvimento contínuo, incluindo novas funcionalidades, exercícios, APIs e testes unitários.

---

