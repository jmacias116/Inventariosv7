create database DBVENTAMVC

go

use DBVENTAMVC

go

create table Venta(
id int primary key identity(1,1),
totalimporte decimal(10,0),
totaldescuento decimal(10,0),
total decimal(10,0),
usuario int,
usuariomod int,
estado int, --0:activo, 1:anulada
fechaReg datetime default getdate()
)

create table Producto(
id int primary key identity(1,1),
descripcion varchar(70),
stock int,
cantidad int,
precio decimal(10,0),
fechaReg datetime default getdate(),
fechaMod datetime default getdate(),
usuario int,
usuarioMod int,
estado int, --0:activo, 1:anulado
)

