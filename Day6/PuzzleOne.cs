using System;
using System.Collections.Generic;
using System.Text;

namespace Day6
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
            // load the puzzle data into memory
            string puzzleData = this.LoadPuzzleDataIntoMemory();

            // split puzzel data where there is a blank line (splits it into question groups)
            string[] eachQuestionGroupArray = puzzleData.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            // will hold the total number of distinct questions found from each group
            int total = 0;
            // go through each questionGroup (currently as a string)
            foreach (string questionGroupData in eachQuestionGroupArray)
            {
                // create a class that will parase the questionGroupData
                CustomsDeclaration.QuestionGroup questionGroup = new CustomsDeclaration.QuestionGroup();

                // prase the string of text so we can find
                questionGroup.parseData(questionGroupData);
                // get the number of distinct questions for this question group
                int distinctQuestions = questionGroup.GetNumberOfDistinctQuestionsAskedForThisGroup();

                // keep track of all the distinct questions we have found
                total += distinctQuestions;
            }

            return total;
            
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
