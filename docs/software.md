# Реалізація інформаційного та програмного забезпечення

В рамках проекту розробляється: 
- SQL-скрипт для створення на початкового наповнення бази даних
```mysql
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema DB_labs
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `DB_labs` DEFAULT CHARACTER SET utf8 ;
-- -----------------------------------------------------

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
  `answer_id` INT UNSIGNED NOT NULL,
  `expert_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `answer_id`, `expert_id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `answer_id_idx` (`answer_id` ASC) VISIBLE,
  INDEX `fk_Answer_User2_idx` (`expert_id` ASC) VISIBLE,
  CONSTRAINT `fk_option_id`
    FOREIGN KEY (`answer_id`)
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
  `сategory_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`, `survey_id`, `сategory_id`),
  INDEX `fk_Survey_has_Category_Survey1_idx` (`survey_id` ASC) VISIBLE,
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  INDEX `fk_SurveyCategory_Category1_idx` (`сategory_id` ASC) VISIBLE,
  CONSTRAINT `fk_survey_id1`
    FOREIGN KEY (`survey_id`)
    REFERENCES `DB_labs`.`Survey` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_SurveyCategory_Category1`
    FOREIGN KEY (`сategory_id`)
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

INSERT INTO category (name) VALUES ("psychology");
INSERT INTO category (name) VALUES ("math");
INSERT INTO category (name) VALUES ("music");
INSERT INTO category (name) VALUES ("machine learning");
INSERT INTO category (name, parent_id) VALUES ("deep learning", 2);
INSERT INTO category (name, parent_id) VALUES ("children psychology", 1);

INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 1, 2);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 6, 8);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4.5, 5, 8);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 5, 11);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (2, 1, 4);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (4.5, 2, 4);
INSERT INTO expertise (expertise_rate, category_id, user_id) VALUES (5, 3, 4);

INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Psychology Behavior Survey", "A survey focused on understanding behavioral patterns in adults.", 
        '2024-10-14 09:00:00', '2024-11-14 23:59:59', 0, 1, 3);
INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Discovery of the inheritance of Bach", "This survey explores the legacy and impact of Johann Sebastian Bach's compositions.", 
        '2024-10-14 14:00:00', '2024-11-14 23:59:59', 0, 1, 8);
INSERT INTO survey (title, description, creation_date, close_date, is_changable, is_active, owner_id) 
VALUES ("Children Psychology Survey", "A survey aimed at understanding the psychological development of children.", 
        '2024-10-14 10:00:00', '2024-12-01 23:59:59', 1, 1, 3);

INSERT INTO surveycategory (survey_id, сategory_id) VALUES (1, 1);
INSERT INTO surveycategory (survey_id, сategory_id) VALUES (2, 1);
INSERT INTO surveycategory (survey_id, сategory_id) VALUES (1, 4);

INSERT INTO question (header, description, survey_id) 
VALUES 
("How often do you experience stress?", "Please select the frequency that best matches your experience of stress.", 1),
("How would you describe your overall emotional well-being?", "Rate your emotional well-being on a scale of 1 to 5.", 1);

INSERT INTO question (header, description, survey_id) 
VALUES 
("How does the child interact with peers?", "Select the option that best describes the child’s social interactions.", 2),
("How does the child usually respond to stressful situations?", "Select the typical response the child has to stress.", 2);

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

INSERT INTO answer (answer_id, expert_id) VALUES (2, 2);
INSERT INTO answer (answer_id, expert_id) VALUES (3, 4);
INSERT INTO answer (answer_id, expert_id) VALUES (5, 2);
INSERT INTO answer (answer_id, expert_id) VALUES (6, 4);
INSERT INTO answer (answer_id, expert_id) VALUES (8, 4);
INSERT INTO answer (answer_id, expert_id) VALUES (12, 4);

INSERT INTO expertcomplaint (description, date, researcher_id, expert_id)
VALUES ('Complaint regarding expert feedback', '2024-10-14 09:30:00', 3, 4);
INSERT INTO surveycomplaint (description, date, survey_id, expert_id)
VALUES ('Complaint about survey data handling by expert', '2024-10-14 09:30:00', 1, 2); 
```

```mysql
-- Example SELECT queries
SELECT content FROM option_q WHERE question_id=5;
SELECT first_name, last_name FROM user WHERE company IS NULL AND is_admin=0;

SELECT content, first_name, last_name FROM option_q INNER JOIN answer INNER JOIN user WHERE option_q.id = answer.answer_id AND answer.expert_id = user.id;
SELECT name, first_name FROM expertise INNER JOIN user INNER JOIN category WHERE expertise_rate >=4 AND expertise.user_id = user.id AND category.id = expertise.category_id;
SELECT ec.description, first_name FROM expertcomplaint AS ec, user AS u WHERE ec.expert_id = u.id;
SELECT c.name, s.title FROM category AS c INNER JOIN survey AS s INNER JOIN surveycategory AS sc ON sc.survey_id = s.id AND sc.сategory_id = c.id;


SELECT user.first_name, survey.title, survey.description
FROM survey 
INNER JOIN user
ON survey.owner_id = user.id;

SELECT user.first_name, expertise_rate, category.name
FROM expertise
RIGHT JOIN category
ON expertise.category_id = category.id
INNER JOIN user
ON expertise.user_id = user.id
ORDER BY user.first_name, category.name;

SELECT survey.title, question.header
FROM question
INNER JOIN survey
ON question.survey_id = survey.id;

SELECT user.first_name, expertise.expertise_rate, category.name
FROM expertise
RIGHT JOIN category
ON expertise.category_id = category.id
INNER JOIN user
ON expertise.user_id = user.id
WHERE expertise.expertise_rate = 5;
```

```mysql
-- Example UPDATE queries
UPDATE user SET email="kate.pril@gmail.com" WHERE id=1;
UPDATE survey SET close_date = "2024-12-14 23:59:59" WHERE id = 3;
```

```mysql
-- Example DELETE queries
DELETE FROM option_q WHERE id=13;

DELETE s
FROM survey AS s INNER JOIN surveycomplaint AS sc
ON sc.survey_id = s.id;
```

- RESTfull сервіс для управління даними

