﻿using System;
using System.Collections.Generic;
using Skill.Framework.UI;
using UnityEditor;
using UnityEngine;

namespace Skill.Editor.UI
{
    /// <summary>
    /// Make Center and Extents field for entering a Bounds.
    /// </summary>
    public class BoundsField : EditorControl
    {
        /// <summary>
        /// Optional label to display above the field.
        /// </summary>
        public GUIContent Label { get; private set; }

        /// <summary>
        /// Occurs when value of BoundsField changed
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// when value of BoundsField changed
        /// </summary>
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        private Bounds _Value;
        /// <summary>
        /// Bounds - The value entered by the user.
        /// </summary>
        public Bounds Value
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
        /// Create a BoundsField
        /// </summary>
        public BoundsField()
        {
            Label = new GUIContent();
            this.Height = 52;
        }

        /// <summary>
        /// Render BoundsField
        /// </summary>
        protected override void Render()
        {
            //if (!string.IsNullOrEmpty(Name)) GUI.SetNextControlName(Name);
            Value = EditorGUI.BoundsField(RenderArea, Label, _Value);
        }
    }
}
