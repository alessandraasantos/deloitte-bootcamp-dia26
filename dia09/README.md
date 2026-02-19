
# Sistema de Monitoramento de Equipamentos Pesados (Vale)



Este projeto foi desenvolvido como o desafio final do Bootcamp Deloitte/Vale. A aplicação consiste em uma API REST para monitoramento e gestão de frotas pesadas em operações de mineração, permitindo o controle de horímetros, localização e status operacional

### Sobre o Projeto

A API gerencia o ciclo de vida de equipamentos como Caminhões, Escavadeiras e Perfuratrizes, garantindo que as regras de negócio da operação sejam respeitadas através de validações rígidas e persistência segura em banco de dados.

### Funcionalidades Principais
 
* CRUD Completo: Criação, leitura, atualização e exclusão de equipamentos.

* Filtros Avançados: Busca dinâmica por tipo, status e codigo (parcial).

* Paginação: Controle de volume de dados via parâmetros page e pageSize.

* Segurança de Dados: - Bloqueio de códigos duplicados (Unique Index).

* Validação de horímetro (não permite valores negativos).

* Tratamento automático de strings (Trim em campos de código).

* Regra de Exclusão: Proteção contra remoção de ativos com histórico de manutenção concluída (retorna 409 Conflict).

### Arquitetura e Tecnologias
Linguagem: C# (.NET 10)

Banco de Dados: PostgreSQL

ORM: Entity Framework Core

Padronização: Snake Case Naming Convention (ex: data_aquisicao no banco).

Containerização: Docker e Docker Compose.

### Como Executar
1. Requisitos

.NET 10 SDK
PostregreSQl
Docker
Insomnia (para testes)

2. Execução via Docker (Recomendado)
Para subir o banco de dados e a API simultaneamente:

Bash
docker-compose up --build
A API estará disponível em: http://localhost:5071

