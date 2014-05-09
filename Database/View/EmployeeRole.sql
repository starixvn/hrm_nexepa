USE [NexEPA]
GO

/****** Object:  View [dbo].[EmployeeRole]    Script Date: 5/8/2014 10:07:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EmployeeRole]
AS
SELECT        empRole.EmployeeId, empRole.RoleId
FROM            (SELECT        EmployeeId, RoleId
                          FROM            dbo.EmployeePermission WITH (NOLOCK)
                          WHERE        (IsAllow = 1) AND (IsDeny = 0)
                          UNION ALL
                          SELECT        EmployeeId, 5 AS RoleId
                          FROM            dbo.Employee
                          WHERE        (Enabled = 1) AND (IsDeleted = 0)
                          UNION ALL
                          SELECT        EmployeeId, 3 AS RoleId
                          FROM            dbo.Employee AS Employee_3
                          WHERE        (Enabled = 1) AND (IsDeleted = 0) AND (GroupId = 13)
                          UNION ALL
                          SELECT        EmployeeId, 2 AS RoleId
                          FROM            dbo.Employee AS Employee_2
                          WHERE        (Enabled = 1) AND (IsDeleted = 0) AND (Roles LIKE '%3%')
                          UNION ALL
                          SELECT        LeaderId AS EmployeeId, 4 AS RoleId
                          FROM            dbo.EmployeeGroup
                          WHERE IsDeleted = 0) AS empRole LEFT OUTER JOIN
                         dbo.EmployeePermission AS empPer WITH (NOLOCK) ON empRole.EmployeeId = empPer.EmployeeId AND empRole.RoleId = empPer.RoleId
WHERE        (empPer.IsDeny IS NULL) OR
                         (empPer.IsDeny = 0)

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "empPer"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 135
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "empRole"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 101
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EmployeeRole'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EmployeeRole'
GO

