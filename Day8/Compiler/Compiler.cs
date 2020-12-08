using System;
using System.Collections.Generic;
using System.Text;

namespace Day8.Compiler
{
    public class Compiler
    {
        private List<Instruction> _listOfInstructions = new List<Instruction>();

        /// <summary>
        /// the previouse instruction that was executed, inishally set to null
        /// </summary>
        public Instruction previouseInstructionThatExecuted { get; set; } = null;

        /// <summary>
        /// the current value the accumulator is set too
        /// </summary>
        public int accumulatorValue { get; set; } = 0;

        /// <summary>
        /// The reason the program stopped
        /// </summary>
        public ReasonProgramFinished reasonProgramFinished { get; set; } = ReasonProgramFinished.NotSet;

        /// <summary>
        /// All the instructions that the compiler will run
        /// </summary>
        public List<Instruction> ListOfInstructionsToRun
        {
            get => this._listOfInstructions;
        }

        public Compiler()
        {

        }

        /// <summary>
        /// Allows the class to be inishalzed with a set of premade instructions
        /// </summary>
        /// <param name="listOfInstructionsToExecute"></param>
        public Compiler(List<Instruction> listOfInstructionsToExecute)
        {
            this._listOfInstructions = listOfInstructionsToExecute;   
        }

        /// <summary>
        /// Resets the compiler and adds the inputed instructions to be exectued
        /// </summary>
        /// <param name="listOfInstructionsToExecute">The instructions to be executed</param>
        public void setInstructions(List<Instruction> listOfInstructionsToExecute)
        {
            // reset the compiler so new instructions can be added
            this.resetCompiler();

            this._listOfInstructions = listOfInstructionsToExecute;
        }

        /// <summary>
        /// resets the compiler and clears out all instructions
        /// </summary>
        public void resetCompiler()
        {
            this._listOfInstructions = new List<Instruction>();
            this.previouseInstructionThatExecuted = null;
            this.accumulatorValue = 0;
            this.reasonProgramFinished = ReasonProgramFinished.NotSet;
        }

        /// <summary>
        /// Takes a list of instructions as a string
        /// and converts them to <see cref="Instruction"/> List
        /// (compiles the code)
        /// </summary>
        /// <param name="listOfInstructions"></param>
        public void parseInstructions(string listOfInstructions)
        {
            // find each line of instructions by splitting the text at each new line
            string[] instructionsSplitIntoLines = listOfInstructions.Split("\r\n");

            // go through each line (each instruction)
            foreach(string anInstrctionAsText in instructionsSplitIntoLines)
            {
                // create a new instruction class to store the instruction data in
                Instruction newInstruction = new Instruction();
                // parse the instruction text
                newInstruction.parseInstructionText(anInstrctionAsText);

                // add the instrcution to the list
                this._listOfInstructions.Add(newInstruction);
            }
        }

        /// <summary>
        /// Runs the compiled code
        /// </summary>
        public void run()
        {
            // the index position we should be looking at in
            // this._listOfInstructions
            int currentExecutionPosition = 0;

            // keep running the code until keepRunning is set to false;
            bool keepRunning = true;

            while(keepRunning)
            {
                // check there is a next instruction
                if(currentExecutionPosition >= this._listOfInstructions.Count)
                {// we have reached the end of the program

                    keepRunning = false;
                    this.reasonProgramFinished = ReasonProgramFinished.ProgramCompleted;
                    break;
                }

                // get the current instruction we should be looking at
                Instruction currentInstruction = this._listOfInstructions[currentExecutionPosition];

                // we don't want the instruction to run more than once
                if(currentInstruction.instructionCounter == 1)
                {// if an instruction has ran allready
                 // it means the program should stop running
                 // so break out of the loop
                    keepRunning = false;
                    this.reasonProgramFinished = ReasonProgramFinished.InfinatLoopDetected;
                    break;
                }

                // find out which instuction to execute
                switch (currentInstruction.instructionType)
                {
                    case InstrcutionType.Accumulator:
                        // executes the accumulator instruction and updates currentExecutionPosition
                        this.runAccumulatorInstruction(currentInstruction, ref currentExecutionPosition);
                        break;

                    case InstrcutionType.Jump:
                        this.runJumpInstruction(currentInstruction, ref currentExecutionPosition);
                        break;

                    case InstrcutionType.NoOPeration:
                        this.runNoOPerationInstruction(currentInstruction, ref currentExecutionPosition);
                        break;

                    case InstrcutionType.UnknownInstruction:
                        throw new Exception("unknown instruction");
                        
                    
                }

                // keep track of how many times this instruction has executed
                currentInstruction.instructionCounter++;
                // set the previouse instruction that was executed to this instrcution
                // This will let us know on the next loop what the previouse instruction was
                this.previouseInstructionThatExecuted = currentInstruction;
            }
        }


        #region instruction operations

        /// <summary>
        /// Increases or decreases the <see cref="accumulatorValue"/> by the ammount given in the current instructions argument
        /// </summary>
        /// <param name="currentInstruction">the instruction to execute (should be an Accumulator instruction)</param>
        /// <param name="currentExecutionPosition">The index position in the array this instrcution exists at. Will be updated to the next instruction position to run within this function</param>
        private void runAccumulatorInstruction(Instruction currentInstruction, ref int currentExecutionPosition)
        {
            // add too or subtract from the accumulatorvalue
            this.accumulatorValue += currentInstruction.argument;
            // move the execution pointer on by one so we can run the next instruction in the list
            // on the next loop
            currentExecutionPosition++;
        }

        /// <summary>
        /// Moves the currentExecutionPosition based on the currentInistructions argument ammount
        /// </summary>
        /// <param name="currentInstruction">The instruction to run (should be a Jump instruction)</param>
        /// <param name="currentExecutionPosition">The index position in the array this instrcution exists at. Will be updated to the next instruction position to run within this function</param>
        private void runJumpInstruction(Instruction currentInstruction, ref int currentExecutionPosition)
        {
            // move the currentExecutionPosition pointer + or - to what ever the argument is set too
            currentExecutionPosition += currentInstruction.argument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentInstruction">The instruction to run (should be a No Operation instruction)</param>
        /// <param name="currentExecutionPosition">The index position in the array this instrcution exists at. Will be updated to the next instruction position to run within this function</param>
        private void runNoOPerationInstruction(Instruction currentInstruction, ref int currentExecutionPosition)
        {
            // increase the current execution position by one (move to the next instruction in the list)
            currentExecutionPosition++;
        }
        #endregion

    }

    public enum ReasonProgramFinished
    {
        // the program ran to completion
        ProgramCompleted,
        // the compiler detected an infant loop
        InfinatLoopDetected,
        // the inishal value that should be set indicating no value has been set
        NotSet
    }
}
