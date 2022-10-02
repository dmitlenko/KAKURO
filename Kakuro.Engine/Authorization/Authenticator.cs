using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Authorization
{
    public class Authenticator
    {
        /**
         * <summary>Users database filename</summary>
         */
        private const string FileName = "users.hdb";

        /**
         * <summary>Users data</summary>
         */
        private UserData userData;

        /**
         * <summary>Current authenticated user</summary>
         */
        public User CurrentUser { get; private set; } = null;

        /**
         * <summary>Default constructor for authenticator</summary>
         */
        public Authenticator()
        {
            userData = new UserData(FileName);
            userData.Read();
        }

        /**
         * <summary>Authorize user using their name and password</summary>
         * <param name="password">User's password</param>
         * <param name="username">User's name</param>
         */
        public bool Authorize(string username, string password)
        {
            User u = userData.Get(username, password);

            if (u != null)
            {
                CurrentUser = u;
                return true;
            }

            return false;
        }

        /**
         * <summary>Creates new user</summary>
         * <param name="displayName">User's diplay name</param>
         * <param name="password">User's password</param>
         * <param name="username">User's name</param>
         */
        public bool Register(string displayName, string username, string password)
        {
            User u = new User(displayName, username, Hasher.Hash(password), userData.NewUID);
            return userData.Add(u, true);
        }
    }
}
