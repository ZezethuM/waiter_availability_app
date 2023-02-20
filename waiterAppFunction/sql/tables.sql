
   
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
        Waiters_Id int NOT NULL,
        Day_Id int NOT NULL,
        foreign key (Waiters_Id) references Waiters(id),
        foreign key (Day_Id) references Weekdays(id)
    );
    
    CREATE TABLE ShiftSchedule (
    id SERIAL PRIMARY KEY,
    Weekday VARCHAR(100) NOT NULL,
    WeekdayDate DATE NOT NULL,
    Waiter_id INT,
    FOREIGN KEY (waiter_id) REFERENCES waiters(id)
    );
    
    INSERT INTO ShiftSchedule(Weekday, WeekdayDate, waiter_id) VALUES ('Monday', '2023-02-20', 1);

    Select firstname from waiters as a
    inner join shiftschedule as b
    on a.id = b.waiter_id;