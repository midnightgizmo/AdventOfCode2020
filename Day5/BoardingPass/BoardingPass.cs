using System;
using System.Collections.Generic;
using System.Text;

namespace Day5.BoardingPass
{
    public class BoardingPass
    {

        /// <summary>
        /// Converts the binary space partitioning bording pass to its equivilent
        /// row and column positions
        /// </summary>
        /// <param name="bordingPass">bording pass to parse (e.g. "FBFBBFFRLR")</param>
        public void parseBoardingPass(string bordingPass)
        {
            string binaryString = string.Empty;

            // find the row number in the boarding pass
            // go through the first 7 char which should be an F or a B
            // and convert them to a binary string
            for(int eachCharPosition = 0; eachCharPosition <= 7; eachCharPosition++)
            {
                switch(bordingPass[eachCharPosition])
                {
                    // represents a zero in binary
                    case 'F':

                        binaryString += "0";
                        break;

                    // represents a 1 in binary
                    case 'B':

                        binaryString += "1";
                        break;
                }
            }

            // we now have the Row number as a binary string, convert that to a number
            this.seatRowLocation = Convert.ToInt32(binaryString, 2);


            // find the column number in the boarding pass
            binaryString = string.Empty;
            // go through the letters at position 7 to 9 in the bording pass
            // and convert them to a binary string
            for (int eachCharPosition = 7; eachCharPosition < 10; eachCharPosition++)
            {
                switch (bordingPass[eachCharPosition])
                {
                    // represents a zero in binary
                    case 'L':

                        binaryString += "0";
                        break;

                    // represents a 1 in binary
                    case 'R':

                        binaryString += "1";
                        break;
                }
            }

            // we now have the Column number as a binary string, convert that to a number
            this.seatColumnLocation = Convert.ToInt32(binaryString, 2);
        }

        /// <summary>
        /// The row number this bording pass is located at
        /// </summary>
        public int seatRowLocation { get; set; }
        /// <summary>
        /// The column number this boarding pass is located at
        /// </summary>
        public int seatColumnLocation { get; set; }

        /// <summary>
        /// Calculates the seatID based on the seat row and seat column
        /// </summary>
        public int seatID
        {
            get
            {
                return (this.seatRowLocation * 8) + seatColumnLocation;
            }
        }
    }
}
