using System;
using System.Collections.Generic;
using System.Text;

namespace Day8.Compiler
{
    public class Instruction : ICloneable
    {
        /// <summary>
        /// The number of times this instruction has executed
        /// </summary>
        public int instructionCounter { get; set; } = 0;

        /// <summary>
        /// The value assinged to this instruction
        /// </summary>
        public int argument { get; set; } = 0;

        /// <summary>
        /// The type of instruction to execute
        /// e.g. Accumlator, jump, etc
        /// </summary>
        public InstrcutionType instructionType { get; set; }

        /// <summary>
        /// Creates a deep copy clone of this class
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Instruction instructionClone = new Instruction();

            instructionClone.instructionCounter = this.instructionCounter;
            instructionClone.argument = this.argument;
            instructionClone.instructionType = this.instructionType;

            return instructionClone;
        }

        /// <summary>
        /// Takes in a line of text and and populates this classes properties
        /// with the data it find
        /// </summary>
        /// <param name="instructionText">Line of text containing an instruction</param>
        public void parseInstructionText(string instructionText)
        {
            // splint the instruction at the space and return an array of 2
            string[] instrionArray = instructionText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // find the instruction type, e.g. Accumulator, jump, etc
            this.instructionType = this.getInstructionType(instrionArray[0]);

            // find the value assoshiated with this instruction
            int theValue = 0;
            if (int.TryParse(instrionArray[1], out theValue) == true)
                this.argument = theValue;

        }

        /// <summary>
        /// converts an instruction from a string to an enum
        /// </summary>
        /// <param name="instruction">the instruction e.g. "acc", "jmp", etc</param>
        /// <returns>the converted instruction or InstrcutionType.UnknownInstruction</returns>
        private InstrcutionType getInstructionType(string instruction)
        {
            // the type of instruction we will return from this function
            // defaulted to unknown instrcution in case the instruction can not
            // be found
            InstrcutionType theInstuctionType = InstrcutionType.UnknownInstruction;

            // find which instruction we have been given
            // and return its enum equiverlent
            switch(instruction)
            {
                case "acc":
                    theInstuctionType = InstrcutionType.Accumulator;
                    break;

                case "jmp":
                    theInstuctionType = InstrcutionType.Jump;
                    break;

                case "nop":
                    theInstuctionType = InstrcutionType.NoOPeration;
                    break;
                    
                default:
                    theInstuctionType = InstrcutionType.UnknownInstruction;
                    break;


            }

            // return the instruction we found
            // or UnknownInstruction
            return theInstuctionType;
        }
    }

    /// <summary>
    /// All the instructions the compiler knows about
    /// </summary>
    public enum InstrcutionType
    {
        Accumulator,
        Jump,
        NoOPeration,
        UnknownInstruction
    }
}
