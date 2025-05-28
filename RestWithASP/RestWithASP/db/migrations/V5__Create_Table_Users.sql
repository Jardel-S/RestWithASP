CREATE TABLE [users] (
    [id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [user_name] NVARCHAR(50) NOT NULL,
    [password] NVARCHAR(130) NOT NULL,
    [full_name] NVARCHAR(120) NOT NULL,
    [refresh_token] NVARCHAR(500) NULL,
    [refresh_token_expiry_time] DATETIME NULL,
    CONSTRAINT UQ_user_name UNIQUE ([user_name])
);
