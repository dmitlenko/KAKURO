using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Core
{
    public class SerealizedList<type>
    {
        /// <summary>
        /// List of elements
        /// </summary>
        private List<type> values = null;

        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Comparator for add the function
        /// </summary>
        public Func<type, type, bool> AddComparator { get; set; }

        /// <summary>
        /// Get count of elements
        /// </summary>
        public int Count
        {
            get
            {
                if (values == null) return -1;
                return values.Count;
            }
        }

        /// <summary>
        /// Constructor for SerealizedList 
        /// </summary>
        /// <param name="fileName">File name</param>
        public SerealizedList(string fileName)
        {
            FileName = fileName;
            AddComparator = (_,_) => false;
        }

        /// <summary>
        /// Constructor for SerealizedList 
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="addComparator">Comparator for the add function</param>
        public SerealizedList(string fileName, Func<type, type, bool> addComparator)
        {
            FileName = fileName;
            AddComparator = addComparator;
        }

        /// <summary>
        /// Load data from file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <returns>True if succed</returns>
        public bool Load()
        {
            if (!File.Exists(FileName))
            {
                values = new List<type>();
                return true;
            }
            else
            {
                try
                {
                    values = (List<type>)Serealizer.Deserialize(FileName);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Save data to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <returns>True if succed</returns>
        public bool Save()
        {
            try
            {
                Serealizer.Serialize(values, FileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Add new rank
        /// </summary>
        /// <param name="item">User rank</param>
        /// <param name="save">Save to file</param>
        /// <returns>True if succed</returns>
        public bool Add(type item, bool save = false)
        {
            if (item == null) return false;

            foreach (type i in values)
                if (AddComparator(item, i)) return false;

            values.Add(item);

            if (save) Save();
            return true;
        }

        /// <summary>
        /// Get item by index
        /// </summary>
        /// <param name="index">Item index</param>
        /// <returns>Item</returns>
        public type Get(int index)
        {
            if (index >= 0 && index < values.Count) return values[index];
            return default(type);
        }

        /// <summary>
        /// Get first element that matches the condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Item</returns>
        public type GetFirst(Predicate<type> predicate)
        {
            return values.Find(predicate);
        }

        /// <summary>
        /// Retrive all elements that match the condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>A <c>List</c> of matched items</returns>
        public List<type> GetAll(Predicate<type> predicate)
        {
            return values.FindAll(predicate);
        }
    }
}
