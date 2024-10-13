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

left to right direction

entity User <<ENTITY >> #52f752
entity User.id <<NUMBER>> #aaffaa
entity User.first_name <<TEXT>> #aaffaa
entity User.last_name <<TEXT>> #aaffaa
entity User.email <<TEXT>> #aaffaa
entity User.phone_number <<TEXT>> #aaffaa
entity User.password <<TEXT>> #aaffaa
entity User.description <<TEXT>> #aaffaa
entity User.age <<NUMBER>> #aaffaa
entity User.gender <<TEXT>> #aaffaa
entity User.company <<TEXT>> #aaffaa
entity User.is_admin <<NUMBER>> #aaffaa

User.id -d-* User
User.first_name -d-* User
User.last_name -d-* User
User.email -d-* User
User.phone_number -d-* User
User.password -d-* User
User.description -d-* User
User.age -d-* User
User.gender -d-* User
User.company -d-* User
User.is_admin -d-* User

entity SurveyComplaint <<ENTITY>> #f59e51
entity SurveyComplaint.id <<NUMBER>> #FFDAB9
entity SurveyComplaint.description <<TEXT>> #FFDAB9
entity SurveyComplaint.date <<DATE>> #FFDAB9

SurveyComplaint.id -u-* SurveyComplaint
SurveyComplaint.description -u-* SurveyComplaint
SurveyComplaint.date -u-* SurveyComplaint

entity ExpertComplaint <<ENTITY>> #626b70
entity ExpertComplaint.id <<NUMBER>> #b9c3c9
entity ExpertComplaint.description <<TEXT>> #b9c3c9
entity ExpertComplaint.date <<DATE>> #b9c3c9

ExpertComplaint.id -u-* ExpertComplaint
ExpertComplaint.description -u-* ExpertComplaint
ExpertComplaint.date -u-* ExpertComplaint

entity Survey <<ENTITY>> #06bfbf
entity Survey.id <<NUMBER>> #9effff
entity Survey.title <<TEXT>> #9effff
entity Survey.description <<TEXT>> #9effff
entity Survey.creation_date <<DATE>> #9effff
entity Survey.close_date <<DATE>> #9effff
entity Survey.is_changeable <<NUMBER>> #9effff
entity Survey.is_active <<NUMBER>> #9effff

Survey.id -d-* Survey
Survey.title -d-* Survey
Survey.description -d-* Survey
Survey.creation_date -d-* Survey
Survey.close_date -d-* Survey
Survey.is_changeable -d-* Survey
Survey.is_active -d-* Survey

entity Question <<ENTITY>> #d147d1
entity Question.id <<NUMBER>> #D8BFD8
entity Question.header <<TEXT>> #D8BFD8
entity Question.description <<TEXT>> #D8BFD8

Question.id -d-* Question
Question.header -d-* Question
Question.description -d-* Question

entity Option <<ENTITY>> #117d59
entity Option.id <<NUMBER>> #1ee8a4
entity Option.content <<TEXT>> #1ee8a4

Option.id -d-* Option
Option.content -d-* Option

entity Category <<ENTITY>> #ab8c0c
entity Category.id <<NUMBER>> #FFCC00
entity Category.name <<TEXT>> #FFCC00

Category.id -u-* Category
Category.name -u-* Category

entity Answer <<ENTITY>> #f74564
entity Answer.id <<NUMBER>> #FFC0CB

Answer.id -d-* Answer

entity Expertise <<ENTITY>> #6f44c9
entity Expertise.id <<NUMBER>> #a785ed
entity Expertise.expertise_rate <<NUMBER>> #a785ed

Expertise.id -u-* Expertise
Expertise.expertise_rate -u-* Expertise

entity SurveyCategory <<ENTITY>> #448094
entity SurveyCategory.id <<NUMBER>> #ADD8E6

SurveyCategory.id --* SurveyCategory


User "1, 1" -- "0 .. *" Survey
User "1, 1" -- "0 .. *" ExpertComplaint :expert
Survey "1, 1" -- "0 .. *" Question
Survey "1, 1" -- "0 .. *" SurveyCategory
Category "1, 1" -- "1 .. *" SurveyCategory
Survey "1, 1" -- "0 .. *" SurveyComplaint
Question "1, 1" -- "0 .. *" Option
Option "1, 1" -- "0 .. *" Answer
Answer "0 .. *" -d- "1, 1" User
User "1, 1" -- "0 .. *" SurveyComplaint
ExpertComplaint "0 .. *" -u- "1, 1" User :researcher
User "1, 1" -d- "0 .. *" Expertise
Expertise "0 .. *" -- "1, 1" Category
Category "0 .. *" --o "0, 1" Category

@enduml
```

<span id="ERModel"></span>
## ER-модель
**ER-модель** описує сутності системи та визначає зв'язки між ними. [[2]](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)

```plantuml
@startuml

left to right direction
  
  
   entity "User" {
    + id: INT 
    + first_name: VARCHAR
    + last_name: VARCHAR
    + email: VARCHAR
    + phone_number: VARCHAR
    + password: VARCHAR 
    + description: TEXT
    + age: TINYINT
    + gender: VARCHAR
    + company: VARCHAR
    + is_admin: TINYINT 
  }
  
     entity "Survey" {
    + id: INT
    + title: VARCHAR
    + description: TEXT 
    + creation_date: DATETIME
    + close_date: DATETIME
    + is_changeable: TINYINT
    + is_active: TINYINT
    + owner_id: INT
  }
  
     entity "Question" {
    + id: INT
    + header: VARCHAR 
    + description: TEXT
    + survey_id: INT
  }

   entity "Option" {
    + id: INT
    + content: TEXT
    + questiron_id: INT    
  }
  
    entity "Answer" {
    + id: INT
    + option_id: INT
    + expert_id: INT  
  }
  
    entity "SurveyComplaint" {
    + id: INT
    + description: TEXT
    + date: DATETIME
    + survey_id: INT
    + expert_id: INT
  }
  
    entity "SurveyCategory"{
    + id: INT
    + survey_id: INT
    + category_id: INT
    }
  
    entity "ExpertComplaint" {
    + id: INT
    + description: TEXT
    + date: DATETIME
    + researcher_id: INT
    + expert_id: INT   
  }
  
  
   entity "Expertise" {
    + id: INT 
    + expertise_rate: DOUBLE
    + user_id: INT
    + category_id: INT
  }
  
  
   entity "Category" {
    + id: INT
    + name: VARCHAR 
    + parent_id: INT
  }
  
    "User" ||--o{ "Survey" :owner
    "User" ||--o{ "SurveyComplaint"
    "User" ||--o{ "ExpertComplaint" : expert
    "User" ||--o{ "ExpertComplaint" : researcher
    "User" ||--o{ "Expertise"
    
    "Survey" ||--o{ "Question"
    "Survey" ||--o{ "SurveyCategory"
    "Survey" ||--o{ "SurveyComplaint"
    
    "Expertise" }o--|| "Category"

    "Question" ||--o{ "Option"
    
    "Option" ||--o{ "Answer"
    
    "Answer" }o--|| "User"
    
    "SurveyCategory" }|--|| "Category"
    
    "Category" }o--o| "Category"

@enduml
```

<span id="RelationalSchema"></span>
## Реляційна схема
**Реляційна схема** - це набір таблиць, кожна з яких відповідає за одну з сутностей реляційної бази даних, та зв'язків між ними. Реляційна схема використовується для представлення реляційної бази даних. [[3]](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)

![Реляційна схема](https://github.com/user-attachments/assets/11482303-861b-4143-84f0-69268ed9e11d)





## Посилання
1. [Бізнес-моделі підприємства: еволюція та класифікація](https://economyandsociety.in.ua/journals/7_ukr/82.pdf)
2. [Entity–relationship model](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)
3. [Relational Schemas](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)
