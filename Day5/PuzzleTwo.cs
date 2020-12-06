using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
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

            // split puzzel data into each line
            string[] puzzleDataSplitIntoLines = puzzleData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            // a place to hole all the seatID's when we have computed them
            List<int> seatIDList = new List<int>();
            // go through each line (bording pass)
            foreach (string aBordingPassAsString in puzzleDataSplitIntoLines)
            {
                BoardingPass.BoardingPass aBordingPass = new BoardingPass.BoardingPass();
                // pass in the bording pass which will parse the data to work out which row, colum and seatID the person has
                aBordingPass.parseBoardingPass(aBordingPassAsString);
                // add this bording passes seat ID to the list
                seatIDList.Add(aBordingPass.seatID);

            }
            // sort the seat ID's from smallest to biggiest
            seatIDList.Sort();
            // this will be the answer to part 2 of the puzzle
            int missingSeatID = -1;
            // go through each seatid in the list seatIDList
            for (int i = 0; i < seatIDList.Count - 1; i++)
            {

                // look to see if the next seat in the list is one number bigger
                // to the current seatid we are looking at in the list.
                // if its not one bigger, we have found the answer to part 2 of the puzzle
                if (seatIDList[i + 1] != seatIDList[i] + 1)
                {
                    //int thisSeatID = seatIDList[i];
                    //int nextSeatID = seatIDList[i + 1];

                    missingSeatID = seatIDList[i] + 1;
                    break;
                }
            }

            // the seat id that was missing in the seatIDList
            return missingSeatID;
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

