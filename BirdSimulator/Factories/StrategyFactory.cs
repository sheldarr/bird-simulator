using Engine.Interfaces;
using Engine.Strategies;
using OpenTK;

namespace Engine.Factories
{
    public static class StrategyFactory
    {
        public static IStrategy CreateVectorFlight(Vector3 vectorFlight)
        {
            return new VectorFlight(vectorFlight);
        }
    }
}
