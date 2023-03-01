 
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

GO
create or alter procedure sp_guardarCliente
@id int,
@nombre  varchar(50)
as
begin
	declare @MsgError nvarchar(MAX)='';
	begin try
if exists(select 1 from cliente where Id=@id)
begin

	update cliente set nombre=@nombre where id=@id;
	set @MsgError='Registro actualizado';
end	else
      begin 
	insert into cliente(nombre) values(@nombre);
	set @MsgError='Registro agregado'
    end

	end try
	begin catch
	set @MsgError=ERROR_MESSAGE();
	RAISERROR(@MsgError,16,227);
	end catch
end

go
create or alter procedure sp_eliminarCliente
@id int
as
begin 
 delete cliente where id=@id;
end;


select * from cliente; 

exec sp_guardarCliente @nombre='r123'