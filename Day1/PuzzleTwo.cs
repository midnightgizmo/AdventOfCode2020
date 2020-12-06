using System;
using System.Collections.Generic;
using System.Text;

namespace Day1
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
            // convert the puzzle data from the PuzzleData.txt file
            // and load it into the PuzzleDataList array
            List<int> PuzzleDataList = ParsePuzzleData();
            // the number we want to look for when summing up the numbers
            int sumOfNumbers = 2020;
            // the answer to the puzzle, set to zero inishal to indicate answer not currently found
            int puzzleAnswer = 0;

            // loop through every number in the array
            for (int outerLoopCount = 0; outerLoopCount < PuzzleDataList.Count; outerLoopCount++)
            {
                // using the current outerLoop index postion, get the number at that location
                int firstNumber = PuzzleDataList[outerLoopCount];
                // loop through every number after the current outerLoopCount index position
                for (int innerLoopCount = outerLoopCount + 1; innerLoopCount < PuzzleDataList.Count; innerLoopCount++)
                {
                    // take note of the current number at innerLoopCount index position
                    int secondNumber = PuzzleDataList[innerLoopCount];

                    // loop through every number after the current innterLoopCount index position
                    for(int thirdLoopCount = innerLoopCount + 1; thirdLoopCount < PuzzleDataList.Count; thirdLoopCount++)
                    {
                        // take note of of the current number at theirdLoopCount index position
                        int thirdNumber = PuzzleDataList[thirdLoopCount];

                        // add all 3 numbers up and see if they equal sumOfNumbers
                        if (firstNumber + secondNumber + thirdNumber == sumOfNumbers)
                        {
                            // we found numbers that equal sumOfNumbers so compute the answer to the
                            // puzzle and break out of this loop
                            puzzleAnswer = firstNumber * secondNumber * thirdNumber;
                            break;
                        }
                    }
                    // check to see if we found the answer to the problem in this loop.
                    // if we need break out of this loop
                    if (puzzleAnswer > 0)
                        break;


                }

                // check to see if we found the answer to the problem in this loop.
                // if we need break out of this loop
                if (puzzleAnswer > 0)
                    break;
            }

            // reutrn the answer to the puzzle
            return puzzleAnswer;
        }


        /// <summary>
        /// converts the puzzledata in the textfile (PuzzleData.txt) so an int array (List<int>)
        /// </summary>
        /// <returns>PuzzleData in for form of a List<int></returns>
        private List<int> ParsePuzzleData()
        {
            // this will hold the puzzle data
            List<int> puzzleDataList = new List<int>();

            // load all the puzzle data from the PuzzleData.txt file
            string puzzleData = LoadPuzzleDataIntoMemory();

            // split the puzzleData data into rows (find '\n' which indicates new line)
            string[] puzzleArray = puzzleData.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // go through each line
            foreach (string singlePuzzlePeace in puzzleArray)
            {
                // convert the current line from a string to an int
                int puzzlePeace;
                if (int.TryParse(singlePuzzlePeace, out puzzlePeace) == true)
                    puzzleDataList.Add(puzzlePeace);
            }

            // return the puzzle data which is now converted to a List<int>
            return puzzleDataList;
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
