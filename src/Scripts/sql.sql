
DECLARE @EntityId UNIQUEIDENTIFIER
select @EntityId = Id from Entities where Name = 'Operation'

DELETE FROM Fields
WHERE EntityId = @EntityId

 DELETE FROM Entities where Id = @EntityId

-- select * from Fields where id = '3b223ff0-9d86-43ee-8777-15ca0296caff'

-- select * from Entities where Id = '940cd55f-a44b-48a8-b7c5-1976e0b06eed'11