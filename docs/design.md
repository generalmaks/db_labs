# Проєктування бази даних

В рамках проєкту розробляється: 
- модель бізнес-об'єктів 
- ER-модель
- реляційна схема

## Модель бізнес-об'єктів

```plantuml
@startuml
entity User
entity User.id
entity User.firstName
entity User.lastName
entity User.email
entity User.role

User.id --* User
User.firstName --* User
User.lastName --* User
User.email --* User
User.role --* User

entity Role
entity Role.id
entity Role.name

Role.id --* Role
Role.name --* Role

entity Survey

entity Question

entity Answer

entity Category
entity Category.id
entity Category.name

Category.id --* Category
Category.name --* Category

@enduml
```
