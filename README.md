# ListaDeTarefas
[![NPM](https://img.shields.io/npm/l/react)](https://github.com/Andre-Meira/ListaDeTarefas/blob/master/LICENSE)
# Sobre o Projeto
  "Lista de Tarefas" É um projeto meu com o inuito de melhorar meus conhecimentos em ASP.NET, Enity Framework.
  
  O projeto tem como base uma cronograma de tarefas, onde cada usuario tem uma tarefa com um nivel de prioridade, se necessario as atividade 
  podem ser Atualizadas e Excluidas, Cada ativade tem uma "Status" sendo ele "Ativo" e "Inativo" para demostrar que aquela tarefa não esta mais em execução
   
   
# Tecnologias Usadas
## Back-end
  -  C#
  - .NET 5.0
  - Entity framework core
## Front-End
  - CSS/HTML/JS

# Data-Base
- ListaTarefaDB
 
## DB USER

CREATE TABLE [dbo].[User] (
    [IdUser]  INT            IDENTITY (1, 1) NOT NULL,
    [Usuario] NVARCHAR (MAX) NULL,
    [Senha ]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([IdUser] ASC)
);

## DB Tarefas

CREATE TABLE [dbo].[Tarefa] (
    [Tarefas_ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Fk_UserTarefa] INT            NULL,
    [Nome_Tarefa]   NVARCHAR (MAX) NULL,
    [Descricao]     NVARCHAR (MAX) NULL,
    [Inicio]        DATETIME2 (7)  NOT NULL,
    [Fim]           DATETIME2 (7)  NOT NULL,
    [Situacao]      INT            NOT NULL,
    [Prioridade]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Tarefas_ID] ASC),
    FOREIGN KEY ([Fk_UserTarefa]) REFERENCES [dbo].[User] ([IdUser])
);


# Como Execultar
  ## Pré-Requisitos 
  - EntityFramework
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Tools
  - Microsoft.EntityFrameworkCore.SqlServer
 
  Git clone https://github.com/Andre-Meira/ListaDeTarefas.git
  
  Se alteração ou erro referente ao banco de dados exclua a pasta models e use o comando: 
  - Scaffold-DbContext "Server=(Server=(localdb)\\mssqllocaldb;DataBase=ListaTarefaDB)" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
 
