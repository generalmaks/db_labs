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
entity User.is_admin <<NUMBER>> #aaffaa

User.id -d-* User
User.first_name -d-* User
User.last_name -d-* User
User.email -d-* User
User.phone_number -d-* User
User.password -d-* User
User.is_admin -d-* User

entity Researcher <<ENTITY>> #0044b0
entity Researcher.id <<NUMBER>> #699ff5
entity Researcher.company <<TEXT>> #699ff5

Researcher.id -d-* Researcher
Researcher.company -d-* Researcher

entity Expert <<ENTITY>> #47442e
entity Expert.id <<NUMBER>> #858063
entity Expert.description <<TEXT>> #858063
entity Expert.age <<NUMBER>> #858063
entity Expert.gender <<TEXT>> #858063

Expert.id -d-* Expert
Expert.description -d-* Expert
Expert.age -d-* Expert
Expert.gender -d-* Expert

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

entity Survey <<ENTITY>> #448094
entity Survey.id <<NUMBER>> #ADD8E6
entity Survey.title <<TEXT>> #ADD8E6
entity Survey.description <<TEXT>> #ADD8E6
entity Survey.creation_date <<DATE>> #ADD8E6
entity Survey.close_date <<DATE>> #ADD8E6
entity Survey.is_changeable <<NUMBER>> #ADD8E6
entity Survey.is_active <<NUMBER>> #ADD8E6

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

entity Answer <<ENTITY>> #117d59
entity Answer.id <<NUMBER>> #1ee8a4
entity Answer.content <<TEXT>> #1ee8a4

Answer.id -d-* Answer
Answer.content -d-* Answer

entity Category <<ENTITY>> #ab8c0c
entity Category.id <<NUMBER>> #FFCC00
entity Category.name <<TEXT>> #FFCC00

Category.id -u-* Category
Category.name -u-* Category

entity SelectedAnswer <<ENTITY>> #f74564
entity SelectedAnswer.id <<NUMBER>> #FFC0CB

SelectedAnswer.id -d-* SelectedAnswer

entity Expertise <<ENTITY>> #6f44c9
entity Expertise.id <<NUMBER>> #a785ed
entity Expertise.expertise_rate <<NUMBER>> #a785ed

Expertise.id -u-* Expertise
Expertise.expertise_rate -u-* Expertise


Researcher "0, 1" -u- "1, 1" User
Expert "0, 1" -u- "1, 1" User
Researcher "1, 1" -d- "0 .. *" Survey
Researcher "1, 1" -d- "0 .. *" ExpertComplaint
Survey "1, 1" -d- "1 .. *" Question
Survey "0 .. *" -d- "1 .. *" Category
Survey "1, 1" -d- "0 .. *" SurveyComplaint
Question "1, 1" -u- "1 .. 10" Answer
Answer "1, 1" -d- "0 .. *" SelectedAnswer
SelectedAnswer "0 .. *" -d- "1, 1" Expert
Expert "1, 1" -d- "0 .. *" SurveyComplaint
ExpertComplaint "0 .. *" -u- "1, 1" Expert
Expert "1, 1" -d- "1 .. *" Expertise
Expertise "0 .. *" -u- "1, 1" Category

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
    + is_admin: TINYINT
  }
  
  
  entity "Researcher" {
    + id: INT 
    + company: VARCHAR
    + user_id: INT
    
  }
  
  entity "Expert" {
    + id: INT
    + description: VARCHAR
    + age: INT 
    + gender: VARCHAR
    + user_id: INT
  }
  
  
   entity "ExpertComplaint" {
    + id: INT
    + description: VARCHAR
    + date: DATETIME
    + researcher_id: INT
    + expert_id: INT   
  }
  
  entity "SurveyComplaint" {
    + id: INT
    + description: VARCHAR
    + date: INT
    + researcher_id: INT
    + expert_id: INT    
  }
  
   entity "Survey" {
    + id: INT
    + title: VARCHAR
    + description: VARCHAR 
    + creation_date: DATETIME
    + close_date: DATETIME
    + is_changeable: TINYINT
    + is_active: TINYINT
    + owner_id: INT
  }

   entity "Question" {
    + id: INT
    + header: VARCHAR 
    + description: VARCHAR
    + survey_id: INT
  }
  
  entity "Answer" {
    + id: INT 
    + content: VARCHAR
    + question_id: INT
    
  }
  
  entity "Category" {
    + id: INT
    + name: VARCHAR 
  }

   entity "Expertise" {
    + id: INT 
    + expertise_rate: INT
    + user_id: INT
    + category_id: INT
  }
  
  entity "SelectedAnswer" {
    + id: INT 
    + user_id: INT
    + answer_id: INT
  }
    
  
  "User" ||--o| "Researcher"
  "User" ||--o| "Expert"
   
  "Researcher" ||--o{ "ExpertComplaint"
  "Researcher" ||--o{ "Survey"
 
  "Expert" |o--o{ "ExpertComplaint"
  "Expert" }o--|{ "Category"
  "Expert" }|--|| "Expertise"
  "Expert" }|--|| "SelectedAnswer"
  "Expert" ||--o{ "SurveyComplaint"
  
  "SurveyComplaint" }o--|| "Survey" 
  
  "Survey" ||--|{ "Question"
  "Survey" }o--|{ "Category"
  
  "Question" ||--|{ "Answer"
  
  "SelectedAnswer" ||--|{ "Answer"
  
  "Expertise" }o--|{ "Category"

  

@enduml
```

<span id="RelationalSchema"></span>
## Реляційна схема
**Реляційна схема** - це набір таблиць, кожна з яких відповідає за одну з сутностей реляційної бази даних, та зв'язків між ними. Реляційна схема використовується для представлення реляційної бази даних. [[3]](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)

![Реляційна схема](https://github.com/user-attachments/assets/1aee78d5-a66b-456c-92b1-e7f1ab6a3ecf)





## Посилання
1. [Бізнес-моделі підприємства: еволюція та класифікація](https://economyandsociety.in.ua/journals/7_ukr/82.pdf)
2. [Entity–relationship model](https://en.wikipedia.org/wiki/Entity%E2%80%93relationship_model)
3. [Relational Schemas](https://www.sciencedirect.com/topics/computer-science/relational-schema#:~:text=A%20relational%20schema%20is%20a,applications%20belong%20to%20one%20schema.)
