using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kakuro.Engine.Core;

namespace Kakuro.Engine.Rankings
{
    public class SerealizedRanksList: SerealizedList<UserRank>
    {
        /// <summary>
        /// Constructor for SerealizedRanksList
        /// </summary>
        /// <param name="filename"></param>
        public SerealizedRanksList(string filename): base(filename)
        {
            Load();
            Sort(true);
        }

        /// <summary>
        /// Default costructor for SerealizedRanksList
        /// </summary>
        public SerealizedRanksList(): base("ranks.hdb")
        {
            Load();
            Sort(true);
        }

        /// <summary>
        /// Sort ranks
        /// </summary>
        /// <param name="ascending">Sort ascending</param>
        public void Sort(bool ascending = false)
        {
            Sort((a, b) =>
            {
                return ascending ? 
                    a.TotalSeconds().CompareTo(b.TotalSeconds()):
                    b.TotalSeconds().CompareTo(a.TotalSeconds());
            });
        }

        /// <summary>
        /// Get user rank by place in top
        /// </summary>
        /// <param name="place">Place</param>
        /// <returns>UserRank</returns>
        public UserRank this[int place]
        {
            get => Get(place);
        }

        /// <summary>
        /// Gets list of ranks
        /// </summary>
        /// <returns>UserRank</returns>
        public List<UserRank> Top()
        {
            return GetAll(a => true);
        }

        /// <summary>
        /// Get user rank by place in top
        /// </summary>
        /// <param name="place">Place</param>
        /// <returns>UserRank</returns>
        public List<UserRank> Top(int x)
        {
            List<UserRank> top = new List<UserRank>();

            for (int i = 0; i < x; i++)
                top.Add(Get(i));

            return top;
        }
    }
}
