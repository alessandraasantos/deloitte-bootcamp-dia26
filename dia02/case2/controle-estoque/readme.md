
## Controle de Estoque ##

## Descrição

Este  projeto consiste em uma aplicação de **controle de estoque em C#**, desenvolvida como aplicação **Console**, com foco na aplicação de **orientação a objetos**, **validação de dados** e **regras de negócio**.

O sistema permite cadastrar, editar, remover e listar produtos em memória, simulando um estoque simples.

---

## Tecnologias Utilizadas

* C#
* .NET (Console Application)
* Programação Orientada a Objetos (POO)


---

##  Descrição das Classes

### 🔹 Produto.cs

Classe responsável por representar um produto do estoque.

**Atributos:**

* `Nome` (string)
* `Preco` (double)
* `Quantidade` (int)

**Principais responsabilidades:**

* Validar regras de negócio no construtor
* Garantir que o produto tenha dados válidos
* Atualizar preço e quantidade de forma controlada

**Regras de negócio aplicadas:**

* Nome não pode ser vazio
* Preço deve ser maior que zero
* Quantidade não pode ser negativa

---

### 🔹 Program.cs

Classe principal responsável pela execução do sistema e interação com o usuário via terminal.

**Funcionalidades disponíveis no menu:**

1. Adicionar produto
2. Editar produto
3. Remover produto
4. Listar produtos
5. Sair

**Características importantes:**

* Uso de `List<Produto>` para armazenar o estoque
* Validação de entradas do usuário
* Tratamento de exceções (`try/catch`) para evitar falhas na execução
* Busca de produtos pelo nome (ignorando maiúsculas e minúsculas)

---

## Como Executar o Projeto

1. Abra o projeto em um ambiente com .NET instalado
2. Compile e execute a aplicação
3. Utilize o menu exibido no console para gerenciar o estoque

