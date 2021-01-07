-- CREATE TABLE profiles (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id)
-- )

-- CREATE TABLE vaults (
--     id INT NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     isPrivate TINYINT,
--     creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId)
--         REFERENCES profiles(id)
--         ON DELETE CASCADE
-- )

-- CREATE TABLE keeps (
--     id int NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     img VARCHAR(255) NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId)
--         REFERENCES profiles(id)
--         ON DELETE CASCADE
-- )

-- ALTER TABLE keeps
-- ADD views int

CREATE TABLE vaultkeeps (
    id int NOT NULL AUTO_INCREMENT,
    vaultId int,
    keepId int,
    creatorId VARCHAR(255) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (creatorId)
        REFERENCES profiles(id)
        ON DELETE CASCADE,
    FOREIGN KEY (vaultId)
        REFERENCES vaults(id)
        ON DELETE CASCADE,
    FOREIGN KEY (keepId)
        REFERENCES keeps(id)
        ON DELETE CASCADE
)

-- TRUNCATE TABLE keeps
-- DROP TABLE vaultkeeps