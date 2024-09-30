# Проєктування бази даних

В рамках проєкту розробляється: 
- модель бізнес-об'єктів 
- ER-модель
- реляційна схема

## Модель бізнес-об'єктів

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
entity Survey.category_id #ADD8E6
entity Survey.owner_id #ADD8E6

Survey.id -u-* Survey
Survey.title -u-* Survey
Survey.category_id -u-* Survey
Survey.description -u-* Survey
Survey.creation_time -u-* Survey
Survey.close_time -u-* Survey
Survey.is_changeable -u-* Survey
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
User "1                         " -l- "1" Role
User "1" -- "0..* " Category
Question "1..*" -r- "1" Survey
Answer "1..10" -r- "1" Question
Category "1..*  " -- "  0..*" Survey
  

@enduml
```
