namespace MonitorAPI.DAL
{
    public static class MonitorInitializer
    {
        public static void Initialize(MonitorContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}