USE [DBMSTool];
GO
/*[FS_Services]表说明*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '服务器-数据库表',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'FS_Services';

--[FS_Services].ServerAddress字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',          -- sysname
                                @value = '服务器地址，如123.123.123.123', -- sql_variant
                                @level0type = 'schema',            -- varchar(128)
                                @level0name = N'dbo',              -- sysname
                                @level1type = N'table',            -- varchar(128)
                                @level1name = N'FS_Services',      -- sysname
                                @level2type = N'column',           -- varchar(128)
                                @level2name = N'ServerAddress';
-- sysname
--[FS_Services].ServerAddress2字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '指向同一个服务器的第二个备用地址',  -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerAddress2';
-- sysname
--[FS_Services].ServerPortNo字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '端口号',               -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerPortNo';
-- sysname
--[FS_Services].LoginName字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '登陆用户名',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginName';
-- sysname
--[FS_Services].LoginPassword字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '登陆密码',              -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginPassword';
-- sysname
--[FS_Services].ServerType字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '数据库类型,1SqlServer,2MySql',               -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServerType';
-- sysname
--[FS_Services].ServerName字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '数据库名称',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'DataBaseName';
-- sysname
--[FS_Services].ServerIntro字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '数据库简介',             -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'DataBaseIntro';
-- sysname
--[FS_Services].TypeSource字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '来源,1电信通,2阿里云,3腾讯云,4华为云...', -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'TypeSource';
-- sysname
--[FS_Services].IsInUse字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '是否使用中，1是，0否',       -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsInUse';
-- sysname
--[FS_Services].Createtime字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '创建时间',              -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';
-- sysname
--[FS_Services].IsDel字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '是否删除，0正常，1已删除',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_Services', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsDel';       -- sysname
/*Users表说明*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '用户表',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'Users';
--[Users].ID字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '主键ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ID';       -- sysname
--[Users].UserName字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '用户姓名',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'UserName';       -- sysname
--[Users].LoginName字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '登录名',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginName';       -- sysname
--[Users].LoginPwd字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '登录密码',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LoginPwd';       -- sysname
--[Users].IsAdmin字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '是否管理员，1是',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsAdmin';       -- sysname
--[Users].LastLoginTime字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '上次登陆时间',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'LastLoginTime';       -- sysname
--[Users].IsValid字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '是否有效，1是',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsValid';       -- sysname
--[Users].IsDel字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '是否删除，1已删除，0正常',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'IsDel';       -- sysname
--[Users].Createtime字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '创建时间',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Users', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';       -- sysname

/*Log_Operation表说明*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '操作日志表',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'Log_Operation';
--[Log_Operation].ID字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '主键ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ID';       -- sysname
--[Log_Operation].UserID字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '操作者的用户ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'UserID';       -- sysname
--[Log_Operation].ServiceID字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '操作的服务器数据库ID',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'ServiceID';       -- sysname
--[Log_Operation].OperaContent字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '操作的内容说明',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'OperaContent';       -- sysname
--[Log_Operation].Createtime字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '创建时间',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'Log_Operation', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'Createtime';       -- sysname
BEGIN
/*FS_ServiceSource表说明*/
EXEC sys.sp_addextendedproperty @name = 'MS_Description',@value = '服务器来源配置表',@level0type = N'SCHEMA',@level0name = N'dbo',@level1type = N'table',@level1name = N'FS_ServiceSource';
--[FS_ServiceSource].UserID字段说明
EXEC sys.sp_addextendedproperty @name = 'MS_Description',     -- sysname
                                @value = '服务器来源类型，如：本地、阿里云、腾讯云、公司机房、华为云等等',     -- sql_variant
                                @level0type = 'schema',       -- varchar(128)
                                @level0name = N'dbo',         -- sysname
                                @level1type = N'table',       -- varchar(128)
                                @level1name = N'FS_ServiceSource', -- sysname
                                @level2type = N'column',      -- varchar(128)
                                @level2name = N'SourceName';       -- sysname
END