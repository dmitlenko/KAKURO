using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kakuro.Engine.Core;

namespace Kakuro.Engine.Rankings
{
    public class Ranker
    {
        /// <summary>
        /// Database file
        /// </summary>
        private SerealizedList<UserRank> rankData = new SerealizedList<UserRank>("ranks.hdb");

        /// <summary>
        /// Default consatructor for Ranker
        /// </summary>
        public Ranker()
        {
            rankData.Load();
            Sort();
        }

        /// <summary>
        /// Sort ranks
        /// </summary>
        /// <param name="ascending">Sort ascending</param>
        public void Sort(bool ascending = false)
        {
            rankData.Sort((a, b) =>
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
            get => rankData.Get(place);
        }

        /// <summary>
        /// Gets list of ranks
        /// </summary>
        /// <returns>UserRank</returns>
        public List<UserRank> Top()
        {
            return rankData.GetAll(a => true);
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
                top.Add(rankData.Get(i));

            return top;
        }

        /// <summary>
        /// Add new rank to list
        /// </summary>
        /// <param name="rank">User rank</param>
        /// <param name="save">Save to file?</param>
        public void Add(UserRank rank, bool save = false)
        { 
            rankData.Add(rank, save);
        }
    }
}
