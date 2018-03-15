CREATE TABLE [dbo].[Posts] (
    [PostID]    INT            IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (MAX) NULL,
    [PostTitle] NVARCHAR (MAX) NULL,
    [PostDesc]  NVARCHAR (MAX) NULL,
    [PostDate]  DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED ([PostID] ASC)
);

