using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
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
            List<int> adaptorsList = this.convertPuzzleDataToList(puzzleData);
            
            long puzzleAnswer = findAllPermutions(adaptorsList);

            return puzzleAnswer;
        }

        private long findAllPermutions(List<int> adaptorsList)
        {
            // While this function works in theory, there are too many permutions to calculate
            // and it would take till the end of time to finish
            //adaptorsList.Insert(0, 0);
            //findNextAdaptor(adaptorsList, 0);
            //return numberOfPermutations;

            adaptorsList.Add(adaptorsList[adaptorsList.Count - 1] + 3);
            adaptorsList.Insert(0, 0);
            long answerToPartTwoOfPuzzle = this.SolvePartTwo(adaptorsList.ToArray());

            return answerToPartTwoOfPuzzle;
        }

        long numberOfPermutations = 0;
        StringBuilder _sb = new StringBuilder();
        private void findNextAdaptor(List<int> adaptorsList, int currentAdaptorIndex)
        {
            int currentAdaptor = adaptorsList[currentAdaptorIndex];

            for (int index = currentAdaptorIndex + 1; index < adaptorsList.Count; index++)
            {
                int adaptor = adaptorsList[index];

                if(adaptor - currentAdaptor < 4)
                {
                    //this._sb.Append(adaptor + " ");
                    if (index == adaptorsList.Count - 1)
                    {
                        numberOfPermutations++;
                        //this._sb.Append("\r\n");
                    }
                    else
                        findNextAdaptor(adaptorsList, index);
                }
            }
        }


        /// <summary>
        /// Converts to puzzlData to a list of adaptors (ints) which
        /// have been sorted from smallest to biggest
        /// </summary>
        /// <param name="puzzleData">data to parse</param>
        /// <returns>list of adaptors sorted from smallest to biggest</returns>
        private List<int> convertPuzzleDataToList(string puzzleData)
        {
            List<int> adaptorsList = new List<int>();

            // split the puzzledata into each line and loop through each line
            foreach (string adator in puzzleData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
                // convert the string to an int and add it to the adaptorsList
                adaptorsList.Add(int.Parse(adator));

            // sort the adaptors from biggest to smallest.
            adaptorsList.Sort();

            return adaptorsList;
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


        static Dictionary<long, long> resultSet = new Dictionary<long, long>();
        /// <summary>
        /// This is not my code.
        /// see https://github.com/DjolenceTipic/Advent-of-Code/blob/main/Advent-of-Code-2020/day-10/Program.cs
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private long SolvePartTwo(int[] inputs)
        {
            //counter++;
            if (resultSet.ContainsKey(inputs.Length))
            {
                return resultSet[inputs.Length];
            }

            if (inputs.Length == 1)
            {
                return 1;
            }

            long total = 0;
            long temp = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                if (inputs[i] - temp <= 3)
                {
                    total += SolvePartTwo(inputs[i..]);
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(inputs.Length, total);

            return total;
        }

    }
}
