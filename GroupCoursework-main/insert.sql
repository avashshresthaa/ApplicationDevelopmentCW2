use RopeyDVD;

insert into DVDCategory(CategoryDescription, AgeRestricted) values('Action', 0);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Horror', 1);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Crime', 1);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Comedy', 0);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Biography', 0);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Adult', 1);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Friction', 0);
insert into DVDCategory(CategoryDescription, AgeRestricted) values('Adventure', 0);


insert into Actor(ActorSurname, ActorFirstName) values('Evans', 'Chris');
insert into Actor(ActorSurname, ActorFirstName) values('Downey', 'Robert');
insert into Actor(ActorSurname, ActorFirstName) values('Hemsworth', 'Chris');
insert into Actor(ActorSurname, ActorFirstName) values('Johansson', 'Scarlett');
insert into Actor(ActorSurname, ActorFirstName) values('Holland', 'Tom');
insert into Actor(ActorSurname, ActorFirstName) values('Pattinson', 'Robert');
insert into Actor(ActorSurname, ActorFirstName) values('Hang Rai', 'Daya');
insert into Actor(ActorSurname, ActorFirstName) values('Malla', 'Saugat');
insert into Actor(ActorSurname, ActorFirstName) values('Maguire', 'Tobey');


insert into Studio(StudioName) values('Marvel Studios');
insert into Studio(StudioName) values('Disney');
insert into Studio(StudioName) values('Sony');
insert into Studio(StudioName) values('DC Comics');
insert into Studio(StudioName) values('Digital Cinema Nepal Pvt. Ltd.');


insert into Producers(ProducerName) values('Kevin Feige');
insert into Producers(ProducerName) values('Matt Reeves');
insert into Producers(ProducerName) values('Nischal Basnet');
insert into Producers(ProducerName) values('Laura Ziskin');


insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('The Batman', '2022-03-04', 200, 400, 1, 4, 2); 
insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('IronMan', '2008-05-02', 50,  100, 1, 1, 1); 
insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('Kabaddi Kabaddi', '2015-11-27', 75,  150, 4, 5, 3); 
insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('Spiderman',     '2002-05-03', 100, 200, 8, 3, 4);  
insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('Avengers: Endgame', '2019-04-26', 550, 850, 1, 1, 1);  
insert into DVDTitle(DvdTitle, DateReleased, StandardCharge, PenaltyCharge, CategoryNumber, StudioNumber, ProducerNumber) values('Avengers: Infinity War', '2018-04-26', 550, 850, 1, 1, 1);  


insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2022-02-14', 1, 1);
insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2021-03-15', 2, 3);
insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2020-01-01', 3, 5);
insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2021-04-21', 4, 3);
insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2022-05-22', 5, 4);
insert into DVDCopy(DatePurchased, DVDNumber, Stock) values('2020-05-22', 6, 3);

insert into LoanType(Loan_Type, LoanDuration) values('Daily', 1);
insert into LoanType(Loan_Type, LoanDuration) values('Weekly', 7);
insert into LoanType(Loan_Type, LoanDuration) values('Monthly', 30);


insert into MembershipCategory(MembershipCategoryDescription, MembershipCategoryTotalLoans) values('Normal', 4);
insert into MembershipCategory(MembershipCategoryDescription, MembershipCategoryTotalLoans) values('Premium', 8);
insert into MembershipCategory(MembershipCategoryDescription, MembershipCategoryTotalLoans) values('VIP', 12);

insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Manandhar', 'Manav', 'Kathmandu',   '2001-03-09', 2);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Shrestha', 'Avash', 'Kathmandu',    '2001-03-19', 3);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Shakya', 'Nischal', 'Kathmandu', '2001-12-01', 1);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Giri', 'Kushal', 'Kathmandu', '2001-10-11', 1);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Shrestha', 'Bibhas', 'Kathmandu',    '1997-07-16', 1);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('KC', 'Suresh', 'Banepa',    '2000-02-01', 3);
insert into Members(MembershipLastName, MembershipFirstName, MembershipAddress, MemberDOB, MembershipCategoryNumber) values('Thapa', 'Priyanka', 'Pokhara',     '2001-08-05', 3);

insert into CastMember(DVDNumber, ActorNumber) values(1, 6);
insert into CastMember(DVDNumber, ActorNumber) values(2, 2);
insert into CastMember(DVDNumber, ActorNumber) values(3, 7);
insert into CastMember(DVDNumber, ActorNumber) values(3, 8);
insert into CastMember(DVDNumber, ActorNumber) values(4, 9);
insert into CastMember(DVDNumber, ActorNumber) values(5, 1);
insert into CastMember(DVDNumber, ActorNumber) values(5, 2);
insert into CastMember(DVDNumber, ActorNumber) values(5, 3);
insert into CastMember(DVDNumber, ActorNumber) values(5, 4);
insert into CastMember(DVDNumber, ActorNumber) values(5, 5);
insert into CastMember(DVDNumber, ActorNumber) values(6, 1);
insert into CastMember(DVDNumber, ActorNumber) values(6, 2);
insert into CastMember(DVDNumber, ActorNumber) values(6, 3);
insert into CastMember(DVDNumber, ActorNumber) values(6, 4);
insert into CastMember(DVDNumber, ActorNumber) values(6, 5);


insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(1, 1, 1, '2022-05-01', '2022-05-02', '2022-05-04');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(2, 2, 2, '2022-04-20', '2022-04-27', '2022-05-01');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(3, 3, 3, '2022-04-01', '2022-05-01', '2022-05-01');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(3, 3, 4, '2022-04-01', '2022-05-01', '2022-05-02');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(2, 4, 5, '2022-05-01', '2022-05-07', '2022-05-07');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(1, 4, 6, '2022-05-01', '2022-05-02', '2022-05-02');
insert into Loan(LoanTypeNumber, CopyNumber, MemberNumber, DateOut, DateDue, DateReturned) values(2, 2, 7, '2022-04-07', '2022-04-08', '2022-05-01');


