create table Organizations(
	id int not null identity(1,1) constraint pk_organizations primary key,
	Name varchar(50) not null,
	Description varchar(100) not null,
	Status varchar(2) not null default 'AC' constraint ck_status_organizations check (status in ('AC', 'IN')),
	CreateAt datetime not null default SWITCHOFFSET(SYSDATETIMEOFFSET(), '-04:00')
);

insert into Organizations(Name, Description) 
values
	('Organization1', 'Description for Organization1');

select * from Organizations;

create table Roles(
	id int not null identity(1,1) constraint pk_roles primary key,
	idOrganization int not null constraint fk_roles_organizations foreign key references Organizations(id),
	Name varchar(50) not null,
	Description varchar(100) not null,
	IsAdmin bit not null default 0,
);

insert into Roles(idOrganization, Name, Description, isAdmin) 
values
	(1, 'Administrador', 'Rol administrador Organization1', 1);

select * from Roles;

create table Users(
	id int not null identity(1,1) constraint pk_users primary key,
	Name varchar(50) not null,
	LastName varchar(50) not null,
	Username varchar(50) not null,
	Email varchar(50) not null,
	Password varchar(max) not null,
	Status varchar(2) not null default 'AC' constraint ck_status_users check (status in ('AC', 'IN'))
);

insert into Users(Name, LastName, Username, Email, Password) 
values
	('Jerermy', 'Solano', 'jsolano', 'jsjeremy4@gmail.com', '$2a$12$OhgamW3Y0guy0F.pl2wKveoA7QdbXaBOSjruaxlQitzmqRokRNtcS');

select * from Users;

create table OrganizationUsers(
	idUser int not null constraint fk_organization_users foreign key references Users(id),
	idOrganization int not null constraint fk2_organization_users foreign key references Organizations(id),
	JoinedAt datetime not null default SWITCHOFFSET(SYSDATETIMEOFFSET(), '-04:00'),
	Roles nvarchar not null
);

insert into OrganizationUsers(idUser, idOrganization, Roles) 
values
	(1, 1, '1');

select * from OrganizationUsers;

create table OrganizationUserRoles(
	idUser int not null constraint fk_organization_user_roles foreign key references Users(id),
	idOrganization int not null constraint fk2_organization_user_roles foreign key references Organizations(id),
	idRole int not null constraint fk3_organization_user_roles foreign key references Roles(id),
);

insert into OrganizationUserRoles(idUser, idOrganization, idRole) 
values
	(1, 1, 1);

select * from OrganizationUserRoles;

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';