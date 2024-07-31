IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Test')
BEGIN
	CREATE DATABASE Test
END

GO
	USE Test
GO
	BEGIN
			
		IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'categories')
		BEGIN
			CREATE TABLE categories(
				Id INT PRIMARY KEY IDENTITY,
				Name VARCHAR(100) NOT NULL
			)
		END

		IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'products')
		BEGIN
			CREATE TABLE products(
				Id INT PRIMARY KEY IDENTITY,
				Name VARCHAR(100) NOT NULL,
				CategoryId INT NOT NULL REFERENCES categories(Id) ON DELETE CASCADE,
				Price INT NOT NULL
			)
		END

	END
