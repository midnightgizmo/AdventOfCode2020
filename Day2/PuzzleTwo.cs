using System;
using System.Collections.Generic;
using System.Text;

namespace Day2
{
    public class PuzzleTwo
    {
        /// <summary>
        /// The main method that is called outside this class that will solve the puzzle
        /// and return the answer
        /// </summary>
        /// <returns>The answer to the puzzle</returns>
        public int solvePuzzle()
        {
            int numTimesFoundAcceptedPassword = 0;

            // load all the puzzle data from the PuzzleData.txt file
            string puzzleData = LoadPuzzleDataIntoMemory();

            // split the puzzleData data into rows (find '\n' which indicates new line)
            string[] puzzleArray = puzzleData.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // go through each line
            foreach (string singlePuzzlePeace in puzzleArray)
            {
                PasswordPolicy passwordPolicy = new PasswordPolicy(singlePuzzlePeace);
                if (passwordPolicy.isPasswordValid_PuzzleTwo())
                    numTimesFoundAcceptedPassword++;
            }

            return numTimesFoundAcceptedPassword;
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
