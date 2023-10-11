CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS albums(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        category VARCHAR(100) NOT NULL,
        title VARCHAR(255) NOT NULL,
        coverImg VARCHAR(500) NOT NULL,
        archived TINYINT DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE albums ;

DELETE FROM albums WHERE id =1;

DELETE FROM accounts WHERE id = '64f76f68599293f30abc2ade';

INSERT INTO
    albums(
        category,
        title,
        coverImg,
        creatorId
    )
VALUES (
        'Sport',
        'Ping Pong Tournament',
        'https://images.unsplash.com/photo-1609710228159-0fa9bd7c0827?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80',
        '64f76f68599293f30abc2ade'
    );

INSERT INTO
    albums(
        category,
        title,
        coverImg,
        creatorId
    )
VALUES (
        'Convention',
        'Ping Pong Association',
        'https://images.unsplash.com/photo-1559136560-16ad036d85d3?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2069&q=80',
        '64f76f68599293f30abc2ade'
    );

INSERT INTO
    albums(
        category,
        title,
        coverImg,
        creatorId
    )
VALUES (
        'Concert',
        'Pong',
        'https://images.unsplash.com/photo-1595693143692-750b5c33e335?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1953&q=80',
        '6526c4304dedc9f41b528495'
    );

SELECT * FROM albums;

SELECT * FROM albums JOIN accounts;

SELECT * FROM albums alb JOIN accounts act ON act.id = alb.creatorId;

SELECT alb.*, act.*
FROM albums alb
    JOIN accounts act ON act.id = alb.creatorId;

SELECT alb.*, act.name
FROM albums alb
    JOIN accounts act ON act.id = alb.creatorId
WHERE title = 'Pong';

SELECT *
FROM accounts act
    JOIN albums alb ON alb.creatorId = act.id
WHERE act.id = '6526c4304dedc9f41b528495'