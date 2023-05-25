# Тестовое задание
### Описание задачи
Имеются два поставщика, которые с помощью HTTP API предоставляют методы для осуществления поиска маршрутов.
Каждый маршрут характеризуется следующими базовыми параметрами:
  - Точка старта
  - Точка прибытия
  - Дата\время старта
  - Дата\время прибытия
  - Цена маршрута
  - TimeToLive для маршрута с такой ценой

Поставщики имеют различные контракты для поиска маршрутов:
- ProviderOneSearchRequest \ ProviderOneSearchResponse (HTTP POST http://provider-one/api/v1/search)
- ProviderTwoSearchRequest \ ProviderTwoSearchResponse (HTTP POST http://provider-two/api/v1/search)

Поставщики так же имеют метод для проверки их работоспособности на данные момент (поставщик может быть недоступен в момент выполнения поиска).
Пусть интерфейсы методов будут одинаковыми:

HTTP GET http://provider-one/api/v1/ping
HTTP GET http://provider-two/api/v1/ping
  - HTTP 200 if provider is ready
  - HTTP 500 if provider is down
  

Необходимо реализовать HTTP API, которое позволит выполнять аггрегированый поиск с фильтрацией, с помощью данных поставщиков (ISearchService):
- Request\response для API соответственно SearchRequest\SearchResponse.
- API должно позволять проверить свою текущую доступность (аналогично каждому из поставщиков).

Так же:
- API должно иметь свой кэш для дальнейшей работы с маршрутами по Route->Guid.
- API должно уметь производить поиск только в рамках закэшированных данных (SearchRequest -> Filters -> OnlyCached).

### Изменения в реализаци
Что бы пользователь или сервис не ожидал ответа по поиску(учитывая что провайдеров может быть 10-20 и ответы от них часто бывают долгими), я принял решение разделить /api/v1/search на две части:
  - POST(ответ SearchId Guid) (создает поиск и отправляет асинхронный запрос в шину через RabbitMq на опрос всех провайдеров и сохранение результатов)
  - GET (запрашивает результаты поиска по SearchId Guid, а так же состояние поиска (Создан/Запущен/Выполнен)) 

### Как запустить
>docker compose up

### Что использовал
- Mapster
- EF
- Refit
- MassTransit
- Scrutor
- Postgres
- RabbitMQ
- Bogus

### Что бы добавил
- Взаимодействие с ef переписал бы на паттерн репозиторий
- Написал бы Unit-тесты, использовал бы: xUnit, FakeItEasy, FluentAssertions
- Добавил бы polly
- Добавил бы логирование в Elastic через Serilog
- Метрики для взаимодействия с провайдерами
- HelthCheck для провайдеров

### Если ты досмотрел до конца тестовое, оставь пожалуйста фидбек HR, это поможет мне дальше развиваться
