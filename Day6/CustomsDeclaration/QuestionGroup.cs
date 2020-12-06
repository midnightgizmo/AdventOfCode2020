using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day6.CustomsDeclaration
{
    public class QuestionGroup
    {
        private List<CustomerDeclarationForm> _customerDeclarationFormList = new List<CustomerDeclarationForm>();
        // all the questions that have been answerd within this group (contains duplicats)
        private List<string> _questionsInGroupList = new List<string>();
        // a list of all the distinct questions in the group
        // (e.g. if we had a b c a d c e, the distnct questions woudl be a b c d e)
        private List<string> _distinctQuestionsInGroupList;

        /// <summary>
        /// Parses the string of questions within the question group data
        /// </summary>
        /// <param name="questionGroupData"></param>
        public void parseData(string questionGroupData)
        {
            


            // each line represents one persons questions they answer yes to
            string[] eachPersonsQuestionsArray = questionGroupData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            // go throuh each line (that contains a list of questions the person answerd yes too)
            foreach(string aPersonsQuestion in eachPersonsQuestionsArray)
            {
                CustomerDeclarationForm customerDeclarationForm = new CustomerDeclarationForm();
                customerDeclarationForm.originalData = aPersonsQuestion;

                // go through each question the person has (each question is a letter of the alphabet which indicates they have answerd yes to it)
                foreach (char aQuestion in aPersonsQuestion)
                {
                    // convert to the char to a string
                    string theQuestionID = aQuestion.ToString();

                    // add this question to the perons declaration form.
                    customerDeclarationForm.eachQuestion.Add(theQuestionID);
                    
                    // keeps track of every question in this question group
                    _questionsInGroupList.Add(theQuestionID);
                }

                // keep track of each declarationForm
                this._customerDeclarationFormList.Add(customerDeclarationForm);
            }

            // finds all distinct questions
            this._distinctQuestionsInGroupList = new List<string>(_questionsInGroupList.Distinct());




        }

        /// <summary>
        /// the number of distinct questions in group
        /// (e.g. if we had a b c a d c e as the questions, the distnct questions woudl be a b c d e,
        /// so the count woudl be 5)
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfDistinctQuestionsAskedForThisGroup()
        {
            //IEnumerable<string> distinctQuestionsAsksed = questionsInGroupList.Distinct();
            //return distinctQuestionsAsksed.Count();
            
            // returns the total number of questions found
            return this._distinctQuestionsInGroupList.Count();
        }

        public List<string> distinctQuestionsInGroupList
        {
            get => this._distinctQuestionsInGroupList;
        }

        /// <summary>
        /// List of all customers in this question group
        /// </summary>
        public List<CustomerDeclarationForm> customerDeclarationFormList
        {
            get => this._customerDeclarationFormList;
        }
    }

    /// <summary>
    /// Model to hold data for a customers declaration form
    /// (holds all the answer the customer marked yes too)
    /// </summary>
    public class CustomerDeclarationForm
    {
        public string originalData { get; set; }
        public List<string> eachQuestion { get; set; } = new List<string>();
    }
}
