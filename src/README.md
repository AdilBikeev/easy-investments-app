# Описание файловой архитектуры

1. ApiGateways - публичные сервисы
    * Web.Bff.EasyInvestments (Web Back For Frontend) - API для пользователей браузера
2. BuildingBLocks (Core)- библиотеки для использования во всем решении
    * EventBus - Набор библиотек для работы с шиной данных
        * EventBus - описание общей схемы работы шины данных
        * EventBusRabbitMQ - описание схемы работы с RabbitMQ
3. Services - микросервисы приложения
    * {ServiceName}.API - сервис, используемый в ApiGateways
        * Application
            * Commands - набор обработчиков событий, служащая прослойкой межде текущей API и *одной* таблицы. Позволяют выполнять операции над *одной таблицой* в рамках одной транзакции
            * DomainEventHandlers -  набор обработчиков событий, служащая прослойкой между текущей API и *несколькими* БД. Позволяют выполнять операции над *несколькими таблицами* в рамках одной транзакции
            * IntegrationEvents - набор событий и их обработчиков для работы с смежными сервисами. Аналогичен DomainEventHandlers, только взаимодействие происходит со смежными системами.
    * {ServiceName}.BackgroundTasks - фоновые задачи сервиса
    * {ServiceName}.Domain - Библиотека для описания БД. Используется только в {ServiceName}/{ServiceName}.Infrastructure
    * {ServiceName}.Infrastructure - описание БД сервиса и логика её формирования с использованием библиотеки {ServiceName}/{ServiceName}.Domain