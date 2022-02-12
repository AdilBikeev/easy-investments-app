# Web.EasyInvestments.HttpAggregator

Http агрегатор между внутренним бэком и фронтом (Bff - Backend for Frontend)

### Автогенерация кода для HttpClient используя swagger
1. Установить NSwagStudio https://github.com/RicoSuter/NSwag/wiki/NSwagStudio
2. Запустите NSwagStudio.exe
3. В качестве значения `Runtime` выберите используемую в данном проекте платформу
4. Выберите ниже вкладку "OpenAPI/Swagger Specification"
5. В качестве значения для поля "Specification URL" введите URL к OpenAPI вида: `http://localhost:44354/swagger/v1/swagger.json.`
6. Нажмите кнопку Create local Copy (Создать локальную копию), чтобы создать представление JSON своей спецификации Swagger.
7. В области Outputs (Выходные данные) установите флажок CSharp Client (Клиент CSharp).
8. В вкладке "CSharp Client" задайте Namespace вида: {NameOpenAPI.API.V{version}}
9. В конце той же вкладки - выбираем куда буде сохранятся выходной файл через поле "Output file path". Желательно сохранять так "{CurrentProjectName}/Generated/{NameOpenAPI.API.V{version}}.cs"
10. Жмем "Generate Outputs", чтобы предварительно посмотреть как будет выглядеть файл, через который нужно будет взаимодействовать
11. Жмем "Generate Files" для генерация кода HttpClient