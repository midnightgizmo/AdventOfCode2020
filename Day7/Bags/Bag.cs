using System;
using System.Collections.Generic;
using System.Text;

namespace Day7.Bags
{
    public class Bag
    {

        public System.Collections.Hashtable parentBags { get; set; } = new System.Collections.Hashtable();

        public string bagColor { get; set; }

        public List<ChildBag> bagsWithinThisBag { get; set; } = new List<ChildBag>();
    }

    public class ChildBag
    {
        public Bag bag { get; set; }
        public int NumberOfThisKindOfBag { get; set; }
    }
}
