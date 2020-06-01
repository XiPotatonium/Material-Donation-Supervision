use 物资调配;

create table Users(    --建立用户表
   PhoneNumber varchar(20) primary key,
   Passwords varchar(20) not null,
   HomeAddress varchar(20) not null,
   UserType varchar(20) constraint usertypes check (UserType = 'NORMAL' or 
           UserType = 'ADMIN' or UserType = 'DELIVERER')
);


create table Materials(   --建立物资表
    --MaterialGUID varchar(10) primary key,
    MaterialName varchar(20) primary key,
	MaterialQuantity int,
	MaterialDescription varchar(50)  
);


create table DonationOrApplication(    --建立捐赠表
    DonateGUID varchar(10) primary key,
	DonatorName varchar(20),
	DonatorAddress varchar(20),
	MaterialName varchar(20),
	MaterialQuantity int,
	DonationState varchar(20) constraint donationstates check ( DonationState = 'Aborted' or DonationState = 'Applying' 
	     or DonationState = 'WaitingDelivery' or DonationState = 'Done'),
    StartTime varchar(15),
	--foreign key(DonatorAddress) references Users(HomeAddress) on delete cascade,
    foreign key(MaterialName) references Materials(MaterialName) on delete cascade,
	foreign key(DonatorName) references Users(PhoneNumber) on delete cascade

    );

create table Applications(         --建立申请表
  ApplyGUID varchar(10) primary key,
	ApplierName varchar(20),
	ApplierAddress varchar(20),
	MaterialName varchar(20),
	MaterialQuantity int,
	ApplicationState varchar(20) constraint applicationstates check (ApplicationState = 'Aborted' or ApplicationState = 'Applying' 
	     or ApplicationState = 'Delivering' or ApplicationState = 'Received' or ApplicationState = 'Done'),
   StartTime varchar(15),

   foreign key(MaterialName) references Materials(MaterialName) on delete cascade,
	foreign key(ApplierName) references Users(PhoneNumber) on delete cascade
);



create table Delivery(                 --建立快递表
    DeliveryGUID varchar(10) primary key,
	DeliveryerName varchar(20),

	DeliveryState varchar(15) not null constraint donatestates check (DeliveryState = 'Waiting' or DeliveryState = 'Processing'
                or DeliveryState = 'Finished'),
    
	DeliverDeparture varchar(15),
	DeliverDestination varchar(15),
    DeliverStartTime varchar(15),
	DeliverFinishTime varchar(15),

 --   foreign key(DeliveryGUID) references DonationOrApplication(DonateOrApplyGUID) on delete cascade,
	foreign key(DeliveryerName) references Users(PhoneNumber) on delete cascade,

);







--create table Applicants(
 --  names varchar(15) primary key,
 --  passwords varchar(15) not null,
 --  phoneNumber varchar(20) not null,
 --  addresses varchar(30)  not null,
 --  loginState varchar(10) not null constraint states check (loginState = 'online' or loginState = 'offline')
--);

--create table Donors(
--   names varchar(15) primary key,
--   passwords varchar(15) not null,
--   loginState varchar(10) not null constraint states check (loginState = 'online' or loginState = 'offline')

--);
--create table Admins(
--  names varchar(15) primary key,
--  passwords varchar(15) not null,
--  loginState varchar(10) not null constraint states check (loginState = 'online' or loginState = 'offline')


--);
--create table Distributors(
--   names varchar(15) primary key,
--   passwords varchar(15) not null,
--   phoneNumber varchar(20) not null,
--   loginState varchar(10) not null constraint states check (loginState = 'online' or loginState = 'offline')


--);

--create table Donate(
  -- MaterialGUID varchar(10) primary key,
--  MaterialName varchar(20),
--  -- number varchar(10),
--   materialCount int,

--   StartID varchar(15),
   

--   Departure varchar(20) not null,
--   Destination varchar(20) not null,
--   DonateState varchar(15) not null constraint donatestates check (DonateState = 'Waiting' or DonateState = 'Processing'
--                or DonateState = 'Finished'),
--   ApplicationState varchar(15) not null constraint applicatonstates check (ApplicationState = 'Aborted' or ApplicationState = 'Applying' 
--            or ApplicationState = 'Delivering' or ApplicationState = 'Received' or ApplicationState = 'Done'),
   
--   StartTime varchar(20),
--   FinishTime varchar(20),

 
--   foreign key(MaterialName) references Donors(names) on delete cascade,
--   foreign key(MaterialName) references Materials(MaterialName) on delete cascade



--);

--create table Applys(
--   names varchar(15) primary key,
--   number varchar(10),
--   materialCount int,
--   foreign key(names) references Applicants(names) on delete cascade,
--   foreign key(number) references Materials(number) on delete cascade



--);

--create table Distribute(
   --name1 varchar(15) not null ,
   --name2 varchar(15) not null,
   --number varchar(10),
  -- DistributeState varchar(20),
   --foreign key(name1) references Distributors(names) on delete cascade,
  -- foreign key(name2) references Applicants(names) on delete cascade,
  -- foreign key(number) references Materials(number) on delete cascade



--);
--grant all previleges on Users
--to 
--grant all privileges
--on Materials
--to admins;
--grant all privileges
--on Applicants
--to admins;
--grant all privileges
--on Donors
--to admins;
--grant all privileges
--on Distributors
--to admins;
--grant all privileges 
--on Donate
--to admins;
--grant all privileges 
--on Applys
--to admins;
--grant all privileges 
--on Distribute
--to admins;


