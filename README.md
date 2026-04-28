# MiniStoreSolution

`MiniStoreSolution` - это учебный проект мини-магазина на `ASP.NET Core Web API`, построенный по многослойной архитектуре.

Проект демонстрирует:
- разделение на слои `API / Application / Domain / Infrastructure`
- работу с `Entity Framework Core` по подходу `Code First`
- миграции и автоматическое применение схемы БД
- CRUD для категорий, товаров и заказов
- связи `one-to-many` и `many-to-many`
- использование `DTO`, `record`, `async/await`, `LINQ`, `Generics`
- валидацию через `Data Annotations`
- глобальную обработку ошибок через `Middleware`
- Swagger для тестирования API

## Стек

- `ASP.NET Core Web API`
- `Entity Framework Core`
- `PostgreSQL`
- `Swagger / OpenAPI`
- встроенный `Dependency Injection`
- встроенный `Logging`

## Структура решения

- `MiniStore.API` - контроллеры, конфигурация, middleware, запуск приложения
- `MiniStore.Application` - бизнес-логика, DTO, интерфейсы, сервисы
- `MiniStore.Domain` - доменные сущности
- `MiniStore.Infrastructure` - `DbContext`, репозитории, миграции, начальные данные

## Архитектура

Проект использует многослойную архитектуру:

1. `API` принимает HTTP-запросы и возвращает ответы клиенту.
2. `Application` содержит бизнес-логику и контракты.
3. `Domain` описывает сущности предметной области.
4. `Infrastructure` отвечает за доступ к данным и работу с БД.

Регистрация зависимостей выполняется через встроенный контейнер `DI` в `Program.cs`.

## Модель данных

### Category
- `Id`
- `Name`

### Product
- `Id`
- `Name`
- `Price`
- `CategoryId`

### Order
- `Id`
- `CreatedAt`
- `OrderProducts`

### OrderProduct
- `OrderId`
- `ProductId`
- `Quantity`

## Связи между сущностями

- `Category -> Product` : один ко многим
- `Order <-> Product` : многие ко многим через промежуточную сущность `OrderProduct`

## Возможности API

### Categories
- `GET /api/category`
- `GET /api/category/{id}`
- `POST /api/category`
- `PUT /api/category/{id}`
- `DELETE /api/category/{id}`

### Products
- `GET /api/product`
- `GET /api/product/{id}`
- `POST /api/product`
- `PUT /api/product/{id}`
- `DELETE /api/product/{id}`

### Orders
- `GET /api/order`
- `GET /api/order/{id}`
- `POST /api/order`
- `PUT /api/order/{id}`
- `DELETE /api/order/{id}`

## DTO и валидация

Для входных и выходных данных используются `DTO` на базе `record`.

В проекте включена валидация через `Data Annotations`:
- имя категории и товара обязательно
- длина имени ограничена
- цена должна быть больше `0`
- заказ должен содержать хотя бы один товар
- `ProductId` и `Quantity` должны быть больше `0`

При ошибке валидации API возвращает `400 Bad Request` в формате `ValidationProblemDetails`.

## Обработка ошибок

В проекте используется глобальный `ExceptionMiddleware`.

Если в приложении возникает необработанное исключение, API возвращает `500 Internal Server Error` в едином формате `ProblemDetails`.

Пример ответа:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
  "title": "Server error.",
  "status": 500,
  "detail": "An unexpected error occurred.",
  "instance": "/api/product/999",
  "traceId": "..."
}
```

## Swagger

Swagger подключен для тестирования API.

После запуска проекта документация доступна по адресу:

- `http://localhost:5207/swagger`
- `https://localhost:7272/swagger`

## Начальные данные

При старте приложения автоматически:
- применяются миграции
- добавляются стартовые данные, если таблицы пустые

Seed-данные:
- категории: `Electronics`, `Books`
- товары: `Laptop`, `Book: C# Basics`

## Требования для запуска

- `.NET SDK 10`
- `PostgreSQL`

## Настройка строки подключения

Строка подключения находится в файле:

- [appsettings.json](C:/Users/user/source/repos/MiniStoreSolution/MiniStore.API/appsettings.json)

Текущее имя строки:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=ministoredb;Username=postgres;Password=1234"
}
```

Перед запуском убедитесь, что PostgreSQL запущен и база доступна по этим параметрам, либо измените строку подключения под свою среду.

## Запуск проекта

### 1. Восстановить зависимости

```bash
dotnet restore
```

### 2. Запустить API

```bash
dotnet run --project MiniStore.API
```

### 3. Открыть Swagger

Откройте:

- `http://localhost:5207/swagger`

или

- `https://localhost:7272/swagger`

## Миграции

Миграции находятся в проекте `MiniStore.Infrastructure`.

Если потребуется создать новую миграцию:

```bash
dotnet ef migrations add MigrationName --project MiniStore.Infrastructure --startup-project MiniStore.API
```

Применить миграции вручную:

```bash
dotnet ef database update --project MiniStore.Infrastructure --startup-project MiniStore.API
```

Также при запуске приложения вызывается `context.Database.Migrate()`, поэтому схема БД обновляется автоматически.

## Примеры запросов

### Создать категорию

```http
POST /api/category
Content-Type: application/json
```

```json
{
  "name": "Accessories"
}
```

### Создать товар

```http
POST /api/product
Content-Type: application/json
```

```json
{
  "name": "Mouse",
  "price": 25.5,
  "categoryId": 1
}
```

### Создать заказ

```http
POST /api/order
Content-Type: application/json
```

```json
{
  "items": [
    {
      "productId": 1,
      "quantity": 2
    },
    {
      "productId": 2,
      "quantity": 1
    }
  ]
}
```

## Что реализовано с точки зрения учебных требований

- многослойная архитектура
- `DI`
- `EF Core Code First`
- миграции
- `PostgreSQL`
- связи `1:N` и `M:N`
- `LINQ`
- `async/await`
- обобщенный репозиторий
- `record` и `DTO`
- полный CRUD
- Swagger
- валидация
- логирование
- глобальный middleware для ошибок

## Возможные улучшения

- добавить unit-тесты и integration-тесты
- вынести `404` и `409` в единый формат `ProblemDetails`
- добавить `FluentValidation`
- подключить `Serilog`
- подготовить frontend-клиент

