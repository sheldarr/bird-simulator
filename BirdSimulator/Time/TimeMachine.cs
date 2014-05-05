using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Engine.Interfaces;

namespace Engine.Time
{
    public class TimeMachine
    {
        public bool Enabled
        {
            get { return _timer.Enabled; }  
            set { _timer.Enabled = value; } 
        }

        public double Quantum
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        private readonly Timer _timer;
        private ICollection<ITimeTraveler> _timeTravelers = new List<ITimeTraveler>();

        public TimeMachine(double quantum)
        {
            _timer = new Timer(quantum);
            _timer.Elapsed += (sender, args) => _timeTravelers.ToList().ForEach(x => x.Tick());
        }

        public void AddTraveler(ITimeTraveler traveler)
        {
            _timeTravelers.Add(traveler);
        }
    }
}
