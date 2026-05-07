# Crud_Tarefas_Dnet

API REST desenvolvida em .NET 8 para gerenciar tarefas com operações de criação, leitura, atualização e exclusão, organizada com arquitetura em camadas, Entity Framework Core e suporte a Docker.
Visão geral

Este projeto implementa um CRUD de tarefas com separação clara entre Controllers, Service, Models e Data, facilitando manutenção, testes e evolução da aplicação.
A solução também inclui migrações do Entity Framework Core, indicando persistência de dados estruturada e versionada.
Tecnologias

    .NET 8.

    ASP.NET Core Web API.

    Entity Framework Core.

    Docker e Docker Compose. 

    SQL Server ou banco relacional compatível com EF Core.

Estrutura do projeto

text
├── CrudTarefas.csproj
├── Crud_Tarefas_Dnet.sln
├── Dockerfile
├── docker-compose.yml
└── src
    ├── Controllers
    │   └── TarefasController.cs
    ├── Data
    │   ├── AppDbContext.cs
    │   └── Migrations
    ├── Models
    │   └── Tarefa.cs
    ├── Program.cs
    └── Service
        ├── ITarefaService.cs
        └── TarefaService.cs

A presença de TarefasController, TarefaService, AppDbContext e Tarefa mostra uma divisão típica entre entrada HTTP, regra de negócio, acesso a dados e modelo de domínio.
Funcionalidades

    Criar tarefas.

    Listar tarefas.

    Atualizar tarefas.

    Remover tarefas.

    Persistir dados via EF Core.

    Executar em container com Docker.

Como executar
Com Docker

    Suba os serviços com Docker Compose.

    A aplicação será iniciada junto com o ambiente configurado no docker-compose.yml.

Localmente

    Restaure os pacotes do projeto.

    Execute as migrations, se necessário.

    Inicie a API pelo Program.cs.

Banco de dados

O projeto contém migrações InitialCreate e AddDatasNaTarefa, o que indica evolução do schema ao longo do desenvolvimento e uso de versionamento de banco com EF Core.
O AppDbContext centraliza o acesso ao banco e serve como ponto de integração entre os modelos e a persistência.

Programador Junior - Eduardo M Dias 
Processo seletivo Avanade - 05/05/2026 
 


