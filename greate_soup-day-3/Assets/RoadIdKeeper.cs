using System.Collections.Generic;

namespace DefaultNamespace
{
    public class RoadIdKeeper
    {
        public static RoadIdKeeper Instance { get; private set; }
        private Dictionary<int, int> dict = new Dictionary<int, int>();

        public RoadIdKeeper()
        {
            Instance = this;
        }

        public int GetRealId(int id) => dict[id];
        public void Add(int id, int realId) => dict.Add(id, realId);
        public bool Has(int id) => dict.ContainsKey(id);
    }
}