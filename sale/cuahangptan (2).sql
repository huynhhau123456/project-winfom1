--create database Store
--use Store
CREATE TABLE Branch(
	BranchID smallint primary key identity(1,1),
	BranchName nvarchar(50),
	BranchAddress nvarchar(100),
	ContactPhone char(30)
)
select * from Branch
INSERT INTO Branch (BranchName, BranchAddress, ContactPhone) VALUES (N'Thành phố Hồ Chí Minh', N'680, Đường Trường Chinh, Phường 15, Quận Tân Bình, Tây Thạnh', '1234567890');
INSERT INTO Branch (BranchName, BranchAddress, ContactPhone) VALUES (N'Hà Nội', N'192 P. Hào Nam, Chợ Dừa, Đống Đa, Hà Nội, Việt Nam', '1234567890');
INSERT INTO Branch (BranchName, BranchAddress, ContactPhone) VALUES (N'Hải Phòng', N'12 Lạch Tray, Street, Ngô Quyền, Hải Phòng 180000, Việt Nam', '1234567890');

CREATE TABLE Users (
    UserName varchar(50) NOT NULL,
    Password varchar(50) NOT NULL,
    UserType varchar(50) NOT NULL,
    BranchID smallint NULL foreign key references Branch(BranchID) ,
    CONSTRAINT PK_Users PRIMARY KEY (UserName),
    CONSTRAINT CK_Users_UserType CHECK (UserType IN ('Employee', 'Manager', 'Customer')),
    CONSTRAINT CK_Users_BranchID CHECK ( -- CHECK để đảm bảo rằng chỉ có UserType là "Nhân viên" hoặc "Quản lý"
        (UserType IN ('Employee', 'Manager') AND BranchID IS NOT NULL)--mới có thể có giá trị không null cho trường BranchID,
        OR (UserType = 'Customer' AND BranchID IS NULL)   --trong khi UserType là "Khách hàng" thì BranchID phải là null
    )
);
--select * from Users
-- Thêm người dùng mới là khách hàng
INSERT INTO Users (UserName, Password, UserType)
VALUES ('khachhang1', 'matkhau1', 'Customer');


INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Admin1', 'Admin1', 'Manager', 1);
INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Admin2', 'Admin2', 'Manager', 2);
INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Admin3', 'Admin3', 'Manager', 3);

INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Emp1', 'Emp1', 'Employee', 1);
INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Emp2', 'Emp2', 'Employee', 2);
INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES ('Emp3', 'Emp3', 'Employee', 3);



CREATE TABLE Category(
	CategoryID smallint primary key identity(1,1),
	CategoryName nvarchar(50),
	Description nvarchar(50)
)
INSERT INTO Category (CategoryName, Description) VALUES (N'Quần áo nam', N'Các sản phẩm liên quan đến quần áo nam')
INSERT INTO Category (CategoryName, Description) VALUES (N'Quần áo nữ', N'Các sản phẩm liên quan đến quần áo nữ')
INSERT INTO Category (CategoryName, Description) VALUES (N'Thực phẩm', N'Các sản phẩm ăn uống')
INSERT INTO Category (CategoryName, Description) VALUES (N'Điện thoại', N'Các sản phẩm liên quan đến điện thoại')
INSERT INTO Category (CategoryName, Description) VALUES (N'Đồ bếp', N'Các sản phẩm liên quan đến nấu ăn')
INSERT INTO Category (CategoryName, Description) VALUES (N'Vật dụng gia dùng', N'Các sản phẩm liên quan đến dọn dẹp nhà cửa')
INSERT INTO Category (CategoryName, Description) VALUES (N'PC', N'Các sản phẩm liên quan đến máy tính bàn')
INSERT INTO Category (CategoryName, Description) VALUES (N'Laptop', N'Các sản phẩm liên quan đến laptop')
INSERT INTO Category (CategoryName, Description) VALUES (N'Linh kiện điện tử', N'Các sản phẩm liên quan đến linh kiện điện tử')
INSERT INTO Category (CategoryName, Description) VALUES (N'Dụng cụ học tập', N'Các sản phẩm về đồ dùng học tập')


CREATE TABLE Product(
	ProductID smallint primary key identity(1,1),
	CategoryID smallint,
	ProductName nvarchar(50),
	Description nvarchar(200),
	Quantity smallint,
	UnitPrice money,
	BranchID smallint foreign key references Branch(BranchID),
	Anh image null,
)
--select * from Product
insert into Product(CategoryID,ProductName,Description,Quantity,UnitPrice,BranchID)values(1,'quan ao nam poly','sddsdsdaw',14,15000,1)
insert into Product(CategoryID,ProductName,Description,Quantity,UnitPrice,BranchID)values(1,'quan ao nam poly','sddsdsdaw',15,15000,2)
insert into Product(CategoryID,ProductName,Description,Quantity,UnitPrice,BranchID)values(1,'quan ao nam poly1','sddsdsdaw',15,15000,1)
ALTER TABLE Product ADD CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID);


CREATE TABLE ProductLsocation(
	LocationID smallint primary key identity(1,1), --ID địa điểm kho hàng
	Address nvarchar(100), --Địa chỉ kho hàng
	Availability smallint --Số lượng nhân viên đang làm việc tại đấy
);
INSERT INTO ProductLsocation VALUES (N'Công ty TNHH Thiên Phúc Trần, số 535 đường Bà Hạt, phường 8, quận 10', 40)
INSERT INTO ProductLsocation VALUES (N'Ngọc Sương – Cửa hàng tạp hóa, số 341/67a Lạc Long Quân, phường 5, quận 11', 30)
INSERT INTO ProductLsocation VALUES (N'H Phone, số 32b đường Hùng Vương, phường 9, quận 5', 15)
INSERT INTO ProductLsocation VALUES (N'Cafe Mộc Miên, số 24 đường Trần Mai Ninh, phường 12, quận  Tân Bình', 34)
INSERT INTO ProductLsocation VALUES (N'Nhà sách Khuông Việt, số 313 Khuông Việt, phường Phú Trung, quận Tân Phú', 24)
INSERT INTO ProductLsocation VALUES (N'Cửa hàng tiện lợi Start life, số 41F/25 đường Đặng Thùy Trâm, phường 13, quận Bình Thạnh', 37)
INSERT INTO ProductLsocation VALUES (N'A75 Bạch Đằng, Phường 2, Tân Bình, Thành phố Hồ Chí Minh', 40)
INSERT INTO ProductLsocation VALUES (N'Ngõ 9, đường Nguyễn Văn Linh, Phường Gia Thụy, Quận Long Biên, Hà Nội.', 40)
INSERT INTO ProductLsocation VALUES (N'367/F370 Đường Bạch Đằng, Phường 2, Quận Tân Bình, TP.HCM', 40)
INSERT INTO ProductLsocation VALUES (N'Số 162/2 Quốc Lộ 1A, Phường Thạnh Xuân, Quận 12, TP.HCM', 40)



CREATE TABLE ProductInventory(
	ProductID smallint,
	LocationID smallint,
	Shelf smallint,
	Bin smallint,
	Quantity smallint,
	ModifiedDate datetime default getdate()
)
select * from ProductInventory
select * from Product
insert into ProductInventory(ProductID,LocationID,Shelf,Bin,Quantity)values(1,1,1,1,30)
insert into ProductInventory(ProductID,LocationID,Shelf,Bin,Quantity)values(3,2,1,1,30)
insert into ProductInventory(ProductID,LocationID,Shelf,Bin,Quantity)values(2,2,1,1,30)
-- INSERT statement
INSERT INTO Product (Quantity)
VALUES (2);

-- UPDATE statement
UPDATE ProductInventory
SET Quantity =  30
WHERE ProductID = 2;

UPDATE Product
SET Quantity = Quantity + 2
WHERE ProductID = 1;
ALTER TABLE ProductInventory ADD CONSTRAINT FK_Product_ID FOREIGN KEY (ProductID) REFERENCES  Product(ProductID)
ALTER TABLE ProductInventory ADD CONSTRAINT FK_Location_ID FOREIGN KEY (LocationID) REFERENCES ProductLsocation(LocationID)

CREATE TABLE Orders(
	OrderID smallint primary key identity(1,1),
	ProductName nvarchar(50),
	CustomerName nvarchar(50),
	Quantity smallint,
	UnitPrice money,
	LineTotal money,
	DateOrdered datetime default getdate(),
	OrderStatus nvarchar(50),
	BranchID smallint foreign key references Branch(BranchID),
	Anh image null,
)
select * from Product
select * from Orders
INSERT INTO Orders (ProductName,CustomerName,Quantity,UnitPrice,LineTotal,OrderStatus,BranchID) VALUES ('quan ao nam poly1','wew',4,15000.00,15000.00,'da mua',2)
--INSERT INTO Orders (CustomerName, LineTotal, DateOrdered, OrderStatus, BranchID) VALUES ('Customer 1', 100.5, '2023-04-10', 'Pending', 1);
--INSERT INTO Orders (CustomerName, LineTotal, DateOrdered, OrderStatus, BranchID) VALUES ('Customer 1', 100.5, '2023-04-10', 'Pending', 2);


CREATE TABLE Customer(
	CustomerID smallint primary key identity(1,1),
	CustomerName nvarchar(50),
	Address nvarchar(50),
	PhoneNumber char(10),
	Balance money,
	Email char(10)
)

--INSERT INTO Customer (CustomerName, Address, PhoneNumber, Email) VALUES ('Customer 1', 'Address 1', '1234567890', 'customer1@example.com');


CREATE TABLE WorkTime(
	WorkTimeID smallint primary key identity(1,1),
	Description nvarchar(30),
	TimeWork nvarchar(30)
)
select * from WorkTime
INSERT INTO WorkTime (Description, TimeWork) VALUES ('Full time', '8 hours/day');
INSERT INTO WorkTime (Description, TimeWork) VALUES ('Full time1', '8 hours/day');
CREATE TABLE Employee(
	EmployeeID smallint primary key identity(1,1),
	CCCD char(10),
	EmployeeName nvarchar(50), 
	Address nvarchar(50),
	PhoneNumber char(50),
	Email char(50),
	Sexual varchar(1), 
	WorkTimeID smallint foreign key references WorkTime(WorkTimeID),
	Job nvarchar(50),
	BranchID smallint foreign key references Branch(BranchID), 
)
select * from Employee
INSERT INTO Employee (CCCD, EmployeeName, BranchID, Address, PhoneNumber, Email, Sexual, WorkTimeID, Job) VALUES ('1234567890', 'Employee 1', 1, 'Address 1', '1234567890', 'employee1@example.com', 'M', 1, 'Manager');
INSERT INTO Employee (CCCD, EmployeeName, BranchID, Address, PhoneNumber, Email, Sexual, WorkTimeID, Job) VALUES ('1234567890', 'Employee 2', 2, 'Address 2', '1234567890', 'employee1@example.com', 'M', 1, 'Manager');


CREATE TABLE OrderHistory( --AUTOMATIC
	OrderHistoryID smallint primary key identity(1,1),
	EmployeeID smallint,
	Activity nvarchar(50),
	DateActive datetime default getdate(),
	Description nvarchar(50),
	CONSTRAINT FK_OrderHistory_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
)
--INSERT INTO OrderHistory (EmployeeID, Activity, Description) VALUES (1, 'Created Order', 'Created new order for customer 1');

--Bảng "Branch": Lưu trữ thông tin về các chi nhánh của công ty, bao gồm tên chi nhánh, địa chỉ và số điện thoại liên lạc.

--Bảng "Users": Lưu trữ thông tin về người dùng của hệ thống, bao gồm tên đăng nhập, mật khẩu, loại người dùng và ID chi nhánh tương ứng.

--Bảng "Category": Lưu trữ thông tin về các danh mục sản phẩm, bao gồm tên danh mục và mô tả.

--Bảng "Product": Lưu trữ thông tin về các sản phẩm, bao gồm tên sản phẩm, mô tả, số lượng, giá bán và ID chi nhánh và ID danh mục tương ứng.

--Bảng "Orders": Lưu trữ thông tin về các đơn hàng, bao gồm tên khách hàng, tổng giá trị đơn hàng, ngày đặt hàng, trạng thái đơn hàng và ID chi nhánh tương ứng.

--Bảng "Customer": Lưu trữ thông tin về khách hàng, bao gồm tên khách hàng, địa chỉ, số điện thoại và địa chỉ email.

--Bảng "OrderDetail": Lưu trữ thông tin về chi tiết các đơn hàng, bao gồm ID sản phẩm, ID đơn hàng, số lượng và giá bán.

--Bảng "WorkTime": Lưu trữ thông tin về các thời gian làm việc, bao gồm mô tả và số giờ làm việc.

--Bảng "Employee": Lưu trữ thông tin về nhân viên, bao gồm số CCCD, tên nhân viên, ID chi nhánh, địa chỉ, số điện thoại, địa chỉ email, giới tính, ID thời gian làm việc và chức vụ.

--Bảng "OrderHistory": Lưu trữ thông tin về lịch sử đơn hàng, bao gồm ID nhân viên, hoạt động thực hiện, mô tả và ngày thực hiện.