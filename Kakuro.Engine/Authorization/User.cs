using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Authorization
{
    /**
     * <summary>Class that represents user</summary>
     */
    [Serializable]
    public class User: IEquatable<User>
    {
        /**
         * <summary>Display name of the user</summary>
         */
        public string DisplayName { get; set; }

        /**
         * <summary>User's name that used for authentication</summary>
         */
        public string Name { get; set; }

        /**
         * <summary>User's password hash</summary>
         */
        public string PasswordHash { get; set; }

        /**
         * <summary>User's unique ID</summary>
         */
        public int UID { get; private set; }

        /**
         * <summary>Defaul constructor for user</summary>
         * <param name="displayName">Display name of the user</param>
         * <param name="name">User's name that used for authentication</param>
         * <param name="passwordHash">User's password hash</param>
         * <param name="uid">User's unique ID</param>
         */
        public User(string displayName, string name, string passwordHash, int uid)
        {
            DisplayName = displayName;
            Name = name;
            PasswordHash = passwordHash;
            UID = uid;
        }

        public bool Equals(User other)
        {
            if (other == null) return false;

            return other.UID.Equals(UID) && 
                other.Name.Equals(Name) && 
                other.DisplayName.Equals(DisplayName) && 
                other.PasswordHash.Equals(PasswordHash);
        }

        public static bool operator ==(User obj1, User obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(User obj1, User obj2) => !(obj1 == obj2);

        public override bool Equals(object obj) => Equals(obj as User);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = DisplayName.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ PasswordHash.GetHashCode();
                hashCode = (hashCode * 397) ^ UID.GetHashCode();
                return hashCode;
            }
        }
    }
}
