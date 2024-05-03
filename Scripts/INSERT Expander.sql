begin TRANSACTION

declare @id UNIQUEIDENTIFIER
set @id = newid()

insert into Expanders
select @id, 'CleanArchitecture.Application', 2, 1

insert into AppExpander
select (select top 1 Id from Apps), @id

commit