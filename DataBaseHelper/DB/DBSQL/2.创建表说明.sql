USE [DBMSTool];
GO
/*[FS_Services]��˵��*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '������-���ݿ��',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'FS_Services';

--[FS_Services].ServerAddress�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',          -- sysname
                                @value = '��������ַ����123.123.123.123', -- sql_variant
                                @level0type = 'schema',            -- varchar(128)
                                @level0name = N'dbo',              -- sysname
                                @level1type = N'table',            -- varchar(128)
                                @level1name = N'FS_Services',      -- sysname
                                @level2type = N'column',           -- varchar(128)
                                @level2name = N'ServerAddress';
-- sysname
--[FS_Services].ServerAddress2�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = 'ָ��ͬһ���������ĵڶ������õ�ַ',  -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerAddress2';
-- sysname
--[FS_Services].ServerPortNo�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�˿ں�',               -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerPortNo';
-- sysname
--[FS_Services].LoginName�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��½�û���',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginName';
-- sysname
--[FS_Services].LoginPassword�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��½����',              -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginPassword';
-- sysname
--[FS_Services].ServerType�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '���ݿ�����,1SqlServer,2MySql',               -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerType';
-- sysname
--[FS_Services].ServerName�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '���ݿ�����',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'DataBaseName';
-- sysname
--[FS_Services].ServerIntro�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '���ݿ���',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'DataBaseIntro';
-- sysname
--[FS_Services].TypeSource�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��Դ,1����ͨ,2������,3��Ѷ��,4��Ϊ��...', -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'TypeSource';
-- sysname
--[FS_Services].IsInUse�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�Ƿ�ʹ���У�1�ǣ�0��',       -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsInUse';
-- sysname
--[FS_Services].Createtime�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����ʱ��',              -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';
-- sysname
--[FS_Services].IsDel�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�Ƿ�ɾ����0������1��ɾ��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsDel';       -- sysname
/*Users��˵��*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '�û���',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'Users';
--[Users].ID�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ID';       -- sysname
--[Users].UserName�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�û�����',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'UserName';       -- sysname
--[Users].LoginName�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��¼��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginName';       -- sysname
--[Users].LoginPwd�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��¼����',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginPwd';       -- sysname
--[Users].IsAdmin�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�Ƿ����Ա��1��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsAdmin';       -- sysname
--[Users].LastLoginTime�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�ϴε�½ʱ��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LastLoginTime';       -- sysname
--[Users].IsValid�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�Ƿ���Ч��1��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsValid';       -- sysname
--[Users].IsDel�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�Ƿ�ɾ����1��ɾ����0����',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsDel';       -- sysname
--[Users].Createtime�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����ʱ��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';       -- sysname

/*Log_Operation��˵��*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '������־��',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'Log_Operation';
--[Log_Operation].ID�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ID';       -- sysname
--[Log_Operation].UserID�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�����ߵ��û�ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'UserID';       -- sysname
--[Log_Operation].ServiceID�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '�����ķ��������ݿ�ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServiceID';       -- sysname
--[Log_Operation].OperaContent�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����������˵��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'OperaContent';       -- sysname
--[Log_Operation].Createtime�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '����ʱ��',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';       -- sysname
BEGIN
/*FS_ServiceSource��˵��*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '��������Դ���ñ�',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'FS_ServiceSource';
--[FS_ServiceSource].UserID�ֶ�˵��
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '��������Դ���ͣ��磺���ء������ơ���Ѷ�ơ���˾��������Ϊ�Ƶȵ�',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_ServiceSource', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'SourceName';       -- sysname
END