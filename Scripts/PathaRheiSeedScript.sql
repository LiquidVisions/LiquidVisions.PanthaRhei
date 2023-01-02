BEGIN TRANSACTION

EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

DELETE FROM Apps
DELETE FROM AppExpander
DELETE FROM Components
DELETE FROM Expanders
DELETE FROM Handlers
DELETE FROM Packages

EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'

-- ADD THE APP
DECLARE @AppId UNIQUEIDENTIFIER = '6c6984a1-c87a-429b-b91f-2a976adb3c0e'
INSERT INTO Apps(Id, Name, FullName)
SELECT @AppId, 'PanthaRhei', 'LiquidVisions.PantaRhei'

-- ADD THE EXPANDER
DECLARE @ExpanderId UNIQUEIDENTIFIER = '38f90309-4f6e-4aed-b0a1-3701afe0ee6f'
INSERT INTO Expanders
SELECT @ExpanderId, 'CleanArchitecture', 1, '.Templates'

-- ADD THE AppExpander MANY-TO-MANY RELATIONSHIP
INSERT INTO AppExpander (AppsId, ExpandersId)
SELECT @AppId, @ExpanderId

-- ADD THE Handlers
INSERT INTO Handlers(Id, Name, [Order], ExpanderId, SupportedGenerationModes)
SELECT NEWID(), 'ScaffoldTemplateHandler', 1, @ExpanderId, 'Default'

INSERT INTO Components(Id, Name, Description, Version ,ExpanderId)
SELECT NEWID(), 'Domain', '', '0.0.1', @ExpanderId UNION ALL
SELECT NEWID(), 'Application', '', '0.0.1', @ExpanderId UNION ALL
SELECT NEWID(), 'Api', '', '0.0.1', @ExpanderId UNION ALL
SELECT NEWID(), 'Infrastructure', '', '0.0.1', @ExpanderId


ROLLBACK