Текст задания:
Задание на разработку десктопного приложения:
Реализовать десктопное приложение для работы с абонентами телефонной компании.

В базе данных определить следующие основные таблицы:

Таблица Abonent для хранения информации об абонентах (фио - обязательно);
Таблица Address для хранения адресов абонентов;
Таблица PhoneNumber для хранения номеров (учесть, что существует 3 типа номера - домашний, рабочий, мобильный);
Таблица Streets для хранения обслуживаемых компанией улиц.

Требования к возможностям приложения:

Основное окно - вывод информации об абонентах в формате таблицы и набор кнопок для прочих возможностей.
Ожидаемый набор кнопок:
- "Поиск" - вызов модального окна "Поиск по номеру"
- "Выгрузить CSV" - для запуска механизма выгрузки CSV
- "Улицы" - для вызова модального окна "Улицы"
Ожидаемые колонки в отображаемой таблицы:
- ФИО абонента
- Улица
- Номер дома
- Номер телефона (домашний)
- Номер телефона (рабочий)
- Номер телефона (мобильный)
Предусмотреть механизмы фильтрации и сортировки отображаемой таблицы по всем колонкам.
Модальное окно "Поиск по номеру" - содержит текстовое поле для ввода номера.
При успехе поиска ожидается вывод в таблице только совпавших по критерию поиска абонентов, при отсутствии совпадений ожидается информативное окно с текстом "Нет абонентов, удовлетворяющих критерию поиска".
Модальное окно "Улицы" отображает информацию об обслуживаемых улицах и количестве абонентов на каждой из них в табличном формате.
Кнопка "Выгрузить CSV" запускает механизм формирования файла report_{текущая дата и время}.csv, в котором содержится информация из таблицы основного окна с учётом фильтров и сортировки.

Особые требования:

В качестве фронтенда использовать WPF
Использовать для работы с БД dapper.
БД - любая
Использовать паттерн MVVM
Результат загрузить в публичный репозиторий GitHub или GitLab

В качестве БД была выбранна MS SQL Server, из не указанного в задании были сделанны кнопки добавления, обновления, удаления абонентов и соответствующие модальные окна. 
