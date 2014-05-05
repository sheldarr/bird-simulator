using Engine.Time;

namespace Engine.Factories
{
    public static class TimeMachineFactory
    {
        public static TimeMachine CreateTimeMachine(int quantum)
        {
            return new TimeMachine(quantum);
        }
    }
}
