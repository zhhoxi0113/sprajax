/****** Object:  StoredProcedure [dbo].[up_RetrieveWebServiceCalls]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_RetrieveWebServiceCalls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_RetrieveWebServiceCalls]
GO
/****** Object:  StoredProcedure [dbo].[up_ClearAllStoredData]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_ClearAllStoredData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_ClearAllStoredData]
GO
/****** Object:  StoredProcedure [dbo].[up_CreateFramework]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateFramework]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateFramework]
GO
/****** Object:  StoredProcedure [dbo].[up_CreateJavaScriptUri]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateJavaScriptUri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateJavaScriptUri]
GO
/****** Object:  StoredProcedure [dbo].[up_CreateSession]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateSession]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateSession]
GO
/****** Object:  StoredProcedure [dbo].[up_CreateWebServiceCollection]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateWebServiceCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateWebServiceCollection]
GO
/****** Object:  StoredProcedure [dbo].[up_CreateWebServicesCall]    Script Date: 05/16/2006 07:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateWebServicesCall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[up_CreateWebServicesCall]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveWebServiceCalls]    Script Date: 05/16/2006 07:24:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_RetrieveWebServiceCalls]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_RetrieveWebServiceCalls]
	-- Add the parameters for the stored procedure here
	@SessionID as uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM WebServicesCall WHERE SessionID = @SessionID
END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_ClearAllStoredData]    Script Date: 05/16/2006 07:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_ClearAllStoredData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[up_ClearAllStoredData]
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM SprajaxSession
	DELETE FROM JavaScriptUri
	DELETE FROM Framework
	DELETE FROM WebServiceCollection
	DELETE FROM WebServicesCall
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_CreateFramework]    Script Date: 05/16/2006 07:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateFramework]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_CreateFramework
	@ID as uniqueidentifier,
	@SessionID as uniqueidentifier,
	@Name as nvarchar(255),
	@Platform as nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO Framework (ID, SessionID, Name, Platform) VALUES (@ID, @SessionID, @Name, @Platform)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_CreateJavaScriptUri]    Script Date: 05/16/2006 07:24:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateJavaScriptUri]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_CreateJavaScriptUri 
	@ID uniqueidentifier,
	@SessionID uniqueidentifier,
	@Uri nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO JavaScriptUri (ID, SessionID, Uri) VALUES (@ID, @SessionID, @Uri)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_CreateSession]    Script Date: 05/16/2006 07:24:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateSession]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_CreateSession 
	@ID uniqueidentifier, @BaseUri nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO SprajaxSession (ID, BaseUri) VALUES (@ID, @BaseUri)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_CreateWebServiceCollection]    Script Date: 05/16/2006 07:24:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateWebServiceCollection]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_CreateWebServiceCollection
	@ID uniqueidentifier,
	@SessionID uniqueidentifier,
	@Uri nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO WebServiceCollection (ID, SessionID, Uri) VALUES (@ID, @SessionID, @Uri)
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[up_CreateWebServicesCall]    Script Date: 05/16/2006 07:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[up_CreateWebServicesCall]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_CreateWebServicesCall
	@ID as uniqueidentifier,
	@SessionID as uniqueidentifier,
	@MethodName as nvarchar(255),
	@Parameters as nvarchar(255),
	@RequestText as ntext = NULL,
	@ResponseText as ntext = NULL,
	@ExceptionMessage as ntext = NULL,
	@ExceptionStackTrace as ntext = NULL
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO WebServicesCall (ID, SessionID, MethodName, Parameters, RequestText, ResponseText, ExceptionMessage, ExceptionStackTrace)
			VALUES (@ID, @SessionID, @MethodName, @Parameters, @RequestText, @ResponseText, @ExceptionMessage, @ExceptionStackTrace)
END
' 
END
GO
