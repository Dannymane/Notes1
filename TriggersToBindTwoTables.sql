
--since 228 tables are equal
GO

CREATE TRIGGER [Dokument_elem_InsertIntoIcomTest] ON [ICOM].[dbo].[Dokument_elem]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON; -- Prevents the display of affected row counts, which is a good practice for triggers to avoid unnecessary overhead.

	--IF is obligatory to avoid infinite loop, even if there is no rows in inserted that math WHERE condition
    IF EXISTS (SELECT 1 FROM inserted WHERE del_delId not in(SELECT del_delId FROM [ICOM_Test].[dbo].[Dokument_elem]))
    INSERT INTO [ICOM_Test].[dbo].[Dokument_elem] (
        del_nazwa,
        del_nazwaSzkol
    )
    SELECT
        del_nazwa,
        del_nazwaSzkol
    FROM inserted i
    WHERE NOT EXISTS ( --will insert only new records that are not in the table
        SELECT 1
        FROM [ICOM_Test].[dbo].[Dokument_elem] d
        WHERE d.del_delId = i.del_delId);
END;
GO

CREATE TRIGGER [Dokument_elem_InsertIntoIcomLive] ON [ICOM_Test].[dbo].[Dokument_elem]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM inserted WHERE del_delId not in(SELECT del_delId FROM [ICOM].[dbo].[Dokument_elem]))
    INSERT INTO [ICOM].[dbo].[Dokument_elem] (
        del_nazwa,
        del_nazwaSzkol
    )
    SELECT
        del_nazwa,
        del_nazwaSzkol
    FROM inserted i
    WHERE NOT EXISTS ( 
        SELECT 1
        FROM [ICOM].[dbo].[Dokument_elem] d
        WHERE d.del_delId = i.del_delId);
END;
GO

CREATE TRIGGER [Dokument_elem_UpdateIcomTest] ON [ICOM].[dbo].[Dokument_elem]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @delId_since_documents_are_equal INT;
    SET @delId_since_documents_are_equal = 228;

    IF EXISTS (SELECT 1 FROM inserted WHERE del_delId >= @delId_since_documents_are_equal)
    BEGIN
        UPDATE [ICOM_Test].[dbo].[Dokument_elem] SET 
            del_nazwa = i.del_nazwa,
            del_nazwaSzkol = i.del_nazwaSzkol
            FROM [ICOM_Test].[dbo].[Dokument_elem] d
            JOIN inserted i ON d.del_delId = i.del_delId
            WHERE i.del_delId >= @delId_since_documents_are_equal;
    END
END;
GO

CREATE TRIGGER [Dokument_elem_DeleteFromIcomTest] ON [ICOM].[dbo].[Dokument_elem]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @delId_since_documents_are_equal INT;
    SET @delId_since_documents_are_equal = 228;

    IF EXISTS (SELECT 1 FROM [ICOM_Test].[dbo].[Dokument_elem] 
                WHERE del_delId IN (SELECT del_delId FROM deleted WHERE del_delId >= @delId_since_documents_are_equal))
    BEGIN
        DELETE FROM [ICOM_Test].[dbo].[Dokument_elem] 
        WHERE del_delId IN (SELECT del_delId FROM deleted WHERE del_delId >= @delId_since_documents_are_equal);

        DECLARE @MaxIdTest INT, @MaxIdLive INT;
        SELECT @MaxIdTest = MAX(del_delId) FROM [ICOM_Test].[dbo].[Dokument_elem];
        SELECT @MaxIdLive = MAX(del_delId) FROM [ICOM].[dbo].[Dokument_elem];

        IF @MaxIdTest IS NULL
            SET @MaxIdTest = 0;
        IF @MaxIdLive IS NULL
            SET @MaxIdLive = 0;

        DECLARE @Sql NVARCHAR(200);
        SET @Sql = 'DBCC CHECKIDENT (''[ICOM_Test].[dbo].[Dokument_elem]'', RESEED, ' + CAST(@MaxIdTest AS NVARCHAR(10)) + ');';
        EXEC sp_executesql @Sql;
        SET @Sql = 'DBCC CHECKIDENT (''[ICOM].[dbo].[Dokument_elem]'', RESEED, ' + CAST(@MaxIdLive AS NVARCHAR(10)) + ');';
        EXEC sp_executesql @Sql;
    END
END;
GO

CREATE TRIGGER [Dokument_elem_DeleteFromIcomLive] ON [ICOM_Test].[dbo].[Dokument_elem]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
        DECLARE @delId_since_documents_are_equal INT;
    SET @delId_since_documents_are_equal = 228;

    IF EXISTS (SELECT 1 FROM [ICOM].[dbo].[Dokument_elem]
                WHERE del_delId IN (SELECT del_delId FROM deleted WHERE del_delId >= @delId_since_documents_are_equal))
    BEGIN
        DELETE FROM [ICOM].[dbo].[Dokument_elem] 
        WHERE del_delId IN (SELECT del_delId FROM deleted WHERE del_delId >= @delId_since_documents_are_equal);

        DECLARE @MaxIdTest INT, @MaxIdLive INT;
        SELECT @MaxIdTest = MAX(del_delId) FROM [ICOM_Test].[dbo].[Dokument_elem];
        SELECT @MaxIdLive = MAX(del_delId) FROM [ICOM].[dbo].[Dokument_elem];

        IF @MaxIdTest IS NULL
            SET @MaxIdTest = 0;
        IF @MaxIdLive IS NULL
            SET @MaxIdLive = 0;

        DECLARE @Sql NVARCHAR(200);
        SET @Sql = 'DBCC CHECKIDENT (''[ICOM_Test].[dbo].[Dokument_elem]'', RESEED, ' + CAST(@MaxIdTest AS NVARCHAR(10)) + ');';
        EXEC sp_executesql @Sql;
        SET @Sql = 'DBCC CHECKIDENT (''[ICOM].[dbo].[Dokument_elem]'', RESEED, ' + CAST(@MaxIdLive AS NVARCHAR(10)) + ');';
        EXEC sp_executesql @Sql;
    END
END;