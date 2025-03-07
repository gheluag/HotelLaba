drop database if exists hotel;
CREATE DATABASE if not exists hotel;

use hotel;

CREATE TABLE room (
    NUM_ROOM INT PRIMARY KEY AUTO_INCREMENT,
    type_room VARCHAR(50) NULL,
    BED INT NULL,
    COST INT NULL,
    status VARCHAR(50) NULL,
    ID_KEY INT NOT NULL
);

CREATE TABLE roomer (
    ID_ROOMER INT PRIMARY KEY AUTO_INCREMENT,
    SURNAME VARCHAR(255) NULL,
    FIRSTNAME VARCHAR(255) NULL,
    PATRONYMIC VARCHAR(50) NULL,
    PASSPORT CHAR(10) NULL,
    PHONE CHAR(11) null,
    NUMROOM int null,
    foreign key (NUMROOM) references room (NUM_ROOM)
);

CREATE TABLE REG_JOURNAL (
    ID_JOURNAL INT PRIMARY KEY AUTO_INCREMENT,
    REG_TIME TIME null,
    ID_ROOMER INT NULL,
    NUM_ROOM INT NULL,
    ENTRY DATE NULL,
    DEPARTURE DATE NULL,
    CONSTRAINT FOREIGN KEY (ID_ROOMER) REFERENCES roomer(ID_ROOMER),
    CONSTRAINT FOREIGN KEY (NUM_ROOM) REFERENCES room(NUM_ROOM)
);

CREATE TABLE ADMIN (
    ID_ADMIN INT PRIMARY KEY AUTO_INCREMENT,
    FIO_ADMIN VARCHAR(50) NULL,
    ID_JOURNAL INT NULL,
    CONSTRAINT FOREIGN KEY (ID_JOURNAL) REFERENCES REG_JOURNAL(ID_JOURNAL)
);



/* таблица номеров*/
INSERT INTO room (NUM_ROOM, type_room, BED, COST, status, ID_KEY)
VALUES 
(101, 'Стандарт', 1, 2500, 'Занят', 101),       
(205, 'Люкс', 2, 5000, 'Свободен', 102),        
(302, 'Стандарт', 2, 3000, 'Занят', 103),       
(410, 'Люкс', 1, 4500, 'Свободен', 104),       
(107, 'Эконом', 1, 2000, 'Занят', 105),         
(208, 'Стандарт', 2, 3000, 'Свободен', 106),    
(315, 'Люкс', 2, 5500, 'Свободен', 107),        
(120, 'Эконом', 1, 1800, 'Свободен', 108),      
(201, 'Стандарт', 1, 2700, 'Свободен', 109),    
(305, 'Люкс', 1, 4800, 'Свободен', 110);  

/* таблица постояльцев*/
INSERT INTO roomer (SURNAME, FIRSTNAME, PATRONYMIC, PASSPORT, PHONE, NUMROOM)
VALUES 
('Иванов', 'Иван', 'Иванович', '1234567890', '79101234567', 101),   
('Сидоров', 'Алексей', 'Дмитриевич', '1122334455', '79111223344', 302), 
('Васильев', 'Дмитрий', 'Николаевич', '3344556677', '79133445566', 107);

INSERT INTO REG_JOURNAL (REG_TIME, ID_ROOMER, NUM_ROOM, ENTRY, DEPARTURE)
VALUES 
('10:00:00', 1, 101, '2023-10-01', '2023-10-05'),  -- Иванов Иван Иванович (номер 101)
('12:45:00', 2, 302, '2023-10-03', '2023-10-06'),  -- Сидоров Алексей Дмитриевич (номер 302)
('15:15:00', 3, 107, '2023-10-05', '2023-10-08');  -- Васильев Дмитрий Николаевич (номер 107)

/* таблица администраторов*/
INSERT INTO ADMIN (FIO_ADMIN, ID_JOURNAL)
VALUES 
('Смирнова Ольга Ивановна', 1),
('Козлов Дмитрий Сергеевич', 2),
('Васильева Елена Алексеевна', 3);




DELIMITER $$
CREATE TRIGGER before_insert_reg_journal
BEFORE INSERT ON REG_JOURNAL
FOR EACH ROW
BEGIN
    SET NEW.REG_TIME = CURRENT_TIME();
END$$
DELIMITER ;




