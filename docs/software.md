# Реалізація інформаційного та програмного забезпечення

### SQL-скрипт для створення на початкового наповнення бази даних
```mysql
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema DB_labs
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema DB_labs
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `DB_labs` DEFAULT CHARACTER SET utf8 ;
-- -----------------------------------------------------

USE `DB_labs` ;

-- -----------------------------------------------------
-- Table `DB_labs`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`User` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(45) NOT NULL,
  `last_name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `phone_number` VARCHAR(45) NULL,
  `password` VARCHAR(45) NOT NULL,
  `is_admin` TINYINT NOT NULL,
  `description` TEXT NULL,
  `age` TINYINT NULL,
  `gender` VARCHAR(45) NULL,
  `company` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idUser_UNIQUE` (`id` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE,
  UNIQUE INDEX `phone_number_UNIQUE` (`phone_number` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`ExpertComplaint`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`ExpertComplaint` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `description` TEXT NULL,
  `date` DATETIME NOT NULL,
  `researcher_id` INT UNSIGNED NOT NULL,
  `expert_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `researcher_id`, `expert_id`),
  INDEX `fk_ExpertComplaint_User2_idx` (`expert_id` ASC) VISIBLE,
  CONSTRAINT `fk_ExpertComplaint_User1`
    FOREIGN KEY (`researcher_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_ExpertComplaint_User2`
    FOREIGN KEY (`expert_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Survey`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Survey` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(45) NOT NULL,
  `description` TEXT NULL,
  `creation_date` DATETIME NOT NULL,
  `close_date` DATETIME NULL,
  `is_changable` TINYINT NOT NULL,
  `is_active` TINYINT NOT NULL,
  `owner_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `owner_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_Survey_User1_idx` (`owner_id` ASC) VISIBLE,
  CONSTRAINT `fk_Survey_User1`
    FOREIGN KEY (`owner_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`SurveyComplaint`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`SurveyComplaint` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `description` TEXT NULL,
  `date` DATETIME NOT NULL,
  `survey_id` INT UNSIGNED NOT NULL,
  `expert_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `survey_id`, `expert_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `survey_id_idx` (`survey_id` ASC) VISIBLE,
  INDEX `fk_SurveyComplaint_User1_idx` (`expert_id` ASC) VISIBLE,
  CONSTRAINT `survey_1_id`
    FOREIGN KEY (`survey_id`)
    REFERENCES `DB_labs`.`Survey` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `expert_id`
    FOREIGN KEY (`expert_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Category` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `parent_id` INT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Expertise`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Expertise` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `expertise_rate` DOUBLE NOT NULL,
  `category_id` INT UNSIGNED NOT NULL,
  `user_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `category_id`, `user_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `category_id_idx` (`category_id` ASC) VISIBLE,
  INDEX `fk_Expertise_User1_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_category_id`
    FOREIGN KEY (`category_id`)
    REFERENCES `DB_labs`.`Category` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Expertise_User1`
    FOREIGN KEY (`user_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Question`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Question` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `header` VARCHAR(60) NOT NULL,
  `description` TEXT NULL,
  `survey_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `survey_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `survey_id_idx` (`survey_id` ASC) VISIBLE,
  CONSTRAINT `fk_survey_id`
    FOREIGN KEY (`survey_id`)
    REFERENCES `DB_labs`.`Survey` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Option_q`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Option_q` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `content` TEXT NOT NULL,
  `question_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `question_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `question_id_idx` (`question_id` ASC) VISIBLE,
  CONSTRAINT `fk_question_id`
    FOREIGN KEY (`question_id`)
    REFERENCES `DB_labs`.`Question` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`Answer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`Answer` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `option_id` INT UNSIGNED NOT NULL,
  `expert_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `option_id`, `expert_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `option_id_idx` (`option_id` ASC) VISIBLE,
  INDEX `fk_Answer_User2_idx` (`expert_id` ASC) VISIBLE,
  CONSTRAINT `fk_option_id`
    FOREIGN KEY (`option_id`)
    REFERENCES `DB_labs`.`Option_q` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Answer_User2`
    FOREIGN KEY (`expert_id`)
    REFERENCES `DB_labs`.`User` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `DB_labs`.`SurveyCategory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DB_labs`.`SurveyCategory` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `survey_id` INT UNSIGNED NOT NULL,
  `category_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `survey_id`, `category_id`),
  INDEX `fk_Survey_has_Category_Survey1_idx` (`survey_id` ASC) VISIBLE,
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_SurveyCategory_Category1_idx` (`category_id` ASC) VISIBLE,
  CONSTRAINT `fk_survey_id1`
    FOREIGN KEY (`survey_id`)
    REFERENCES `DB_labs`.`Survey` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_SurveyCategory_Category1`
    FOREIGN KEY (`category_id`)
    REFERENCES `DB_labs`.`Category` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
  ```

```mysql
-- Fill database with data
 USE db_labs;

INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin) 
VALUES ("Dima", "Valai", "dima@gmail.com", "+380113223344", "jagiq273ty", 0);
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin) 
VALUES ("Vasya", "Vasylenko", "vasya@gmail.com", "+380113223334", "jagiq2732y", 1);
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin) 
VALUES ("Kate", "Pril", "kate@gmail.com", "+380112223344", "jagiq973ty", 1);
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin, description, age, gender) 
VALUES ("John", "Doe", "john.doe@example.com", "+1234567890", "q23xttrfg", 0, "Expert in psychology with 10 years of experience in behavioral research.", 45, "Male");
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin, company) 
VALUES ("Jane", "Smith", "jane.smith@example.com", "+0987654321", "gqd2ex3fc", 0, "MindTech Solutions");
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin, description, age, gender) 
VALUES ("Alice", "Johnson", "alice.johnson@example.com", "+1987654321", "p9lmdsk34", 0, "Data analyst specializing in machine learning and data-driven decision making.", 30, "Female");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, age, gender)
VALUES("Maksim", "Zinets", "Makson@example.com", "+123124124", "1234567890", 1, 18, "Male");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES("Immanuel", "Kant", "critic@example.com", "+14881488", "0987654321", 0, "No description given", 52, "Male", "Phenomen Industries");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, age, gender)
VALUES("Friedrich", "Nietzsche", "ubermensch@example.com", "+1357924680", "securepassword", 0, 56, "Male");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES("Margaret", "Thatcher", "godsavethequeen@example.com", "+23469346", "26343464", 0, "British Statewoman", 88, "Female", "British Parliament");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES("Napoleon", "Bonaparte", "vivalafrance@example.com", "+11111111", "11111111", 0, "Emperor of France", 45, "Male", "French Empire");
INSERT INTO user (first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES ("Andrii", "Solomka", "asolomka@gmail.com", "+3806666666", "12345", 1, "Node js developer", 52, "Male", "Donbass Coil");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES("Loli", "Crop", "brawlstars@example.com", "+774329602", "pididivdidi", 0, "Beer-drink master", 25, "Male", "Beer World");
INSERT INTO User(first_name, last_name, email, phone_number, password, is_admin, description, age, gender, company)
VALUES("Andrii", "Cruco", "kapibara@example.com", "+0664326434", "********", 0, "Non expert expert", 39, "Male", "Beer World");



INSERT INTO category (name) VALUES ("psychology");
INSERT INTO category (name) VALUES ("math");
INSERT INTO category (name) VALUES ("music");
INSERT INTO category (name) VALUES ("machine learning");
INSERT INTO category (name, parent_id) VALUES ("deep learning", 2);
INSERT INTO category (name, parent_id) VALUES ("children psychology", 1);
INSERT INTO category (name) VALUES("IT");
INSERT INTO category (name) VALUES("Philosophy");
INSERT INTO category (name) VALUES("Politics");
INSERT INTO category (name, parent_id) VALUES("Analytical philosophy", 2);
INSERT INTO category (name, parent_id) VALUES("Continental philosophy", 2);
INSERT INTO category (name, parent_id) VALUES("Conquests", 3);
INSERT INTO category (name, parent_id) VALUES("Backend development", 7);
INSERT INTO category (name) VALUES("Physics");

INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 1, 2);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 6, 8);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4.5, 5, 8);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 5, 11);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (2, 1, 4);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4.5, 2, 4);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 3, 4);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (5, 2, 2);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (1, 1, 1);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (5, 4, 2);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (4, 2, 3);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (5, 5, 3);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (3, 3, 4);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (5, 3, 5);
INSERT INTO expertise(expertise_rate, category_id, user_id) VALUES (5, 6, 5);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4, 13, 14);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4, 14, 14);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (2, 14, 13);


INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Psychology Behavior Survey", "A survey focused on understanding behavioral patterns in adults.", 
        '2024-10-14 09:00:00', '2024-11-14 23:59:59', 0, 1, 3);
INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Discovery of the inheritance of Bach", "This survey explores the legacy and impact of Johann Sebastian Bach's compositions.", 
        '2024-10-14 14:00:00', '2024-11-14 23:59:59', 0, 1, 8);
INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Children Psychology Survey", "A survey aimed at understanding the psychological development of children.", 
        '2024-10-14 10:00:00', '2024-12-01 23:59:59', 1, 1, 3);
INSERT INTO survey(title, description, creation_date, close_date, is_changable, is_active, owner_id)
VALUES("Pure reason critique", "This test is to show your knowledge about pure reason critique", '2024-11-14 23:59:59', '2024-12-14 23:59:59', 1, 1, 2);
INSERT INTO survey(title, description, creation_date, close_date, is_changable, is_active, owner_id)
VALUES("Margaret Thatcher presidency", "In this survey, we want you to rate Margaret Thatcher policies as a prime-minister", '1979-5-4 8:00:00', '1900-11-28 23:59:59', 0, 0, 4);
INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id)
VALUES ("Importance of physics", "A survey focused on importance of physics for people.",
        '2024-10-14 23:00:00', '2024-11-16 23:59:59', 0, 1, 14);

INSERT INTO surveycategory (survey_id, category_id) VALUES (1, 1);
INSERT INTO surveycategory (survey_id, category_id) VALUES (2, 1);
INSERT INTO surveycategory (survey_id, category_id) VALUES (1, 4);
INSERT INTO surveycategory(category_id, survey_id) VALUES (2, 1);
INSERT INTO surveycategory(category_id, survey_id) VALUES (4, 1);
INSERT INTO surveycategory(category_id, survey_id) VALUES (3, 2);

INSERT INTO question (header, description, survey_id) 
VALUES 
("How often do you experience stress?", "Please select the frequency that best matches your experience of stress.", 1),
("How would you describe your overall emotional well-being?", "Rate your emotional well-being on a scale of 1 to 5.", 1);

INSERT INTO question (header, description, survey_id) 
VALUES 
("How does the child interact with peers?", "Select the option that best describes the child’s social interactions.", 2),
("How does the child usually respond to stressful situations?", "Select the typical response the child has to stress.", 2);
INSERT INTO question(header, description, survey_id)
VALUES("Economic policies", "How are you satisfied with Thatchers economic policies?", 2);
INSERT INTO question(header, description, survey_id)
VALUES("What is phenomenon in Kant`s philosophy?", "There`s only one right answer", 1);
INSERT INTO question (header, description, survey_id)
VALUES
("Physics is important?", "Many people mean that physics is unimportant. Do you think so?", 6),
("How dificult is it to answer the previous question?", "Scale of 1 to 5.", 6);

INSERT INTO option_q(content, question_id)
VALUES("An object that exists independently of our perception", 1);
INSERT INTO option_q(content, question_id)
VALUES("An object as it appears in our experience through the senses and cognitive faculties", 1);
INSERT INTO option_q(content, question_id)
VALUES("An abstract idea, unrelated to actual perception", 1);
INSERT INTO option_q(content, question_id)
VALUES("An emotional reaction of a person to external objects", 1);

INSERT INTO option_q(content, question_id) VALUES("Very satisfied", 1);
INSERT INTO option_q(content, question_id) VALUES("Satisfied", 1);
INSERT INTO option_q(content, question_id) VALUES("It didn`t affect me", 1);
INSERT INTO option_q(content, question_id) VALUES("Unsatisfied", 1);
INSERT INTO option_q(content, question_id) VALUES("Very unsatisfied", 1);
INSERT INTO option_q (content, question_id) VALUES ("Never", 3);
INSERT INTO option_q (content, question_id) VALUES ("Sometimes", 3);
INSERT INTO option_q (content, question_id) VALUES ("Always", 3);
INSERT INTO option_q (content, question_id) VALUES ("1", 4);
INSERT INTO option_q (content, question_id) VALUES ("2", 4);
INSERT INTO option_q (content, question_id) VALUES ("3", 4);
INSERT INTO option_q (content, question_id) VALUES ("5", 4);
INSERT INTO option_q (content, question_id) VALUES ("Very friendly", 5);
INSERT INTO option_q (content, question_id) VALUES ("Neutral", 5);
INSERT INTO option_q (content, question_id) VALUES ("Aggressive", 5);
INSERT INTO option_q (content, question_id) VALUES ("Remains calm", 6);
INSERT INTO option_q (content, question_id) VALUES ("Shows moderate anxiety", 6);
INSERT INTO option_q (content, question_id) VALUES ("Has a breakdown", 6);
INSERT INTO option_q(content, question_id) VALUES("Yes", 7);
INSERT INTO option_q(content, question_id) VALUES("No", 7);
INSERT INTO option_q(content, question_id) VALUES("Easy", 8);
INSERT INTO option_q(content, question_id) VALUES("Medium", 8);
INSERT INTO option_q(content, question_id) VALUES("Hard", 8);



INSERT INTO answer (option_id, expert_id) VALUES (2, 2);
INSERT INTO answer (option_id, expert_id) VALUES (3, 4);
INSERT INTO answer (option_id, expert_id) VALUES (5, 2);
INSERT INTO answer (option_id, expert_id) VALUES (6, 4);
INSERT INTO answer (option_id, expert_id) VALUES (8, 4);
INSERT INTO answer (option_id, expert_id) VALUES (12, 4);
INSERT INTO answer (option_id, expert_id) VALUES(7, 2);
INSERT INTO answer (option_id, expert_id) VALUES(24, 13);
INSERT INTO answer (option_id, expert_id) VALUES(27, 13);

INSERT INTO expertcomplaint (description, date, researcher_id, expert_id)
VALUES ('Complaint regarding expert feedback', '2024-10-14 09:30:00', 3, 4);
INSERT INTO surveycomplaint (description, date, survey_id, expert_id)
VALUES ('Complaint about survey data handling by expert', '2024-10-14 09:30:00', 1, 2);


INSERT INTO expertcomplaint (description, date, researcher_id, expert_id)
VALUES ('Complaint about expert`s options are biased', '2024-10-14 09:30:00', 4, 3);
INSERT INTO surveycomplaint (description, date, survey_id, expert_id)

VALUES ('Complaint about test`s complication', '2024-10-14 09:30:00', 2, 2);

INSERT INTO surveycategory (id, survey_id, category_id) VALUES (7, 5, 11);
INSERT INTO surveycategory (id, survey_id, category_id) VALUES (8, 3, 1);
INSERT INTO surveycategory (id, survey_id, category_id) VALUES (9, 2, 7);
INSERT INTO surveycategory (survey_id, category_id) VALUES (6, 14);
```

```mysql
-- Example SELECT queries
SELECT content FROM option_q WHERE question_id=5;
SELECT first_name, last_name FROM user WHERE company IS NULL AND is_admin=0;
SELECT first_name, last_name FROM user WHERE age > 52;
SELECT id FROM question WHERE header LIKE "Economic policies";
SELECT option_id FROM answer WHERE id > 2 AND expert_id = 2;
SELECT * FROM user ORDER BY first_name DESC;
SELECT title FROM survey WHERE owner_id=3;
SELECT title FROM survey WHERE owner_id = (SELECT id FROM user WHERE email = 'critic@example.com');
SELECT name FROM category WHERE parent_id=2;
SELECT name from category WHERE parent_id=2 OR parent_id IS NULL;
SELECT email FROM user WHERE is_admin=1;

-- Count owner's number of surveys
SELECT COUNT(id) FROM survey WHERE owner_id = 1;

-- Get sorted categories
SELECT name FROM category ORDER BY name;

-- Select the content of option and name of expert, who selected it
SELECT content, first_name, last_name
FROM option_q INNER JOIN answer
INNER JOIN user WHERE option_q.id = answer.option_id AND answer.expert_id = user.id;

-- Select complaints and author's names
SELECT ec.description, first_name
FROM expertcomplaint AS ec, user AS u WHERE ec.expert_id = u.id;

-- Select category of the survey and survey title
SELECT c.name, s.title
FROM category AS c
INNER JOIN survey AS s
INNER JOIN surveycategory AS sc ON sc.survey_id = s.id AND sc.category_id = c.id;

-- Select survey author's name, title and description
SELECT user.first_name, survey.title, survey.description
FROM survey
INNER JOIN user
ON survey.owner_id = user.id;

-- Select expert's names and their expertise
SELECT user.first_name, expertise_rate, category.name
FROM expertise
RIGHT JOIN category
ON expertise.category_id = category.id
INNER JOIN user
ON expertise.user_id = user.id
ORDER BY user.first_name, category.name;

-- Select surveys' titles and headers of their question
SELECT survey.title, question.header
FROM question
INNER JOIN survey
ON question.survey_id = survey.id;

-- Select users' names and expertise if their expertise is equal to 5
SELECT user.first_name, expertise.expertise_rate, category.name
FROM expertise
RIGHT JOIN category
ON expertise.category_id = category.id
INNER JOIN user
ON expertise.user_id = user.id
WHERE expertise.expertise_rate = 5;

-- Select all the surveys' titles and matching questions
SELECT  question.header, survey.title
FROM question RIGHT JOIN survey ON survey.id = question.survey_id;

-- Select users, whose id is smaller than 4
SELECT user.first_name, user.last_name, survey.title, survey.description
FROM user INNER JOIN survey ON user.id = survey.owner_id
WHERE user.id < 4;

-- Select users' full names and expertise
SELECT user.first_name, user.last_name,category.name, expertise.expertise_rate
FROM user INNER JOIN expertise ON user.id = expertise.user_id
LEFT JOIN category ON  expertise.category_id=category.id;

-- Select surveys' titles, headers and descriptions of their question
SELECT survey.title AS survey_title,
       question.header AS question_header,
       question.description AS question_description
FROM survey INNER JOIN question ON survey.id = question.survey_id;

-- Select all the surveys and matching complaints
SELECT survey.title AS survey_title,
       surveycomplaint.description AS survey_complaint
FROM survey LEFT JOIN surveycomplaint ON survey.id = surveycomplaint.survey_id;

-- Select users and their expertise
SELECT expertise.expertise_rate,
       user.first_name AS user_first_name
FROM expertise CROSS JOIN user ON expertise.user_id = user.id;

-- Select emails of admins, whose expertise rate is higher than average
SELECT category.name AS category_name,
       user.email AS user_email,
       user.is_admin AS user_admin_status,
       expertise.expertise_rate
FROM user
RIGHT JOIN expertise ON expertise.user_id = user.id
LEFT JOIN category ON category.id = expertise.category_id
WHERE expertise.expertise_rate > (SELECT AVG(expertise_rate) FROM expertise)
AND user.is_admin = 1;

-- Count number of answers per option_q in survey
SELECT content, COUNT(expert_id) AS answer_count
FROM answer
RIGHT JOIN option_q ON answer.option_id = option_q.id
WHERE question_id = 7
GROUP BY content;

-- Get all expert responses to the survey
SELECT header AS question_header, content AS answer_name
FROM answer
JOIN option_q ON answer.option_id = option_q.id
JOIN question ON option_q.question_id = question.id
WHERE expert_id = 13 AND survey_id = 6
ORDER BY question.id;

-- Get experts with higher expertise_rate than average
SELECT user.id, CONCAT(first_name, " ", last_name), expertise_rate
FROM expertise
JOIN user ON expertise.user_id = user.id
WHERE expertise_rate > (SELECT AVG(expertise_rate) FROM expertise WHERE category_id =14)
AND category_id = 14;
```

```mysql
-- Example UPDATE queries
UPDATE user SET email="kate.pril@gmail.com" WHERE id=3;
UPDATE survey SET close_date = "2024-12-14 23:59:59" WHERE id = 3;

UPDATE survey SET is_active = 0, close_date = "2024-10-14" WHERE id = 1;
UPDATE survey SET title = "Psychological skills" WHERE id = 3;
UPDATE survey SET title = "Best woman" WHERE owner_id = 2;
UPDATE user SET first_name = "Donald" WHERE first_name = "Friedrich";

UPDATE answer SET option_id = "1" WHERE id = 1;

UPDATE surveycategory SET survey_id=4 WHERE id=3;
UPDATE surveycategory SET category_id=1 WHERE survey_id=3;
UPDATE db_labs.expertise SET expertise_rate=3.5, user_id=11 WHERE category_id=4;
```

```mysql
-- Example DELETE queries
DELETE FROM option_q WHERE id=13;
DELETE FROM option_q WHERE content = "Yes" AND question_id = 7;
DELETE FROM user WHERE id=5;
DELETE FROM surveycategory WHERE id=9;
```

### RESTfull сервіс для управління даними

