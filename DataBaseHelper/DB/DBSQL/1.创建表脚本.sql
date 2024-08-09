USE [DBMSTool]
GO
--服务器-数据库表
CREATE TABLE FS_Services
(
    ID             INT          IDENTITY(1, 1) PRIMARY KEY, --主键ID
    ServerAddress  VARCHAR(256),                             --服务器地址，如16.132.112.123
    ServerAddress2 VARCHAR(256),                             --指向同一个服务器的第二个备用地址
    ServerPortNo   INT          NOT NULL DEFAULT 0,         --端口号
	LoginName	NVARCHAR(128),								--登陆名
	LoginPassword	NVARCHAR(128),							--登陆密码
	ServerType	INT  NOT NULL DEFAULT 0,					--数据库类型，1SqlServer（默认），2MySql							
    DataBaseName     NVARCHAR(128),                          --数据库名称
    DataBaseIntro    NVARCHAR(512),                         --数据库简介
    SourceID     INT          NOT NULL DEFAULT 0,         --来源,FS_ServiceSource.ID,电信通,阿里云,腾讯云,华为云...
    IsInUse        INT          NOT NULL DEFAULT 0,			--是否使用中，1是
    Createtime     DATETIME,                                --创建时间
    IsDel          INT          DEFAULT 0 NOT NULL
);

--服务器来源表，比如阿里云、腾讯云、华为云、电信通等
CREATE TABLE FS_ServiceSource
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY,   --主键ID
	SourceName	NVARCHAR(128),					--服务器来源名称，电信通、阿里云、腾讯云等
	Createtime     DATETIME                   --创建时间
)
--用户表
CREATE TABLE Users
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY, --主键ID
	UserName	NVARCHAR(256) ,		--用户姓名
	LoginName	NVARCHAR(256),		--登录名
	LoginPwd	NVARCHAR(256),		--登陆密码
	IsAdmin	bit ,					--是否管理员，1是
	LastLoginTime	DATETIME,		--上次登陆时间
	IsValid	BIT,					--是否有效，1是
	IsDel	INT,					--是否删除，1已删除，0正常
	Createtime DATETIME				--创建时间
)

--日志表
CREATE TABLE Log_Operation
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY,	--主键ID
	UserID	INT NOT NULL DEFAULT 0,				--操作人ID
	ServiceID	INT NOT NULL DEFAULT 0,			--操作的服务器数据库
	OperaContent  NVARCHAR(4000),		--操作的内容
	Createtime DATETIME,				--创建时间
)













































