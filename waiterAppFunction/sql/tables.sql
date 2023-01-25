
   
    CREATE TABLE WEEKDAYS(
        Id SERIAL NOT NULL,
        Day varchar(40) NOT NULL,
        PRIMARY KEY(Id)
        );

    CREATE TABLE WAITERS(
        Id SERIAL NOT NULL,
        FirstName varchar(40) NOT NULL,
        PRIMARY KEY(Id)
    );

    CREATE TABLE SCHEDULE(
        WaiterId int NOT NULL,
        DayId int NOT NULL
    );

    CREATE TABLE SCHEDULE(
        Waiters_Id int NOT NULL,
        Day_Id int NOT NULL,
        foreign key (Waiters_Id) references Waiters(id),
        foreign key (Day_Id) references Weekdays(id)
    );