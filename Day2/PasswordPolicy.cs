using System;
using System.Collections.Generic;
using System.Text;

namespace Day2
{
    public class PasswordPolicy
    {

        /// <summary>
        /// Takes in a password policy e.g. 6-10 s: snkscgszxsssscss
        /// and parses the data
        /// </summary>
        /// <param name="rawData">a single password policy</param>
        public PasswordPolicy(string rawData)
        {
            // make a reference to the password policy
            this._RawData = rawData;

            // split the password policy up on the spaces
            string[] policyRows = rawData.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // finds the 2 numbers that are at the begining of the password policy
            this.parseColumnOne(policyRows[0]);
            // finds the individual charactor in the middle of the password policy
            this.CharToLookFor = policyRows[1][0];
            // finds the password (last bit of info in the password policy)
            this.password = policyRows[2];
        }

        /// <summary>
        /// The password
        /// </summary>
        public string password;
        /// <summary>
        /// The individual charactor in the middle of the password policy
        /// </summary>
        public char CharToLookFor;
        /// <summary>
        /// The first number in the password policy
        /// </summary>
        public int minTimesCharShouldOccur = 0;
        /// <summary>
        /// The second number in the password policy
        /// </summary>
        public int MaxTimesCharShouldOccur = 0;

        /// <summary>
        /// the unparsed PasswordPolicy from the puzzelData (single line of data)
        /// </summary>
        private string _RawData = string.Empty;
        /// <summary>
        /// The unparsed PasswordPolicy from the puzzelData (single line of data)
        /// </summary>
        public string RawData
        {
            get => this._RawData;
        }

        /// <summary>
        /// Checks to see if the password is valid based on
        /// the rules set in Puzzle one
        /// </summary>
        /// <returns></returns>
        public bool isPasswordValid_PuzzleOne()
        {
            int NumTimesCharFound = 0;
            // go through each letter in the password
            for(int index = 0; index < this.password.Length; index++)
            {
                // check to see if the current char is the char we are looking for
                if (this.password[index] == this.CharToLookFor)
                    NumTimesCharFound++; // keep track of number of times the CharToLookFor was been found in this password
            }

            // check to see if the number of times we found the this.CharToLookFor falls
            // within the min and max numbers.
            if ((NumTimesCharFound >= this.minTimesCharShouldOccur) &&
                (NumTimesCharFound <= this.MaxTimesCharShouldOccur))
                return true; // password passes checcks
            else
                return false;// password fails checks
        }

        /// <summary>
        /// Checks to see if the password is valid based on
        /// the rules set in Puzzle two
        /// </summary>
        /// <returns></returns>
        public bool isPasswordValid_PuzzleTwo()
        {
            // keep track of if we found matches to the rules for the password
            bool wasFirstPositionFound = false;
            bool wasSecondPositionFound = false;

            // check the passwords char at minTimesCharSHouldOccur to see if it matches this.CharTolookFor
            if (this.password[minTimesCharShouldOccur - 1] == this.CharToLookFor)
                wasFirstPositionFound = true; // first condition met

            // check the passwords char at MaxTimesCharShouldOccur to see if it matches this.CharToLookFor
            if (this.password[MaxTimesCharShouldOccur - 1] == this.CharToLookFor)
                wasSecondPositionFound = true; // second condition met

            // to be a valid password only one of above if statments can be true
            if (wasFirstPositionFound != wasSecondPositionFound)
                return true; // one of them is true so its a valid password
            else
                return false; // both if statments were eaither false or both were true
        }



        /// <summary>
        /// Extracts the data from the first column off the password policy
        /// </summary>
        /// <param name="data"></param>
        private void parseColumnOne(string data)
        {
            // there are 2 numbers seperated by a '-'
            string[] splitValues = data.Split('-', StringSplitOptions.RemoveEmptyEntries);

            // get the first number
            int.TryParse(splitValues[0], out this.minTimesCharShouldOccur);
            // get the second number
            int.TryParse(splitValues[1], out this.MaxTimesCharShouldOccur);
        }
    }
}
