# Projekt
Projekt podzielono na 4 projekty. Projekt Domain przechowuje encje oraz interfejsy repozytoriów. Infrastructure jest projektem, który pobiera, modyfikuje dane z bazy danych. Główna logika aplikacji znajduje się w Application. Projekt UI ma za zadanie przedstawić dane odpowiednio przerobione przez Application użykownikowi. Do wymiany danych pomiędzy UI a Application służy interfejs IRequestHandler. W całej solucji zastosowano kontener IoC CastleWindsor. Doadatkowo projekt posiada testy integracyjne oraz jednostkowe. 
Technologie:
- .Net Framework 4.8
- SQLite
- NUnit
- CastleWindsor
- Dapper

Schemat projektu

![](https://raw.githubusercontent.com/kamasjdev/Project_Orders_in_a_Restaurant/master/projects_app.jpg)

Do zapisywania zamówień wykorzystano bazę SQLite. Baza danych została dodana lokalnie w projekcie. Ułożenie tabel pokazano na rysunku poniżej.

![](https://raw.githubusercontent.com/kamasjdev/Project_Orders_in_a_Restaurant/master/schemat_bazy_danych.png)



Poniżej przedstawiono kod do utworzenia bazy danych
```sql
CREATE TABLE products(
Id TEXT NOT NULL,
ProductName TEXT NOT NULL,
Price REAL NOT NULL,
ProductKind INTEGER NOT NULL,
PRIMARY KEY (Id)
)

CREATE TABLE orders (
Id TEXT NOT NULL,
OrderNumber TEXT NOT NULL,
Created TEXT NOT NULL,
Price REAL NOT NULL,
Email TEXT NOT NULL,
Note TEXT,
PRIMARY KEY (Id)
)

CREATE TABLE additions (
Id TEXT NOT NULL,
AdditionName TEXT NOT NULL,
Price REAL NOT NULL,
ProductKind INTEGER NOT NULL,
PRIMARY KEY (Id)
)

CREATE TABLE product_sales (
Id TEXT NOT NULL,
ProductId TEXT NOT NULL,
OrderId TEXT,
AdditionId TEXT,
EndPrice REAL NOT NULL,
Email TEXT NOT NULL,
ProductSaleState INTEGER NOT NULL,
CONSTRAINT FK_PRODUCTS FOREIGN KEY (ProductId) REFERENCES products,
CONSTRAINT FK_ORDERS FOREIGN KEY (OrderId) REFERENCES orders,
CONSTRAINT FK_ADDITIONS FOREIGN KEY (AdditionId) REFERENCES additions
)

CREATE TABLE migrations (
Id TEXT,
Name TEXT,
Version INTEGER NOT NULL
)