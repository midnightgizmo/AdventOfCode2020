using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
    public class PuzzleOne
    {

        /// <summary>
        /// The main method that is called outside this class that will solve the puzzle
        /// and return the answer
        /// </summary>
        /// <returns>The answer to the puzzle</returns>
        public long solvePuzzle()
        {
            // load the data from the PuzzleData.txt
            string puzzleData = this.LoadPuzzleDataIntoMemory();
            // convert puzzle data to a number array
            long[] numberArray = this.convertPuzzleInputToArray(puzzleData);

            


            // goes through all the numebrs followoing the rules for part 1 of the
            // puzzle and returns the number where any 2 number from the last
            // 25 numbers before it do not sum up to that number.
            return this.findInvalidNumber(numberArray);
        }

        /// <summary>
        /// Converts the puzzle input to an int array
        /// </summary>
        /// <param name="puzzleData">puzzle input</param>
        /// <returns></returns>
        private long[] convertPuzzleInputToArray(string puzzleData)
        {
            // split the puzzle input by each line
            string[] eachLineOfPuzzleInputArray = puzzleData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            // make an int array that is the size of the total number of lines in the puzzle input
            long[] numberArray = new long[eachLineOfPuzzleInputArray.Length];

            // go through each line in the puzzle input and convert the number that is currently
            // a string to an int and put it into the numberArray
            for (int index = 0; index < eachLineOfPuzzleInputArray.Length; index++)
                numberArray[index] = long.Parse(eachLineOfPuzzleInputArray[index]);

            return numberArray;
        }

        private long findInvalidNumber(long[] numberArray)
        {
            // this indicats the starting point (which number to look at first)
            int preambleNumberLength = 25;
            // go through each number starting at the preambleNumberLength index
            for (int index = preambleNumberLength; index < numberArray.Length; index++)
            {
                // see if the current index number can be made up by adding any 2 of the preeding 25 numbers before it.
                // if they can't, the answer will return false
                bool wasSumOfTwoNumbersFound = this.findSumOfTwoNumbers(numberArray[index], numberArray, index - preambleNumberLength, index - 1);
                // if the answer was false we have found the answer to puzzle
                if (wasSumOfTwoNumbersFound == false)
                    return numberArray[index];// return the current number we were looking at (could not find sum of 2 numbers for this numebr we are looking at)
            }

            // return long.MinValue to indicate there was a problem
            return long.MinValue;
        }

        /// <summary>
        /// Looks through a set of numbers in passed in array and sees if the sum of any 2 of the numbers
        /// equal to NumberToFind. Will only look through the numbers in the array based on the index positions
        /// passed in (indexStartPosition & indexEndPosition)
        /// </summary>
        /// <param name="NumberToFind">the number to find</param>
        /// <param name="numbersArray">array of numbers to look through</param>
        /// <param name="indexStartPosition">starting position in the array of numbers to look through</param>
        /// <param name="indexEndPosition">end position in array of numbrs to look through</param>
        /// <returns></returns>
        private bool findSumOfTwoNumbers(long NumberToFind, long[] numbersArray, int indexStartPosition, int indexEndPosition)
        {
            // loop through numbersArray starting at indexStartPosition
            for(int firstNumberIndexPosition = indexStartPosition; firstNumberIndexPosition < indexEndPosition; firstNumberIndexPosition++)
            {
                // get the number at firstNumberIndexPosition
                long firstNumber = numbersArray[firstNumberIndexPosition];

                // loop through numbersArray starting at (firstNumberIndexPosition + !)
                for (int secondNumberIndexPosition = firstNumberIndexPosition + 1; secondNumberIndexPosition <= indexEndPosition; secondNumberIndexPosition++)
                {
                    // take note of the number at secondNumberIndexPosition
                    long secondNumber = numbersArray[secondNumberIndexPosition];

                    // check to see if firstNumber + secondNumber are euqal to NumberToFind
                    if (firstNumber + secondNumber == NumberToFind)
                        // return true to indicate we found 2 numbers within the range of indexStartPosition & indexEndPosition equal NumberToFind
                        return true; 

                }
            }

            // return false to indicate there are no 2 numbers within the range of
            // indexStartPosition & indexEndPosition that equal NumberToFind
            return false;
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
