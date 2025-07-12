USE [AdventureWorks]
GO

CREATE SCHEMA [Membership]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contains objects related to membership objects.' , @level0type=N'SCHEMA',@level0name=N'Membership'
GO


