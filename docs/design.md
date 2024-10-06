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
entity User.is_admin <<NUMBER>> #aaffaa

User.id --* User 
User.first_name --* User
User.last_name --* User
User.email --* User
User.phone_number --* User
User.password --* User
User.is_admin --* User

entity Researcher <<ENTITY>> #0044b0
entity Researcher.id <<NUMBER>> #699ff5
entity Researcher.company <<TEXT>> #699ff5
entity Researcher.user_id <<NUMBER>> #699ff5

Researcher.id --* Researcher
Researcher.company --* Researcher
Researcher.user_id --* Researcher

entity Expert <<ENTITY>> #c20000
entity Expert.id <<NUMBER>> #ffa6a6
entity Expert.description <<TEXT>> #ffa6a6
entity Expert.age <<NUMBER>> #ffa6a6
entity Expert.gender <<TEXT>> #ffa6a6
entity Expert.user_id <<NUMBER>> #ffa6a6

Expert.id --* Expert
Expert.description --* Expert
Expert.age --* Expert
Expert.gender --* Expert
Expert.user_id --* Expert

entity SurveyComplaint <<ENTITY>> #f59e51
entity SurveyComplaint.id <<NUMBER>> #FFDAB9
entity SurveyComplaint.description <<TEXT>> #FFDAB9
entity SurveyComplaint.date <<DATE>> #FFDAB9
entity SurveyComplaint.expert_id <<NUMBER>> #FFDAB9
entity SurveyComplaint.survey_id <<NUMBER>> #FFDAB9

SurveyComplaint.id --* SurveyComplaint
SurveyComplaint.description --* SurveyComplaint
SurveyComplaint.date --* SurveyComplaint
SurveyComplaint.expert_id --* SurveyComplaint
SurveyComplaint.survey_id --* SurveyComplaint

entity ExpertComplaint <<ENTITY>> #626b70
entity ExpertComplaint.id <<NUMBER>> #b9c3c9
entity ExpertComplaint.description <<TEXT>> #b9c3c9
entity ExpertComplaint.date <<DATE>> #b9c3c9
entity ExpertComplaint.researcher_id <<NUMBER>> #b9c3c9
entity ExpertComplaint.expert_id <<NUMBER>> #b9c3c9

ExpertComplaint.id --* ExpertComplaint
ExpertComplaint.description --* ExpertComplaint
ExpertComplaint.date --* ExpertComplaint
ExpertComplaint.researcher_id --* ExpertComplaint
ExpertComplaint.expert_id --* ExpertComplaint

entity Survey <<ENTITY>> #448094
entity Survey.id <<NUMBER>> #ADD8E6
entity Survey.title <<TEXT>> #ADD8E6
entity Survey.description <<TEXT>> #ADD8E6
entity Survey.creation_date <<DATE>> #ADD8E6
entity Survey.close_date <<DATE>> #ADD8E6
entity Survey.is_changeable <<NUMBER>> #ADD8E6
entity Survey.is_active <<NUMBER>> #ADD8E6
entity Survey.owner_id <<NUMBER>> #ADD8E6

Survey.id -u-* Survey
Survey.title -u-* Survey
Survey.description -u-* Survey
Survey.creation_date -u-* Survey
Survey.close_date -u-* Survey
Survey.is_changeable -u-* Survey
Survey.is_active -u-* Survey
Survey.owner_id -u-* Survey

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

Answer.id -u-* Answer 
Answer.content -u-* Answer
Answer.question_id -u-* Answer

entity Category <<ENTITY>> #ab8c0c
entity Category.id <<NUMBER>> #FFCC00
entity Category.name <<TEXT>> #FFCC00

Category.id -u-* Category
Category.name -u-* Category

entity SelectedAnswer <<ENTITY>> #f74564
entity SelectedAnswer.id <<NUMBER>> #FFC0CB
entity SelectedAnswer.user_id <<NUMBER>> #FFC0CB
entity SelectedAnswer.answer_id <<NUMBER>> #FFC0CB

SelectedAnswer.id --* SelectedAnswer
SelectedAnswer.user_id --* SelectedAnswer
SelectedAnswer.answer_id --* SelectedAnswer

entity Expertise <<ENTITY>> #6f44c9
entity Expertise.id <<NUMBER>> #a785ed
entity Expertise.user_id <<NUMBER>> #a785ed
entity Expertise.category_id <<NUMBER>> #a785ed
entity Expertise.expertise_rate <<NUMBER>> #a785ed

Expertise.id -u-* Expertise
Expertise.user_id -u-* Expertise
Expertise.category_id -u-* Expertise
Expertise.expertise_rate -u-* Expertise


User "1  " -d- "0" Researcher
User "1  " -d- "0" Expert
Researcher "1, 1" -d- "0 .. *" Survey
Expert "1, 1" -l- "0 .. *" SurveyComplaint
Researcher "1, 1" -r- "0 .. *" ExpertComplaint
Survey "1, 1" -d- "1 .. *" Question
Survey "0 .. *" -r- "1 .. *" Category
Survey "1 .. 1" -l- "0 .. *" SurveyComplaint
Question "1, 1" -d- "1 .. 10" Answer
Answer "1 .. *" -d- "1, 1" SelectedAnswer
SelectedAnswer "1, 1" -u- "1 .. *" Expert
Expert "0 .. *" -d- "1 .. *" Category
Expert "0 .. 1" -l- "0 .. *" ExpertComplaint
Expert "1 .. *" -d- "1, 1" Expertise
Expertise "1, 1" -l- "0 .. *" Category

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
    + is_admin: TINYINT
    + first_name: VARCHAR
    + last_name: VARCHAR
    + email: VARCHAR
    + phone_number: VARCHAR
    + password: VARCHAR  
  }
  
  entity "Researcher" {
    + id: INT 
    + user_id: INT
    + company: VARCHAR
  }
  
  entity "Expert" {
    + age: INT 
    + gender: VARCHAR
    + user_id: INT
    + id: INT
    + description: VARCHAR
  }
  
   entity "ExpertComplaint" {
    + id: INT
    + expert_id: INT
    + researcher_id: INT
    + date: INT
    + description: VARCHAR 
  }
  
  entity "SurveyComplaint" {
    + id: INT
    + researcher_id: INT
    + survey_id: INT
    + date: INT
    + description: VARCHAR
  }
  
   entity "Survey" {
    + id: INT
    + owner_id: INT
    + creation_date: INT
    + close_date: INT
    + is_changeable: TINYINT
    + is_active: TINYINT
    + title: VARCHAR 
    + description: VARCHAR  
  }
  
   entity "Question" {
    + id: INT
    + survey_id: INT
    + header: VARCHAR 
    + description: VARCHAR
  }
  
  entity "Answer" {
    + id: INT 
    + question_id: INT
    + content: VARCHAR
  }
  
  entity "Category" {
    + id: INT
    + name: VARCHAR 
  }
  
   entity "Expertise" {
    + id: INT 
    + user_id: INT
    + category_id: INT
    + expertise_rate: INT
  }
  
  entity "SelectedAnswer" {
    + id: INT 
    + user_id: INT
    + answer_id: INT
  }
    
  
  "User" -- "Researcher"
  "User" -- "Expert"
  
  "Researcher" -u- "SurveyComplaint" 
  "Researcher" -- "ExpertComplaint"
  "Researcher" -- "Survey"
  
  "Expert" -- "ExpertComplaint"
  "Expert" -- "Category"
  "Expert" -- "Expertise"
  "Expert" -- "SelectedAnswer"
  
  "Survey" -- "Question"
  "Survey" -- "Category"
  
  "Question" -- "Answer"
  
  "Answer" -- "SelectedAnswer"
  

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
