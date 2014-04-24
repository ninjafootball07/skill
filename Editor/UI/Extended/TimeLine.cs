﻿using UnityEngine;
using System.Collections;
using Skill.Framework.UI;

namespace Skill.Editor.UI.Extended
{
    /// <summary>
    /// Defines base behavior for a time line
    /// </summary>
    public interface ITimeLine
    {
        /// <summary> ZoomFactor </summary>
        double ZoomFactor { get; }
        /// <summary> Start time of visible area </summary>
        double StartVisible { get; set; }
        /// <summary> End time of visible area </summary>
        double EndVisible { get; set; }
        /// <summary> Maximum available time to scroll</summary>
        double MaxTime { get; set; }
        /// <summary> Minimum available time to scroll</summary>
        double MinTime { get; set; }
        /// <summary> Start time of selected time </summary>
        double StartSelection { get; }
        /// <summary> End time of selected time </summary>
        double EndSelection { get; }
        /// <summary> Lenght of selection time </summary>
        double SelectionLenght { get; }

        /// <summary> position of time </summary>
        double TimePosition { get; set; }


        /// <summary>
        /// Select section of time
        /// </summary>
        /// <param name="startTime">start time</param>
        /// <param name="endTime">end time</param>
        void SelectTime(double startTime, double endTime);

        /// <summary>
        /// Scroll time line
        /// </summary>
        /// <param name="deltaTime">delta time to scroll</param>
        void Scroll(double deltaTime);
        /// <summary>
        /// Zoom time line
        /// </summary>
        /// <param name="deltaTime">delta time to zoom ( negative for zoom out, positive to zoom in)</param>
        void Zoom(double deltaTime);

        /// <summary>
        /// Convert delta pixel to delta time base on current zoom
        /// </summary>
        /// <param name="deltaPixel">delta pixel</param>
        /// <returns></returns>
        double GetDeltaTime(float deltaPixel);

        /// <summary>
        /// Get time at mouse position
        /// </summary>
        /// <param name="mousePosX">MousePosition.x</param>
        /// <returns>time</returns>
        double GetTime(float mousePosX);


        /// <summary>
        /// Get closest snap time to specified time
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Snapped time</returns>
        double GetSnapedTime(double time);

    }


    public class TimeLine : Grid, ITimeLine
    {
        /// <summary>
        /// Minimum allowed time between MinTime and MaxTime
        /// </summary>
        public const double MinTimeLine = 0.01f;

        private double _StartVisibleTime;
        private double _EndVisibleTime;
        private double _MaxTime;
        private double _MinTime;
        private double _StartSelectionTime;
        private double _EndSelectionTime;
        private double _TimePosition;
        private bool _SelectionEnable;

        private TimeBar _Timebar;
        private TrackView _TrackView;

        /// <summary> TimeBar </summary>
        public TimeBar Timebar { get { return _Timebar; } }
        /// <summary> TrackView  </summary>
        public TrackView TrackView { get { return _TrackView; } }

        /// <summary> ZoomFactor </summary>
        public double ZoomFactor { get; private set; }
        /// <summary> Start time of visible area </summary>
        public double StartVisible
        {
            get { return _StartVisibleTime; }
            set
            {
                _StartVisibleTime = value;
                if (_StartVisibleTime < _MinTime) _StartVisibleTime = _MinTime;
                if (_StartVisibleTime > _EndVisibleTime - MinTimeLine) _StartVisibleTime = _EndVisibleTime - MinTimeLine;
                OnLayoutChanged();
            }
        }
        /// <summary> End time of visible area </summary>
        public double EndVisible
        {
            get { return _EndVisibleTime; }
            set
            {
                _EndVisibleTime = value;
                if (_EndVisibleTime > _MaxTime) _EndVisibleTime = _MaxTime;
                if (_EndVisibleTime < _StartVisibleTime + MinTimeLine) _EndVisibleTime = _StartVisibleTime + MinTimeLine;
                OnLayoutChanged();
            }
        }
        /// <summary> Maximum available time to scroll</summary>
        public double MaxTime
        {
            get { return _MaxTime; }
            set
            {
                _MaxTime = value;
                if (_MaxTime < _MinTime + MinTimeLine) _MaxTime = _MinTime + MinTimeLine;
                if (_MaxTime < _EndVisibleTime) _EndVisibleTime = _MaxTime;
                SelectTime(_StartSelectionTime, _EndSelectionTime);
                OnLayoutChanged();
            }
        }
        /// <summary> Minimum available time to scroll</summary>
        public double MinTime
        {
            get { return _MinTime; }
            set
            {
                _MinTime = value;
                if (_MinTime > _MaxTime - MinTimeLine) _MinTime = _MaxTime - MinTimeLine;
                if (_MinTime < _StartVisibleTime) _StartVisibleTime = _MinTime;
                SelectTime(_StartSelectionTime, _EndSelectionTime);
                OnLayoutChanged();
            }
        }
        /// <summary> Start time of selected time </summary>
        public double StartSelection { get { return _StartSelectionTime; } }
        /// <summary> End time of selected time </summary>
        public double EndSelection { get { return _EndSelectionTime; } }
        /// <summary> Lenght of selection time </summary>
        public double SelectionLenght { get { return _EndSelectionTime - _StartSelectionTime; } }
        public bool SelectionEnable
        {
            get { return _SelectionEnable; }
            set
            {
                _SelectionEnable = value;
                if(!_SelectionEnable)
                {
                    _EndSelectionTime = _EndSelectionTime = 0;
                }
            }
        }

        /// <summary> Selected time </summary>
        public double TimePosition
        {
            get { return _TimePosition; }
            set
            {
                if (value < _MinTime) value = _MinTime;
                else if (value > _MaxTime) value = _MaxTime;

                if (_TimePosition != value)
                {
                    _TimePosition = value;
                    OnTimePositionChanged();
                }
            }
        }        

        /// <summary> Occurs when PositionChanged changed </summary>
        public event System.EventHandler TimePositionChanged;
        /// <summary> Occurs when PositionChanged changed </summary>
        protected virtual void OnTimePositionChanged()
        {
            if (TimePositionChanged != null) TimePositionChanged(this, System.EventArgs.Empty);
        }


        /// <summary>
        /// Create a ZoomPanel
        /// </summary>
        public TimeLine()
        {
            this._SelectionEnable = true;
            this._StartVisibleTime = 0;
            this._EndVisibleTime = 1.0;
            this._MaxTime = 10.0;
            this._MinTime = 0.0;

            this.Padding = new Thickness(2);

            this.RowDefinitions.Add(24, GridUnitType.Pixel);
            this.RowDefinitions.Add(4, GridUnitType.Pixel);
            this.RowDefinitions.Add(1, GridUnitType.Star);

            _Timebar = new TimeBar(this) { Row = 0, Column = 0, Margin = new Thickness(0, 0, 16, 0) };
            _TrackView = new TrackView(this) { Row = 2, Column = 0, ScrollbarThickness = 16, Padding = new Thickness(0, 0, 16, 0) };

            this.Controls.Add(_Timebar);
            this.Controls.Add(_TrackView);
        }

        /// <summary>
        /// Select section of time
        /// </summary>
        /// <param name="startTime">start time</param>
        /// <param name="endTime">end time</param>
        public void SelectTime(double startTime, double endTime)
        {
            if (!_SelectionEnable) return;
            _StartSelectionTime = startTime;
            if (_StartSelectionTime < _MinTime) _StartSelectionTime = _MinTime;
            else if (_StartSelectionTime > _MaxTime) _StartSelectionTime = _MaxTime;

            _EndSelectionTime = endTime;
            if (_EndSelectionTime < _MinTime) _EndSelectionTime = _MinTime;
            else if (_EndSelectionTime > _MaxTime) _EndSelectionTime = _MaxTime;

            if (_StartSelectionTime > _EndSelectionTime) _StartSelectionTime = _EndSelectionTime;
        }

        /// <summary>
        /// LayoutChanged
        /// </summary>
        protected override void OnLayoutChanged()
        {
            ZoomFactor = (this._MaxTime - this._MinTime) / (this._EndVisibleTime - this._StartVisibleTime);
            base.OnLayoutChanged();
        }

        /// <summary>
        /// Scroll time line
        /// </summary>
        /// <param name="deltaTime">delta time to scroll</param>
        public void Scroll(double deltaTime)
        {
            if (deltaTime < 0)
            {
                double preStartTime = StartVisible;
                StartVisible += deltaTime;
                EndVisible += StartVisible - preStartTime;
            }
            else
            {
                double preEndTime = EndVisible;
                EndVisible += deltaTime;
                StartVisible += EndVisible - preEndTime;
            }
            EditorFrame.RepaintParentEditorWindow(this);
        }

        /// <summary>
        /// Zoom time line
        /// </summary>
        /// <param name="deltaTime">delta time to zoom ( negative for zoom out, positive to zoom in)</param>
        public void Zoom(double deltaTime)
        {
            deltaTime *= 0.5f;
            if (deltaTime < 0)
            {
                StartVisible += deltaTime;
                EndVisible -= deltaTime;
            }
            else
            {
                EndVisible -= deltaTime;
                StartVisible += deltaTime;
            }
        }

        /// <summary>
        /// Convert delta pixel to delta time base on current zoom
        /// </summary>
        /// <param name="deltaPixel">delta pixel</param>
        /// <returns></returns>
        public double GetDeltaTime(float deltaPixel)
        {
            return Timebar.GetDeltaTime(deltaPixel);
        }

        /// <summary>
        /// Get time at mouse position
        /// </summary>
        /// <param name="mousePosX">MousePosition.x</param>
        /// <returns>time</returns>
        public double GetTime(float mousePosX)
        {
            return Timebar.GetTime(mousePosX);
        }

        /// <summary>
        /// Get closest snap time to specified time
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>Snapped time</returns>
        public double GetSnapedTime(double time)
        {
            return Timebar.GetSnapedTime(time);
        }

        /// <summary>
        /// Clear all controls from TrackView
        /// </summary>
        public void Clear()
        {
            TrackView.Controls.Clear();
        }
    }
}