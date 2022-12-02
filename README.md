# SkillProfi

Для запуска всех узлов нужно указать несколько запускаемых проектов: SkillProfiApi, SkillProfiWEBMVC, SkillProfiWPF, SkillProfiTelegramBot. В режиме Запуск (с отладкой, т.к. без отладки могут быть разные параметры, например у Api Другой порт и из-за этого Api не будет найден). Для SkillProfiTelegramBot, в Program, нужно указать token бота.

В Базе данных API с помощью InMemoryDB и Методов в бд, данные создаются автоматически. Нужно только в папку Pictures добавить свои изображенния с нужными именам (как в бд), хотя и без них вроде все предусмотрено

Для того чтобы войти в WPF приложение можно ввести имя и пароль admin