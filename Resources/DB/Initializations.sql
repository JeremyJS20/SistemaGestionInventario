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

CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Key] NVARCHAR(100) NOT NULL UNIQUE,
    Category NVARCHAR(100) NOT NULL,
    Name NVARCHAR(200) NOT NULL
);

INSERT INTO Permissions ([Key], Category, Name) VALUES
('ART_VIEW',   'Artículos', 'Ver Artículos'),
('ART_EDIT',   'Artículos', 'Editar Artículos'),
('ART_CREATE', 'Artículos', 'Crear Artículos'),
('ART_DELETE', 'Artículos', 'Eliminar Artículos');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('INVMNMNT_VIEW',   'Gestion de Articulos', 'Ver Articulos'),
('INVMNMNT_EDIT',   'Gestion de Articulos', 'Editar Articulos'),
('INVMNMNT_CREATE', 'Gestion de Articulos', 'Crear Articulos'),
('INVMNMNT_DELETE', 'Gestion de Articulos', 'Eliminar Articulos');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('INVTYPE_VIEW',   'Tipos de Inventario', 'Ver Tipos de Inventario'),
('INVTYPE_EDIT',   'Tipos de Inventario', 'Editar Tipos de Inventario'),
('INVTYPE_CREATE', 'Tipos de Inventario', 'Crear Tipos de Inventario'),
('INVTYPE_DELETE', 'Tipos de Inventario', 'Eliminar Tipos de Inventario');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('INVWHEX_VIEW',   'Gestion de Existencias', 'Ver Existencias'),
('INVWHEX_EDIT',   'Gestion de Existencias', 'Editar Existencias'),
('INVWHEX_CREATE', 'Gestion de Existencias', 'Crear Existencias'),
('INVWHEX_DELETE', 'Gestion de Existencias', 'Eliminar Existencias');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('WH_VIEW',   'Almacenes', 'Ver Almacenes'),
('WH_EDIT',   'Almacenes', 'Editar Almacenes'),
('WH_CREATE', 'Almacenes', 'Crear Almacenes'),
('WH_DELETE', 'Almacenes', 'Eliminar Almacenes');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('TRSCTN_VIEW',   'Transacciones', 'Ver Transacciones'),
('TRSCTN_EDIT',   'Transacciones', 'Editar Transacciones'),
('TRSCTN_CREATE', 'Transacciones', 'Crear Transacciones'),
('TRSCTN_DELETE', 'Transacciones', 'Eliminar Transacciones');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('USR_VIEW',   'Control de Acceso', 'Ver Usuarios'),
('USR_EDIT',   'Control de Acceso', 'Editar Usuarios'),
('USR_CREATE', 'Control de Acceso', 'Crear Usuarios'),
('USR_DELETE', 'Control de Acceso', 'Eliminar Usuarios');

INSERT INTO Permissions ([Key], Category, Name) VALUES
('ROLE_VIEW',   'Control de Acceso', 'Ver Roles'),
('ROLE_EDIT',   'Control de Acceso', 'Editar Roles'),
('ROLE_CREATE', 'Control de Acceso', 'Crear Roles'),
('ROLE_DELETE', 'Control de Acceso', 'Eliminar Roles');

select * from Permissions;

create table RolePermissions(
	idRole int not null constraint fk_role_permissions foreign key references Roles(id),
	idPermission int not null constraint fk2_role_permissions foreign key references Permissions(id)
);

insert into RolePermissions(idRole, idPermission) 
values
	(1, 1), 
	(1, 13),
	(1, 17);

select * from RolePermissions;

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';

select
	u.id as UserId,
	u.Username,
	u.Email,
	o.id as OrganizationId,
	o.Name as OrganizationName,
	(
        SELECT 
			r.id, 
			r.Name,
			(
                SELECT 
					p.id,
					p.Name,
					p.[Key]
                FROM RolePermissions rp
                INNER JOIN Permissions p ON p.id = rp.idPermission
                WHERE rp.idRole = r.id
                FOR JSON PATH
            ) AS Permissions
        FROM Roles as r
        INNER JOIN OrganizationUserRoles as our ON our.idOrganization = o.id
        WHERE our.idUser = u.id
        FOR JSON PATH
    ) AS Roles
from Users as u
inner join OrganizationUsers as ou on ou.idUser = u.id
inner join Organizations as o on o.id = ou.idOrganization
where 
	u.Email = 'jsjeremy4@gmail.com';