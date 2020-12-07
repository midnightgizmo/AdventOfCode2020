using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    class PuzzleOne
    {
        /// <summary>
        /// The main method that is called outside this class that will solve the puzzle
        /// and return the answer
        /// </summary>
        /// <returns>The answer to the puzzle</returns>
        public int solvePuzzle()
        {
            // load the data from the PuzzleData.txt
            string puzzleData = this.LoadPuzzleDataIntoMemory();
            // create an instance of bagParser which will parse the puzzleData text
            Bags.BagParser bagParser = new Bags.BagParser();
            // parze the puzzle data text
            System.Collections.Hashtable bags = bagParser.parseBagText(puzzleData);
            // find the "shiny gold" bag
            Bags.Bag aBag = (Bags.Bag)bags["shiny gold"];
            
            // count how many unique parent bags the "shiny gold" bag has
            this.findParentBags(aBag);
            // return the unique number of parent bags the "shiny gold" bag has
            return this._uniqueParentBags.Count;
        }
        
        /// <summary>
        /// This will be used for the answer to part one of the puzzle. The number of bags
        /// found will be the answer. Used in the function findParentBags
        /// </summary>
        private System.Collections.Hashtable _uniqueParentBags = new System.Collections.Hashtable();
        /// <summary>
        /// find all the parent bags and check track of the ones we find within the verable _quniqueParentBags
        /// </summary>
        /// <param name="aBag">the bag to start checking from</param>
        private void findParentBags(Bags.Bag aBag)
        {
            
            // go through each bags prents
            foreach(System.Collections.DictionaryEntry entry in aBag.parentBags)
            {
                // get the current bags parent we are looking at in the loop
                Bags.Bag parentBag = (Bags.Bag)entry.Value;

                // keep track of the parent bags we have found and add them to the list
                // but only add them if its the first time we have come accross this parent bag
                // Dont add if we have come accross it before within the recursive findParentBags function
                if (_uniqueParentBags.ContainsKey(parentBag.bagColor) == false)
                    _uniqueParentBags.Add(parentBag.bagColor, parentBag);

                // check this parent to see if it has any parent bags
                findParentBags(parentBag);

            }

            return ;
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
