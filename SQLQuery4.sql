set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on
go

create database [Test_Cargo1]
go

use [Test_Cargo1]
go

create table [dbo].[License]
(
	[ID_License] [int] not null identity(1,1),
	[Type_License] [varchar] (10) not null
	constraint [PK_License] primary key clustered
	([ID_License] ASC) on [PRIMARY],
	constraint [UQ_Type_License] unique ([Type_License])
)
go

insert into [dbo].[License] ([Type_License])
values ('B'), ('C')
go

select [Type_License] as 'Категория' from [dbo].[License]
order by [Type_License] ASC
go

update [dbo].[License] set [Type_License] = 'D'
where [ID_License] = 2
go

delete [dbo].[License] where [ID_License] = 1
go

create table [dbo].[Type_Organization]
(
	[ID_Type_Organization] [int] not null identity(1,1),
	[Type_Name] [varchar] (max) not null
	constraint [PK_Type_Organization] primary key clustered
	([ID_Type_Organization] ASC) on [PRIMARY]
)
go

insert into [dbo].[Type_Organization] ([Type_Name])
values ('ПАО'), ('НПАО'), ('ООО')
go

select [Type_Name] as 'Тип организации' from [dbo].[Type_Organization]
order by [Type_Name] ASC
go

create table [dbo].[Brand]
(
	[ID_Brand] [int] not null identity(1,1),
	[Name_Brand] [varchar] (50) not null
	constraint [PK_Brand] primary key clustered
	([ID_Brand] ASC) on [PRIMARY],
	constraint [UQ_Name_Brand] unique ([Name_Brand])
)
go

insert into [dbo].[Brand] ([Name_Brand])
values ('Mersedes'), ('ГАЗ')
go

select [Name_Brand] as 'Марка' from [dbo].[Brand]
order by [Name_Brand] DESC
go

create table [dbo].[Delivery_Points]
(
	[ID_Delivery_Point] [int] not null identity(1,1),
	[Delivery_Address] [varchar] (max) not null
	constraint [PK_Delivery_Point] primary key clustered
	([ID_Delivery_Point] ASC) on [PRIMARY]
)
go

insert into [dbo].[Delivery_Points] ([Delivery_Address])
values ('Ул. Вернадского, д. 3, стр. 4'), ('Ул. Витебская, д. 8, к. 5'), ('Ул. Вернадского, д. 6, к. 3'), ('Ул. Арбат, д. 8, к. 3'), ('Ул. Арбат, д. 1, стр. 3')
go

select [Delivery_Address] as 'Адрес точки доставки' from [dbo].[Delivery_Points]
order by [Delivery_Address] ASC
go

create table [dbo].[Post]
(
	[ID_Post] [int] not null identity(1,1),
	[Name_Post] [varchar] (50) not null
	constraint [PK_Post] primary key clustered
	([ID_Post] ASC) on [PRIMARY],
	constraint [UQ_Name_Post] unique ([Name_Post])
)
go

insert into [dbo].[Post] ([Name_Post])
values ('Водитель'),('Менеджер по заказам'),('Менеджер по продажам'),('Директор')
go

select [Name_Post] as 'Должность' from [dbo].[Post]
order by [Name_Post] ASC
go

create table [dbo].[Country]
(
	[ID_Country] [int] not null identity(1,1),
	[Name_Country] [varchar] (50) not null
	constraint [PK_Country] primary key clustered
	([ID_Country] ASC) on [PRIMARY],
	constraint [UQ_Name_Country] unique ([Name_Country])
)
go

insert into [dbo].[Country] ([Name_Country])
values ('Германия'), ('Россия')
go

select [Name_Country] as 'Должность' from [dbo].[Country]
order by [Name_Country] ASC
go

create table [dbo].[Cargo]
(
	[ID_Cargo] [int] not null identity(1,1),
	[Description_Cargo] [varchar] (max) not null,
	[Weight_Cargo] [varchar] (8) not null,
	[Length_Cargo] [varchar] (8) not null,
	[Width_Cargo] [varchar] (8) not null,
	[Height_Cargo] [varchar] (8) not null
	constraint [PK_Cargo] primary key clustered
	([ID_Cargo] ASC) on [PRIMARY],
	constraint [CH_Weight_Cargo] check ([Weight_Cargo] like ('[0-9][0-9][0-9] кг')
	or [Weight_Cargo] like ('[0-9][0-9][0-9][0-9] кг') or [Weight_Cargo] like ('[0-9][0-9][0-9][0-9][0-9] кг')),
	constraint [CH_Length_Cargo] check ([Length_Cargo] like ('[0-9][0-9][0-9] мм')
	or [Length_Cargo] like ('[0-9][0-9][0-9][0-9] мм') or [Length_Cargo] like ('[0-9][0-9][0-9][0-9][0-9] мм')),
	constraint [CH_Width_Cargo] check ([Width_Cargo] like ('[0-9][0-9][0-9] мм')
	or [Width_Cargo] like ('[0-9][0-9][0-9][0-9] мм') or [Width_Cargo] like ('[0-9][0-9][0-9][0-9][0-9] мм')),
	constraint [CH_Height_Cargo] check ([Height_Cargo] like ('[0-9][0-9][0-9] мм')
	or [Height_Cargo] like ('[0-9][0-9][0-9][0-9] мм') or [Height_Cargo] like ('[0-9][0-9][0-9][0-9][0-9] мм'))
)
go

update [dbo].[Cargo] set [Description_Cargo] = 'Одежда' where [ID_Cargo] = 4
go

delete [dbo].[Cargo] where [Description_Cargo] = 'Одежда'
go

insert into [dbo].[Cargo] ([Description_Cargo], [Weight_Cargo], [Length_Cargo], [Width_Cargo], [Height_Cargo])
values ('Строительные материалы', '5000 кг', '5000 мм', '2000 мм', '1000 мм'), ('Строительные материалы', '8000 кг', '8000 мм', '2000 мм', '1000 мм'),
('Мебель', '2300 кг', '2321 мм', '1516 мм', '1000 мм'), ('Мебель', '3000 кг', '3151 мм', '1923 мм', '1000 мм'), ('Строительные материалы', '1000 кг', '1000 мм', '2000 мм', '1000 мм')
go

select [Description_Cargo] as 'Описание', [Weight_Cargo] as 'Вес', [Length_Cargo]+' '+[Width_Cargo]+' '+[Height_Cargo] as 'Габариты' from [dbo].[Cargo]
order by [Description_Cargo] ASC
go

create table [dbo].[Model]
(
	[ID_Model] [int] not null identity(1,1),
	[Name_Model] [varchar] (50) not null,
	[Brand_ID] [int] not null
	constraint [PK_Model] primary key clustered
	([ID_Model] ASC) on [PRIMARY],
	constraint [UQ_Name_Model] unique ([Name_Model]),
	constraint [FK_Brand_Model] foreign key ([Brand_ID])
	references [dbo].[Brand] ([ID_Brand])
)
go

insert into [dbo].[Model] ([Name_Model], [Brand_ID])
values ('Atego', 1), ('Arocs', 1), ('Газель NN', 2)
go

select [Name_Model] as 'Модель', [Name_Brand] as 'Марка'  from [dbo].[Model]
inner join [dbo].[Brand] on [ID_Brand] = [Brand_ID]
go

select [Name_Model] as 'Модель', [Name_Brand] as 'Марка'  from [dbo].[Model]
inner join [dbo].[Brand] on [ID_Brand] = [Brand_ID]
where [Name_Brand] = 'ГАЗ'
go

create table [dbo].[Transport]
(
	[ID_Transport] [int] not null identity(1,1),
	[Length_Transport] [varchar] (8) not null,
	[Width_Transport] [varchar] (8) not null,
	[Height_Transport] [varchar] (8) not null,
	[Load_Copacity] [varchar] (8) not null,
	[Copacity] [varchar] (9) not null,
	[Number_Transport] [varchar] (10) not null,
	[Model_ID] [int] not null
	constraint [PK_Transport] primary key clustered
	([ID_Transport] ASC) on [PRIMARY],
	constraint [CH_Length_Transport] check ([Length_Transport] like ('[0-9][0-9][0-9][0-9] мм')),
	constraint [CH_Width_Transport] check ([Width_Transport] like ('[0-9][0-9][0-9][0-9] мм')),
	constraint [CH_Height_Transport] check ([Height_Transport] like ('[0-9][0-9][0-9][0-9] мм')),
	constraint [CH_Load_Copacity] check ([Load_Copacity] like ('[0-9][0-9][0-9][0-9] кг')
	or [Load_Copacity] like ('[0-9][0-9][0-9][0-9][0-9] кг')),
	constraint [CH_Copacity] check ([Copacity] like ('[0-9][0-9] куб. м')),
	constraint [CH_Number_Transport] check ([Number_Transport] like ('[А-Я][0-9][0-9][0-9][А-Я][А-Я]-[0-9][0-9]')
	or [Number_Transport] like ('[А-Я][0-9][0-9][0-9][А-Я][А-Я]-[0-9][0-9][0-9]')),
	constraint [UQ_Number_Transport] unique ([Number_Transport]),
	constraint [FK_Model_Transport] foreign key ([Model_ID])
	references [dbo].[Model] ([ID_Model])
)
go

insert into [dbo].[Transport] ([Model_ID], [Length_Transport],[Width_Transport],[Height_Transport],[Load_Copacity],[Copacity],[Number_Transport])
values (1, '8584 мм', '2300 мм', '2740 мм', '8000 кг', '43 куб. м', 'Д232ОА-78'), (2, '9340 мм', '2300 мм', '3650 мм', '9000 кг', '45 куб. м', 'О003БВ-102'),
(3, '6373 мм', '2513 мм', '2175 мм', '4600 кг', '29 куб. м', 'С831АК-161'), (3, '6373 мм', '2513 мм', '2175 мм', '4600 кг', '29 куб. м', 'К715АС-161'),
(3, '6373 мм', '2513 мм', '2175 мм', '4600 кг', '29 куб. м', 'С512СА-78')
go

select [Length_Transport]+' '+[Width_Transport]+' '+[Height_Transport] as 'Габариты', [Load_Copacity] as 'Грузоподъемность', [Copacity] as 'Вместимость', 
[Number_Transport] as 'Номер', [Name_Model] as 'Модель' from [dbo].[Transport]
inner join [dbo].[Model] on [ID_Model] = [Model_ID]
go

create table [dbo].[Customer]
(
	[ID_Customer] [int] not null identity(1,1),
	[Name_Customer_Organization] [varchar] (50) not null,
	[Organization_Address] [varchar] (max) not null,
	[TIN] [varchar] (12) not null,
	[BIC] [varchar] (9) not null,
	[OKPO] [varchar] (8) not null,
	[Login_Customer] [varchar] (50) not null,
	[Password_Customer] [varchar] (32) not null,
	[Name_Customer] [varchar] (32) not null,
	[Surname_Customer] [varchar] (32) not null,
	[Lastname_Customer] [varchar] (32) null default('-'),
	[Type_Organization_ID] [int] not null
	constraint [PK_Customer] primary key clustered
	([ID_Customer] ASC) on [PRIMARY],
	constraint [CH_TIN] check 
	([TIN] like ('[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	constraint [UQ_TIN] unique ([TIN]),
	constraint [CH_BIC] check ([BIC] like ('[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	constraint [CH_OKPO] check ([OKPO] like ('[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	constraint [UQ_OKPO] unique ([OKPO]),
	constraint [CH_Login_Customer] check 
	(len([Login_Customer]) >=8),
	constraint [UQ_Login_Customer] unique ([Login_Customer]),
	constraint [CH_Password_Customer] check 
	([Password_Customer] like ('%[!@#$%^&*()]%')),
	constraint [FK_Type_Organization_Customer] foreign key ([Type_Organization_ID])
	references [dbo].[Type_Organization] ([ID_Type_Organization])
)
go


insert into [dbo].[Customer] ([Type_Organization_ID], [Name_Customer_Organization], [Organization_Address], [TIN], [BIC], [OKPO],
[Login_Customer], [Password_Customer], [Name_Customer], [Surname_Customer], [Lastname_Customer])
values (3, '"Заказчик"', 'Ул. Чертановская, д. 3, стр. 3', '623874628773', '487238473', '00432445', 'Ivanov22', 'pa$$w0rd', 'Иван', 'Иванов', 'Иванович'),
(3, '"Заказчик простой челАвек"', 'Ул. Степановская, д. 6, к. 1', '584638576223', '004342561', '21123156', 'Smirnov1', 'pa$$w0rd', 'Иван', 'Смирнов', 'Петрович'),
(2, '"Мы не заказчики"', 'Ул. Чертановская, д. 2, стр. 1', '464736767782', '000423213', '32847234', 'Dyatlov22', 'pa$$w0rd', 'Василий', 'Дятлов', 'Андреевич'),
(1, '"Привезите пж"', 'Ул. Арбат, д. 2, к. 6', '784738748772', '762314876', '00345343', 'Petrov23', 'pa$$w0rd', 'Петр', 'Петров', 'Петрович'),
(3, '"Галилео"', 'Ул. Молочная, д. 13, к. 5', '723847282341', '594823589', '00232312', 'Lohmatin', 'pa$$w0rd', 'Сергей', 'Лохматин', 'Васильевич')
go


select [Name_Customer]+' '+[Surname_Customer]+' '+[Lastname_Customer] as 'Фио покупателя', [Login_Customer] as 'Логин', [Password_Customer] as 'Пароль',
[TIN] as 'ИНН', [BIC] as 'БИК', [OKPO] as 'ОКПО', [Type_Name] as 'Тип организации', [Name_Customer_Organization] as 'Название организации',
[Organization_Address] as 'Адрес организации' from [dbo].[Customer]
inner join [dbo].[Type_Organization] on [ID_Type_Organization] = [Type_Organization_ID]
go

create table [dbo].[Carrier]
(
	[ID_Carrier] [int] not null identity(1,1),
	[Name_Carrier] [varchar] (50) not null,
	[Type_Organization_ID] [int] not null
	constraint [PK_Carrier] primary key clustered
	([ID_Carrier] ASC) on [PRIMARY],
	constraint [UQ_Name_Carrier] unique ([Name_Carrier]),
	constraint [FK_Type_Organization_Carrier] foreign key ([Type_Organization_ID])
	references [dbo].[Type_Organization] ([ID_Type_Organization])
)
go

update [dbo].[Carrier] set [Type_Organization_ID] = 2 where [ID_Carrier] = 1
go

delete [dbo].[Carrier] where [ID_Carrier] = 2
go

insert into [dbo].[Carrier] ([Name_Carrier], [Type_Organization_ID])
values ('"Перевезу даром"', 1), ('"Куплю перевозку"', 3), ('"ПЕРЕВОЗКИ2202"', 2),
('"Анапа2007"', 3), ('"Перевозитель"', 3)
go

select [Name_Carrier] as 'Название организации', [Type_Name] as 'Тип организации' from [dbo].[Carrier]
inner join [dbo].[Type_Organization] on [ID_Type_Organization] = [Type_Organization_ID]
go

select [Name_Carrier] as 'Название организации', [Type_Name] as 'Тип организации' from [dbo].[Carrier]
inner join [dbo].[Type_Organization] on [ID_Type_Organization] = [Type_Organization_ID]
where [Type_Name] = 'ООО' 
go

create table [dbo].[Employee]
(
	[ID_Employee] [int] not null identity(1,1),
	[Name_Employee] [varchar] (32) not null,
	[Surname_Employee] [varchar] (32) not null,
	[Lastname_Employee] [varchar] (32) null default('-'),
	[SNILS] [varchar] (14) not null,
	[FOMS] [varchar] (16) not null,
	[Login_Employee] [varchar] (50) not null,
	[Password_Employee] [varchar] (32) not null,
	[Carrier_ID] [int] not null
	constraint [PK_Employee] primary key clustered
	([ID_Employee] ASC) on [PRIMARY],
	constraint [UQ_SNILS] unique ([SNILS]),
	constraint [CH_SNILS] check
	([SNILS] like ('[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9] [0-9][0-9]')),
	constraint [UQ_FOMS] unique ([FOMS]),
	constraint [CH_FOMS] check
	([FOMS] like ('[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')),
	constraint [UQ_Login_Employee] unique ([Login_Employee]),
	constraint [CH_Login_Employee] check (len([Login_Employee])>=8),
	constraint [CH_Password_Employee] check 
	([Password_Employee] like ('%[!@#$%^&*()]%')),
	constraint [FK_Carrier_Employee] foreign key ([Carrier_ID])
	references [dbo].[Carrier] ([ID_Carrier])
)
go

update [dbo].[Employee] set [Name_Employee] = 'Васька' where [ID_Employee] = 1
go

delete [dbo].[Employee] where [ID_Employee] = 1
go

insert into [dbo].[Employee] ([Name_Employee], [Surname_Employee], [Lastname_Employee], [SNILS], [FOMS], [Login_Employee], [Password_Employee], [Carrier_ID])
values ('Иван', 'Болванов', 'Вахтангович', '634-323-232 00', '7849123112311240', 'bolvanov', 'pa$$w0rd', 1),
('Дмитрий', 'Дмитриев', 'Дмитриевич', '152-512-232 00', '2384198471152980', 'dmitriev', 'pa$$w0rd', 2),
('Олег', 'Олегов', 'Олегович', '124-512-535 00', '8712638716238760', 'olegovi4', 'pa$$w0rd', 3),
('Олег', 'Олегов', 'Олегович', '654-412-123 00', '4739247392487910', 'oleg1978', 'pa$$w0rd', 4),
('Павел', 'Павлов', 'Павлович', '412-123-321 00', '3487162487619280', 'pavlov22', 'pa$$w0rd', 5)
go

select [Name_Employee]+' '+[Surname_Employee]+' '+[Lastname_Employee] as 'ФИО сотрудника', [SNILS] as 'СНИЛС', [FOMS] as 'ФОМС', [Login_Employee] as 'Логин',
[Password_Employee] as 'Пароль', [Name_Carrier] as 'Название организации перевозчика' from [dbo].[Employee]
inner join [dbo].[Carrier] on [ID_Carrier] = [Carrier_ID]
go

create table [dbo].[Route_Sheet]
(
	[ID_Route_Sheet] [int] not null identity(1,1),
	[Number_Route_Sheet] [varchar] (max) not null,
	[Date_Route_Sheet] [date] not null default(getdate()),
	[Time_Route_Sheet] [time] not null default(getdate()),
	[Carrier_ID] [int] not null,
	[Transport_ID] [int] not null
	constraint [PK_Route_Sheet] primary key clustered
	([ID_Route_Sheet] ASC) on [PRIMARY],
	constraint [CH_Number_Route_Sheet] check
	([Number_Route_Sheet] like ('[0-9][0-9][0-9][0-9]-[0-9][0-9]')),
	constraint [FK_Carrier_Route_Sheet] foreign key ([Carrier_ID])
	references [dbo].[Carrier] ([ID_Carrier]),
	constraint [FK_Transport_Route_Sheet] foreign key ([Transport_ID])
	references [dbo].[Transport] ([ID_Transport]),
)
go

update [dbo].[Route_Sheet] set [Number_Route_Sheet] = '2372-22' where [ID_Route_Sheet] = 3
go

delete [dbo].[Route_Sheet] where [ID_Route_Sheet] = 5
go

insert into [dbo].[Route_Sheet] ([Number_Route_Sheet], [Date_Route_Sheet], [Time_Route_Sheet], [Carrier_ID], [Transport_ID])
values ('0001-21', Convert(date, '24.12.2021'), Convert(time, '17:43:00'), 1, 1),
('0002-21', Convert(date, '28.12.2021'), Convert(time, '18:22:00'), 2, 2),
('0003-22', Convert(date, '01.01.2022'), Convert(time, '05:23:00'), 3, 3),
('0004-22', Convert(date, '01.12.2022'), Convert(time, '23:32:00'), 4, 4),
('0005-22', Convert(date, '03.12.2022'), Convert(time, '20:21:00'), 5, 5)
go

select [Number_Route_Sheet] as 'Номер', [Date_Route_Sheet] as 'Дата', [Time_Route_Sheet] as 'Время', [Name_Carrier] as 'Перевозчик',
[Name_Model]+' '+[Number_Transport] from [dbo].[Route_Sheet]
inner join [dbo].[Carrier] on [ID_Carrier] = [Carrier_ID]
inner join [dbo].[Transport] on [ID_Transport] = [Transport_ID] inner join [dbo].[Model] on [ID_Model] = [Model_ID]
go

create table [dbo].[Application]
(
	[ID_Application] [int] not null identity(1,1),
	[Number_Application] [varchar] (max) not null,
	[Date_Application] [date] not null default(getdate()),
	[Time_Application] [time] not null default(getdate()),
	[Status_Application] [varchar] (15) not null,
	[Customer_ID] [int] not null,
	[Route_Sheet_ID] [int] not null
	constraint [PK_Application] primary key clustered
	([ID_Application] ASC) on [PRIMARY],
	constraint [CH_Status_Application] check 
	([Status_Application] in ('Доставлен', 'Доставляется', 'Отменен')),
	constraint [FK_Customer_Application] foreign key ([Customer_ID])
	references [dbo].[Customer] ([ID_Customer]),
	constraint [FK_Route_Sheet_Application] foreign key ([Route_Sheet_ID])
	references [dbo].[Route_Sheet] ([ID_Route_Sheet])
)
go

insert into [dbo].[Application] ([Number_Application], [Date_Application], [Time_Application], [Status_Application], [Customer_ID], [Route_Sheet_ID])
values ('1', Convert(date, '23.12.2021'), Convert(time, '17:43'), 'Доставлен', 1, 1),
('2', Convert(date, '28.12.2021'), Convert(time, '16:23'), 'Доставлен', 2, 2),
('3', Convert(date, '01.01.2022'), Convert(time, '00:00'), 'Доставлен', 3, 3),
('4', Convert(date, '01.01.2022'), Convert(time, '19:32'), 'Доставлен', 4, 4),
('5', Convert(date, '03.01.2022'), Convert(time, '17:21'), 'Доставляется', 5, 5)
go

select [Number_Application] as 'Номер заявки', [Date_Application] as 'Дата', [Time_Application] as 'Время', [Status_Application] as 'Статус',
[Name_Customer]+' '+[Surname_Customer]+' '+[Lastname_Customer] as 'ФИО покупателя', [Number_Route_Sheet] as 'Номер маршрутного листа' from [dbo].[Application]
inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID]
inner join [dbo].[Route_Sheet] on [ID_Route_Sheet] = [Route_Sheet_ID]
go

select [Number_Application] as 'Номер заявки', [Date_Application] as 'Дата', [Time_Application] as 'Время', [Status_Application] as 'Статус',
[Name_Customer]+' '+[Surname_Customer]+' '+[Lastname_Customer] as 'ФИО покупателя', [Number_Route_Sheet] as 'Номер маршрутного листа' from [dbo].[Application]
inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID]
inner join [dbo].[Route_Sheet] on [ID_Route_Sheet] = [Route_Sheet_ID]
where Convert(int, [Number_Application]) >= 3
go

create table [dbo].[Points_Route_Sheet]
(
	[ID_Point_Route_Sheet] [int] not null identity(1,1),
	[Delivery_Point_ID] [int] not null,
	[Route_Sheet_ID] [int] not null
	constraint [PK_Point_Route_Sheet] primary key clustered
	([ID_Point_Route_Sheet] ASC) on [PRIMARY],
	constraint [FK_Delivery_Point] foreign key ([Delivery_Point_ID])
	references [dbo].[Delivery_Points] ([ID_Delivery_Point]),
	constraint [FK_Route_Sheet] foreign key ([Route_Sheet_ID])
	references [dbo].[Route_Sheet] ([ID_Route_Sheet]),
)
go

insert into [dbo].[Points_Route_Sheet] ([Delivery_Point_ID], [Route_Sheet_ID])
values (1,1), (2,1), (1,2), (4,2), (1,3), (1,4), (3,4), (1,5), (5,5)
go

select [Delivery_Address] as 'Адрес доставки', [Number_Route_Sheet] as 'Номер маршрутного листа' from [dbo].[Points_Route_Sheet]
inner join [dbo].[Delivery_Points] on [ID_Delivery_Point] = [Delivery_Point_ID]
inner join [dbo].[Route_Sheet] on [ID_Route_Sheet] = [Route_Sheet_ID]
order by [Number_Route_Sheet] ASC
go

create table [dbo].[Post_Customer]
(
	[ID_Post_Customer] [int] not null identity(1,1),
	[Post_ID] [int] not null,
	[Customer_ID] [int] not null
	constraint [PK_Post_Customer] primary key clustered
	([ID_Post_Customer] ASC) on [PRIMARY],
	constraint [FK_Post] foreign key ([Post_ID])
	references [dbo].[Post] ([ID_Post]),
	constraint [FK_Customer] foreign key ([Customer_ID])
	references [dbo].[Customer] ([ID_Customer]),
)
go

insert into [dbo].[Post_Customer] ([Post_ID], [Customer_ID])
values (2,1), (2,2), (4,3), (3,4), (4,5)
go

select [Name_Customer]+' '+[Surname_Customer]+' '+[Lastname_Customer] as 'ФИО заказчика', [Name_Post] as 'Должность' from [dbo].[Post_Customer]
inner join [dbo].[Customer] on [ID_Customer] = [Customer_ID]
inner join [dbo].[Post] on [ID_Post] = [Post_ID]
go

create table [dbo].[Country_Trasnport]
(
	[ID_Country_Transport] [int] not null identity(1,1),
	[Country_ID] [int] not null,
	[Transport_ID] [int] not null
	constraint [PK_Country_Transport] primary key clustered
	([ID_Country_Transport] ASC) on [PRIMARY],
	constraint [FK_Country] foreign key ([Country_ID])
	references [dbo].[Country] ([ID_Country]),
	constraint [FK_Transport] foreign key ([Transport_ID])
	references [dbo].[Transport] ([ID_Transport]),
)
go

insert into [dbo].[Country_Trasnport] ([Country_ID], [Transport_ID])
values (1,1), (1,2), (2,3), (2,4), (2,5)
go

select [Number_Transport] as 'Номер', [Name_Country] as 'Страна' from [dbo].[Country_Trasnport]
inner join [dbo].[Country] on [ID_Country] = [Country_ID]
inner join [dbo].[Transport] on [ID_Transport] = [Transport_ID]
go

create table [dbo].[Post_Employee]
(
	[ID_Post_Employee] [int] not null identity(1,1),
	[Post_ID] [int] not null,
	[Employee_ID] [int] not null
	constraint [PK_Post_Employee] primary key clustered
	([ID_Post_Employee] ASC) on [PRIMARY],
	constraint [FK_Post_Employee] foreign key ([Post_ID])
	references [dbo].[Post] ([ID_Post]),
	constraint [FK_Employee] foreign key ([Employee_ID])
	references [dbo].[Employee] ([ID_Employee]),
)
go

insert into [dbo].[Post_Employee] ([Employee_ID], [Post_ID])
values (1,1),(2,1),(3,1),(4,1),(5,1)
go

select [Name_Employee]+' '+[Surname_Employee]+' '+[Lastname_Employee] as 'ФИО сотрудника', [Login_Employee] as 'Логин', [Name_Post] as 'Должность' from [dbo].[Post_Employee]
inner join [dbo].[Employee] on [ID_Employee] = [Employee_ID]
inner join [dbo].[Post] on [ID_Post] = [Post_ID]
go

create table [dbo].[Cargo_Application]
(
	[ID_Cargo_Application] [int] not null identity(1,1),
	[Cargo_ID] [int] not null,
	[Application_ID] [int] null
	constraint [PK_Cargo_Application] primary key clustered
	([ID_Cargo_Application] ASC) on [PRIMARY],
	constraint [FK_Cargo] foreign key ([Cargo_ID])
	references [dbo].[Cargo] ([ID_Cargo]),
	constraint [FK_Application] foreign key ([Application_ID])
	references [dbo].[Application] ([ID_Application])
)
go

insert into [dbo].[Cargo_Application] ([Cargo_ID], [Application_ID])
values (1,1), (2,2), (3,3), (4,4), (5,5)
go

select [Description_Cargo] as 'Название товара', [Number_Application] as 'Номер заявки' from [dbo].[Cargo_Application]
inner join [dbo].[Cargo] on [ID_Cargo] = [Cargo_ID]
inner join [dbo].[Application] on [ID_Application] = [Application_ID]
go

create table [dbo].[Employee_License]
(
	[ID_Employee_License] [int] not null identity(1,1),
	[Employee_ID] [int] not null,
	[License_ID] [int] null
	constraint [PK_Employee_License] primary key clustered
	([ID_Employee_License] ASC) on [PRIMARY],
	constraint [FK_Employee_License] foreign key ([Employee_ID])
	references [dbo].[Employee] ([ID_Employee]),
	constraint [FK_License] foreign key ([License_ID])
	references [dbo].[License] ([ID_License])
)
go

insert into [dbo].[Employee_License] ([Employee_ID], [License_ID])
values (1,1), (2,1), (3,1), (3,2), (4,1),(5,1)
go

select [Name_Employee]+' '+[Surname_Employee]+' '+[Lastname_Employee] as 'ФИО', [Login_Employee] as 'Логин',
[Type_License] as 'Категория прав' from [dbo].[Employee_License]
inner join [dbo].[Employee] on [ID_Employee] = [Employee_ID]
inner join [dbo].[License] on [ID_License] = [License_ID]
go
