using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Rankings
{
    class Ranker
    {
        /// <summary>
        /// Database file
        /// </summary>
        private RankDataFile rankData = new RankDataFile();

        /// <summary>
        /// Ranks list
        /// </summary>
        private List<UserRank> Ranks { get; set; }

        /// <summary>
        /// Default consatructor for Ranker
        /// </summary>
        public Ranker()
        {
            rankData.Load();
            Ranks = rankData.Ranks;
            Sort();
        }

        /// <summary>
        /// Sort ranks
        /// </summary>
        /// <param name="ascending">Sort ascending</param>
        public void Sort(bool ascending = false)
        {
            Ranks.Sort((a, b) =>
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
            get => Ranks[place];
        }

        /// <summary>
        /// Gets list of ranks
        /// </summary>
        /// <returns>UserRank</returns>
        public List<UserRank> Top()
        {
            return Ranks;
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
                top.Add(Ranks[i]);

            return top;
        }
    }
}
