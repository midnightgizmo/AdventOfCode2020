using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
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
            // load the data from the PuzzleData.txt
            string puzzleData = this.LoadPuzzleDataIntoMemory();
            // convert puzzle data to a number array
            long[] numberArray = this.convertPuzzleInputToArray(puzzleData);

            // get the answer from puzzleOne, we need this number to use in puzzle two
            long numberToFind = new PuzzleOne().solvePuzzle();
            // go throuch each number in the numberArray
            for(int index = 0; index < numberArray.Length; index++)
            {
                // try adding up the numbers in the array starting at the index position in the array
                // to see if they add up to numberToFind. Only add as many numbes is necasary
                // to equal numberToFind. If unable to find a sum that matches numberToFind
                // return long.MinValue to indicate could not be found, so we need to try the next
                // for loop
                long answer = findSumOfNumbersThatMatchInput(numberToFind, numberArray, index);
                // check to see if we found the answer
                if(answer != long.MinValue)
                    return answer;
            }

            // indicates we did not find the answer
            return long.MinValue;
        }

        /// <summary>
        /// Try adding up the numbers in the array starting at the indexStartPosition in the array
        /// to see if they add up to NumberToFind. Only add as many numbes is necasary
        /// to equal numberToFind. If unable to find a sum that matches numberToFind
        /// return long.MinValue to indicate could not be found
        /// </summary>
        /// <param name="NumberToFind">The number we are looking for</param>
        /// <param name="numbersArray">The array of numbers to look for the answer</param>
        /// <param name="indexStartPosition">position in the array to start at</param>
        /// <returns>sum of the smallest and largest numbers used to calculatee NumberToFind</returns>
        private long findSumOfNumbersThatMatchInput(long NumberToFind, long[] numbersArray, int indexStartPosition)
        {
            // the first number we will look at
            long firstNumber;
            // keeps track of all the numbers added together
            long sumOfNumbers;
            // all the numbers we have used to sum together to get the answer (all numbes should sum up to NumberToFind)
            List<long> numbersUsed = new List<long>();

            // take note of the first number
            firstNumber = numbersArray[indexStartPosition];
            // add the first number to the numbers used array
            numbersUsed.Add(firstNumber);
            // add the first number to the sum of numbers
            sumOfNumbers = firstNumber;
            // go through all numbers after indexStartPosition until we get a sum of NumberToFind
            // If we can't find NumberToFind sumOfNumbers gets bigger than numberToFInd we will reutn long.MinValue to
            // indicate the number could not be found
            for (int currentNumberIndex = indexStartPosition + 1; currentNumberIndex < numbersArray.Length; currentNumberIndex++)
            {
                // get the current number we are looking at
                long currentNumber = numbersArray[currentNumberIndex];
                // add current number to the sumOfNumbers
                sumOfNumbers += currentNumber;
                // add current number to thelist of numbers used array
                numbersUsed.Add(currentNumber);
                // check to see if sumOfNumbers == NumberTofIND
                if (sumOfNumbers == NumberToFind)
                {// we found a match

                    // sort the numbers used so far so we can get the biggest number, and smallest number
                    numbersUsed.Sort();

                    // cacualte the answer to puzzle 2 by adding the smallest number
                    // and biggest number (in the array of numbers we used) together
                    return numbersUsed[0] + numbersUsed[numbersUsed.Count - 1];
                }
                // Check to see if SumOfNumber is bigger than NumberToFind
                else if (sumOfNumbers > NumberToFind)
                    // our sumOfNumbers is too big, no point carrying on with this for loop.
                    // reutn long.MinValue to indicate we could not find the answer
                    return long.MinValue;
            }

            // we tried all numbers but could not find a match
            // return long.MinValue to indicate we could not find a match
            return long.MinValue;
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
