CREATE TABLE books (
        id bigint IDENTITY(1,1) PRIMARY KEY,
        author NVARCHAR(MAX) NULL,
        launch_date DATETIME2(6) NOT NULL,
        price DECIMAL(18,2) NOT NULL,
        title NVARCHAR(MAX) NULL
    );
