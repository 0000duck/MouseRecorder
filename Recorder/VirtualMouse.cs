﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Recorder
{
    public static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }

        public static void MoveTo(float x, float y)
        {
            float min = 0;
            float max = 65535;

            float currentWidth = (float)Recorder.currentScreen.Bounds.Width;
            float currentHeight = (float)Recorder.currentScreen.Bounds.Height;

            int mappedX = (int)Remap(x, 0.0f, currentWidth, min, max);
            int mappedY = (int)Remap(y, 0.0f, currentHeight, min, max);

            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, mappedX, mappedY, 0, 0);
        }

        public static void MoveTo(string coordinates)
        {
            coordinates = coordinates.Replace("{", "").Replace("}", "");
            float x = Convert.ToInt64(coordinates.Substring(0, coordinates.IndexOf(",")));
            float y = Convert.ToInt64(coordinates.Substring(coordinates.IndexOf(",") + 1));

            float min = 0;
            float max = 65535;

            float currentWidth = (float)Recorder.currentScreen.Bounds.Width;
            float currentHeight = (float)Recorder.currentScreen.Bounds.Height;

            int mappedX = (int)Remap(x, 0.0f, currentWidth, min, max);
            int mappedY = (int)Remap(y, 0.0f, currentHeight, min, max);

            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, mappedX, mappedY, 0, 0);
        }

        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void WheelUp()
        {
            mouse_event(MOUSEEVENTF_WHEEL, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 120, 0);
        }

        public static void WheelDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, -120, 0);
        }

        internal static void WheelButtonDown()
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        internal static void WheelButtonUp()
        {
            mouse_event(MOUSEEVENTF_MIDDLEUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
    }
}
