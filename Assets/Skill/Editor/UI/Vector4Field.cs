﻿using System;
using System.Collections.Generic;
using Skill.Framework.UI;
using UnityEditor;
using UnityEngine;

namespace Skill.Editor.UI
{
    /// <summary>
    /// Make an X, Y, Z and W field for entering a Vector4.
    /// </summary>
    public class Vector4Field : EditorControl
    {
        private string _Label;
        /// <summary>
        /// Label to display above the field.
        /// </summary>
        public String Label
        {
            get { return _Label; }
            set
            {
                _Label = value;
                if (_Label == null)
                    _Label = string.Empty;
            }
        }

        /// <summary>
        /// Occurs when value of Vector4Field changed
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// when value of Vector4Field changed
        /// </summary>
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        private Vector4 _Value;
        /// <summary>
        /// Vector4 - The value entered by the user.
        /// </summary>
        public Vector4 Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnValueChanged();
                }
            }
        }

        /// <summary>
        /// Create an instance of Vector4Field
        /// </summary>
        public Vector4Field()
        {
            _Label = string.Empty;
            this.Height = 38;
        }

        /// <summary>
        /// Render Vector4Field
        /// </summary>
        protected override void Render()
        {
            //if (!string.IsNullOrEmpty(Name)) GUI.SetNextControlName(Name);
            Value = EditorGUI.Vector4Field(RenderArea, _Label, _Value);
        }
    }
}
