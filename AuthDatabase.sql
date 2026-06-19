


-- [RefreshTokens]
CREATE TABLE [dbo].[RefreshTokens] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    INT            NOT NULL,
    [Token]     NVARCHAR (MAX) NULL,
    [ExpiresAt] DATETIME       NULL,
    [CreatedAt] DATETIME       NULL,
    [RevokedAt] DATETIME       NULL
);

--[Roles]
CREATE TABLE [dbo].[Roles] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL
);

--[Users]
CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [Email]     NVARCHAR (255) NULL,
    [Password]  NVARCHAR (255) NOT NULL,
    [CreatedAt] DATETIME       NULL,
    [RoleId]    INT            NULL
);
