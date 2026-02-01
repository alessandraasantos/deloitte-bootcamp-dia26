# Projeto de Mineração (POO)

## Descrição

Projeto desenvolvido para aplicar conceitos de **Programação Orientada a Objetos**, como encapsulamento, regras de negócio, controle de acesso e relacionamento entre classes, conforme proposto na atividade.

O sistema simula o processo de **extração de minério**, **produção** e **armazenamento em estoque**.

---

## Estrutura do Projeto

* **Mina**: representa a mina de extração e controla o acesso ao método de extração de minério.
* **Minerio**: entidade que representa o minério extraído.
* **Producao**: representa o processo produtivo da mina, com regras de capacidade e refinamento.
* **Estoque**: representa o armazenamento do minério produzido, com controle de quantidade.

Relacionamento entre classes:

* 1 **Mina** → n **Produções**
* 1 **Produção** → n **Estoques**

---

## Conceitos Aplicados

* Encapsulamento de atributos
* Controle de acesso (segurança)
* Regras de negócio nas entidades
* Comportamentos de domínio
* Relacionamento entre objetos

---

## Execução

O arquivo `Program.cs` cria as instâncias das classes e demonstra o fluxo completo:
extração do minério → produção → armazenamento em estoque.

---

