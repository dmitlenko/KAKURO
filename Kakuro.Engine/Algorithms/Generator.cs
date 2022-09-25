using System.Collections.Generic;

namespace Kakuro.Engine.Algorithms
{
    public class Generator
    {
        private Kakuro k;
        private int[] candidate_cells;
        private Dictionary<int, bool[]> cell_validity;
        private Dictionary<int, int> cell_index;

        /**
         * <summary>Default constructor for Generator</summary>
         */
        public Generator()
        {
            k = null;
            candidate_cells = null;
            cell_validity = null;
            cell_index = null;
        }

        /**
         * <summary>Generate valid kakuro board</summary>
         * <param name="width">Board width</param>
         * <param name="height">Board height</param>
         * <param name="difficulty">Board difficulty</param>
         * <returns>Returns kakuro board</returns>
         */
        public Kakuro Generate(int width, int height, int difficulty)
        {
            k = new Kakuro(width, height);
            k.Difficulty = difficulty;
        }
    }
}