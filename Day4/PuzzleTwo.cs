using System;
using System.Collections.Generic;
using System.Text;

namespace Day4
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
            return this.parseDataAndValidPassports();
        }

        private int parseDataAndValidPassports()
        {
            int NumOfValidPassports = 0;
            // load the PuzzleData from text file into memory
            string puzzleData = this.LoadPuzzleDataIntoMemory();

            // split into each passport (passports as sepated by a blank line)
            string[] passportsAsString = puzzleData.Split("\r\n\r\n");

            // go through each passport
            foreach (string aPassportString in passportsAsString)
            {
                // create a passportInfo class to store all the information 
                Passport.PassportInfo passportInfo = new Passport.PassportInfo();
                // parse the passport string
                passportInfo.parseInfo(aPassportString);
                // check to see if the passport is valid, if it is, increase
                // the NumOfValidPassports by 1;
                if (passportInfo.doesPassportPassValidationChecks_PuzzleTwo() == true)
                    NumOfValidPassports++;
            }

            // return the number of valid passports
            return NumOfValidPassports;

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
