
# Projeto de API com CRUD – Lotes de Minério

## Descrição

Esta API foi desenvolvida em **.NET 10** com **C#** para realizar operações CRUD (Create, Read, Update, Delete) em lotes de minério. O projeto utiliza **DTOs** para transferência de dados, **Entity Framework Core** para persistência e **PostgreSQL** como banco de dados Integrado ao **Dbvear**.

---

## Estrutura do Projeto

* **Minhaapi/Data**: Contém arquivos relacionados à configuração do banco de dados.
* **Minhaapi/Models**: Contém as classes que representam as entidades da aplicação.

  * `LoteMinerio.cs` – Modelo de lote de minério.
* **Minhaapi/Dtos**: Contém classes DTO (Data Transfer Object) para entrada e saída de dados.

  * `CreateLoteMinerioDto.cs` – DTO para criar um lote.
  * `LoteMinerioResponseDto.cs` – DTO para resposta.
  * `LotesMinerioUpdateDto.cs` – DTO para atualizar um lote.
  * `LotesMinerioDeleteDto.cs` – DTO para deletar um lote.
* **Minhaapi/Controllers**: Contém os controladores que expõem os endpoints da API.

  * `LotesMinerioControllers.cs` – Controller responsável pelo CRUD.

---

## Funcionalidades

A API permite:

1. **Criar** um novo lote de minério.
2. **Consultar** lotes existentes.
3. **Atualizar** informações de um lote.
4. **Deletar** um lote específico.

---

## Endpoints

| Método | Endpoint          | Descrição                          |
| ------ | ----------------- | ---------------------------------- |
| POST   | `/api/lotes`      | Cria um novo lote de minério       |
| GET    | `/api/lotes`      | Retorna todos os lotes             |
| GET    | `/api/lotes/{id}` | Retorna um lote específico pelo ID |
| PUT    | `/api/lotes/{id}` | Atualiza um lote existente         |
| DELETE | `/api/lotes/{id}` | Deleta um lote existente           |

---

## Tecnologias Utilizadas

* C# / .NET 7
* Entity Framework Core
* PostgreSQL
* Docker (para ambientes)
* Visual Studio Code

---

## Como Rodar a API

1. Clone o repositório:

```bash
git clone <https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git>
```


2. Navegue até a pasta do projeto:

```bash
cd Deloitte-Bootcamp-dia26/Minhaapi
```

3. Restaure os pacotes e rode a aplicação:

```bash
dotnet restore
dotnet run
```

4. Acesse a API via navegador ou ferramenta como **Insomnia/Postman**:

```
https://localhost:5035/api/LotesMinerio

```
## Comandos Git úteis

### Listar branches locais

```bash
git branch
```

### Listar branches locais e remotas

```bash
git branch -a
```

### Baixar atualizações do remoto

```bash
git fetch
```

### Trocar para a branch develop

```bash
git checkout develop
```

### Atualizar a branch develop local com o remoto

```bash
git pull origin develop
```

> Esse fluxo garante que você esteja trabalhando com a versão mais recente do projeto na branch **develop**.

---

## Observações

* Certifique-se de ter o **PostgreSQL** rodando e atualizado no `appsettings.json`.
* DTOs foram utilizados para separar a estrutura de dados da API das entidades do banco.
* A API segue boas práticas de desenvolvimento, como arquitetura limpa e padronização de nomes.

---


