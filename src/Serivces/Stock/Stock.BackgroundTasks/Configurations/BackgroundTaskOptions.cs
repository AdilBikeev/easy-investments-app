namespace Stock.BackgroundTasks.Configurations
{
    /// <summary>
    /// Настройки для фоновой задачи.
    /// </summary>
    public class BackgroundTaskOptions
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Строка подключения к шине данных.
        /// </summary>
        public string EventBusConnection { get; set; }

        /// <summary>
        /// Таймер в мс. для частоты вызова выполнения фонового процесса.
        /// </summary>
        public int CheckUpdateTime { get; set; }

        /// <summary>
        /// Наименование подписчика.
        /// </summary>
        public string SubscriptionClientName { get; set; }
    }
}
