using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    public class PuzzleOne
    {
        // keeps track of the number of times a jold difference was discoverd
        // the key equals the jolt difference, the value equals the number of times it was discoverd
        private System.Collections.Hashtable _joldDifferences = new System.Collections.Hashtable();
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


            // set to zero to indicate the charging outlet which is set to zero
            int currentJolts = 0;
            for (int i = 0; i < adaptorsList.Count; i++)
            {
                int currentAdaptor = adaptorsList[i];
                // what is the difference betwee the currentJolts and the adaptor we are about to plug in
                int joltsDifference = currentAdaptor - currentJolts;

    
                // keeps track of the number of times this jolt difference has been encounted
                this.addJoltDifferencToHashTable(this._joldDifferences, joltsDifference);

                // set currentJolts to currentAdaptor for the next time around in the loop
                currentJolts = currentAdaptor;
            }

            // always add 3 at the end because the final adaptor has a +3 on it
            int rating = currentJolts + 3;
            // add the plus 3 differnet to the joldDifferences hash table
            this.addJoltDifferencToHashTable(this._joldDifferences, 3);

            // work out the puzzle answer by multiplyer the total number of 1 jolt differences
            // by the total number of 3 volt differences.
            int puzzleAnswer = (int)this._joldDifferences[1] * (int)this._joldDifferences[3];


            return puzzleAnswer;
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

        private void addJoltDifferencToHashTable(System.Collections.Hashtable joldDifferences, int joltDifference)
        {
            // check to see if we have previusly had this jolts difference
            if (joldDifferences.ContainsKey(joltDifference))
            {
                // joldsDifference should allready be in hash table so just add 1 to it
                joldDifferences[joltDifference] = (int)joldDifferences[joltDifference] + 1;
            }
            // not had this jolts difference yet, so just set its value to 1 in the hash table
            else
                joldDifferences[joltDifference] = 1;
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
