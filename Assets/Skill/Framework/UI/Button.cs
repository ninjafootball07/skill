using UnityEngine;
using System.Collections;
using System;

namespace Skill.Framework.UI
{
    /// <summary>
    /// Make a single press button. The user clicks them and something happens immediately.
    /// </summary>
    public class Button : FocusableControl
    {

        /// <summary>
        /// Text, image and tooltip for this button.
        /// </summary>
        public GUIContent Content { get; private set; }


        /// <summary>
        /// Occurs when users clicks the button
        /// </summary>
        public event EventHandler Click;
        /// <summary>
        /// when users clicks the button
        /// </summary>
        protected virtual void OnClick()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Create a Button
        /// </summary>
        public Button()
        {
            Content = new GUIContent();
        }

        /// <summary>
        /// Render button
        /// </summary>
        protected override void Render()
        {
            if (!string.IsNullOrEmpty(Name)) GUI.SetNextControlName(Name);
            if (Style != null)
            {
                if (GUI.Button(RenderArea, Content, Style))
                    OnClick();
            }
            else
            {
                if (GUI.Button(RenderArea, Content))
                    OnClick();
            }
        }

        /// <summary>
        /// Handle specified command. button respond to Enter command
        /// </summary>
        /// <param name="command">Command to handle</param>
        /// <returns>True if command is handled, otherwise false</returns>   
        public override bool HandleCommand(UICommand command)
        {
            if (command.Key ==  KeyCommand.Enter)
            {
                OnClick();
                return true;
            }
            return base.HandleCommand(command);
        }

    }

}