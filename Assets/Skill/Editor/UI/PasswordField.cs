﻿using System;
using System.Collections.Generic;
using Skill.Framework.UI;
using UnityEditor;
using UnityEngine;

namespace Skill.Editor.UI
{
    /// <summary>
    /// Make a text field where the user can enter a password.
    /// </summary>
    public class PasswordField : EditorControl
    {
        /// <summary>
        /// Optional label to display in front of the password field.
        /// </summary>
        public GUIContent Label { get; private set; }        

        private string _Password;
        /// <summary>
        /// The password to edit.
        /// </summary>
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                if (_Password != value)
                {
                    _Password = value;
                    OnPasswordChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when Password of PasswordField changed
        /// </summary>
        public event EventHandler PasswordChanged;
        /// <summary>
        /// when Password of PasswordField changed
        /// </summary>
        protected virtual void OnPasswordChanged()
        {
            if (PasswordChanged != null) PasswordChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Create a PasswordField
        /// </summary>
        public PasswordField()
        {
            this._Password = string.Empty;
            this.Label = new GUIContent();
            this.Height = 16;
        }

        /// <summary>
        /// Render Render
        /// </summary>
        protected override void Render()
        {
            string result;
            if (!string.IsNullOrEmpty(Name)) GUI.SetNextControlName(Name);
            if (Style != null)
            {
                result = EditorGUI.PasswordField(RenderArea, Label, Password, Style);
            }
            else
            {
                result = EditorGUI.PasswordField(RenderArea, Label, Password);
            }

            Password = result;
        }
    }
}
