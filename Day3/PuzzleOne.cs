using System;
using System.Collections.Generic;
using System.Text;

namespace Day3
{
    public class PuzzleOne
    {
        /// <summary>
        /// The main method that is called outside this class that will solve the puzzle
        /// and return the answer
        /// </summary>
        /// <returns>The answer to the puzzle</returns>
        public int solvePuzzle()
        {
            // converts the puzzledata in the textfile (PuzzleData.txt) to a Terain.Map class
            Terrain.Map aMap = this.ParsePuzzleData();

            int row = 1;
            int column = 3;
            // Keeps track of the number of trees we hit
            int treeCount = 0;

            // if set to false the do while loop will exit
            bool shouldCarryOn = true;
            do
            {
                // check the map at the current column,row and see what is there
                switch(aMap[column,row])
                {
                    // we found a tree
                    case Terrain.GridCellType.Tree:
                        treeCount++; // increment treeCount by one to say we found a tree
                        break;

                    // if we found nothing we have reached the end of the map (verticaly)
                    case Terrain.GridCellType.nothing:
                        shouldCarryOn = false; // setting to false will break us out of the do while loop
                        break;
                }

                // move position on the map (move one row over and 3 coulumns down)
                row++;
                column += 3;

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
            // load the puzzle data from disk
            string fileData = this.LoadPuzzleDataIntoMemory();

            // parase the puzzle data
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
