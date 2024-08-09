USE [DBMSTool]
GO
--������-���ݿ��
CREATE TABLE FS_Services
(
    ID             INT          IDENTITY(1, 1) PRIMARY KEY, --����ID
    ServerAddress  VARCHAR(256),                             --��������ַ����16.132.112.123
    ServerAddress2 VARCHAR(256),                             --ָ��ͬһ���������ĵڶ������õ�ַ
    ServerPortNo   INT          NOT NULL DEFAULT 0,         --�˿ں�
	LoginName	NVARCHAR(128),								--��½��
	LoginPassword	NVARCHAR(128),							--��½����
	ServerType	INT  NOT NULL DEFAULT 0,					--���ݿ����ͣ�1SqlServer��Ĭ�ϣ���2MySql							
    DataBaseName     NVARCHAR(128),                          --���ݿ�����
    DataBaseIntro    NVARCHAR(512),                         --���ݿ���
    SourceID     INT          NOT NULL DEFAULT 0,         --��Դ,FS_ServiceSource.ID,����ͨ,������,��Ѷ��,��Ϊ��...
    IsInUse        INT          NOT NULL DEFAULT 0,			--�Ƿ�ʹ���У�1��
    Createtime     DATETIME,                                --����ʱ��
    IsDel          INT          DEFAULT 0 NOT NULL
);

--��������Դ�����簢���ơ���Ѷ�ơ���Ϊ�ơ�����ͨ��
CREATE TABLE FS_ServiceSource
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY,   --����ID
	SourceName	NVARCHAR(128),					--��������Դ���ƣ�����ͨ�������ơ���Ѷ�Ƶ�
	Createtime     DATETIME                   --����ʱ��
)
--�û���
CREATE TABLE Users
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY, --����ID
	UserName	NVARCHAR(256) ,		--�û�����
	LoginName	NVARCHAR(256),		--��¼��
	LoginPwd	NVARCHAR(256),		--��½����
	IsAdmin	bit ,					--�Ƿ����Ա��1��
	LastLoginTime	DATETIME,		--�ϴε�½ʱ��
	IsValid	BIT,					--�Ƿ���Ч��1��
	IsDel	INT,					--�Ƿ�ɾ����1��ɾ����0����
	Createtime DATETIME				--����ʱ��
)

--��־��
CREATE TABLE Log_Operation
(
	ID		INT	  IDENTITY(1, 1) PRIMARY KEY,	--����ID
	UserID	INT NOT NULL DEFAULT 0,				--������ID
	ServiceID	INT NOT NULL DEFAULT 0,			--�����ķ��������ݿ�
	OperaContent  NVARCHAR(4000),		--����������
	Createtime DATETIME,				--����ʱ��
)













































