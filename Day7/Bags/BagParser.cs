using System;
using System.Collections.Generic;
using System.Text;

namespace Day7.Bags
{
    public class BagParser
    {
        // keeps track of all bags we have come across
        private System.Collections.Hashtable _Bags = new System.Collections.Hashtable();
        /// <summary>
        /// Parses the bag rules and returns a hashtable of all the bags
        /// </summary>
        /// <param name="bagText">bag rules</param>
        /// <returns></returns>
        public System.Collections.Hashtable parseBagText(string bagText)
        {
            // split into each line
            string[] bagRulesArray = bagText.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            // loop through each line (each bag rules(
            foreach(string aBagRule in bagRulesArray)
            {
                // parses the bag rules and adds the found bags
                // to the _Bags hashtable
                parseBagRuleText(aBagRule);
            }

            // all founds bags which have been sorted.
            // parent bags points to child bags and child bags point to parent bags
            return this._Bags;
        }

        /// <summary>
        /// parse the bag rules and extrac the data into there asoshiated bags,
        /// pointing all child bags to there parent bags and all parent bags to there
        /// child bags
        /// </summary>
        /// <param name="bagRuleText"></param>
        private void parseBagRuleText(string bagRuleText)
        {
            // example of text we recieve
            // light red bags contain 1 bright white bag, 2 muted yellow bags.
            // or
            // bright white bags contain 1 shiny gold bag.
            // or 
            // faded blue bags contain no other bags.

            // first thing, split the text on the words "bags contain"
            // should be an array with 2 items
            string[] bagRuleTextArray = bagRuleText.Split("bags contain", StringSplitOptions.RemoveEmptyEntries);

            // get the bags color
            string bagColor = bagRuleTextArray[0].Trim();
            // create a bag class that will hold all the data about this bag
            Bag aBag;// = new Bag();

            // check to see if the bag color we have got is a bag that allready exists in the hash table
            aBag = (Bag)this._Bags[bagColor];
            if (aBag == null)
            {// bag does not exist in the hash table to create one and add it to the hashtable
                aBag = new Bag();
                this._Bags.Add(bagColor,aBag);
            }

            // note down the bag color
            aBag.bagColor = bagColor;
            // add the add to the hash table using the bags color as the key
            //this._Bags[aBag.bagColor] = aBag;

            // check to see if this bag has any child bags
            if (this.doesBagContainChildBags(bagRuleTextArray[1]) == false)
                // bag does not have any child bags
                return;

            // We now know the bag contains 1 or more child bags.
            // if there is only one child bag color it will just return an array of sized 1
            string[] childBags = bagRuleTextArray[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            // go through each child bag rule
            foreach(string childBag in childBags)
            {
                // extract the data from the child bag rule
                // and point it to any parents it may have, and
                // point it to any children it might have
                this.parseChildBagText(childBag, aBag);
            }


        }

        /// <summary>
        /// returns true if contains one or more child bags, else false
        /// </summary>
        /// <param name="childBagsText">the text after the words "bags contain"</param>
        /// <returns></returns>
        private bool doesBagContainChildBags(string childBagsText)
        {
            return !childBagsText.Trim().Contains("no other bags");
        }

        /// <summary>
        /// Finds all the child bags and adds them to the parent bag.
        /// Will also add the child bags to the _Bags hashtable if they are not
        /// allready in it
        /// </summary>
        /// <param name="childBagText"></param>
        /// <param name="parentBag"></param>
        private void parseChildBagText(string childBagText, Bag parentBag)
        {
            Bag childBag;
            string[] bagTextArray = childBagText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // the first index in the array should the the number of this type of bag
            int bagCount = 0;
            int.TryParse(bagTextArray[0], out bagCount);

            // get the bag name
            // not a very elegant solution but it will get the bag name
            string bagColor = childBagText.Substring(bagTextArray[0].Length + 2).Replace("bags", "").Replace("bag","").Replace(".","").Trim();

            // try and find this bag color in the hash table (we may have come across it before)
            childBag = (Bag)this._Bags[bagColor];

            // did we find the bag ? childBag will be null if we did not find it
            // (meaning this is a new bag color we have not come across before
            if(childBag == null)
            {
                // create a child bag to store the child bag details in
                childBag = new Bag();
                // set the bags color
                childBag.bagColor = bagColor;
                
                // add the bag to the hashtable
                this._Bags[bagColor] = childBag;

            }

            // set the childs parent
            if (childBag.parentBags.Contains(childBag.bagColor) == false)
                childBag.parentBags.Add(parentBag.bagColor, parentBag);

            // create a container to add the child bag to and the number of times this bag appears in its parent
            ChildBag aChildBagContainer = new ChildBag();
            // add the child bag to the child bag container
            aChildBagContainer.bag = childBag;
            // set the number of times this child bag exists in the parent bag
            aChildBagContainer.NumberOfThisKindOfBag = bagCount;
            // add the child bag container along with the child bag to the parent bag
            parentBag.bagsWithinThisBag.Add(aChildBagContainer);

            
        }
    }
}
