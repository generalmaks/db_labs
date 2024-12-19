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

#### Entities
##### CategoryEntity
````C#
namespace RestApiLab6.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public CategoryEntity Parent { get; set; }
}
````
##### ExpertiseEntity
````C#
namespace RestApiLab6.Entities;

public class ExpertiseEntity
{
    public int Id { get; set; }
    public double ExpertiseRate { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity CategoryEntity { get; set; }
    public int UserId { get; set; }
    public UserEntity UserEntity { get; set; }
}
````
##### SurveyEntity
````C#
namespace RestApiLab6.Entities;

public class SurveyEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public bool IsChangable { get; set; }
    public bool IsActive { get; set; }
    public int OwnerId { get; set; }
    public UserEntity Owner { get; set; }
}
````
##### UserEntity
````C#
namespace RestApiLab6.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public string? Description { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Company { get; set; }
    public List<ExpertiseEntity> Expertises { get; set; }
    public List<SurveyEntity> Surveys { get; set; }
}
````
#### Configurations
##### CategoryConfiguration
````C#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("category");

        builder.HasKey(c => c.Id)
            .HasName("PK");

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(c => c.ParentId)
            .HasColumnName("parent_id");

        builder.HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId);
    }
}
````
##### ExpertiseConfiguration
````C#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class ExpertiseConfiguration : IEntityTypeConfiguration<ExpertiseEntity>
{
    public void Configure(EntityTypeBuilder<ExpertiseEntity> builder)
    {
        builder.ToTable("expertise");

        builder.HasKey(e => new { e.Id, e.CategoryId, e.UserId }).HasName("PK");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(e => e.ExpertiseRate)
            .IsRequired()
            .HasColumnName("expertise_rate");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(e => e.CategoryId)
            .IsRequired()
            .HasColumnName("category_id");

        builder.HasOne(e => e.UserEntity)
            .WithMany(u => u.Expertises)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.CategoryEntity)
            .WithMany()
            .HasForeignKey(e => e.CategoryId);
    }
}
````
##### SurveyConfiguration
````C#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<SurveyEntity>
{
    public void Configure(EntityTypeBuilder<SurveyEntity> builder)
    {
        builder.ToTable("survey");

        builder.HasKey(s => s.Id)
            .HasName("PK");

        builder.Property(s => s.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(s => s.Title)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("title");

        builder.Property(s => s.Description)
            .HasColumnType("text")
            .HasColumnName("description");

        builder.Property(s => s.CreationDate)
            .IsRequired()
            .HasColumnName("creation_date");

        builder.Property(s => s.CloseDate)
            .HasColumnName("close_date");

        builder.Property(s => s.IsChangable)
            .IsRequired()
            .HasColumnType("tinyint(1)")
            .HasColumnName("is_chaneable");

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasColumnType("tinyint(1)")
            .HasColumnName("is_active");

        builder.Property(s => s.OwnerId)
            .HasColumnName("owner_id");

        builder.HasOne(s => s.Owner)
            .WithMany(u => u.Surveys)
            .HasForeignKey(s => s.OwnerId);
    }
}
````
##### UserConfiguration
````C#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id)
            .HasName("PK");

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("first_name");

        builder.Property(u => u.LastName)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("last_name");

        builder.Property(u => u.Email)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("email");

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(45)
            .HasColumnName("phone_number");

        builder.Property(u => u.Password)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("password");

        builder.Property(u => u.IsAdmin)
            .HasColumnType("tinyint(1)")
            .IsRequired()
            .HasColumnName("is_admin");

        builder.Property(u => u.Description)
            .HasColumnType("text")
            .HasColumnName("description");

        builder.Property(u => u.Age)
            .HasColumnName("age");

        builder.Property(u => u.Gender)
            .HasMaxLength(45)
            .HasColumnName("gender");

        builder.Property(u => u.Company)
            .HasMaxLength(45)
            .HasColumnName("company");
    }
}
````
#### Models
##### Category
````C#
namespace RestApiLab6.Models;

public class Category
{
    public Category(string name, int? parentId)
    {
        Name = name;
        ParentId = parentId;
    }

    public string Name { get; }
    public int? ParentId { get; }
}
````
##### Expertise
````C#
namespace RestApiLab6.Models;

public class Expertise
{
public Expertise(double expertiseRate, int categoryId, int userId)
{
ExpertiseRate = expertiseRate;
CategoryId = categoryId;
UserId = userId;
}

    public double ExpertiseRate { get; }
    public int CategoryId { get; }
    public int UserId { get; }
}
````
##### Survey
````c#
namespace RestApiLab6.Models;

public class Survey
{
    public Survey(string title, string? description, DateTime creationDate, DateTime? closeDate, bool isChangable,
        bool isActive, int ownerId)
    {
        Title = title;
        Description = description;
        CreationDate = creationDate;
        CloseDate = closeDate;
        IsChangable = isChangable;
        IsActive = isActive;
        OwnerId = ownerId;
    }

    public string Title { get; }
    public string? Description { get; }
    public DateTime CreationDate { get; }
    public DateTime? CloseDate { get; }
    public bool IsChangable { get; }
    public bool IsActive { get; }
    public int OwnerId { get; }
}
````
##### User
````c#
namespace RestApiLab6.Models;

public class User
{
    public User(string firstName, string lastName, string email, string phoneNumber, string password, bool isAdmin,
        string description,
        int? age, string gender, string company)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        IsAdmin = isAdmin;
        Description = description;
        Age = age;
        Gender = gender;
        Company = company;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public string Password { get; }
    public bool IsAdmin { get; }
    public string? Description { get; }
    public int? Age { get; }
    public string? Gender { get; }
    public string? Company { get; }
}
````


##### IdentifiedCategory
````c#
using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedCategory : Category, IHasId
{
    public IdentifiedCategory(int id, string name, int? parentId = null) : base(name, parentId)
    {
        Id = id;
    }

    public IdentifiedCategory(CategoryEntity c) : this(c.Id, c.Name, c.ParentId)
    {
    }

    public int Id { get; }
}
````
##### IdentifiedExpertise
````c#
using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedExpertise : Expertise, IHasId
{
    public IdentifiedExpertise(int id, double expertiseRate, int categoryId, int userId)
        : base(expertiseRate, categoryId, userId)
    {
        Id = id;
    }

    public IdentifiedExpertise(ExpertiseEntity e) : this(e.Id, e.ExpertiseRate, e.CategoryId, e.UserId)
    {
    }

    public int Id { get; }
}
````
##### IdentifiedSurver
````c#
using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedSurvey : Survey, IHasId
{
    public IdentifiedSurvey(int id, string title, string? description, DateTime creationDate, DateTime? closeDate,
        bool isChangable, bool isActive, int ownerId)
        : base(title, description, creationDate, closeDate, isChangable, isActive, ownerId)
    {
        Id = id;
    }

    public IdentifiedSurvey(SurveyEntity s)
        : this(s.Id, s.Title, s.Description, s.CreationDate, s.CloseDate, s.IsChangable, s.IsActive, s.OwnerId)
    {
    }

    public int Id { get; }
}
````
##### IdentifiedUser
````c#
using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedUser : User, IHasId
{
    public IdentifiedUser(int id, string firstName, string lastName, string email, string? phoneNumber, string password,
        bool isAdmin, string? description, int? age, string? gender, string? company)
        : base(firstName, lastName, email, phoneNumber, password, isAdmin, description, age, gender, company)
    {
        Id = id;
    }

    public IdentifiedUser(UserEntity u) : this(u.Id, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Password,
        u.IsAdmin, u.Description, u.Age, u.Gender, u.Company)
    {
    }

    public int Id { get; }
}
````
##### Інтерфейс IHasId
````c#
namespace RestApiLab6.Models;

public interface IHasId
{
    int Id { get; }
}
````
#### SurveyDbContext
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.Configurations;
using RestApiLab6.Entities;

namespace RestApiLab6.MyDbContext;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ExpertiseEntity> Expertises { get; set; }
    public DbSet<SurveyEntity> Surveys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpertiseConfiguration());
        modelBuilder.ApplyConfiguration(new SurveyConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
````
#### Repositories
##### CategoryRepository
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class CategoryRepository
{
    private SurveyDbContext _dbContext;

    public CategoryRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedCategory>> GetAll()
    {
        var categories = await _dbContext.Categories
            .Select(c => new IdentifiedCategory(c))
            .ToListAsync();

        return categories;
    }

    public async Task<IdentifiedCategory> Get(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        if (category is null)
            throw new NullReferenceException($"Category with id {id} could not be found");

        return new IdentifiedCategory(category);
    }

    public async Task<IdentifiedCategory> Add(Category category)
    {
        if (await _dbContext.Categories.AnyAsync(c => c.Name == category.Name))
            throw new Exception($"Category with name {category.Name} already exists");

        var categoryEntity = new CategoryEntity()
        {
            Name = category.Name,
            ParentId = category.ParentId
        };

        await _dbContext.Categories.AddAsync(categoryEntity);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedCategory(categoryEntity);
    }

    public async Task UpdateName(int id, string name)
    {
        if (await _dbContext.Categories.AnyAsync(c => c.Name == name))
            throw new Exception($"Category with name {name} already exists");

        await _dbContext.Categories.Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Name, name));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}
````
#### ExpertiseRepository
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class ExpertiseRepository
{
    private SurveyDbContext _dbContext;

    public ExpertiseRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedExpertise>> GetAllByUser(int userId)
    {
        return await _dbContext.Expertises
            .Where(e => e.UserId == userId)
            .Select(e => new IdentifiedExpertise(e))
            .ToListAsync();
    }

    public async Task<IdentifiedExpertise> Get(int id)
    {
        var expertise = await _dbContext.Expertises.FindAsync(id);

        if (expertise == null)
            throw new Exception($"Expertise with id {id} not found");

        return new IdentifiedExpertise(expertise);
    }

    public async Task<IdentifiedExpertise> Add(Expertise expertise)
    {
        bool isExist = await _dbContext.Expertises
            .AnyAsync(e =>
                e.UserId == expertise.UserId
                && e.CategoryId == expertise.CategoryId);

        if (isExist)
            throw new Exception("Expertise already exists");

        var createdExpertise = new ExpertiseEntity()
        {
            ExpertiseRate = expertise.ExpertiseRate,
            CategoryId = expertise.CategoryId,
            UserId = expertise.UserId
        };

        await _dbContext.Expertises.AddAsync(createdExpertise);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedExpertise(createdExpertise);
    }

    public async Task UpdateRate(int id, double rate)
    {
        await _dbContext.Expertises
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.ExpertiseRate, rate));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Expertises.Where(e => e.Id == id)
            .ExecuteDeleteAsync();
    }
}
````
##### SurveyRepository
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class SurveyRepository
{
    private SurveyDbContext _dbContext;

    public SurveyRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedSurvey>> GetAllByOwner(int ownerId)
    {
        return await _dbContext.Surveys
            .Where(s => s.OwnerId == ownerId)
            .Select(s => new IdentifiedSurvey(s))
            .ToListAsync();
    }

    public async Task<List<IdentifiedSurvey>> GetAllWithTitlePart(string titlePart)
    {
        return await _dbContext.Surveys
            .Where(s => s.Title.Contains(titlePart))
            .Select(s => new IdentifiedSurvey(s))
            .ToListAsync();
    }

    public async Task<Survey> GetById(int id)
    {
        var survey = await _dbContext.Surveys.FindAsync(id);
        if (survey is null)
            throw new Exception($"Survey with id {id} not found");

        return new IdentifiedSurvey(survey);
    }

    public async Task<IdentifiedSurvey> Add(Survey survey)
    {
        var createdSurvey = new SurveyEntity()
        {
            Title = survey.Title,
            Description = survey.Description,
            CreationDate = survey.CreationDate,
            CloseDate = survey.CloseDate,
            IsChangable = survey.IsChangable,
            IsActive = survey.IsActive,
            OwnerId = survey.OwnerId
        };

        await _dbContext.Surveys.AddAsync(createdSurvey);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedSurvey(createdSurvey);
    }

    public async Task UpdateTitle(int id, string title)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.Title, title));
    }

    public async Task UpdateDescription(int id, string description)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.Description, description));
    }

    public async Task UpdateActivity(int id, bool isActive)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.IsActive, isActive));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
    }
}
````
##### UserRepository
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class UserRepository
{
    private SurveyDbContext _dbContext;

    public UserRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedUser>> GetAll()
    {
        return await _dbContext.Users
            .Select(u => new IdentifiedUser(u))
            .ToListAsync();
    }

    public async Task<IdentifiedUser> Get(int id)
    {
        var user = await _dbContext.Users
            .FindAsync(id);

        if (user is null)
            throw new Exception($"User with id {id} not found");

        return new IdentifiedUser(user);
    }

    public async Task<IdentifiedUser> Create(User user)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
            throw new Exception($"Email {user.Email} already taken");

        var phone = user.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (await _dbContext.Users.AnyAsync(u => u.PhoneNumber == phone))
                throw new Exception($"PhoneNumber {user.PhoneNumber} already taken");
        }

        var userEntity = new UserEntity()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            IsAdmin = user.IsAdmin,
            Description = user.Description,
            Age = user.Age
        };

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();

        var created = new IdentifiedUser(userEntity);

        return created;
    }

    public async Task UpdatePhoneNumber(int id, string phoneNumber)
    {
        if (await _dbContext.Users.AnyAsync(u => u.PhoneNumber == phoneNumber))
            throw new Exception($"PhoneNumber {phoneNumber} already taken");

        var user = await _dbContext.Users.FindAsync(id);

        if (user is null)
            throw new Exception($"User with id {id} not found");

        user.PhoneNumber = phoneNumber;
    }

    public async Task UpdatePassword(int id, string password)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Password, password));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }
}
````

#### Controllers
##### CategoryController
````c#
using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private CategoryRepository _categoryRepo;

    public CategoryController(CategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    [Route("categories")]
    public async Task<ActionResult<List<IdentifiedCategory>>> GetCategories()
    {
        try
        {
            var categories = await _categoryRepo.GetAll();
            return Ok(categories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("categories/{id}")]
    public async Task<ActionResult<IdentifiedCategory>> GetCategory(int id)
    {
        try
        {
            var category = await _categoryRepo.Get(id);
            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("categories")]
    public async Task<ActionResult<List<IdentifiedCategory>>> CreateCategory([FromBody] Category category)
    {
        try
        {
            var created = await _categoryRepo.Add(category);
            return Created($"/categories/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("categories/{id}")]
    public async Task<ActionResult> UpdateCategoryName(int id, string name)
    {
        try
        {
            await _categoryRepo.UpdateName(id, name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("categories/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoryRepo.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
````
##### ExpertiseController
````c#
using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class ExpertiseController : ControllerBase
{
    private ExpertiseRepository _expertiseRepository;

    public ExpertiseController(ExpertiseRepository expertiseRepository)
    {
        _expertiseRepository = expertiseRepository;
    }

    [HttpGet]
    [Route("expertises/user/{userId}")]
    public async Task<ActionResult<List<IdentifiedExpertise>>> GetExpertisesByUser(int userId)
    {
        try
        {
            var expertises = await _expertiseRepository.GetAllByUser(userId);
            return Ok(expertises);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("expertises/{id}")]
    public async Task<ActionResult<IdentifiedExpertise>> GetExpertiseById(int id)
    {
        try
        {
            var expertise = await _expertiseRepository.Get(id);
            return Ok(expertise);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("expertises")]
    public async Task<ActionResult<IdentifiedExpertise>> CreateExpertise(Expertise expertise)
    {
        try
        {
            var created = await _expertiseRepository.Add(expertise);
            return Created($"/expertises/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("expertises/{id}")]
    public async Task<ActionResult> UpdateExpertiseRate(int id, double rate)
    {
        try
        {
            await _expertiseRepository.UpdateRate(id, rate);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("expertises/{id}")]
    public async Task<ActionResult> DeleteExpertise(int id)
    {
        try
        {
            await _expertiseRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
````
##### SurveyController
````c#
using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class SurveyController : ControllerBase
{
    private SurveyRepository _surveyRepository;

    public SurveyController(SurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    [HttpGet]
    [Route("surveys/owner/{ownerId}")]
    public async Task<ActionResult<List<IdentifiedSurvey>>> GetSurveysByOwner(int ownerId)
    {
        try
        {
            var surveys = await _surveyRepository.GetAllByOwner(ownerId);
            return Ok(surveys);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("surveys/{titlePart:alpha}")]
    public async Task<ActionResult<List<IdentifiedSurvey>>> GetSurveysByTitlePart(string titlePart)
    {
        try
        {
            var surveys = await _surveyRepository.GetAllWithTitlePart(titlePart);
            return Ok(surveys);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("surveys/{id:int}")]
    public async Task<ActionResult<IdentifiedSurvey>> GetSurveyById(int id)
    {
        try
        {
            var survey = await _surveyRepository.GetById(id);
            return Ok(survey);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("surveys")]
    public async Task<ActionResult<IdentifiedSurvey>> CreateSurvey(Survey survey)
    {
        try
        {
            var created = await _surveyRepository.Add(survey);
            return Created($"/surveys/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/title")]
    public async Task<ActionResult> UpdateSurveyTitle(int id, string title)
    {
        try
        {
            await _surveyRepository.UpdateTitle(id, title);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/description")]
    public async Task<ActionResult> UpdateSurveyDescription(int id, string description)
    {
        try
        {
            await _surveyRepository.UpdateDescription(id, description);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("surveys/update/activity")]
    public async Task<ActionResult> UpdateSurveyActivity(int id, bool isActive)
    {
        try
        {
            await _surveyRepository.UpdateActivity(id, isActive);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("surveys/delete/{id:int}")]
    public async Task<ActionResult> DeleteSurvey(int id)
    {
        try
        {
            await _surveyRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
````
##### UserController
````c#
using Microsoft.AspNetCore.Mvc;
using RestApiLab6.Models;
using RestApiLab6.Repositories;

namespace RestApiLab6.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<List<IdentifiedUser>>> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("users/{id}")]
    public async Task<ActionResult<IdentifiedUser>> GetUser(int id)
    {
        try
        {
            var user = await _userRepository.Get(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("users")]
    public async Task<ActionResult<IdentifiedUser>> CreateUser(User user)
    {
        try
        {
            var created = await _userRepository.Create(user);
            return Created($"/users/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("users/{id}")]
    public async Task<ActionResult> UpdatePhoneNumber(int id, string phoneNumber)
    {
        try
        {
            await _userRepository.UpdatePhoneNumber(id, phoneNumber);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("users/update/{id}")]
    public async Task<ActionResult> UpdatePassword(int id, string password)
    {
        try
        {
            await _userRepository.UpdatePassword(id, password);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("users/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            await _userRepository.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
````
#### Program.cs
````c#
using Microsoft.EntityFrameworkCore;
using RestApiLab6.MyDbContext;
using RestApiLab6.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SurveyDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ExpertiseRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
````

#### Файли конфігурації
##### appsettings.json
````json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````

##### appsettings.Development.json
````json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
      "DbConnection": "Server=localhost; Port=3306; Database=Survey; User Id=root; Password=12345678"
  }
}
````
##### launchSettings.json
````json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5100",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7239;http://localhost:5100",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
````
#### Під'єднання до бази даних
```json
"DbConnection": "Server=localhost;Port=3306;Database=lab6;User Id=root;Password=12345678"
```