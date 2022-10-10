using Kakuro.Engine.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Authorization
{
    public class SerealizedUsersList : SerealizedList<User>
    {
        /// <summary>
        /// Constructor for SerealizedUserList
        /// </summary>
        /// <param name="fileName">File name</param>
        public SerealizedUsersList(string fileName) : base(fileName) {
            base.AddComparator = (a, b) =>
            {
                return a.Name == b.Name;
            };
        }

        ///<summary>
        /// Gets an user by their UID
        ///</summary>
        ///<param name="uid">UID</param>
        ///<returns>User if found, null if not</returns>
        ///
        public new User Get(int uid)
        {
            var ret = base.GetFirst(u => u.UID == uid);
            return ret == default(User) ? null : ret;
        }

        /// <summary>
        /// Gets an user by their name and password
        /// </summary>
        /// <param name="username">Password</param>
        /// <param name="password">User's name</param>
        /// <param name="ishash">Is password hash? (def. <c>false</c>)</param>
        /// <returns>User if found, null if not</returns>
        public User Get(string username, string password, bool ishash = false)
        {
            var ret = base.GetFirst(u =>
            {
                if (ishash && u.Name == username && u.PasswordHash == password) return true;
                else if (u.Name == username && Hasher.Verify(password, u.PasswordHash)) return true;

                return false;
            });

            return ret == default(User) ? null : ret;
        }
    }
}
