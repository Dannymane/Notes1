SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[DSPoleceniaDokInsert] ON [dbo].[DispoStarttabellePolecenia]
AFTER UPDATE
AS
BEGIN
	IF 
		UPDATE (dpp_pokazDispo)

	BEGIN
		DECLARE @oldPokazDispo BIT
			,@newPokazDispo BIT
			,@turnusBeginDate DATETIME
			,@companyId INT
			,@endOfCurrentYear DATETIME,
			@workPost VARCHAR(30);

		SET @endOfCurrentYear = (CAST(CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-12-31' AS DATETIME));

		SELECT @oldPokazDispo = deleted.dpp_pokazDispo
		FROM deleted

		SELECT @newPokazDispo = inserted.dpp_pokazDispo
		FROM inserted

		SELECT @turnusBeginDate = inserted.dpp_dataWyjazdu
				FROM inserted

		IF @turnusBeginDate >= '2022-01-01'
			SET @companyId = 136;
		ELSE
			SET @companyId = 117;

		IF (
				@newPokazDispo = 1
				AND ISNULL(@oldPokazDispo, 0) = 0
				)
		BEGIN
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			--SET NOCOUNT ON;
			DECLARE @personalNr NVARCHAR(7)
			DECLARE @turnusId INT
			DECLARE @startTurnusu DATETIME
			DECLARE @koniecTurnusu DATETIME
			DECLARE @modifiedBy NVARCHAR(150)
			DECLARE @status INT
			DECLARE @kierowca BIT
			DECLARE @statusStudent INT
			DECLARE @startTurnusuMinusDay DATETIME
			DECLARE @length INT
			DECLARE @counter INT = 0
			DECLARE @today DATETIME = GETDATE()
			DECLARE @umowaOPrace BIT
			DECLARE @prowadziDzialalnosc BIT
			DECLARE @IndividualDateStartTurnus DATETIME
			DECLARE @IndividualDateFinishTurnus DATETIME
			DECLARE @NewStartDateMainDocuments DATETIME
			DECLARE @employeeAge INT
			DECLARE @currentPitStake DECIMAL
			DECLARE @NewEndYearDocuments DATETIME

			SET @length = (
					SELECT COUNT(1)
					FROM inserted
					INNER JOIN Turnusy ON inserted.dpp_turnusId = Turnusy.ID
					);
			SET @umowaOPrace = (
					SELECT pd.pra_umowaPraca
					FROM inserted
					INNER JOIN dbo.PracownikDane pd ON pd.pra_praId = inserted.dpp_praId
					)

			SET @prowadziDzialalnosc = (
					SELECT pd.pra_dzialalnosc
					FROM inserted
					INNER JOIN dbo.PracownikDane pd ON pd.pra_praId = inserted.dpp_praId
					)

			SET @employeeAge = (
			SELECT FLOOR(DATEDIFF(DAY, pd.pra_dataUrodzenia, GETDATE()) / 365.25)
					FROM inserted
					INNER JOIN dbo.PracownikDane pd ON pd.pra_praId = inserted.dpp_praId
					)

			WHILE @counter < @length
			BEGIN
				SET @counter = @counter + 1 (
						SELECT @personalNr = dpp_personalNr
							,@turnusId = dpp_turnusId
							,@startTurnusu = DataOd
							,@koniecTurnusu = DataDo
							,@modifiedBy = dpp_modifiedBy
							,@kierowca = dpp_kierowca
							,@IndividualDateStartTurnus = dpp_dataWyjazdu
							,@IndividualDateFinishTurnus = dpp_dataPowrotu
						FROM (
							SELECT inserted.dpp_personalNr
								,inserted.dpp_turnusId
								,Turnusy.DataOd
								,Turnusy.DataDo
								,inserted.dpp_modifiedBy
								,inserted.dpp_status
								,inserted.dpp_kierowca
								,inserted.dpp_dataWyjazdu
								,inserted.dpp_dataPowrotu
								,ROW_NUMBER() OVER (
									ORDER BY inserted.dpp_dppId
									) AS rowNo
							FROM inserted
							INNER JOIN Turnusy ON inserted.dpp_turnusId = Turnusy.ID
							) AS TEMP
						WHERE rowNo = @counter
						)
				SET @startTurnusuMinusDay = DATEADD(day, - 1, @startTurnusu)


				--Zmiany z generowaniem dokumentow glownych
				IF(@startTurnusu >= datefromparts(YEAR(GETDATE())+1,1,1))
					SET @NewStartDateMainDocuments = datefromparts(YEAR(GETDATE())+1,1,1)
				ELSE
					SET @NewStartDateMainDocuments = GETDATE()

				IF YEAR(@NewStartDateMainDocuments) > YEAR(GETDATE())
					SET @NewEndYearDocuments = DATEFROMPARTS(YEAR(@NewStartDateMainDocuments), 12, 31);
				ELSE
					SET @NewEndYearDocuments = DATEFROMPARTS(YEAR(GETDATE()), 12, 31);

				--Regulamin do zagranicznych umow
				--EXEC [CreateWorkerDocument] @personalNr
				--	,1
				--	,@NewStartDateMainDocuments
				--	,@koniecTurnusu
				--	,@modifiedBy
				--	,@turnusId
				--	,@companyId

				--Umowa zlecenie zagranica
				EXEC [CreateWorkerDocument] @personalNr
					,8
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId

				--Upowaznienie na A1
				--EXEC [CreateWorkerDocument] @personalNr
				--	,21
				--	,@startTurnusu
				--	,@koniecTurnusu
				--	,@modifiedBy
				--	,@turnusId
				--Oswiadczenie o miejscu zamieszkania
				--IF DB_NAME() = 'ICOM'
				--EXEC [CreateWorkerDocument] @personalNr
				--	,13
				--	,@startTurnusu
				--	,@koniecTurnusu
				--	,@modifiedBy
				--	,@turnusId
				--Oswiadczenie o miejscu zamieszkania - US-55
				EXEC [CreateWorkerDocument] @personalNr
					,85
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId
				--Umowa zlecenie ZUS Zagranica ISSP
				--EXEC [CreateWorkerDocument] @personalNr
				--	,9
				--	,@startTurnusuMinusDay
				--	,@koniecTurnusu
				--	,@modifiedBy
				--	,@turnusId
				--Oświadczenie o pracy w PL
				EXEC [CreateWorkerDocument] @personalNr
					,79
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId
				--Aneks1
				EXEC [CreateWorkerDocument] @personalNr
					,10
					,@IndividualDateStartTurnus
					,@IndividualDateFinishTurnus
					,@modifiedBy
					,@turnusId
					,@companyId
				--Aneks2
				EXEC [CreateWorkerDocument] @personalNr
					,11
					,@IndividualDateStartTurnus
					,@IndividualDateFinishTurnus
					,@modifiedBy
					,@turnusId
					,@companyId

			IF EXISTS (SELECT 1 FROM dbo.Turnusy WHERE ID = @turnusId AND KundenID = 217)
				BEGIN
				-- 228 Umowa o świadczenie usług Zagranica INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 228 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 228, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- 229 Umowa o świadczenie usług marketingowych INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 229 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 229, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- 231 Oświadczenie zleceniobiorcy INVV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 231 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 231, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- 234 Informacja o przetwarzaniu danych osobowych INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 234 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 234, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- 235 Oświadczenie do A1 Invent Sp. z o.o. INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 235 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 235, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- 236 Oświadczenie do A1 Invent2GO Sp. z o.o. Sp. k. INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 236 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 236, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

				-- Dokumenty generowane każdorazowo
				-- 230 Aneks nr 1 INV KL
				EXEC [CreateWorkerDocument] @personalNr, 230, @IndividualDateStartTurnus, @IndividualDateFinishTurnus, @modifiedBy, @turnusId, 6
				
				-- 232 Oświadczenie zleceniobiorcy do umowy - zakwaterowanie INV
				EXEC [CreateWorkerDocument] @personalNr, 232, @IndividualDateStartTurnus, @IndividualDateFinishTurnus, @modifiedBy, @turnusId, 6

				-- 233 Oświadczenie o okresie dyspozycyjności INV
				EXEC [CreateWorkerDocument] @personalNr, 233, @IndividualDateStartTurnus, @IndividualDateFinishTurnus, @modifiedBy, @turnusId, 6
				-- Koniec Dokumenty generowane każdorazowo
				
				-- 237 Regulamin do zagranicznych turnusów INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 237
					  AND dp.dop_deleted = 0
					  AND @NewStartDateMainDocuments BETWEEN CAST(dop_waznyOd AS Date) AND CAST(dop_waznyDo AS Date)
					  AND CAST(dop_waznyDo AS Date) = CAST(@NewEndYearDocuments AS Date)
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 237, @NewStartDateMainDocuments, @NewEndYearDocuments, @modifiedBy, @turnusId, 6
				END

				-- 238 Informator kierowcy INV
				IF @kierowca = 1
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 238, @IndividualDateStartTurnus, @IndividualDateFinishTurnus, @modifiedBy, @turnusId, 6
				END

				-- 239 deklaracja rezygnacji z PPK INV - 
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 239
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
							AND dp.dop_deleted = 0 AND dp.dop_companyId = 6 AND dp.dop_waznyDo is null
						)
						AND @employeeAge >= 18 AND @employeeAge <= 54
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,239
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
					    ,6
				END

				-- 240 broszura informacyjna PPK INV
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 240
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
							AND dp.dop_deleted = 0
						)
						AND @employeeAge >= 18 AND @employeeAge <= 70
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,240
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
					    ,6
				END

                -- 241 Informator ubezpieczonego  - Karta do druku INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 241 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 241, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
				END

                -- 242 Prywatne Ubezpieczenie Medyczne - warunki INV
				IF NOT EXISTS (
					SELECT 1 
					FROM Dokument_pracownik dp 
					WHERE dp.dop_personalNr = @personalNr 
					  AND dp.dop_delId = 242 
					  AND dp.dop_deleted = 0
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr, 242, @NewStartDateMainDocuments, @endOfCurrentYear, @modifiedBy, @turnusId, 6
                END
		END
											  
				--IF NOT EXISTS (
				--	SELECT 1
				--	FROM Dokument_pracownik dp
				--	join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
				--	WHERE dp.dop_delId = 147
				--		AND dp.dop_personalNr = @personalNr
				--		AND  pd.pra_createdDate < dp.dop_createdDate
				--		AND ISNULL(dp.dop_deleted, 0) = 0
				--	)
				--	and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (217))
				--BEGIN
				--	EXEC [CreateWorkerDocument] @personalNr
				--		,147
				--		,@NewStartDateMainDocuments
				--		,@endOfCurrentYear
				--		,@modifiedBy
				--		,@turnusId
				--	    ,@companyId
				--End

														
				--IF NOT EXISTS (
				--	SELECT 1
				--	FROM Dokument_pracownik dp
				--	join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
				--	WHERE dp.dop_delId = 148
				--		AND dp.dop_personalNr = @personalNr
				--		AND dp.dop_trnId = @turnusId
				--		AND ISNULL(dp.dop_deleted, 0) = 0
				--	)
				--	and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (217))
				--BEGIN
				--	EXEC [CreateWorkerDocument] @personalNr
				--		,148
				--		,@IndividualDateStartTurnus
				--		,@IndividualDateFinishTurnus
				--		,@modifiedBy
				--		,@turnusId
				--	    ,@companyId
				--End

				--Zaswiadczenie o zatrudnieniu
				IF (
						@umowaOPrace = 1
						AND (
							NOT EXISTS (
								SELECT 1
								FROM [dbo].[Dokument_pracownik] AS dp
								WHERE dp.dop_personalNr = @personalNr
									AND dp.dop_delId = 92
									AND dp.dop_deleted = 0
								)
							)
						)
					EXEC [CreateWorkerDocument] @personalNr
						,92
						,@today
						,NULL
						,@modifiedBy
						,@turnusId
				     	,@companyId

				--Test BHP
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 20
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
						)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,20
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
					    ,@companyId
				END

				--BHP Fresnapf
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 102
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
							AND ISNULL(dp.dop_deleted, 0) = 0
						)
						and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (205, 206, 207, 208, 209, 210, 211, 212, 213, 214)) -- zagraniczny Fressnapf
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,102
						,@NewStartDateMainDocuments
						,@NewEndYearDocuments
						,@modifiedBy
						,@turnusId
					    ,@companyId
				END

				--Oswiadczenie o dzialalnosci gospodarczej
				IF (
						@prowadziDzialalnosc = 1
						AND (
							NOT EXISTS (
								SELECT 1
								FROM [dbo].[Dokument_pracownik] AS dp
								WHERE dp.dop_personalNr = @personalNr
									AND dp.dop_delId = 129
									AND dp.dop_deleted = 0
								)
							)
						)
					EXEC [CreateWorkerDocument] @personalNr
						,129
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
				     	,@companyId

				--Aneks3
				IF EXISTS (
						SELECT TOP 1 *
						FROM [dbo].[DispoStarttabellePolecenia] dsp
						INNER JOIN [dbo].[Turnusy] trn ON dsp.dpp_turnusId = trn.ID
						INNER JOIN [dbo].[TurnusStawkiDoDokumentow] tsd ON tsd.tsd_fncId = dsp.dpp_stanowiskoId
							AND tsd.tsd_rngId = dsp.dpp_rangaId
							AND tsd.tsd_trnId = trn.ID
						WHERE dsp.dpp_personalNr = @personalNr
							AND trn.ID = @turnusId
							AND tsd.tsd_brutto <>  (select top 1 dop_stawkaBrutto 
													from dbo.Dokument_pracownik dp 
													where
														dp.dop_personalNr = @personalNr 
														and dp.dop_waznyOd <= @IndividualDateStartTurnus
														and dp.dop_waznyDo >= @IndividualDateFinishTurnus
														and dop_stawkaBrutto is not null))
						
					EXEC [CreateWorkerDocument] @personalNr
						,83
						,@IndividualDateStartTurnus
						,@IndividualDateFinishTurnus
						,@modifiedBy
						,@turnusId
				    	,@companyId

				IF (@kierowca = 1)
					EXEC [CreateWorkerDocument] @personalNr
						,90
						,@IndividualDateStartTurnus
						,@IndividualDateFinishTurnus
						,@modifiedBy
						,@turnusId
				    	,@companyId

				--deklaracja rezygnacji z PPK
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 130
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
							AND dp.dop_deleted = 0 AND dp.dop_companyId = @companyId AND dp.dop_waznyDo is null
						)
						AND @employeeAge >= 18 AND @employeeAge <= 54
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,130
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
					    ,@companyId
				END

				--broszura informacyjna PPK
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik dp
						join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
						WHERE dp.dop_delId = 131
							AND dp.dop_personalNr = @personalNr
							AND  pd.pra_createdDate < dp.dop_createdDate
							AND dp.dop_deleted = 0
						)
						AND @employeeAge >= 18 AND @employeeAge <= 70
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,131
						,NULL
						,NULL
						,@modifiedBy
						,@turnusId
					    ,@companyId
				END

						   
				IF EXISTS (
						SELECT 1
						FROM Dokument_pracownik
						WHERE dop_delId=8
						AND dop_personalNr = @personalNr
						AND dop_deleted = 0
						AND CAST( dop_waznyOd AS Date ) = CAST( @NewStartDateMainDocuments AS Date )
				)
				--Harmonogram planowany
				EXEC [CreateWorkerDocument] @personalNr
					,134
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId


				--Aneks zmiana treść do umowy o świadczenie usług inwentaryzacji
				IF GETDATE() > '2024-06-30'
				EXEC [CreateWorkerDocument] @personalNr
						,164
						,'2024-07-01'
						,@endOfCurrentYear
						,@modifiedBy
						,@turnusId
					    ,@companyId    

					
				--Regulamin do zagranicznych turnusów - aktualizacja
				IF NOT EXISTS (
						SELECT 1
						FROM Dokument_pracownik
						WHERE dop_delId = 138
						  AND dop_personalNr = @personalNr
						  AND dop_deleted = 0
						  AND @NewStartDateMainDocuments BETWEEN CAST(dop_waznyOd AS Date) AND CAST(dop_waznyDo AS Date)
						  AND CAST(dop_waznyDo AS Date) = CAST(@NewEndYearDocuments AS Date)
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,138
						,@NewStartDateMainDocuments
						,@NewEndYearDocuments
						,@modifiedBy
						,@turnusId
						,@companyId
				END


				UPDATE dbo.PracownikDane
				SET dop_canShowPpkPopup = 1
				WHERE dbo.PracownikDane.pra_personalNr = @personalNr
			
							   
				EXEC [CreateWorkerDocument] @personalNr
					,140
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId

												  
				EXEC [CreateWorkerDocument] @personalNr
					,141
					,@NewStartDateMainDocuments
					,@koniecTurnusu
					,@modifiedBy
					,@turnusId
					,@companyId

					--Umowa o świadczenie usług Zagranica FR - 158

                    IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 158
						AND dp.dop_personalNr = @personalNr
						AND  pd.pra_createdDate < dp.dop_createdDate
						AND ISNULL(dp.dop_deleted, 0) = 0
					)
					and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (311, 312))
				BEGIN
				DECLARE @DynamicStartDate DATE;
    
					-- Pobranie daty dop_dataPodpisania dla z umowy zagranica
					SELECT TOP 1 @DynamicStartDate = dop_dataPodpisania
					FROM [ICOM_Test].[dbo].[Dokument_pracownik]
					WHERE dop_personalNr = @personalNr
						AND dop_deleted = 0
						AND dop_delId = 8
						AND dop_waznyDo > GETDATE()
						AND dop_waznyOd < @IndividualDateFinishTurnus
					ORDER BY dop_waznyOd DESC;

					EXEC [CreateWorkerDocument] @personalNr
						,158
						,@DynamicStartDate
						,@endOfCurrentYear
						,@modifiedBy
						,@turnusId
					    ,@companyId
                END
                --Umowa o świadczenie usług Zagranica FR - 159

                    IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 159
						AND dp.dop_personalNr = @personalNr
						AND  pd.pra_createdDate < dp.dop_createdDate
						AND ISNULL(dp.dop_deleted, 0) = 0
						AND dp.dop_trnId = @turnusId
					)
					and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (311))
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,159
						,@IndividualDateStartTurnus
						,@IndividualDateFinishTurnus
						,@modifiedBy
						,@turnusId
					    ,@companyId
                END
                --Umowa o świadczenie usług Zagranica FR - 160

                    IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 160
						AND dp.dop_personalNr = @personalNr
						AND  pd.pra_createdDate < dp.dop_createdDate
						AND ISNULL(dp.dop_deleted, 0) = 0
						AND dp.dop_trnId = @turnusId
					)
					and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (312))
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,160
						,@IndividualDateStartTurnus
						,@IndividualDateFinishTurnus
						,@modifiedBy
						,@turnusId
					    ,@companyId
                END
                 --Umowa o świadczenie usług Zagranica FR - 161

                    IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 161
						AND dp.dop_personalNr = @personalNr
						AND  pd.pra_createdDate < dp.dop_createdDate
						AND ISNULL(dp.dop_deleted, 0) = 0
						AND dp.dop_trnId = @turnusId
					)
					and exists (select 1 from dbo.Turnusy t where t.ID = @turnusId and t.KundenID in (311, 312))
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,161
						,@IndividualDateStartTurnus
						,@IndividualDateFinishTurnus
						,@modifiedBy
						,@turnusId
					    ,@companyId    
                    END
				--Oświadczenie dotyczące polityki firmy

                IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					join dbo.PracownikDane pd on pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 162
						AND dp.dop_personalNr = @personalNr
						AND  pd.pra_createdDate < dp.dop_createdDate
						AND ISNULL(dp.dop_deleted, 0) = 0
					)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,162
						,@today
						,@endOfCurrentYear
						,@modifiedBy
						,@turnusId
					    ,@companyId    
                END

				--Umowa o świadczenie usług Zagranica IT
				IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					JOIN dbo.PracownikDane pd ON pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 165
					  AND dp.dop_personalNr = @personalNr
					  AND pd.pra_createdDate < dp.dop_createdDate
					  AND ISNULL(dp.dop_deleted, 0) = 0
				)
				AND EXISTS (
					SELECT 1 
					FROM dbo.Turnusy t
					WHERE t.ID = @turnusId
					  AND EXISTS (
						  SELECT 1 
						  FROM dbo.Kunde k 
						  WHERE k.[Kunden-ID] = t.KundenID
							AND k.[LandId] = 109
					  )
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,165
						,@NewStartDateMainDocuments
						,@NewEndYearDocuments
						,@modifiedBy
						,@turnusId
					    ,@companyId
				End

				-- Aneks nr 1 do umowy IT
				IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					JOIN dbo.PracownikDane pd ON pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 166
					  AND dp.dop_personalNr = @personalNr
					  AND pd.pra_createdDate < dp.dop_createdDate
					  AND ISNULL(dp.dop_deleted, 0) = 0
				)
				AND EXISTS (
					SELECT 1 
					FROM dbo.Turnusy t
					WHERE t.ID = @turnusId
					  AND EXISTS (
						  SELECT 1 
						  FROM dbo.Kunde k 
						  WHERE k.[Kunden-ID] = t.KundenID
							AND k.[LandId] = 109
					  )
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,166
						,@IndividualDateStartTurnus
						,@koniecTurnusu
						,@modifiedBy
						,@turnusId
					    ,@companyId
				End

				-- Oświadczenia Zleceniobiorcy IT 
				IF NOT EXISTS (
					SELECT 1
					FROM Dokument_pracownik dp
					JOIN dbo.PracownikDane pd ON pd.pra_personalNr = dp.dop_personalNr
					WHERE dp.dop_delId = 167
					  AND dp.dop_personalNr = @personalNr
					  AND pd.pra_createdDate < dp.dop_createdDate
					  AND ISNULL(dp.dop_deleted, 0) = 0
				)
				AND EXISTS (
					SELECT 1 
					FROM dbo.Turnusy t
					WHERE t.ID = @turnusId
					  AND EXISTS (
						  SELECT 1 
						  FROM dbo.Kunde k 
						  WHERE k.[Kunden-ID] = t.KundenID
							AND k.[LandId] = 109
					  )
				)
				BEGIN
					EXEC [CreateWorkerDocument] @personalNr
						,167
						,@IndividualDateStartTurnus
						,@NewEndYearDocuments
						,@modifiedBy
						,@turnusId
					    ,@companyId
				End
			END
					--    FETCH NEXT FROM MY_CURSOR 
					--END
					--CLOSE MY_CURSOR
					--DEALLOCATE MY_CURSOR
		END
	END
END

/****** Object:  StoredProcedure [dbo].[CreateWorkerDocument]    Script Date: 17.12.2019 14:17:33 ******/
SET ANSI_NULLS ON
GO
ALTER TABLE [dbo].[DispoStarttabellePolecenia] ENABLE TRIGGER [DSPoleceniaDokInsert]
GO
