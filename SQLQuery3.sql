 
create database ERPCliente;
use ERPCliente;
 
create table Cliente(
Id int primary key identity (1,1),
Nombre varchar(50)
);

insert into Cliente values('Pepe 1');
insert into Cliente values('Pepe 2');
select * from Cliente;
create or alter procedure sp_obtenerCliente
as
begin
	select * from  cliente;
end;

go
create or alter procedure sp_obtenerCliente2
as
begin
	select * from  cliente;
end;
go
create or alter procedure sp_obtenerCliente3
as
begin
	select * from  cliente;
end;