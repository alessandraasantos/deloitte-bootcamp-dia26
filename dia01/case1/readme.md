
# API Piloto – Cadastro de Usuários

Esta é uma **API REST simples em ASP.NET Core** desenvolvida como projeto piloto para praticar a organização de uma API em camadas, uso de **DTOs**, **Controllers** e configuração básica com **Swagger**.

Atualmente, a API possui um endpoint para **validação/criação de usuários**, retornando uma resposta de sucesso quando os dados são válidos.

---

## Tecnologias Utilizadas

* **.NET 10**
* **C#**
* **Swagger (OpenAPI)**
* **Visual Studio / VS Code**

---

##  Estrutura do Projeto

```bash
api-piloto/
│
├── Controllers/
│   └── UsuariosController.cs
│
├── DTOs/
│   └── UsuarioCreateDto.cs
│
├── Services/
│   └── IUsuarioService.cs
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── api-piloto.csproj
```

### Controllers

Responsáveis por receber as requisições HTTP.

* **UsuariosController**

  * Possui o endpoint `POST` para criação/validação de usuários.

### DTOs (Data Transfer Objects)

Utilizados para transferir dados entre a requisição e a aplicação.

* **UsuarioCreateDto**

  * Representa os dados necessários para criar um usuário.

### Services

Define regras de negócio da aplicação.

* **IUsuarioService**

  * Interface que define o método de criação de usuário.

---

## 🔗 Endpoint Disponível

### ➕ Criar Usuário

**POST** `/api/usuarios`

 **Body (JSON):**

```json
{
  "nome": "João",
  "email": "joao@email.com",
  "senha": "123456"
}
```
 **Resposta de Sucesso:**

```json
"Usuário válido!"
```

> No momento, o endpoint apenas valida a requisição e retorna uma mensagem de sucesso. Não há persistência em banco de dados.

---

##  Swagger (Documentação)

Após rodar a aplicação, a documentação interativa pode ser acessada em:

```
https://localhost:{porta}/swagger
```

O Swagger permite testar os endpoints diretamente pelo navegador.

---

##  Como Executar o Projeto

1. Clone o repositório:

```bash
git clone <https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git>
```

2. Acesse a pasta do projeto:

```bash
cd api-piloto
```

3. Execute a aplicação:

```bash
dotnet run
```

4. Acesse o Swagger pelo navegador.

---

## Objetivo do Projeto

Este projeto tem como objetivo:

* Praticar a criação de APIs REST em .NET
* Aplicar organização em camadas (Controller, DTO e Service)
* Utilizar Swagger para documentação
* Consolidar conceitos iniciais de desenvolvimento backend

---


