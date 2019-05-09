-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************
 
-- ************************************** [dbo].[Person]
 
CREATE TABLE [dbo].[Person]
(
 [Id]        int IDENTITY (1, 1) NOT NULL ,
 [Fornavn]   varchar(50) NOT NULL ,
 [Efternavn] varchar(50) NOT NULL ,
 [Email]     varchar(50) NOT NULL ,
 
 
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
 
 
 
 
 
 
 
 
-- ************************************** [dbo].[PersonData]
 
CREATE TABLE [dbo].[PersonData]
(
 [Id]           int IDENTITY (1, 1) NOT NULL ,
 [Hastighed]    decimal(18,6) NOT NULL ,
 [Acceleration] decimal(18,6) NOT NULL ,
 [Tid]          varchar(50) NOT NULL ,
 [FK_person]    int NOT NULL ,
 
 
 CONSTRAINT [PK_PersonData] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_27] FOREIGN KEY ([FK_person])  REFERENCES [dbo].[Person]([Id])
);
GO
 
 
CREATE NONCLUSTERED INDEX [fkIdx_27] ON [dbo].[PersonData]
 (
  [FK_person] ASC
 )
 
GO