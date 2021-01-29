
CREATE extension IF NOT EXISTS "uuid-ossp";


DROP TABLE IF EXISTS System;
--系统表
CREATE TABLE IF NOT EXISTS System(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
	Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    Code VARCHAR(256)
);
INSERT INTO System(Id, CreateTime, CreateUserId, Name, Remark, Disable, IsDelete, Code) 
VALUES('e540a81d-bbc3-4767-8517-aa8b00fba169', now(), 'bfbfa658-16aa-4064-b5a5-aa8d00929262', '铁路安全人员防护系统', '', 'f', 'f', 'RailwayProtectionSystem');


DROP TABLE IF EXISTS SystemConfig;
--系统配置
CREATE TABLE IF NOT EXISTS SystemConfig(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
	CreateUserId UUID,
    LastUpdateUserId UUID,
    SystemId UUID NOT NULL,
    ConfigType INT NOT NULL,
    ConfigData  TEXT
);

DROP TABLE IF EXISTS SystemModule;
--系统模块关系表
CREATE TABLE IF NOT EXISTS SystemModule(
	Id UUID PRIMARY KEY NOT NULL,
	SystemId UUID NOT NULL,
	ModuleId UUID NOT NULL,
	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
	Remark Varchar(100)
);

CREATE INDEX SystemModule_SystemId_Index ON SystemModule(SystemId);
CREATE INDEX SystemModule_ParentId_Index ON SystemModule(ModuleId);

INSERT INTO SYSTEMMODULE (Id,SystemId,ModuleId,CreateTime,LastUpdateTime) 
VALUES (uuid_generate_v1(),'e540a81d-bbc3-4767-8517-aa8b00fba169','4b69cf47-94ac-434d-ac17-ce9897e3c6b7',now(),now());

INSERT INTO SYSTEMMODULE (Id,SystemId,ModuleId,CreateTime,LastUpdateTime) 
VALUES (uuid_generate_v1(),'e540a81d-bbc3-4767-8517-aa8b00fba169','18d68120-a92e-426b-a63c-17ca5b75d7a9',now(),now());

INSERT INTO SYSTEMMODULE (Id,SystemId,ModuleId,CreateTime,LastUpdateTime) 
VALUES (uuid_generate_v1(),'e540a81d-bbc3-4767-8517-aa8b00fba169','720e8b2b-51f6-48a8-b44a-45f94c03c17d',now(),now());

INSERT INTO SYSTEMMODULE (Id,SystemId,ModuleId,CreateTime,LastUpdateTime) 
VALUES (uuid_generate_v1(),'e540a81d-bbc3-4767-8517-aa8b00fba169','70600c8b-4d8e-4d05-90c8-89f500ff9530',now(),now());

DROP TABLE IF EXISTS Module;
--模块列表
--模块类型 1-模块组 2-模块名  3-按钮名
CREATE TABLE IF NOT EXISTS Module(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    ModuleType INT NOT NULL,
    Url VARCHAR(256),
    OrderNo serial NOT NULL,
    ParentId UUID,
    XPath VARCHAR(1024) DEFAULT ''
);
CREATE INDEX Module_ParentId_Index ON Module(ParentId);

INSERT INTO Module(Id, CreateTime, Name, Remark, ModuleType,Url, ParentId, XPath) 
VALUES ('4b69cf47-94ac-434d-ac17-ce9897e3c6b7', now(), '基础管理', '', 1, '','00000000-0000-0000-0000-000000000000','基础管理');

INSERT INTO Module(Id, CreateTime, Name, Remark, ModuleType,Url, ParentId, XPath) 
VALUES ('18d68120-a92e-426b-a63c-17ca5b75d7a9', now(), '系统管理', '', 2, '/system.html','4b69cf47-94ac-434d-ac17-ce9897e3c6b7','基础管理/系统管理');

INSERT INTO Module(Id, CreateTime, Name, Remark, ModuleType,Url, ParentId, XPath) 
VALUES ('720e8b2b-51f6-48a8-b44a-45f94c03c17d', now(), '组织管理', '', 2, '/organize.html','4b69cf47-94ac-434d-ac17-ce9897e3c6b7','基础管理/组织管理');

INSERT INTO Module(Id, CreateTime, Name, Remark, ModuleType,Url, ParentId, XPath)
VALUES ('70600c8b-4d8e-4d05-90c8-89f500ff9530', now(), '用户管理', '', 2, '/user.html','4b69cf47-94ac-434d-ac17-ce9897e3c6b7','基础管理/用户管理');


DROP TABLE IF EXISTS Organization;
--组织表
CREATE TABLE IF NOT EXISTS Organization(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    Name VARCHAR(128),
	SystemId UUID NOT NULL,
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE
);
insert into Organization(Id,CreateTime,LastUpdateTime,Name,SystemId) 
values ('a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6',now(),now(),'海洋王','e540a81d-bbc3-4767-8517-aa8b00fba169');


DROP TABLE IF EXISTS Department;
--部门表
CREATE TABLE IF NOT EXISTS Department(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    OrganizationId UUID NOT NULL,
    ParentId UUID,
    XPath VARCHAR(1024) DEFAULT ''
)
    WITH (
  OIDS=FALSE
);
CREATE INDEX Department_OrganizationId_Index ON Department(OrganizationId);
CREATE INDEX Department_ParentId_Index ON Department(ParentId);


DROP TABLE IF EXISTS Role;
---角色表
CREATE TABLE IF NOT EXISTS Role(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
	RoleGroup INTEGER NOT NULL,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    OrganizationId UUID NOT NULL
);
CREATE INDEX Role_OrganizationId_Index ON Role(OrganizationId);
insert into Role (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values ('b182e4a3-48c9-4d17-baf0-1221accb3673',now(),now(),0,'超级管理员','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');
insert into Role (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values ('1754cd9f-2c66-4525-ad42-74f73613b6f3',now(),now(),1,'管理员','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');
insert into Role (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values (uuid_generate_v1(),now(),now(),2,'移动端用户','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');


DROP TABLE IF EXISTS RoleGroup;
---角色表
--角色类型 0-超级管理员 1-管理员  2-移动端管理员
CREATE TABLE IF NOT EXISTS RoleGroup(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
	RoleType INTEGER NOT NULL,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    OrganizationId UUID NOT NULL
);
CREATE INDEX RoleGroup_id_Index ON RoleGroup(Id);
CREATE INDEX RoleGroup_OrganizationId_Index ON RoleGroup(OrganizationId);

insert into RoleGroup (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values (uuid_generate_v1(),now(),now(),0,'超级管理员组','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');
insert into RoleGroup (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values (uuid_generate_v1(),now(),now(),1,'管理员组','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');
insert into RoleGroup (Id,CreateTime,LastUpdateTime,RoleGroup,Name,OrganizationId) values (uuid_generate_v1(),now(),now(),2,'移动端用户组','a8a1d2e2-c4e8-4760-8ca1-e9f4ba6cb6f6');

DROP TABLE IF EXISTS RoleGroupPermission;
--角色模块(权限)
CREATE TABLE IF NOT EXISTS RoleGroupPermission(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    RoleGroupId UUID NOT NULL,
    ModuleId UUID NOT NULL,
);

CREATE INDEX RoleGroupPermission_RoleGroupId_Index ON RoleGroupPermission(RoleGroupId);
CREATE INDEX RoleGroupPermission_ModuleId_Index ON RoleGroupPermission(ModuleId);

INSERT INTO RoleGroupPermission (Id,CreateTime,LastUpdateTime,RoleGroupId,ModuleId) 
Values (uuid_generate_v1(),now(),now(),'b182e4a3-48c9-4d17-baf0-1221accb3673','4b69cf47-94ac-434d-ac17-ce9897e3c6b7');

INSERT INTO RoleGroupPermission (Id,CreateTime,LastUpdateTime,RoleGroupId,ModuleId) 
Values (uuid_generate_v1(),now(),now(),'b182e4a3-48c9-4d17-baf0-1221accb3673','18d68120-a92e-426b-a63c-17ca5b75d7a9');

INSERT INTO RoleGroupPermission (Id,CreateTime,LastUpdateTime,RoleGroupId,ModuleId) 
Values (uuid_generate_v1(),now(),now(),'b182e4a3-48c9-4d17-baf0-1221accb3673','720e8b2b-51f6-48a8-b44a-45f94c03c17d');

INSERT INTO RoleGroupPermission (Id,CreateTime,LastUpdateTime,RoleGroupId,ModuleId) 
Values (uuid_generate_v1(),now(),now(),'b182e4a3-48c9-4d17-baf0-1221accb3673','70600c8b-4d8e-4d05-90c8-89f500ff9530');

DROP TABLE IF EXISTS UserAccount;
---用户表
CREATE TABLE IF NOT EXISTS UserAccount(
	Id UUID PRIMARY KEY NOT NULL,
   	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    Name VARCHAR(128),
    Remark VARCHAR(1024),
    Disable BOOLEAN NOT NULL DEFAULT FALSE,
    IsDelete BOOLEAN NOT NULL DEFAULT FALSE,
    Account VARCHAR(128) NOT NULL,
    Code VARCHAR(128),
    Password VARCHAR(128) NOT NULL,
    OrganizationId UUID NOT NULL,
    DepartmentId UUID NOT NULL,
    UserType INT NOT NULL,
    Sex INT NOT NULL,
    Description VARCHAR(1024),
    WebToken VARCHAR(128),
    MobileToken VARCHAR(128),
    LoginFaultCount INT NOT NULL DEFAULT 0,
	LsFirstLogin Boolean NOT NULL DEFAULT FALSE
);

CREATE INDEX UserAccount_Account_Index ON UserAccount(Account);
CREATE INDEX UserAccount_Code_Index ON UserAccount(Code);
CREATE INDEX UserAccount_Password_Index ON UserAccount(Password);
CREATE INDEX UserAccount_OrganizationId_Index ON UserAccount(OrganizationId);
CREATE INDEX UserAccount_DepartmentId_Index ON UserAccount(DepartmentId);

-- 添加管理员
INSERT INTO useraccount(Id,CreateTime,Name,Account,Code,Password,OrganizationId,DepartmentId,UserType,Sex,LoginFaultCount,LsFirstLogin)
VALUES(uuid_generate_v1(),now(),'admin','sAdmin','admin','0192023A7BBD73250516F069DF18B500','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000',0,0,0,false);


DROP TABLE IF EXISTS UserRoleGroupGroup;
--用户角色表
CREATE TABLE IF NOT EXISTS UserRoleGroupGroup(
	Id UUID PRIMARY KEY NOT NULL,
	CreateTime TIMESTAMP WITHOUT TIME ZONE,
	LastUpdateTime TIMESTAMP WITHOUT TIME ZONE,
    CreateUserId UUID,
    LastUpdateUserId UUID,
    UserId UUID NOT NULL,
    RoleGroup integer[] NOT NULL
);
CREATE INDEX UserRoleGroup_UserId_Index ON UserRoleGroupGroup(UserId);
CREATE INDEX UserRoleGroup_RoleId_Index ON UserRoleGroupGroup(RoleGroup);
