using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
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

            // create an instance of compiler that will run the code
            Compiler.Compiler compiler = new Compiler.Compiler();

            // covert the text file into a list of instcutions the compiler will understand
            compiler.parseInstructions(puzzleData);

            // go through the instructions when we come accross a jump or no operation change
            // the code and then run it to see if the program executes correctly.
            // once we find a sequence where the code executes correctly, return that executions
            // acccumilators value as the answer to the puzzle
            int accumulatorValue = this.findCorrectCompilerSequence(compiler.ListOfInstructionsToRun);


            // the answer to part 2 of the puzzle
            return accumulatorValue;
        }

        /// <summary>
        /// The passed in instructions has infinat loop in its exection.
        /// This function alter the instructions to find the correct sequence
        /// which allows the code to run to completion
        /// </summary>
        /// <param name="originalInstructions">the instructions that when run cause an infant loop</param>
        /// <returns>the accumulator value on the execution of code that runs the correct instructions or int.MinValue if there was a problem</returns>
        private int findCorrectCompilerSequence(List<Compiler.Instruction> originalInstructions)
        {
            // create an instance of the compiler which will run the code
            Compiler.Compiler compiler = new Compiler.Compiler();

            // go through each instruction
            for(int eachInstructionIndex = 0; eachInstructionIndex < originalInstructions.Count; eachInstructionIndex++)
            {
                // when we find a jump or No OPeration instruction, swop them around and rerun the code.
                // if the code executes without terminating because of infant loop, we have found
                // the sequence of instructions that should be executed

                // get the current instruction
                Compiler.Instruction currentInstruction = originalInstructions[eachInstructionIndex];

                // check to see if this is a nump or NoOPeration instruction.
                // we don't care about any other kind of instruction, so just
                // carry onto the next loop if we don't find one.
                switch (currentInstruction.instructionType)
                {
                    // we found a jump instruction
                    case Compiler.InstrcutionType.Jump:



                        // if the instructions have previusly been ran, there currentInstruction.instructionCounter will not be zero which
                        // will effect the exectuion of the program and most likely make the program terminate
                        // because it thinks an infinant loop has been detected.
                        // So reset all instruction counters to zero
                        this.resetInstructionCounterOnInstructionList(originalInstructions);

                        // change the current instruction from a jump to a no operation
                        currentInstruction.instructionType = Compiler.InstrcutionType.NoOPeration;
                        // reset the compiler so its ready to run a new sequence of instructions
                        compiler.resetCompiler();
                        // give the compiler the new instructions
                        compiler.setInstructions(originalInstructions);
                        // run the new sequence
                        compiler.run();

                        // check to see if the program terminated sucsesfully or it terminated because
                        // of an infant loop
                        if(compiler.reasonProgramFinished == Compiler.ReasonProgramFinished.ProgramCompleted)
                        {// we have found the correct instructions for the program to run
                         // no need to carry on doing any more checks

                            // return from this function with accumulator value for the sequence we just ran
                            return compiler.accumulatorValue;
                        }
                        else
                        {// program did not execute sucsesfully so this sequnce of instructions is wrong
                            
                            // reset the instruction back to its original instruction and move
                            // onto the next instruction int he loop
                            currentInstruction.instructionType = Compiler.InstrcutionType.Jump;
                        }

                        break;

                    // we found a No Operation instruction
                    case Compiler.InstrcutionType.NoOPeration:

                        // if the instructions have previusly been ran, there currentInstruction.instructionCounter will not be zero which
                        // will effect the exectuion of the program and most likely make the program terminate
                        // because it thinks an infinant loop has been detected.
                        // So reset all instruction counters to zero
                        this.resetInstructionCounterOnInstructionList(originalInstructions);

                        // change the current instruction from a no operation to a jump instruction
                        currentInstruction.instructionType = Compiler.InstrcutionType.Jump;
                        // reset the compiler so its ready to run a new sequence of instructions
                        compiler.resetCompiler();
                        // give the compiler the new instructions
                        compiler.setInstructions(originalInstructions);
                        // run the new sequence
                        compiler.run();

                        // check to see if the program terminated sucsesfully or it terminated because
                        // of an infant loop
                        if (compiler.reasonProgramFinished == Compiler.ReasonProgramFinished.ProgramCompleted)
                        {// we have found the correct instructions for the program to run
                         // no need to carry on doing any more checks

                            // return from this function with accumulator value for the sequence we just ran
                            return compiler.accumulatorValue;
                        }
                        else
                        {// program did not execute sucsesfully so this sequnce of instructions is wrong


                            // reset the instruction back to its original instruction and move
                            // onto the next instruction int he loop
                            currentInstruction.instructionType = Compiler.InstrcutionType.NoOPeration;
                        }


                        break;
                }    

            }
            // this should never be hit but if it is, there is a problem
            return int.MinValue;
        }

        private void resetInstructionCounterOnInstructionList(List<Compiler.Instruction> instructionsList)
        {
            foreach (Compiler.Instruction anInstruction in instructionsList)
                anInstruction.instructionCounter = 0;
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
