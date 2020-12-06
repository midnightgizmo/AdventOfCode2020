using System;
using System.Collections.Generic;
using System.Text;

namespace Day3
{
    public class PuzzleTwo
    {
        /// <summary>
        /// The main method that is called outside this class that will solve the puzzle
        /// and return the answer
        /// </summary>
        /// <returns>The answer to the puzzle</returns>
        public long solvePuzzle()
        {
            // converts the puzzledata in the textfile (PuzzleData.txt) to a Terain.Map class
            Terrain.Map aMap = this.ParsePuzzleData();

            // going to need to use a long instead of an int because
            // the final answer is bigger than an int can hold
            List<long> TressFoundOnSlopes = new List<long>();

            // try differnet slopes and see how many trees we hit on each slope
            TressFoundOnSlopes.Add(findTreesOnSlope(aMap, 1, 1));
            TressFoundOnSlopes.Add(findTreesOnSlope(aMap, 1, 3));
            TressFoundOnSlopes.Add(findTreesOnSlope(aMap, 1, 5));
            TressFoundOnSlopes.Add(findTreesOnSlope(aMap, 1, 7));
            TressFoundOnSlopes.Add(findTreesOnSlope(aMap, 2, 1));

            // will hold the answer to the second part of the puzzle
            long answer = 1;
            // times all the numbers together
            foreach (int numTrees in TressFoundOnSlopes)
                answer *= numTrees;

            return answer;
        }

        private int findTreesOnSlope(Terrain.Map map, int row, int column)
        {
            // indicates how many rows to move down the map on each do while loop below
            int rowCountOn = row;
            // indicates how mnay columns to move accross the map on each wo while loop below
            int columnCountOn = column;

            // will hold the total number of trees we hit on this slope
            int treeCount = 0;

            // if set to false the do while loop will exit
            bool shouldCarryOn = true;
            do
            {
                // check the map at the current column,row and see what is there
                switch (map[columnCountOn, rowCountOn])
                {
                    // we found a tree
                    case Terrain.GridCellType.Tree: // increment treeCount by one to say we found a tree
                        treeCount++;
                        break;

                    // if we found nothing we have reached the end of the map (verticaly)
                    case Terrain.GridCellType.nothing:
                        shouldCarryOn = false; // setting to false will break us out of the do while loop
                        break;
                }

                // move to the next position on the slope
                rowCountOn += row;
                columnCountOn += column;

            } while (shouldCarryOn);

            // the number of trees we hit on our way down
            return treeCount;
        }

        /// <summary>
        /// converts the puzzledata in the textfile (PuzzleData.txt) to a Terain.Map class
        /// </summary>
        /// <returns></returns>
        private Terrain.Map ParsePuzzleData()
        {
            string fileData = this.LoadPuzzleDataIntoMemory();

            Terrain.Map aMap = new Terrain.Map(fileData);

            return aMap;
        }

        /// <summary>
        /// Loads the content of PuzzleData.txt into memory
        /// </summary>
        /// <returns>Contents of PuzzleData.txt as a string</returns>
        private string LoadPuzzleDataIntoMemory()
        {
            // will hold the data loaded from PuzzleData.txt
            string fileData = string.Empty;
            // PuzzleData.txt has been set to be copied to output directory (meaning it will be in the same folder
            // as the executable file) so we need to find the location of the where the exe is being executed from
            string currentWorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            // create the location of where the file exists on disk
            currentWorkingDirectory += "\\PuzzleData.txt";

            // try and load the file from disk
            try
            {
                fileData = System.IO.File.ReadAllText(currentWorkingDirectory);
            }
            catch (Exception e)
            {

            }
            // return the data loaded from PuzzleData.txt
            return fileData;
        }
    }
}
