use BookstoreDb;

----Create table for book -----

create table Book(
BookId int identity(1,1) not null primary key,
BookName varchar(270) not null,
AuthorName varchar(200) not null,
Rating  varchar(10) not null,
RatingCount int ,
DiscountPrice varchar(10) not null,
ActualPrice varchar(10) not null,
Description varchar(max) not null,
BookImage varchar(250),
BookQuantity int not null
)

select * from Book


--------Adding book--------
create procedure AddBook
(
@BookName varchar(270),
@AuthorName varchar(200),
@Rating varchar(10),
@RatingCount int,
@DiscountPrice varchar(10),
@ActualPrice varchar(10),
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
	insert into Book(BookName,AuthorName,Rating,RatingCount,DiscountPrice,ActualPrice,Description,BookImage,BookQuantity)
	values(@BookName,@AuthorName,@Rating,@RatingCount,@DiscountPrice,@ActualPrice,@Description,@BookImage,@BookQuantity);
end;


----------------Updating Book-----------------

create procedure UpdateBook
(
@BookId int,
@BookName varchar(270),
@AuthorName varchar(200),
@Rating varchar(10),
@RatingCount int,
@DiscountPrice varchar(10),
@ActualPrice varchar(10),
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
update Book set 
BookName=@BookName,
AuthorName=@AuthorName,
Rating=@Rating,
RatingCount=@RatingCount,
DiscountPrice=@DiscountPrice,
ActualPrice=@ActualPrice,
Description=@Description,
BookImage=@BookImage,
BookQuantity=@BookQuantity
where BookId=@BookId			
end;


--------------Delete Book---------
create procedure DeleteBook
(
@BookId int
)
as
begin
delete from Book Where BookId=@BookId
end;


------------ Get Book by Id ----------------
Create procedure GetBookById
(
@BookId int
)
as 
begin
select * from Book where BookId=@BookId
end;

-------------- Get all book ----------------
Create procedure GetAllBooks
as 
begin
select * from Book
end;