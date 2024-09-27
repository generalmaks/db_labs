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
    
    usecase "Дії з акаунтами користувачів" as UC_4.1
    usecase "Зв'язатись з користувачем" as UC_4.2
    usecase "Дії з опитуваннями" as UC_4.3

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
**Діаграми прецедентів бізнес акторів**

</center>

## Діаграми use case
### Use case діаграма дослідника
<center>

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
### Діаграма use case для незареєстрованного користувача

<center>

```plantuml
@startuml
    actor "Користувач" as User #ffed94

    usecase "Створити акаунт" as UC_1.1
    usecase "Увійти в акаунт" as UC_1.2  
    usecase "Видалити акаунт" as UC_1.3
    usecase "Редагувати дані акаунта" as UC_1.4
    
    usecase "Зареєструватись за допомогою пошти" as UC_1.1.1

    usecase "Відновити пароль" as UC_1.2.1
    
    usecase "Підтвердити дію" as UC_1.34.1
    
    usecase "Змінити пароль" as UC_1.4.1
    usecase "Змінити особисті дані" as UC_1.4.2
    
    User -up-> UC_1.1
    User -right-> UC_1.2
    User -down-> UC_1.3
    User -left-> UC_1.4
    
    UC_1.1.1 ..> UC_1.1 :extends
    UC_1.2.1 .left.> UC_1.2 :extends
    UC_1.34.1 .right.> UC_1.3 :extends
    UC_1.34.1 .up.> UC_1.4 :extends
    UC_1.4.1 .right.> UC_1.4 :extends
    UC_1.4.2 ..> UC_1.4 :extends
    
    right footer
        Модель прецедентів користувача.
        НТУУ КПІ ім.І.Сікорського
        Київ-2024
    end footer
    
@enduml
```

**Діаграма прецедентів користувача**

</center>

### Діаграма use case для адміністратора

<center>

```plantuml
@startuml
actor "Адміністратор системи" as Admin #94f1ff

    usecase "Зв'язатись з користувачем" as UC_1.1
    usecase "Дії з акаунтами \nкористувачів" as UC_1.2
    usecase "Дії з опитуваннями" as UC_1.3
    
    usecase "Обрати спосіб зв'язку" as UC_1.1.1
    usecase "Відстежити статус \nвідповіді" as UC_1.1.2
    usecase "Надіслати повідомлення" as UC_1.1.3
    
    usecase "Видалити акаунт \nкористувача" as UC_1.2.1
    usecase "Відновити акаунт \nкористувача" as UC_1.2.2
    
    usecase "Видалити опитування" as UC_1.3.1
    usecase "Відновити опитування" as UC_1.3.2
    
    usecase "Підтвердити дію" as UC_1.2.12
    usecase "Підтвердити дію" as UC_1.3.12
    
    Admin -down-> UC_1.1
    Admin -right-> UC_1.2
    Admin -left-> UC_1.3
    
    UC_1.1.1 .up.> UC_1.1 :extends
    UC_1.1.2 .left.> UC_1.1 :extends
    UC_1.1.3 .right.> UC_1.1 :extends
    
    UC_1.2.1 ..> UC_1.2 :extends
    UC_1.2.2 ..> UC_1.2 :extends
    
    UC_1.3.1 ..> UC_1.3 :extends
    UC_1.3.2 ..> UC_1.3 :extends
    
    UC_1.2.12 ..> UC_1.2.1 :extends
    UC_1.2.12 ..> UC_1.2.2 :extends
    
    UC_1.3.12 ..> UC_1.3.1 :extends
    UC_1.3.12 ..> UC_1.3.2 :extends
        
    right footer
        Модель прецедентів адміністратора системи.
        НТУУ КПІ ім. І.Сікорського
        Київ-2024
    end footer

@enduml
```

**Діаграма прецедентів адміністратора**

</center>

### Діаграма use case для експерта

<center>

```plantuml
@startuml
actor "Експерт" as Expert #eacffa

    usecase "Пройти опитування" as UC_1.1
    usecase "Перегляд опитувань" as UC_1.2
    
    usecase "Завершити опитування" as UC_1.1.1
    usecase "Змінити відповідь" as UC_1.1.2
    
    usecase "Відфільтрувати опитування" as UC_1.2.1
    
    usecase "Підтвердити дію" as UC_1.1.1.1
    usecase "Зберегти" as UC_1.1.2.1
    
    Expert -left-> UC_1.1
    Expert -right-> UC_1.2
    
    UC_1.1.1 .up.> UC_1.1 :extends
    UC_1.1.2 ..> UC_1.1 :extends
    
    UC_1.2.1 .left.> UC_1.2 :extends
    
    UC_1.1.1.1 .>> UC_1.1.1 :extends
    UC_1.1.2.1 .>> UC_1.1.2 :extends
        
    right footer
        Модель прецедентів експерта.
        НТУУ КПІ ім. І.Сікорського
        Київ-2024
    end footer

@enduml
```

**Діаграма прецедентів експерта**

</center>