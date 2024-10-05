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

entity User <<ENTITY>> #52f752
entity User.id <<NUMBER>> #aaffaa
entity User.first_name <<TEXT>> #aaffaa
entity User.last_name <<TEXT>> #aaffaa
entity User.email <<TEXT>> #aaffaa
entity User.phone_number <<TEXT>> #aaffaa
entity User.password <<TEXT>> #aaffaa
entity User.role_id <<NUMBER>> #aaffaa

User.id --* User 
User.first_name --* User
User.last_name --* User
User.email --* User
User.phone_number --* User
User.password --* User
User.role_id --* User

entity Role <<ENTITY>> #f59e51
entity Role.id <<NUMBER>> #FFDAB9
entity Role.name <<TEXT>> #FFDAB9

Role.id --* Role
Role.name --* Role

entity Survey <<ENTITY>> #448094
entity Survey.id <<NUMBER>> #ADD8E6
entity Survey.title <<TEXT>> #ADD8E6
entity Survey.description <<TEXT>> #ADD8E6
entity Survey.creation_date <<DATE>> #ADD8E6
entity Survey.close_date <<DATE>> #ADD8E6
entity Survey.is_changeable <<NUMBER>> #ADD8E6
entity Survey.is_active <<NUMBER>> #ADD8E6
entity Survey.owner_id <<NUMBER>> #ADD8E6

Survey.id --* Survey
Survey.title --* Survey
Survey.description --* Survey
Survey.creation_date --* Survey
Survey.close_date --* Survey
Survey.is_changeable --* Survey
Survey.is_active --* Survey
Survey.owner_id --* Survey

entity Question <<ENTITY>> #d147d1
entity Question.id <<NUMBER>> #D8BFD8
entity Question.header <<TEXT>> #D8BFD8
entity Question.description <<TEXT>> #D8BFD8
entity Question.survey_id <<NUMBER>> #D8BFD8

Question.id --* Question
Question.header --* Question
Question.description --* Question
Question.survey_id --* Question

entity Answer <<ENTITY>> #117d59
entity Answer.id <<NUMBER>> #1ee8a4
entity Answer.content <<TEXT>> #1ee8a4
entity Answer.question_id <<NUMBER>> #1ee8a4

Answer.id -d-* Answer 
Answer.content -r-* Answer
Answer.question_id -u-* Answer

entity Category <<ENTITY>> #ab8c0c
entity Category.id <<NUMBER>> #FFCC00
entity Category.name <<TEXT>> #FFCC00

Category.id --* Category
Category.name -* Category

entity SelectedAnswer <<ENTITY>> #f74564
entity SelectedAnswer.id <<NUMBER>> #FFC0CB
entity SelectedAnswer.user_id <<NUMBER>> #FFC0CB
entity SelectedAnswer.answer_id <<NUMBER>> #FFC0CB

SelectedAnswer.id --* SelectedAnswer
SelectedAnswer.user_id --* SelectedAnswer
SelectedAnswer.answer_id --* SelectedAnswer

entity Expertise
entity Expertise.id
entity Expertise.user_id
entity Expertise.category_id
entity Expertise.expertise_rate <<NUMBER>>

Expertise.id --* Expertise
Expertise.user_id --* Expertise
Expertise.category_id --* Expertise
Expertise.expertise_rate --* Expertise


User "1  " -- "0..*" Survey
User "1                         " -l- "1..*" Role
User "0..*" -- "0..* " Category
Question "1..*" -- "1" Survey
Answer "1..10" -- "1" Question
Category "1..*  " -- "                                  0..*" Survey
User "1" -- "0..*"  SelectedAnswer
Answer "1" -- "0..*" SelectedAnswer
Expertise "0..*" -- "0..1" User
Expertise "1..*" -- "0..1" Category

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
    + is_changeable: TINYINT
    + is_active: TINYINT
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

![Реляційна схема](https://github.com/user-attachments/assets/9b6d65f1-b248-401d-8b84-e6596b406229)


## Посилання
1. [Бізнес-моделі підприємства: еволюція та класифікація](https://economyandsociety.in.ua/journals/7_ukr/82.pdf)
2. [Entity–relationship model](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)
3. [Relational Schemas](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)
