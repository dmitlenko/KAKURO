using Kakuro.Engine.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Authorization
{
    public class UserData
    {
        /**
         * <summary>Array of users</summary>
         */
        private List<User> users = null;

        /**
         * <summary>Database filename</summary>
         */
        public string FileName { get; private set; } = null;

        /**
         * <summary>Get new UID</summary>
         */
        public int NewUID { 
            get
            {
                if (users == null) return -1;
                return users.Count;
            } 
        }

        /**
         * <summary>Constructor for UserDataFile</summary>
         * <param name="filename">Database filename</param>
         */
        public UserData(string filename)
        {
            FileName = filename;
        }

        /**
         * <summary>Constructor for UserDataFile</summary>
         */
        public UserData() { }

        /**
         * <summary>Read users data from file</summary>
         * <param name="filename">Database filename</param>
         */
        public bool Read(string filename)
        {
            if (!File.Exists(filename))
            {
                users = new List<User>();
                return true;
            } else
            {
                try
                {
                    users = (List<User>)Serealizer.Deserialize(filename);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /**
         * <summary>Read users data from file</summary>
         */
        public bool Read()
        {
            if (FileName == null) return false;

            return Read(FileName);
        }

        /**
         * <summary>Save database to file</summary>
         * <param name="filename">Database filename</param>
         */
        public bool Save(string filename)
        {
            try
            {
                Serealizer.Serialize(users, filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /**
         * <summary>Save database to file</summary>
         */
        public bool Save()
        {
            if (FileName == null) return false;

            return Save(FileName);
        }

        /**
         * <summary>Gets an user by their UID</summary>
         * <param name="uid">UID</param>
         * <returns>User if found, null if not</returns>
         */
        public User Get(int uid)
        {
            foreach(User u in users)
                if (u.UID == uid) return u;

            return null;
        }

        /**
         * <summary>Gets an user by their name and password</summary>
         * <param name="passwordHash">Password</param>
         * <param name="username">User's name</param>
         * <param name="ishash">Is password hash? (def. <c>false</c>)</param>
         * <returns>User if found, null if not</returns>
         */
        public User Get(string username, string password, bool ishash = false)
        {
            foreach (User u in users)
                if (ishash && u.Name == username && u.PasswordHash == password) return u;
                else if (u.Name == username && Hasher.Verify(password, u.PasswordHash)) return u;

            return null;
        }

        /**
         * <summary>Adds new user</summary>
         * <param name="user">User</param>
         * <param name="save">Save to file if done? (def. <c>false</c>)</param>
         * <returns>True if user added</returns>
         */
        public bool Add(User user, bool save = false)
        {
            if (user == null) return false;

            foreach(User u in users)
                if (u.Name == user.Name) return false;

            users.Add(user);

            if (save) Save();
            return true;
        }
    }
}
