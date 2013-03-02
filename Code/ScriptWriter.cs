/**
    Craig Giles
    For use with 'Project Ocarina RPG Engine' developed in 2010
    
    ScriptWriter.cs
    ---------------

    The Script Writer was a basic .cs file that allowed me to generate scripts
    for my game engine while taking advantage of Visual Studio's integrated
    features such as intellisense. The Script Writer is broken down into two
    parts:

    Commands:
        This section of the script writer file lists all of the commands that 
        can be used to create scripts for the game. It will translate your
        script into usable commands. For example, if in your script you type
            "Loop(10)" 

        it is assumed that you would like to make the next set of commands loop
        10 times. The Loop() command will pass back a LoopCommand object which 
        contains the correct looping value.

    ScriptWriter:
        This is the section I used to create my scripts. In this section is a 
        commented out version of the scripting system that I used as an example.
        This script describes a traditional 2d game cutscene where the camera, 
        characters, music, and effects all happen in specified times. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ocarina {
    class ScriptWriter {

        #region CONDITIONS Prototypes
        public static InventoryContainsItemCommand InventoryContainsItem(string itemId) {
            return new InventoryContainsItemCommand(itemId);
        }
        #endregion


        #region ACTIONS Prototypes

        #region Base Commands
        public static WaitCommand Wait(object delay) {
            return new WaitCommand(float.Parse(delay.ToString()));
        }
        #endregion

        #region Block Commands
        // Block Commands
        private void IfThen(object value) { } //Needed this to satisfy the script writer, but wont work with reflection  
        public static IfThenCommand IfThen(object[] value) {
            return new IfThenCommand(value);
        }

        private void Loop(int value) { } //Needed this to satisfy the script writer, but wont work with reflection  
        public static LoopCommand Loop(object[] value) {
            //value[0] is the loop count
            //value[1] to value[end] is the commands
            return new LoopCommand(value);
        }
        #endregion

        #region Unit / Actor Commands
        // Unit / Actor 
        public static CreateActorCommand CreateActor(object actorId, object asset, object x, object y) {
            return new CreateActorCommand(actorId.ToString(), asset.ToString(), new Vector2(float.Parse(x.ToString()), float.Parse(y.ToString())));
        }

        public static RemoveActorCommand RemoveActor(string actorId) {
            return new RemoveActorCommand(actorId);
        }

        public static GiveExperienceCommand GiveExperience(string actorId, object value) {
            return new GiveExperienceCommand(actorId, int.Parse(value.ToString()));
        }

        public static SetExperienceCommand SetExperience(string actorId, object value) {
            return new SetExperienceCommand(actorId, int.Parse(value.ToString()));
        }

        public static SetLevelCommand SetLevel(string actorId, object value) {
            return new SetLevelCommand(actorId, int.Parse(value.ToString()));
        }

        public static LearnSkillCommand LearnSkill(string actorId, string skillId) {
            return new LearnSkillCommand(actorId, skillId);
        }

        public static UnLearnSkillCommand UnLearnSkill(string actorId, string skillId) {
            return new UnLearnSkillCommand(actorId, skillId);
        } 
        #endregion

        #endregion


        #region Dialog Prototypes
        public static DialogCommand Dialog(object asset) {
            return new DialogCommand(asset.ToString());
        }

        //private void DialogPrompt(string actorId, string portraitId) { }
        //public static DialogPromptCommand DialogPrompt(object[] value) {
        //    return new DialogPromptCommand(value);
        //}

        //public static AddPromptCommand AddPrompt(object text, object script) {
        //    return new AddPromptCommand(text.ToString(), script.ToString());
        //}

        //public static SayCommand Say(object text) {
        //    return new SayCommand(text.ToString());
        //}

        //public static SetTextColorCommand SetTextColor(object color) {
        //    return new SetTextColorCommand(color.ToString());
        //}

        //public static ClearWindowCommand ClearWindow() {
        //    return new ClearWindowCommand();
        //}
        #endregion


        #region MISC Prototypes
        private void Break() { }
        private void Continue() { }
        #endregion


        public ScriptWriter() {
            
            /* Sample Script for cut scene */
            //CreateActor("Locke", x, y);
            //CreateActor("Terra", x, y);

            //FadeIn(5000);
            //PlayMusic("Music/introduction");
            //WalkTo("Terra", x, y);
            //Dialog("Dialog/Act01/TerraLockeIntro/001");
            //WalkTo("Locke", x, y);
            //PlayAnimation("Locke", "SurpriseDown");
            //Dialog("Dialog/Act01/TerraLockeIntro/002");
            //WalkTo("Locke", x, y);
            //Wait(2000);
            //WalkTo("Locke", x, y);
            //PlayAnimation("Locke", "ThinkingLeft");
            //Wait(2000);
            //PlayAnimation("Locke", "AgreeDown");
            //Dialog("Dialog/Act01/TerraLockeIntro/003");

        }
    }
}
