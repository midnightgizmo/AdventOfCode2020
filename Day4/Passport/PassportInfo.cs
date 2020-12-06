using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4.Passport
{
    public class PassportInfo
    {
        public void parseInfo(string info)
        {
            // each bit of password info can be seperated by a space or a new line.
            // To make it easier for us to parse, turn all new lines into a space
            // This will make it so all info is on one line seperated by a space
            string passportInfoAsString = info.Replace("\r\n", " ");

            // split the passport up into each field by splitting on the space " " and then loop through
            // each one
            foreach(string passportField in passportInfoAsString.Split(" ",StringSplitOptions.RemoveEmptyEntries))
            {
                string[] keyValuePairs = passportField.Split(':');
                // find which field we have and extract its infomration.
                // also se its corisponding isPressent to true so we know
                // we have found that field
                this.parseKeyValuePair(keyValuePairs);
            }
            
        }

        public bool doesPassportPassInspection_PuzzleOne()
        {
            // all the below must be pressent for it to be a valid passport
            if (this.isBirthYearPressent &&
                this.isIssueYearPressent &&
                this.isExpirationYearPressent &&
                this.isHeightPressent &&
                this.isHairColorPressent &&
                this.isEyeColorPressent &&
                this.isPassportIDPressent)
                return true;
            // one or more of the above are missing so it is an invalid passport
            else
                return false;
        }

        public bool doesPassportPassValidationChecks_PuzzleTwo()
        {
            // all the below must be valid (true) for it to pass checks)
            if (this.isBirthYearValid &&
                this.isIssueYearValid &&
                this.isExpirationYearValid &&
                this.isHeightValid &&
                this.isHairColorValid &&
                this.isEyeColorValid &&
                this.isPassportIDValid)
                return true;
            // one ore more checks were false so passport did not pass checks.
            else
                return false;
        }

        /// <summary>
        /// Extract the value from the key and update the assoshiated isXPressent property
        /// to true to say it has been found
        /// </summary>
        /// <param name="keyValuePair">container and key and a value in that order</param>
        private void parseKeyValuePair(string[] keyValuePair)
        {
            // we are assuming all key value pairs are valid and we
            // are able to do conversions from strings to ints ect withouth
            // any problems.
            // Really should do some more error checking hear incase they 
            // dont' convert

            // used for part two of the puzzle where more checks are done
            // on the inputed values
            Regex re;
            Match result;

            // look to see which key we have
            switch (keyValuePair[0])
            {
                // birth year
                case "byr":

                    this.birthYear = int.Parse(keyValuePair[1]);
                    this.isBirthYearPressent = true;
                    
                    if (birthYear >= 1920 && birthYear <= 2002)
                        this.isBirthYearValid = true;
                    break;

                // issue year
                case "iyr":

                    this.issueYear = int.Parse(keyValuePair[1]);
                    this.isIssueYearPressent = true;

                    if (issueYear >= 2010 && issueYear <= 2020)
                        this.isIssueYearValid = true;
                    break;

                // expiration year
                case "eyr":

                    this.expirationYear = int.Parse(keyValuePair[1]);
                    this.isExpirationYearPressent = true;

                    if (expirationYear >= 2020 && expirationYear <= 2030)
                        this.isExpirationYearValid = true;
                    break;

                // height
                case "hgt":

                    this.height = keyValuePair[1];
                    this.isHeightPressent = true;

                    // use regex to find if its valid pattern
                    // valid patterns are numbers followed by eaither 'cm or 'in'
                    // e.g. "14in" or "12cm"
                    //
                    // The regex pattern we will use
                    // ^(\d+)(cm|in)$
                    // The above regex patter explained.
                    // ^ means the string must start with whats ever is inside the brackets
                    // (\d+) d means number + means one or more of the d (so one more more numbers)
                    // (cm|in) string must be 'cm' or 'in'
                    // $ means string must end in whats in side the brackes to the left of the $. so must end in 'cm' or 'in'

                    // using System.Text.RegularExpressions (see top of file)
                    re = new System.Text.RegularExpressions.Regex(@"^(\d+)(cm|in)$");
                    // run the regular expression agaist this height
                    result = re.Match(this.height);

                    // did the height pass the regex check?
                    if (result.Success)
                    {// height passed regex

                        // the result.Groups is an array of 3
                        //[0] = the hole match e.g. '12in'
                        //[1] = the number part of the text (\d+)
                        //[2] = the string part of the text (cm|in)
                        string numberPart = result.Groups[1].Value;
                        string alphaPart = result.Groups[2].Value;

                        // convert the persons height which is at moment a string to a number
                        int personHeight = 0;
                        int.TryParse(numberPart, out personHeight);

                        // see if we are working in 'in' or 'cm'
                        switch (alphaPart)
                        {
                            // inches
                            case "in":

                                if (personHeight >= 59 && personHeight <= 76)
                                    this.isHeightValid = true;
                                break;

                            // centimeters
                            case "cm":

                                if (personHeight >= 150 && personHeight <= 193)
                                    this.isHeightValid = true;
                                break;
                        }
                    }

                    
                    break;

                // hair color
                case "hcl":

                    this.hairColor = keyValuePair[1];
                    this.isHairColorPressent = true;

                    // Use regex to work out if the hairColor value is correct.
                    // It should be a "#" followed by exactly six characters 0-9 or a-f.
                    // regex to use to check for this is
                    //^(#)([a-f]|[0-9]){9}$
                    // The above regex patter explained.
                    // ^ means the string must start with whats ever is inside the brackets
                    // (#) look for the string "#"
                    // ([a-f]|[0-9]) look for any letter a to f or any number 0 to 9
                    // {6} means what ever is to the left of it (the stuff in the brackets) must occur 6 times 
                    // $ means string must end in whats in side the brackes () to the left of the $. so must end 6 chars that are made 
                    // up of a to f and or 0 to 9

                    // using System.Text.RegularExpressions (see top of file)
                    re = new System.Text.RegularExpressions.Regex(@"^(#)([a-f]|[0-9]){6}$");
                    // run the regular expression agaist this height
                    result = re.Match(this.hairColor);

                    // did the haircolor pass the regex checks?
                    if (result.Success)
                        this.isHairColorValid = true;

                    break;

                // eye color
                case "ecl":

                    this.eyeColor = keyValuePair[1];
                    this.isEyeColorPressent = true;

                    // uthe eye color must be one of the following
                    // amb blu brn gry grn hzl oth.
                    re = new System.Text.RegularExpressions.Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$");
                    // run the regular expression agaist this height
                    result = re.Match(this.eyeColor);
                    // did the eye color pass the regex
                    if (result.Success)
                        this.isEyeColorValid = true;

                    break;

                // passport id
                case "pid":

                    this.passportID = keyValuePair[1];
                    this.isPassportIDPressent = true;

                    // check passport id is a nine-digit number, including leading zeroes
                    re = new System.Text.RegularExpressions.Regex(@"^([0-9]){9}$");
                    // run the regular expression agaist this height
                    result = re.Match(this.passportID);
                    // did the passportID pass the regex
                    if (result.Success)
                        this.isPassportIDValid = true;

                    break;

                // country id
                case "cid":

                    this.countryID = int.Parse(keyValuePair[1]);
                    this.isCountryIDPressent = true;
                    break;
            }
        }
        /// <summary>
        /// byr (Birth Year)
        /// </summary>
        public int birthYear { get; set; }
        public bool isBirthYearPressent { get; set; } = false;
        public bool isBirthYearValid { get; set; } = false;
        /// <summary>
        /// iyr (Issue Year)
        /// </summary>
        public int issueYear { get; set; }
        public bool isIssueYearPressent { get; set; } = false;
        public bool isIssueYearValid { get; set; } = false;
        /// <summary>
        /// eyr (Expiration Year)
        /// </summary>
        public int expirationYear { get; set; }
        public bool isExpirationYearPressent { get; set; } = false;
        public bool isExpirationYearValid { get; set; } = false;
        /// <summary>
        /// hgt (Height)
        /// </summary>
        public string height { get; set; }
        public bool isHeightPressent { get; set; } = false;
        public bool isHeightValid { get; set; } = false;
        /// <summary>
        /// hcl (Hair Color)
        /// </summary>
        public string hairColor { get; set; }
        public bool isHairColorPressent { get; set; } = false;
        public bool isHairColorValid { get; set; } = false;
        /// <summary>
        /// ecl (Eye Color)
        /// </summary>
        public string eyeColor { get; set; }
        public bool isEyeColorPressent { get; set; } = false;
        public bool isEyeColorValid { get; set; } = false;
        /// <summary>
        /// pid (Passport ID)
        /// </summary>
        public string passportID { get; set; }
        public bool isPassportIDPressent { get; set; } = false;
        public bool isPassportIDValid { get; set; } = false;
        /// <summary>
        /// cid (Country ID)
        /// </summary>
        public int countryID { get; set; }
        public bool isCountryIDPressent { get; set; } = false;
    }
}
