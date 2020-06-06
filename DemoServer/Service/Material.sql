use 物资调配;

create table Users(    --建立用户表
   PhoneNumber varchar(20) primary key,
   Passwords varchar(20) not null,
   UserID int not null,
   HomeAddress varchar(20),
  -- UserType varchar(20) constraint usertypes check (UserType = 'NORMAL' or 
    --       UserType = 'ADMIN' or UserType = 'DELIVERER')
);


create table Materials(   --建立物资表
   
	MaterialID int primary key,
    MaterialName varchar(20) unique,

	MaterialQuantity int,
	MaterialDescription varchar(80),
	MaterialConstraint varchar(80)
);


create table Donation(    --建立捐赠表
    DonateID int primary key,
    DonateGUID varchar(10) unique,
	DonatorName varchar(20),
	DonatorAddress varchar(20),
	MaterialName varchar(20),
	MaterialQuantity int,
	DonationState varchar(20) constraint donationstates check ( DonationState = 'Aborted' or DonationState = 'Applying' 
	     or DonationState = 'WaitingDelivery' or DonationState = 'Done'),
    StateIndex int,
    StartTime varchar(15),
	--foreign key(DonatorAddress) references Users(HomeAddress) on delete cascade,
    foreign key(MaterialName) references Materials(MaterialName) on delete cascade,
	foreign key(DonatorName) references Users(PhoneNumber) on delete cascade

    );

create table Applications(         --建立申请表
  ApplyID int primary key,
  ApplyGUID varchar(10) unique,
  ApplierId int,
	--ApplierName varchar(20),
	ApplierAddress varchar(20),
	MaterialName varchar(20),
	MaterialQuantity int,
	ApplicationState varchar(20) constraint applicationstates check (ApplicationState = 'Aborted' or ApplicationState = 'Applying' 
	     or ApplicationState = 'Delivering' or ApplicationState = 'Received' or ApplicationState = 'Done'),
		 StateIndex int,
   StartTime varchar(15),
  -- ApplierAddress varchar(30),

  -- foreign key(MaterialName) references Materials(MaterialName) on delete cascade,
	--foreign key(ApplierName) references Users(PhoneNumber) on delete cascade
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

insert into Materials 
values(30000,'84消毒液(500mL)',0,'84消毒液主要用于各种物体表面和环境的消毒，可有效杀灭病菌',' ');

insert into Materials 
values(30001,'医用酒精(500mL)',0,'医用酒精成份主要是乙醇，主要用于日常消毒，使病毒蛋白质失活，防止病毒扩散',' ');

insert into Materials
values(30002,'普通医用口罩',0,'普通医用口罩可以有效遮挡细菌、病毒等致病微生物的感染',' ');

insert into Materials
values(30003,'KN95口罩',0,'KN95口罩比普通医用口罩标准更加严格，对疫情防控更有效',' ');

insert into Materials
values(30004,'护目镜',0,'长期接触易感人群者应佩戴护目镜，防止病菌进入眼睛',' ');
