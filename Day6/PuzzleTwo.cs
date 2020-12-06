using System;
using System.Collections.Generic;
using System.Text;

namespace Day6
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
            // load the puzzle data into memory
            string puzzleData = this.LoadPuzzleDataIntoMemory();

            // split puzzel data where there is a blank line (splits it into question groups)
            string[] eachQuestionGroupArray = puzzleData.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            // keeps track of all question groups where everyone answers yes to the same question
            int NoQuestionWhichEveryOneAnswerdYes = 0;
            // go through each questionGroup (currently as a string)
            foreach (string questionGroupData in eachQuestionGroupArray)
            {
                // create a QuestionGroup which will parase the questionGroupData string
                CustomsDeclaration.QuestionGroup questionGroup = new CustomsDeclaration.QuestionGroup();

                // Parse the Question Group data so we can see what questions people have answerd
                // yes too and work out the distinct questions in the group
                questionGroup.parseData(questionGroupData);

                // keep track of which questions everyone in the group answerd yes too
                int NoQuestionsWhichEveryOneAnswerdYesinGroup = 0;
                // go through each distinct question
                foreach(string distinctQuestion in questionGroup.distinctQuestionsInGroupList)
                {
                    // we will check this value at the end of the foreach loop.
                    // if it is still set to to true at that point we have found
                    // a question where everyone in the group has answerd yes to it.
                    // if anyone answerd no, this would be set to false
                    bool isQuestionPresentInEveryDeclarationForm = true;
                    // if the current customers had no questions
                    if (questionGroup.customerDeclarationFormList.Count == 0)
                    {
                        // set to false to indicate not all customers have answerd
                        // yes to the distinctQuestion we are looking at
                        isQuestionPresentInEveryDeclarationForm = false;
                    }
                    // customer has at least one question
                    else
                    {
                        // go through each question the customer has
                        foreach (CustomsDeclaration.CustomerDeclarationForm aForm in questionGroup.customerDeclarationFormList)
                        {
                            // if the customer does not have the distinct question
                            if (aForm.eachQuestion.Contains(distinctQuestion) == false)
                            {
                                // set to false to indicate not all customers have answerd
                                // yes to the distinctQuestion we are looking at
                                isQuestionPresentInEveryDeclarationForm = false;
                                break;
                            }
                        }
                    }

                    // check to see if distinctQuestion was found in the every customers declaration form
                    if (isQuestionPresentInEveryDeclarationForm == true)
                    {
                        // increment the counter by one to show that we found
                        // a question that was answerd yes by everyone customer in this group
                        NoQuestionsWhichEveryOneAnswerdYesinGroup++;
                    }
                }

                // keep track of the total number of questions which everyoen answerd yes too
                // in each group
                NoQuestionWhichEveryOneAnswerdYes += NoQuestionsWhichEveryOneAnswerdYesinGroup;
            }

            return NoQuestionWhichEveryOneAnswerdYes;

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
