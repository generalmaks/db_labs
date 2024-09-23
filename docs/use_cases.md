# Розроблення функціональних вимог до системи

## Загальна діаграма прецедентів
<center>

```plantuml
@startuml

    actor "Користувач" as User #ffed94
    actor "Дослідник" as Researcher #bffaa2
    actor "Експерт" as Expert #eacffa
    actor "Адміністратор системи" as Admin #94f1ff
    
    usecase "Створити акаунт" as UC_1.1
    usecase "Увійти в акаунт" as UC_1.2  
    usecase "Видалити акаунт" as UC_1.3
    usecase "Редагувати дані акаунта" as UC_1.4
    
    usecase "Управляти опитуваннями" as UC_2.1    
    usecase "Отримати аналітику опитування" as UC_2.4
    usecase "Переглянути створені опитування" as UC_2.5
    usecase "Редагувати категорії опитувань" as UC_2.6
    
    usecase "Пройти опитування" as UC_3.1
    usecase "Перегляд опитувань" as UC_3.2
    
    usecase "Видалити акаунт користувача" as UC_4.1
    usecase "Зв'язатись з користувачем" as UC_4.2
    usecase "Видалити опитування" as UC_4.3

    User --> UC_1.1
    User --> UC_1.2
    User --> UC_1.3
    User --> UC_1.4
    
    Researcher --|> User 
    Expert --|> User
    Admin --|> User
    
    Researcher --> UC_2.1
    Researcher -up-> UC_2.4
    Researcher -up-> UC_2.5
    Researcher -> UC_2.6
    
    Expert --> UC_3.1
    Expert --> UC_3.2
    
    
    Admin -up-> UC_4.1
    Admin -> UC_4.2
    Admin -up-> UC_4.3
    
    right footer
        Модель прецедентів.
        НТУУ КПІ ім.І.Сікорського
        Киів-2024
    end footer

@enduml
```
**Діаграма прецедентів**

</center>

## Діаграми use case
### Use case діаграма дослідника
<center style="
    border-radius:4px;
    border: 1px solid #cfd7e6;
    box-shadow: 0 1px 3px 0 rgba(89,105,129,.05), 0 1px 1px 0 rgba(0,0,0,.025);
    padding: 0em;"
    >

```plantuml
@startuml
    actor "Дослідник" as Researcher #bffaa2
    
    usecase "Управляти оптуваннями" as UC_1.1
    
    usecase "Створити опитування" as UC_1.1.1
    usecase "Закрити опитування" as UC_1.1.2
    usecase "Видалити опитування" as UC_1.1.3
    
    usecase "Отримати аналітику опитування" as UC_1.2
    usecase "Переглянути створені опитування" as UC_1.3
    usecase "Редагувати категорії опитувань" as UC_1.4
    
    usecase "Створити питання" as UC_1.1.1.1
    usecase "Додати категорію до опитування" as UC_1.1.1.2
    usecase "Змінити категорію опитування" as UC_1.1.1.3
    
    usecase "Підтверити дію" as UC_1.2.1
    
    usecase "Створити категорію опитувань" as UC_1.4.1
    usecase "Переглянути категорії опитувань" as UC_1.4.2
    
    Researcher -up-> UC_1.1
    Researcher -down-> UC_1.2
    Researcher -left-> UC_1.3
    Researcher -right-> UC_1.4
    
    UC_1.1.1 ..> UC_1.1 :extends
    UC_1.1.2 ..> UC_1.1 :extends
    UC_1.1.3 ..> UC_1.1 :extends
    
    UC_1.1.1.1 ..> UC_1.1.1 :extends
    UC_1.1.1.2 ..> UC_1.1.1 :extends
    UC_1.1.1.3 ..> UC_1.1.1 :extends
    
    UC_1.2.1 ..> UC_1.2 :extends
    UC_1.2.1 ..> UC_1.3 :extends
    
    UC_1.4.1 ..> UC_1.4 :extends
    UC_1.4.2 ..> UC_1.4 :extends
    
    right footer
        Модель прецедентів дослідника.
        НТУУ КПІ ім.І.Сікорського
        Киів-2024
    end footer
    
@enduml
```
**Діаграма прецедентів дослідника**

</center>