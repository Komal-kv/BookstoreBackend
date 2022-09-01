use BookstoreDb

create table Cart
(
CartId int identity(1,1) primary key,
Book_Quantity int default 1,
UserId int not null foreign key (UserId) references Users(UserId),
BookId int not null Foreign key (BookId) references Book(BookId)
)

select * from Cart

---------------- For adding the cart ---------
Create procedure SpAddCart
( 
  @BookQuantity int,
  @UserId int,
  @BookId int
)
As
Begin
	insert into cart(Book_Quantity,UserId,BookId)
	values ( @BookQuantity,@UserId, @BookId);
End


-------------- For remove cart ----------
Create Procedure SpRemoveCart
(
@CartId int
)
As
Begin
	Delete from Cart where CartId = @CartId;
End


------------------- For getting books from Cart --------------
create proc SpGetAllBookInCart
(
@UserId int
)
AS
Begin
	select
		CartId,
		c.BookId,
		UserId,
		BookQuantity,
		BookName,
		BookImage,
		AuthorName,
		DiscountPrice,
		ActualPrice
		from Cart c
		join Book b
		on c.BookId = b.BookId
		where UserId = @UserId;
end


---------- For updating BookQuantity in cart --------
create procedure SpUpdateCart
(
	@BookQuantity int,
	@BookId int,
	@UserId int,
	@CartId int
)
as
begin
update Cart set BookId=@BookId,
				UserId=@UserId,
				Book_Quantity=@BookQuantity
				where CartId=@CartId;
end