Create database BookstoreDb;

use BookstoreDb;

Create Table Users(
	UserId int identity (1,1) primary key,
	FullName varchar(200) not null,
	Email varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber bigint
);

select * from Users

------UserRegistration--------

Create procedure spRegister     
(        
    @FullName VARCHAR(200),         
    @Email varchar(255),               
    @Password VARCHAR(255),
	@MobileNumber bigint       
) 
as         
Begin try
    Insert into Users(FullName, Email, Password, MobileNumber)         
    Values (@FullName ,@Email ,@Password, @MobileNumber);
End try
begin catch 
SELECT 
ERROR_NUMBER() as ErrorNumber,
ERROR_STATE() as ErrorState,
ERROR_PROCEDURE() as ErrorProcedure,
ERROR_LINE() as ErrorLine,
ERROR_MESSAGE() as ErrorMessage;
end catch



-------Login-------------

Create procedure spAddUserLogIn
(
	@Email varchar(200),
	@Password varchar(250)
)
as         
Begin         
    Select * from Users where Email = @Email and Password =@Password 
End

exec spAddUserLogIn 'divyavairagade@gmail.com' , 'Komal123'


-------------------- Forget Password ---------------------------

Create procedure spUserForgetPasswrd
(
	@Email varchar(200)
)
as         
Begin         
    Select * from Users where Email = @Email
End


--------------------- Reset Password ---------------------------

Create procedure spUserResetPaswrd
(
	@Email varchar(200),
	@Password varchar(250)
)
as         
Begin         
    Update Users
	set Password = @Password where Email = @Email
End

