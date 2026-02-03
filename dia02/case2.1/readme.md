# Sistema de Controle de Check-in de Visitantes


## Objetivo
Registrar e gerenciar o check-in e check-out de visitantes, permitindo o controle de entradas, buscas e listagens através de um menu interativo no console.

## Funcionalidades
- Cadastrar visitantes (nome, documento, horário de chegada e primeira visita)
- Listar visitantes cadastrados
- Buscar visitante pelo nome
- Registrar saída de um visitante
- Tratar erros utilizando `try/catch`

### Funcionalidades extras
- Listar visitantes ordenados por **ID**
- Filtrar visitantes em primeira visita

## Conceitos Aplicados
- Sintaxe básica e tipos de dados (`string`, `int`, `bool`, `DateTime`)
- Estruturas de controle (`if`, `switch`, `while`)
- Programação Orientada a Objetos
- Properties (`get` e `set`)
- Collections (`List<T>`)
- Tratamento de exceções (`try/catch`)

## Estrutura do Projeto
O projeto foi desenvolvido em uma aplicação Console e está organizado em classes, mesmo estando em um único arquivo:
- **Visitante**: representa o modelo do visitante
- **ControleVisitantes**: responsável pelas regras de negócio
- **Menu**: responsável pelo menu e interação com o usuário

##  Como executar
1. Clone o repositório: git clone: **https://github.com/alessandraasantos/deloitte-bootcamp-dia26.git**
2. Abra o projeto em uma IDE compatível com C# (Visual Studio ou VS Code)
3. Execute o projeto usando: **dotnet run**
4. Abra o terminal para interagir com o sistema




