# Projekt
W projekcie użyto wzorzec decorator. Schemat blokowy klas przedstawiono na rysunku poniżej
!(https://github.com/kamasjdev/testrepo/blob/main/schemat_dekoratora.png)

Do zapisywania zamówień wykorzystano bazę MSSQL. Baza danych została dodana lokalnie w projekcie. Ułożenie tabel pokazano na rysunku poniżej.

!(https://github.com/kamasjdev/testrepo/blob/main/schemat_bazy_danych.png)



Poniżej przedstawiono kod do utworzenia bazy danych
```sql
CREATE TABLE [dbo].[Produkty](
	[pr_id] [int] IDENTITY(1,1) NOT NULL,
	[pr_nazwa] [nchar](50) NOT NULL,
	[pr_cena] [real] NOT NULL,
	[zm_id] [int] NOT NULL,
 CONSTRAINT [PK_Produkty] PRIMARY KEY CLUSTERED 
(
	[pr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Zamowienia](
	[zm_id] [int] IDENTITY(1,1) NOT NULL,
	[zm_nr_zamowienia] [int] NOT NULL,
	[zm_data_zamowienia] [datetime] NOT NULL,
	[zm_koszt] [real] NOT NULL,
	[zm_email] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Zamowienia] PRIMARY KEY CLUSTERED 
(
	[zm_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Produkty]  WITH CHECK ADD  CONSTRAINT [FK_Produkty_Zamowienia1] FOREIGN KEY([zm_id])
REFERENCES [dbo].[Zamowienia] ([zm_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produkty] CHECK CONSTRAINT [FK_Produkty_Zamowienia1]
