# Описание файловой архитектуры

1. ApiGateways - публичные сервисы
    * Web.Bff.EasyInvestments (Web Back For Frontend) - API для пользователей браузера
2. BuildingBLocks (Core)- библиотеки для использования во всем решении
    * EventBus - Набор библиотек для работы с шиной данных
        * EventBus - описание общей схемы работы шины данных
        * EventBusRabbitMQ - описание схемы работы с RabbitMQ
3. Services - микросервисы приложения
    * {ServiceName}/{ServiceName}.API - сервис, используемый в ApiGateways
    * {ServiceName}/{ServiceName}.BackgroundTasks - фоновые задачи сервиса
    * {ServiceName}/{ServiceName}.Domain - Библиотека для описания БД. Используется только в {ServiceName}/{ServiceName}.Infrastructure
    * {ServiceName}/{ServiceName}.Infrastructure - описание БД сервиса и логика её формирования с использованием библиотеки {ServiceName}/{ServiceName}.Domain