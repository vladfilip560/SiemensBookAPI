CREATE TABLE book (
	bid INT NOT NULL AUTO_INCREMENT,
    title VARCHAR(30),
    author VARCHAR(30),
    publisher VARCHAR(30),
    numberTotal INT,
    numberGiven INT,
    PRIMARY KEY (bid)
);
INSERT INTO book (title, author, publisher, numberTotal, numberGiven) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 'Scribner', 5, 2),
('To Kill a Mockingbird', 'Harper Lee', 'J.B. Lippincott & Co.', 3, 1),
('1984', 'George Orwell', 'Secker & Warburg', 4, 0),
('Pride and Prejudice', 'Jane Austen', 'T. Egerton', 2, 1),
('The Catcher in the Rye', 'J.D. Salinger', 'Little, Brown and Company', 6, 3);
