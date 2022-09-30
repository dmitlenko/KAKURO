using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Algorithms
{
    /**
     * <summary>Class to represent all possible combinations of numbers in a given amount of white squares.</summary>
     */
    public class Combos
    {
        /**
         * <summary>The Combos class is Singleton</summary>
         */
        private static Combos _instance;

        /**
         * <summary>Default getter for the Combos, if it doesn't exist it creates it</summary>
         */
        public static Combos Instance { 
            get { 
                if(_instance == null )
                    _instance = new Combos();
                return _instance;
            } 
        }

        /**
         * <summary>
         * The first ArrayList stores the number of white boxes, the second ArrayList stores the sum 
         * of these white boxes and the third ArrayList stores a HashSet of the possible combinations
         * of numbers in these white cells.
         * </summary>
         */
        public List<List<List<HashSet<int>>>> Values;

        /**
         * <summary>
         * The String stores "white boxes+sum" and the HashSet stores unused values in the HashSet 
         * of values.
         * </summary>
         */
        public Dictionary<string, HashSet<int>> UnusedValues;

        /**
         * <summary>Default constructor for Combos class</summary>
         */
        public Combos()
        {
            ReadCombos();
            ReadUnusedValues();
        }

        public HashSet<int> CompatibleValues(HashSet<int> used_values, int cell, int sum)
        {
            HashSet<int> possible_values = new HashSet<int>();
            HashSet<int> combo;

            for(int i = 0; i < Values[cell - 1][sum - GetMinValue(cell)].Count; i++)
            {
                combo = new HashSet<int>(Values[cell - 2][sum - GetMinValue(cell)][i]);
                if (used_values.IsSubsetOf(combo))
                {
                    combo.RemoveWhere((value) => used_values.Contains(value));
                    if (combo.Any()) possible_values.UnionWith(combo);
                }
            }

            return possible_values;
        }

        /**
         * <summary>Get the possible values that can go in a set of white cells</summary>
         * <param name="cell">The number of white cells</param>
         * <param name="sum">The sum of these white cells</param>
         */
        public HashSet<int> PossibleValues(int cell, int sum)
        {
            HashSet<int> possible_values = new HashSet<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            HashSet<int> unused = UnusedValues.ContainsKey(cell.ToString() + sum) ? UnusedValues[cell.ToString() + sum] : new HashSet<int>();
            if(unused != null) possible_values.RemoveWhere((value) => unused.Contains(value));
            return possible_values;
        }

        /**
         * <summary>Know if a sum in a quantity of white boxes is possible</summary>
         * <param name="sum">The sum of these white cells</param>
         * <param name="cell">The number of white cells</param>
         * <returns>Returns true if possible, otherwise returns false</returns>
         */
        public bool ExistsSumOfCells(int sum, int cell)
        {
            int index = sum - GetMinValue(cell);
            if (cell == 1) return true;
            return (index >= 0 && index < Values[cell - 2].Count);
        }

        /**
         * <summary>Obtain the maximum number of possible sums in a certain amount of white cells</summary>
         * <param name="cell">The number of white boxes</param>
         * <returns>Returns the maximum number of possible sums in a given amount of white cells</returns>
         */
        private int ComputeMaxCombinations(int cell)
        {
            return GetMaxValue(cell) - GetMinValue(cell) + 1;
        }

        /**
         * <summary>Get the minimum sum in a certain amount of white cells</summary>
         * <param name="cell">The number of white cells</param>
         * <returns>Returns the minimum sum in a specified amount of white cells</returns>
         */
        private int GetMinValue(int cell)
        {
            return (cell * (cell + 1)) / 2;
        }

        /**
         * <summary>Get the maximum sum in a certain amount of white cells</summary>
         * <param name="cell">The number of white cells</param>
         * <returns>Returns the maximum sum in a given amount of white cells</returns> 
         */
        private int GetMaxValue(int cell)
        {
            return 45 - ((9 - cell) * (10 - cell) / 2);
        }

        /**
         * <summary>Read all possible combos from the combos.txt file</summary>
         */
        private void ReadCombos()
        {
            StringReader srline = new StringReader(Properties.Resources.combos.ToString());
            Values = new List<List<List<HashSet<int>>>>(8);

            for(int i = 2; i <= 9; i++)
            {
                int combinations = ComputeMaxCombinations(i);
                List<List<HashSet<int>>> sum_combos = new List<List<HashSet<int>>>(combinations);
                string line;

                for (int j = 0; j < combinations; j++)
                {
                    List<HashSet<int>> combo_set = new List<HashSet<int>>();
                    line = srline.ReadLine();

                    foreach (string s in line.Split(' '))
                    {
                        combo_set.Add(GetSetFromString(s));
                    }

                    sum_combos.Add(combo_set);
                }

                Values.Add(sum_combos);
            }
        }

        /**
         * <summary>Read all unused values from the unused.txt file</summary>
         */
        private void ReadUnusedValues()
        {
            StringReader srline = new StringReader(Properties.Resources.unused.ToString());
            string line;

            UnusedValues = new Dictionary<string, HashSet<int>>(64);

            for(int i = 0; i < 64; i++)
            {
                line = srline.ReadLine();
                string[] s = line.Split(' ');

                UnusedValues[s[0]] = GetSetFromString(s[1]);
            }
        }

        /**
         * <summary>Convert the numbers of a String into a HashSet of Integers</summary>
         * <param name="str">String of numbers</param>
         * <returns><c>HashSet</c> of numbers</returns>
         */
        private HashSet<int> GetSetFromString(string str)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (char s in str)
                set.Add(s - '0');
            return set;
        }
    }
}
