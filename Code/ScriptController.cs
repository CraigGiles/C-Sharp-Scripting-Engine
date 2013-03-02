/**
    Craig Giles
    For use with 'Project Ocarina RPG Engine' developed in 2010
    
    ScriptController.cs
    -------------------
    The Script Controller is the main driver for the scripting system 
    developed for the XNA Based Ocarina Engine. The controller is broken
    up into several sections;

    Load Script:
        Loading the script takes the XNA Content Manager, loads the string
        representation of the script (saved in a .txt file.. Sample 
        scripts are uploaded in the TestScripts folder), applies
        any cleanup required for these scripts, and runs it through
        the interpreter. 

    Interpreter:
        This is the meat of the class and is where all the 'magic' happens.
        The interpret function is commented pretty well so read through
        the code and comments to get a better understanding on what and
        how it's working

    Utility Functions:
        These functions provide utility for the scripting language in order
        to interpret the scripts correctly. For example, in the scripting
        language you can pass parameters.. some of these parameters may be
        scriptable commands, while others might be strings. Reflection is
        heavily used in this section of the file.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Reflection;

namespace Ocarina {
    class ScriptController {
        public ContentManager Content {get;set;}
        public ScriptController(ContentManager content) { this.Content = content; }

        #region Load and Interpret Script
        public void LoadScript(string asset) {
            string script = string.Empty;
            script = Content.Load<string>(asset);
            string[] lines = cleanup(script);
            interpret(lines);
        }

        private void interpret(string[] script) {
            List<BaseCommand> commands = new List<BaseCommand>();
            List<string> lines = new List<string>();
            lines.AddRange(script);
            int i = 0;

            while (i < lines.Count) {
                // Data used to to convert the script from text to meaningful objects
                #region Interpreting Data
                // Boolean values to indicate which branch of script we're interpreting
                bool blockStatement = false;
                bool singleStatement = false;

                // What commands are between the { }'s in a block command (as strings)
                List<string> blockCommandsInScriptForm = new List<string>();

                // The final set of parameters that include the parameters for the
                // block command as well as a list of commands to execute
                List<object> invokeParameters = new List<object>();
                List<object> blockCommands = new List<object>();

                // Get the current line of the script, and check which branch we're
                // going to interpret. 
                string currentLine = lines[i];
                if (currentLine == string.Empty) { ++i; continue; }
                if (isBlockCommand(currentLine)) blockStatement = true;
                if (blockStatement && !lines[i + 1].StartsWith("{")) singleStatement = true;
                #endregion

                // Single Statement Block commands are block commands (such as
                // Loop or IfThen statements) but don't have any { }'s and therefore
                // only execute a single statement in its block.
                #region Single Statement Block Command
                if (blockStatement && singleStatement) {
                    //the next command is the only command needed for the block statement
                    MethodInfo blockCommand = GetMethodInfo(currentLine);
                    object[] blockParameters = GetParameterList(currentLine);
                    ++i;
                    currentLine = lines[i];
                    blockCommandsInScriptForm.Add(currentLine);

                    // Convert each script command to an actual command to execute
                    foreach (string s in blockCommandsInScriptForm) {
                        blockCommands.Add(ConvertStringToCommand(s));
                        //MethodInfo info = GetMethodInfo(s);
                        //object[] plist = GetParameterList(s);
                        //blockCommands.Add(info.Invoke(this, plist) as BaseCommand);
                    }

                    // Add all of these commands to one big array and send it off
                    // to the correct class for processing
                    invokeParameters.AddRange(blockParameters);
                    invokeParameters.AddRange(blockCommands);
                    object[] ary = { invokeParameters.ToArray() };
                    commands.Add((BaseCommand)blockCommand.Invoke(this, ary));
                }
                #endregion

                // Multi-Statement Block Commands are block commands (such as
                // Loop or IfThen statements) followed by a block of code surrounded
                // by { }'s. 
                #region Multi-Statement Block Command
                else if (blockStatement && !singleStatement) {
                    // what command is the block? (Loop, if / then / else, etc) and what
                    // parameters does it need to function?
                    MethodInfo blockCommand = GetMethodInfo(currentLine);
                    object[] blockParameters = GetParameterList(currentLine);

                    ++i;
                    lines[i] = lines[i].Substring(lines[i].IndexOf('{') + 1).Trim();

                    //get all block commands
                    while (true) {
                        //is current line the end of our block? break out!
                        currentLine = lines[i];
                        //if (currentLine == string.Empty) continue;
                        if (currentLine.StartsWith("}")) break;
                        //otherwise, add the command to the block command list
                        blockCommandsInScriptForm.Add(currentLine);
                        ++i;
                    }

                    // Get rid of the } so we have a command on the next line
                    lines[i] = lines[i].Substring(lines[i].IndexOf('}') + 1).Trim();

                    // Convert each script command to an actual command to execute
                    foreach (string s in blockCommandsInScriptForm) {
                        blockCommands.Add(ConvertStringToCommand(s));
                    }

                    // Add all of these commands to one big array and send it off
                    // to the correct class for processing
                    invokeParameters.AddRange(blockParameters);
                    invokeParameters.AddRange(blockCommands);
                    object[] ary = { invokeParameters.ToArray() };
                    commands.Add((BaseCommand)blockCommand.Invoke(this, ary));
                }
                #endregion

                // Single Statement Commands are commands that are executed just
                // like function calls
                #region Single Statement Command
                else //we're not in a block statement
                {
                    commands.Add(ConvertStringToCommand(currentLine));
                    ++i;
                }
                #endregion
            }//end while
        }
        #endregion

        #region Utility Functions
        private bool isScriptCommand(string s) {
            CommandList cmd = CommandList.Else;
            ConditionList cmd2 = ConditionList.InventoryContainsItem;
            int sub = s.IndexOf('(');
            if (sub <= 0) return false;
            string comm = s.Substring(0, sub);
            return (Enum.TryParse<CommandList>(comm, out cmd) || Enum.TryParse<ConditionList>(comm, out cmd2));
        }

        private bool isBlockCommand(string s) {
            BlockCommands cmd = BlockCommands.Else;
            return Enum.TryParse<BlockCommands>(s.Substring(0, s.IndexOf('(')), out cmd);
        }

        private BaseCommand ConvertStringToCommand(string s) {
            MethodInfo mi = GetMethodInfo(s);
            object[] parameters = GetParameterList(s);
            return mi.Invoke(this, parameters) as BaseCommand;
        }

        private MethodInfo GetMethodInfo(string s) {
            string command = string.Empty;
            int firstPar = s.IndexOf('(') + 1;
            int secondPar = s.IndexOf(')');
            command = s.Substring(0, firstPar - 1);
            return typeof(ScriptWriter).GetMethod(command);
        }

        private object[] GetParameterList(string s) {
            List<object> parameters = new List<object>();
            
            int firstPar = s.IndexOf('(') + 1;
            int secondPar = s.LastIndexOf(')');

            string tmp = s.Substring(firstPar, secondPar - firstPar);
            string[] p = SpecialSplitString(tmp);
            
            //if there is no parameters, just return
            if (p.Length == 1 && p[0] == string.Empty) return parameters.ToArray();

            for (int i = 0; i < p.Length; ++i) {
                p[i] = p[i].Replace("\"", string.Empty);
                //either the parameter is a scriptable command,
                //string, or empty string
                if (isScriptCommand(p[i].ToString())) {
                    parameters.Add(ConvertStringToCommand(p[i].ToString()));
                }
                else if (p[i].ToString().Trim() != string.Empty)
                    parameters.Add(p[i].ToString().Trim());
                
            }

            return parameters.ToArray();
        }

        /// <summary>
        /// Custom split so dialog with "text, and more text","another parameter"
        /// would be split correctly
        /// </summary>
        public string[] SpecialSplitString(string tmp) {
            List<string> l = new List<string>();
            List<string> quotes = new List<string>();
            string asdf = string.Empty;

            bool delete = false;
            foreach (char c in tmp) {
                //first " .. set delete to true (delete == inside quotes)
                if (c == '"' && !delete) { delete = true; continue; }
                
                //This special case happens when we've hit the end of a quotation
                //mark, and therefore the string should be appended to the list
                else if (c == '"' && delete) {
                    l.Add(asdf);
                    asdf = string.Empty;
                    delete = false;
                }

                // This special case happens when we've hit a comma
                // but are not inside quotation marks. IE: vector2
                else if (c == ',' && !delete) {
                    if (asdf != string.Empty) {
                        l.Add(asdf);
                        asdf = string.Empty;
                    }
                    continue;
                }

                // this special case happens when we're dealing with
                //something like InventoryContainsItem("MysticalKey")
                // and will add the ) onto the end of the previous string.
                else if (c == ')') {
                    l[l.Count - 1] += c;
                }
                else
                    asdf += c;
            }

            if (asdf != string.Empty) l.Add(asdf);
            return l.ToArray();
        }

        private string[] cleanup(string script) {
            //script = script.Replace('"', '\r');
            script = script.Replace("\r", string.Empty);
            script = script.Replace("\n", string.Empty);
            script = script.Replace("\t", string.Empty);
            string[] lines = script.Split(';');
            for (int i = 0; i < lines.Length; ++i) lines[i] = lines[i].Trim();
            return lines;
        }
        #endregion
    }
}
