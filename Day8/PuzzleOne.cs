using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
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
            // load the data from the PuzzleData.txt
            string puzzleData = this.LoadPuzzleDataIntoMemory();

            // create an instance of compiler that will run the code
            Compiler.Compiler compiler = new Compiler.Compiler();

            // covert the text file into a list of instcutions the compiler will understand
            compiler.parseInstructions(puzzleData);
            // run the program until completion or an invinat loop is dectected
            compiler.run();

            // get the accumulator value which is the answer to the puzzle
            return compiler.accumulatorValue;
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
