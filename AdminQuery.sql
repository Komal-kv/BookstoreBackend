use BookstoreDb

Create Table Admin
(
AdminId int identity(1,1) primary key,
FullName varchar (200) not null,
Email varchar (200) NOT NULL,
Password varchar (250) NOT NULL,
MobileNumber BigInt NOT NULL,
)
Insert into Admin 
values('Komal Vairagade','komalvairagade@gmail.com','Komal@123','9075772585');

select * from Admin


---------------- Store procedure for Admin login --------------
create procedure SpAdminLogin
(
	@Email varchar(200),
	@Password varchar(250)
)
as
begin
	select * from Admin where Email=@Email and Password=@Password; 
end

exec SpAdminLogin 'komalvairagade@gmail.com' , 'Komal@123'



