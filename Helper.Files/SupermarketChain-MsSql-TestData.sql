-- Vendors
SET IDENTITY_INSERT [dbo].[Vendors] ON 

INSERT [dbo].[Vendors] ([Id], [Name]) VALUES (1, N'Nestle Sofia Corp.')
INSERT [dbo].[Vendors] ([Id], [Name]) VALUES (2, N'Targovishte Bottling Company Ltd.')
INSERT [dbo].[Vendors] ([Id], [Name]) VALUES (3, N'Zagorka Corp.')

SET IDENTITY_INSERT [dbo].[Vendors] OFF

-- Measures
SET IDENTITY_INSERT [dbo].[Measures] ON 

INSERT [dbo].[Measures] ([Id], [Name]) VALUES (1, N'liters')
INSERT [dbo].[Measures] ([Id], [Name]) VALUES (2, N'pieces')

SET IDENTITY_INSERT [dbo].[Measures] OFF

-- Locations
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([Id], [Name]) VALUES (1, N'Supermarket “Kaspichan – Center”')
INSERT [dbo].[Locations] ([Id], [Name]) VALUES (2, N'Supermarket “Bourgas – Plaza”')
INSERT [dbo].[Locations] ([Id], [Name]) VALUES (3, N'Supermarket “Bay Ivan” – Zmeyovo')
INSERT [dbo].[Locations] ([Id], [Name]) VALUES (4, N'Supermarket “Plovdiv – Stolipinovo”')

SET IDENTITY_INSERT [dbo].[Locations] OFF

-- Products
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [MeasureId], [VendorId]) VALUES (1, N'Beer “Beck’s”', 1, 3)
INSERT [dbo].[Products] ([Id], [Name], [MeasureId], [VendorId]) VALUES (3, N'Beer “Zagorka”', 1, 3)
INSERT [dbo].[Products] ([Id], [Name], [MeasureId], [VendorId]) VALUES (4, N'Chocolate “Milka”', 2, 1)
INSERT [dbo].[Products] ([Id], [Name], [MeasureId], [VendorId]) VALUES (5, N'Vodka “Targovishte”', 1, 2)

SET IDENTITY_INSERT [dbo].[Products] OFF

-- Expenses
SET IDENTITY_INSERT [dbo].[Expenses] ON 

INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (1, CAST(30.00 AS Decimal(18, 2)), 1, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (2, CAST(40.00 AS Decimal(18, 2)), 1, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (3, CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (4, CAST(180.00 AS Decimal(18, 2)), 2, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (5, CAST(120.00 AS Decimal(18, 2)), 3, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (6, CAST(180.00 AS Decimal(18, 2)), 3, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (7, CAST(30.00 AS Decimal(18, 2)), 1, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (8, CAST(40.00 AS Decimal(18, 2)), 1, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (9, CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (10, CAST(180.00 AS Decimal(18, 2)), 2, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (11, CAST(120.00 AS Decimal(18, 2)), 3, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (12, CAST(180.00 AS Decimal(18, 2)), 3, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (13, CAST(30.00 AS Decimal(18, 2)), 1, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (14, CAST(40.00 AS Decimal(18, 2)), 1, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (15, CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (16, CAST(180.00 AS Decimal(18, 2)), 2, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (17, CAST(120.00 AS Decimal(18, 2)), 3, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (18, CAST(180.00 AS Decimal(18, 2)), 3, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (19, CAST(30.00 AS Decimal(18, 2)), 1, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (20, CAST(40.00 AS Decimal(18, 2)), 1, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (21, CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (22, CAST(180.00 AS Decimal(18, 2)), 2, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (23, CAST(120.00 AS Decimal(18, 2)), 3, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (24, CAST(180.00 AS Decimal(18, 2)), 3, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (25, CAST(30.00 AS Decimal(18, 2)), 1, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (26, CAST(40.00 AS Decimal(18, 2)), 1, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (27, CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (28, CAST(180.00 AS Decimal(18, 2)), 2, CAST(N'2013-08-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (29, CAST(120.00 AS Decimal(18, 2)), 3, CAST(N'2013-07-01' AS Date))
INSERT [dbo].[Expenses] ([Id], [Value], [VendorId], [Month]) VALUES (30, CAST(180.00 AS Decimal(18, 2)), 3, CAST(N'2013-08-01' AS Date))

SET IDENTITY_INSERT [dbo].[Expenses] OFF

-- Sales
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (1, CAST(40.00 AS Decimal(18, 2)), 1, CAST(1.20 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (2, CAST(37.00 AS Decimal(18, 2)), 3, CAST(1.00 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (3, CAST(7.00 AS Decimal(18, 2)), 4, CAST(2.85 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 3)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (5, CAST(14.00 AS Decimal(18, 2)), 5, CAST(8.50 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (6, CAST(12.00 AS Decimal(18, 2)), 4, CAST(2.90 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (7, CAST(65.00 AS Decimal(18, 2)), 3, CAST(0.92 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (8, CAST(4.00 AS Decimal(18, 2)), 5, CAST(7.80 AS Decimal(18, 2)), CAST(N'2014-07-20' AS Date), 3)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (9, CAST(11.00 AS Decimal(18, 2)), 3, CAST(1.00 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (10, CAST(78.00 AS Decimal(18, 2)), 3, CAST(0.92 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (11, CAST(146.00 AS Decimal(18, 2)), 3, CAST(0.88 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (13, CAST(20.00 AS Decimal(18, 2)), 5, CAST(8.50 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (15, CAST(67.00 AS Decimal(18, 2)), 5, CAST(7.70 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (16, CAST(3.00 AS Decimal(18, 2)), 5, CAST(7.80 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 3)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (17, CAST(43.00 AS Decimal(18, 2)), 1, CAST(1.20 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (18, CAST(75.00 AS Decimal(18, 2)), 1, CAST(1.05 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (19, CAST(9.00 AS Decimal(18, 2)), 4, CAST(2.90 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (20, CAST(5.00 AS Decimal(18, 2)), 4, CAST(2.85 AS Decimal(18, 2)), CAST(N'2014-07-21' AS Date), 3)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (21, CAST(16.00 AS Decimal(18, 2)), 3, CAST(1.00 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (22, CAST(90.00 AS Decimal(18, 2)), 3, CAST(0.92 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (23, CAST(230.00 AS Decimal(18, 2)), 3, CAST(0.88 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (24, CAST(24.00 AS Decimal(18, 2)), 5, CAST(8.50 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 2)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (25, CAST(12.00 AS Decimal(18, 2)), 5, CAST(7.70 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (26, CAST(18.00 AS Decimal(18, 2)), 1, CAST(1.20 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 1)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (27, CAST(60.00 AS Decimal(18, 2)), 1, CAST(1.05 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 4)
INSERT [dbo].[Sales] ([Id], [Quantity], [ProductId], [PricePerUnit], [DateOfSale], [LocationId]) VALUES (28, CAST(14.00 AS Decimal(18, 2)), 4, CAST(2.90 AS Decimal(18, 2)), CAST(N'2014-07-22' AS Date), 1)

SET IDENTITY_INSERT [dbo].[Sales] OFF
