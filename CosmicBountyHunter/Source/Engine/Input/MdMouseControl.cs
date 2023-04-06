﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace CosmicHunter
{
    public class MdMouseControl
    {
        public bool dragging, rightDrag;

        public Vector2 newMousePosition, oldMousePosition, firstMousePosition, newMouseAdjustedPosition, systemCursorPosition, screenLock;
        
        public MouseState newMouse, oldMouse, firstMouse;

        public MdMouseControl()
        {
            dragging = false;

            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            firstMouse = newMouse;

            newMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            firstMousePosition = new Vector2(newMouse.Position.X, newMouse.Position.Y);

            GetMouseAndAdjust();

            //screenLock = new Vector2((int)(systemCursorPos.X/Globals.screenWidth), (int)(systemCursorPos.Y/Globals.screenHeight));
        }

        public MouseState First
        {
            get
            {
                return firstMouse;
            }
        }

        public MouseState New
        {
            get 
            { 
                return newMouse; 
            }
        }

        public MouseState Old
        {
            get 
            { 
                return oldMouse; 
            }
        }

        public void Update()
        {
            GetMouseAndAdjust();

            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePosition = newMousePosition = GetScreenPos(firstMouse);
            }
        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePosition = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return Globals.GetDistance(newMousePosition, firstMousePosition);
        }

        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePosition = GetScreenPos(newMouse);
        }

        public int GetMouseWheelChange()
        {
            return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
        }

        public Vector2 GetScreenPos(MouseState MOUSE)
        {
            Vector2 tempVec = new Vector2(MOUSE.Position.X, MOUSE.Position.Y);

            return tempVec;
        }

        public virtual bool LeftClick()
        {
            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;

            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }

            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }

            return false;
        }

        public virtual bool RightClick()
        {
            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;

            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    rightDrag = true;
                }
            }

            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if (newMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released && oldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }

            return false;
        }

        public void SetFirst()
        {
        }
    }
}