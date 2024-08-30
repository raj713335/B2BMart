GO
Create Database B2BMart;
GO

USE B2BMart;
GO

----- DROP TABLES ------
IF OBJECT_ID('dbo.[User]')  IS NOT NULL
DROP TABLE dbo.[User]
GO

IF OBJECT_ID('dbo.[Address]')  IS NOT NULL
DROP TABLE dbo.[Address]
GO

IF OBJECT_ID('dbo.[Product]')  IS NOT NULL
DROP TABLE dbo.[Product]
GO

IF OBJECT_ID('dbo.[Orders]')  IS NOT NULL
DROP TABLE dbo.[Orders]
GO

IF OBJECT_ID('dbo.[OrderItem]')  IS NOT NULL
DROP TABLE dbo.[OrderItem]
GO

IF OBJECT_ID('dbo.[DeliveryMethod]')  IS NOT NULL
DROP TABLE dbo.[DeliveryMethod]
GO

IF OBJECT_ID('dbo.[ProductType]')  IS NOT NULL
DROP TABLE dbo.[ProductType]
GO

IF OBJECT_ID('dbo.[ProductBrand]')  IS NOT NULL
DROP TABLE dbo.[ProductBrand]
GO


IF OBJECT_ID('dbo.[role]')  IS NOT NULL
DROP TABLE dbo.[role]
GO

IF OBJECT_ID('dbo.user_roles')  IS NOT NULL
DROP TABLE dbo.user_roles
GO


CREATE TABLE dbo.[User] (
	UserId int IDENTITY(1,1) CONSTRAINT pk_UserId PRIMARY KEY,
	IsDeleted bit Not Null,
	EmailId varchar(50) Not null CONSTRAINT chk_user_email UNIQUE,
	FirstName varchar(100) Not null,
	LastName varchar(100) Not null,
	PasswordSalt Varbinary(512) Not null,
	PasswordHash Varbinary(512) Not null,
	PhoneNumber varchar(50) Not null,
	UserType varchar(20) Not null CONSTRAINT chk_userType CHECK ([UserType] IN ('Admin', 'Seller', 'Consumer')),	--seller or consumer
	UserName varchar(20) Not null CONSTRAINT chk_user_unq UNIQUE,
	CreatedBy varchar(100) Not Null,
	UpdatedBy varchar(100),
	CreatedAt datetime Not Null,
	UpdatedAt datetime);
GO



CREATE TABLE dbo.[Address](
	AddressId	int Identity(1,1) CONSTRAINT pk_addressId Primary key,
	UserID INT Not null references dbo.[User](UserId),
	[Address1] varchar(200) Not null,
	[State] varchar(50) Not Null,
	[City] varchar(50) Not Null,
	[ZipCode] varchar(10) Not Null,
	[Country] varchar(50) Not Null,
	AddressType varchar(20) Not null,	--billing, shipping
	CreatedBy varchar(100) Not Null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not Null,
	UpdatedAt	datetime
);
GO


CREATE TABLE dbo.[ProductType] (
	ID int IDENTITY(1,1) Not Null Primary key,
	ProductTypeName varchar(100) Not Null,
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO


CREATE TABLE dbo.[ProductBrand] (
	ID int IDENTITY(1,1) Not Null Primary key, 
	ProductBrandName varchar(100) Not Null,
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO


CREATE TABLE dbo.[Product] (
	ProductId int IDENTITY(1,1) Primary key,
	IsDeleted bit Not null,
	Product_Name varchar(255) Not null,
	Product_code varchar(255) Not null,
	[Description] varchar(3000) Not null,
	PictureUrl varchar(600) Not Null,
	ProductTypeId INT Not Null references dbo.[ProductType](ID),
	ProductBrandId INT Not Null references dbo.[ProductBrand](ID),
	Price decimal(10,2) Not Null CONSTRAINT chk_price CHECK ([Price] > 0),
	Sellerid int references dbo.[User](UserId) Not null,
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO


CREATE TABLE dbo.[DeliveryMethod] (
	DeliveryId int IDENTITY(1,1) primary key,
	ShortName varchar(255) Not null,
	DeliveryTime varchar(255) Not null,
	[Description] varchar(255) Not null,
	Price float Not Null,
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO


CREATE TABLE dbo.[Orders] (
	OrderId int IDENTITY(1,1) primary key, 
	IsDeleted bit Not null,
	Amount float Not null,
	IsCancelled bit Not null,
	IsPaid bit Not null,
	UserId int references dbo.[User](UserId),
	DeliveryId int references dbo.[DeliveryMethod](DeliveryId),
	AddressId int references dbo.[Address](AddressId),
	PaymentIntentId varchar(1000) not null,
	[Status]  varchar(20) Not null,
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO


CREATE TABLE dbo.[OrderItem] (
	OrderItemId int IDENTITY(1,1) primary key,
	IsDeleted bit Not null,
	UserId int references dbo.[User](UserId),
	Productid int references dbo.[Product](ProductId),
	OrderId int references dbo.[Orders](OrderId),
	Price float Not null,
	Quantity int Not null,
	[Status] int Not null,	--shipping, in transit, order placed
	[StatusDescription] varchar(3000),	--shipping, in transit, order placed
	CreatedBy varchar(100) Not null,
	UpdatedBy varchar(100),
	CreatedAt	datetime Not null,
	UpdatedAt	datetime);
GO



CREATE TABLE dbo.[role] (
	roleId int IDENTITY primary key,
	[type] int not null) ;
GO



CREATE TABLE dbo.user_roles (
	[user_id] int not null,
	role_id int not null,
	primary key ([user_id], role_id));
GO


alter table dbo.user_roles add constraint FK__roleid__role foreign key (role_id) references dbo.[role] (roleId);
alter table dbo.user_roles add constraint FK__userId__user foreign key ([user_id]) references dbo.[user] (userId);
GO

--INSERT INTO dbo.[role]([type]) VALUES('USER');
--INSERT INTO dbo.[role]([type]) VALUES('ADMIN');
--GO