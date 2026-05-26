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

Pasta Raiz ./      
eduardo@eduardo-IdeaPad-Gaming-3-15IMH05:~/Documentos/Projeto_Crud_NET/Crud_Tarefas_Dnet$ ls
Backend             CrudTarefas.sln        Dockerfile  README.md  app.db            bin               cypress             node_modules  out   package.json
CrudTarefas.csproj  Crud_Tarefas_Dnet.sln  Properties  Tests      appsettings.json  crud-tarefas-web  docker-compose.yml  obj                 package-lock.json


A presença de TarefasController, TarefaService, AppDbContext e Tarefa mostra uma divisão típica entre entrada HTTP, regra de negócio, acesso a dados e modelo de domínio.
Funcionalidades

    Criar tarefas.

    Listar tarefas.

    Atualizar tarefas.

    Remover tarefas.

    Persistir dados via EF Core.

    Executar em container com Docker.

Como executar o projeto 

Com Docker

    Suba os serviços com Docker Compose.

    A aplicação será iniciada junto com o ambiente configurado no docker-compose.yml.
    Para rodar a imagem banco de dados e a API 
    
    sudo docker compose up -d

Localmente


 BACKEND :

✅ Passo a passo

    Certifique-se de estar na pasta do projeto (onde está o .csproj principal, por exemplo CrudTarefas.csproj):
    bash

cd ~/Documentos/Projeto_Crud_NET/Crud_Tarefas_Dnet

 OBS.
 
 O dotnet ef precisa estar instalado. Se não estiver, instale com:
    bash

dotnet tool install --global dotnet-ef

Gerar uma migration nova (se você fez alterações nos Models ou no AppDbContext):
    bash
 Execute as migrations, se necessário.
 
dotnet ef migrations add InitialCreate --project CrudTarefas.csproj
        
Aplicar as migrations no banco de dados:
    bash
dotnet ef database update --project Backend/src/CrudTarefas.csproj

 Restaure os pacotes do projeto.

🔎 Observações importantes

Se você estiver rodando pelo projeto da raiz (CrudTarefas.csproj), ajuste o caminho:
    bash
Atualizando o Banco de dados :

dotnet ef migrations add InitialCreate --project CrudTarefas.csproj

dotnet ef database update --project CrudTarefas.csproj

Para rodar o projeto : dotnet run
 
eduardo@eduardo-IdeaPad-Gaming-3-15IMH05:~/Documentos/Projeto_Crud_NET/Crud_Tarefas_Dnet$ dotnet run 

Building...
info: Microsoft.Hosting.Lifetime[1
      Now listening on: http://localhost:5050
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/eduardo/Documentos/Projeto_Crud_NET/Crud_Tarefas_Dnet

Frontend :      
Rodando Frontend : Angular -  ng serve 
eduardo@eduardo-IdeaPad-Gaming-3-15IMH05:~/Documentos/Projeto_Crud_NET/Crud_Tarefas_Dnet/crud-tarefas-web$ ng serve
Initial chunk files | Names         |  Raw size
main.js             | main          | 103.51 kB | 
styles.css          | styles        |   8.79 kB | 

                    | Initial total | 112.29 kB

Application bundle generation complete. [2.546 seconds] - 2026-05-26T19:08:51.293Z

Watch mode enabled. Watching for file changes...
NOTE: Raw file sizes do not reflect development server per-request transformations.
  ➜  Local:   http://localhost:4200/
  ➜  press h + enter to show help

  

O projeto contém migrações InitialCreate e AddDatasNaTarefa, o que indica evolução do schema ao longo do desenvolvimento e uso de versionamento de banco com EF Core.
O AppDbContext centraliza o acesso ao banco e serve como ponto de integração entre os modelos e a persistência.

Programador Junior - Eduardo M Dias 
Processo seletivo Avanade - 05/05/2026 
 


