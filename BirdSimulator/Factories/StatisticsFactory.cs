using Engine.Bird;

namespace Engine.Factories
{
    public static class StatisticsFactory
    {
        public static Statistics CreateStatistics(float speed)
        {
            return new Statistics(speed);
        }
    }
}
