﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace CosmicHunter
{
    public class MdKeyboard
    {
        public KeyboardState newKeyboard, oldKeyboard;

        public List<MdKey> pressedKeys = new List<MdKey>(), previousPressedKeys = new List<MdKey>();

        public MdKeyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();      //get everything that happens on the keyboard

            GetPressedKeys();
        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<MdKey>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }

        public bool GetPress(string KEY)                    //loops through all keys and checks if given key is pressed
        {
            for (int i = 0; i < pressedKeys.Count; i++)     //search the key we're looking for
            {
                if (pressedKeys[i].key == KEY)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear();

            //go through all of the keys
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {
                pressedKeys.Add(new MdKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
            }
        }

        public bool GetSinglePress(string KEY)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                bool isIn = false;

                for (int j = 0; j < previousPressedKeys.Count; j++)
                {
                    if (pressedKeys[i].key == previousPressedKeys[j].key)
                    {
                        isIn = true;
                        break;
                    }
                }

                if (!isIn && (pressedKeys[i].key == KEY || pressedKeys[i].print == KEY))
                {
                    return true;
                }
            }

            return false;
        }
    }
}