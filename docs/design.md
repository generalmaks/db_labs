# Проєктування бази даних

## Короткий зміст: 
- [модель бізнес-об'єктів](#BusinessObjectsModel)
- [ER-модель](#ERModel)
- [реляційна схема](#RelationalSchema)

 
<span id="BusinessObjectsModel"></span>
## Модель бізнес-об'єктів
**Модель бізнес-об'єктів** - це опис системи, в рамках якої відображаються всі об’єкти (сутності) даної системи. [[1]](https://economyandsociety.in.ua/journals/7_ukr/82.pdf)

```plantuml
@startuml

entity User #aaffaa
entity User.id #aaffaa
entity User.first_name #aaffaa
entity User.last_name #aaffaa
entity User.email #aaffaa
entity User.phone_number #aaffaa
entity User.password #aaffaa
entity User.expertise_rate #aaffaa
entity User.role_id #aaffaa
entity User.category_id #aaffaa

User.id -d-* User 
User.first_name -d-* User
User.last_name -d-* User
User.email -d-* User
User.phone_number -d-* User
User.password -d-* User
User.expertise_rate -d-* User
User.role_id -d-* User
User.category_id -d-* User

entity Role #FFDAB9
entity Role.id #FFDAB9
entity Role.name #FFDAB9

Role.id --* Role
Role.name --* Role

entity Survey #ADD8E6
entity Survey.id #ADD8E6
entity Survey.title #ADD8E6
entity Survey.description #ADD8E6
entity Survey.creation_time #ADD8E6
entity Survey.close_time #ADD8E6
entity Survey.is_changeable #ADD8E6
entity Survey.is_active #ADD8E6
entity Survey.category_id #ADD8E6
entity Survey.owner_id #ADD8E6

Survey.id -u-* Survey
Survey.title -u-* Survey
Survey.category_id -u-* Survey
Survey.description -u-* Survey
Survey.creation_time -u-* Survey
Survey.close_time -u-* Survey
Survey.is_changeable -u-* Survey
Survey.is_active -u-* Survey
Survey.owner_id -u-* Survey

entity Question #D8BFD8
entity Question.id #D8BFD8
entity Question.header #D8BFD8
entity Question.description #D8BFD8
entity Question.survey_id #D8BFD8

Question.id --* Question
Question.header --* Question
Question.description --* Question
Question.survey_id --* Question

entity Answer #FFC0CB
entity Answer.id #FFC0CB
entity Answer.content #FFC0CB
entity Answer.question_id #FFC0CB

Answer.id -d-* Answer 
Answer.content -r-* Answer
Answer.question_id -u-* Answer

entity Category #FFCC00
entity Category.id #FFCC00
entity Category.name #FFCC00

Category.id -l-* Category
Category.name -l-* Category

User "1  " -- "0..*" Survey
User "1                         " -l- "1..*" Role
User "0..*" -- "0..* " Category
Question "1..*" -r- "1" Survey
Answer "1..10" -r- "1" Question
Category "1..*  " -- "                                  0..*" Survey
  

@enduml
```
<span id="ERModel"></span>
## ER-модель
**ER-модель** описує сутності системи та визначає зв'язки між ними. [[2]](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)

```plantuml
@startuml

left to right direction
  
  entity "Role" {
    + id: INT
    + name: VARCHAR
  }
  
  entity "User" {
    + id: INT 
    + first_name: VARCHAR
    + last_name: VARCHAR
    + email: VARCHAR
    + phone_number: VARCHAR
    + password: VARCHAR
    + expertise_rate: DOUBLE
    + role_id: INT
    + category_id: INT
  }
  
  entity "Category" {
    + id: INT
    + name: VARCHAR
  }
  
  entity "Survey" {
    + id: INT
    + title: VARCHAR
    + description: TEXT
    + creation_time: DATETIME 
    + close_time: DATETIME
    + is_changeable: BOOLEAN
    + is_active: BOOLEAN
    + category_id: INT 
    + owner_id: INT
  }
  
  entity "Questions" {
    + id: INT
    + header: VARCHAR
    + description: TEXT
    + survey_id: INT
  }
  
  entity "Answers" {
    + id: INT
    + content: TEXT
    + question_id: INT
  }
  
  "User" ||--|{ "Role"
  "User" }o--o{ "Category"
  "User" ||--o{ "Survey"
  "Survey" ||--|{ "Questions"
  "Questions" ||--|{ "Answers"
  "Survey" }o--|{ "Category"
@enduml
```

<span id="RelationalSchema"></span>
## Реляційна схема
**Реляційна схема** - це набір таблиць, кожна з яких відповідає за одну з сутностей реляційної бази даних, та зв'язків між ними. Реляційна схема використовується для представлення реляційної бази даних. [[3]](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)

![Реляційна схема](https://github.com/user-attachments/assets/2e31fd4d-6137-484f-8565-f9547ea4ec5d)

## Посилання
1. [Бізнес-моделі підприємства: еволюція та класифікація](https://economyandsociety.in.ua/journals/7_ukr/82.pdf)
2. [Entity–relationship model](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)
3. [Relational Schemas](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)