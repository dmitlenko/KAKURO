using Kakuro.Engine.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Rankings
{
    class RankDataFile
    {
        /// <summary>
        /// List of user ranks
        /// </summary>
        public List<UserRank> Ranks { get; private set; } = null;

        /// <summary>
        /// File name for database
        /// </summary>
        private string RankFileName = "ranks.hdb";

        /// <summary>
        /// Default constructor for RankList
        /// </summary>
        public RankDataFile() { }

        /// <summary>
        /// Constructor for RankList
        /// </summary>
        /// <param name="rankFileName">Databese filename</param>
        public RankDataFile(string rankFileName)
        {
            RankFileName = rankFileName;
        }

        /// <summary>
        /// Load database from file
        /// </summary>
        /// <param name="filename">Database filename</param>
        /// <returns>True if succed</returns>
        public bool Load(string filename)
        {
            if (!File.Exists(filename))
            {
                Ranks = new List<UserRank>();
                return true;
            }
            else
            {
                try
                {
                    Ranks = (List<UserRank>)Serealizer.Deserialize(filename);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Load database from default file
        /// </summary>
        /// <returns>True if succed</returns>
        public bool Load()
        {
            return Load(RankFileName);
        }

        /// <summary>
        /// Save database to file
        /// </summary>
        /// <param name="filename">Database filename</param>
        /// <returns>True if succed</returns>
        public bool Save(string filename)
        {
            try
            {
                Serealizer.Serialize(Ranks, filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Save database to file
        /// </summary>
        /// <returns>True if succed</returns>
        public bool Save()
        {
            if (RankFileName == null) return false;

            return Save(RankFileName);
        }

        /// <summary>
        /// Add new rank
        /// </summary>
        /// <param name="rank">User rank</param>
        /// <param name="save">Save to file</param>
        /// <returns>True if succed</returns>
        public bool Add(UserRank rank, bool save = false)
        {
            if (rank == null) return false;

            foreach (UserRank r in Ranks)
                if (r.UID == rank.UID) return false;
            Ranks.Add(rank);

            if (save) Save();
            return true;
        }

        /// <summary>
        /// Gets a rank by their UID
        /// </summary>
        /// <param name="uid">UID</param>
        /// <returns>True if succed</returns>
        public UserRank Get(int uid)
        {
            foreach (UserRank r in Ranks)
                if (r.UID == uid) return r;

            return null;
        }
    }
}
