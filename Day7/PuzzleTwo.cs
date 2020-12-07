using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
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
            // load the data from the PuzzleData.txt
            string puzzleData = this.LoadPuzzleDataIntoMemory();
            // create an instance of bagParser which will parse the puzzleData text
            Bags.BagParser bagParser = new Bags.BagParser();
            // parze the puzzle data text
            System.Collections.Hashtable bags = bagParser.parseBagText(puzzleData);
            // find the "shiny gold" bag
            Bags.Bag aBag = (Bags.Bag)bags["shiny gold"];

            int NoParentBags = 1;
            // count the number of child bags within aBag
            int answer = this.FindAllChildBags_Test(aBag, NoParentBags);

            return answer;
        }

        /// <summary>
        /// Recurive function that counts the number of child bags within a bag
        /// </summary>
        /// <param name="aBag">Recursivly count all child bags</param>
        /// <param name="NoOfParentBags">the number of parent bags</param>
        /// <returns></returns>
        private int FindAllChildBags_Test(Bags.Bag aBag, int NoOfParentBags)
        {
            int bagCount = 0;
            
            // loop through each child bag within this bag
            foreach (Bags.ChildBag childBag in aBag.bagsWithinThisBag)
            {
            
                // caculate the number of child bags there are based on the number of parent bags
                bagCount += childBag.NumberOfThisKindOfBag * NoOfParentBags;

                
                // count the number of childdren this child bag has
                bagCount += this.FindAllChildBags_Test(childBag.bag, childBag.NumberOfThisKindOfBag * NoOfParentBags);
            }

           
            return bagCount;
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
