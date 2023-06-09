﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicHunter
{
    //active game logic
    public class World : IUpdatable
    {
        public Vector2 offset;
        public UI userInterface;

        public User user;
        public AIPlayer aiPlayer;

        public List<Projectile2d> projectiles = new List<Projectile2d>();

        public PassObject resetWorld, changeGameState;
        public GameGlobals globals;

        public World(PassObject resetWorld, PassObject changeGameState)
        {
            this.resetWorld = resetWorld;
            this.changeGameState = changeGameState;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;

            user = new User(1);             //current player has ID 1
            aiPlayer = new AIPlayer(2);     //AI player had ID 2

            offset = new Vector2(0, 0);

            userInterface = new UI(resetWorld);
            globals = new GameGlobals();
        }

        public User GetUser(World world)
        {
            return world.user;
        }

        public virtual void Update()
        {
            if (!user.hero.dead)                                //keep updating the game while the hero is still alive, game will pause when the hero dies
            {
                user.Update(offset, aiPlayer);
                aiPlayer.Update(offset, user);

                for (int i = 0; i < projectiles.Count; i++)     //create the mobs after
                {
                    projectiles[i].Update(offset, aiPlayer.units.ToList<Unit>());

                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
                
                if (Globals.keyboard.GetPress("Enter") || userInterface.playButton.isPressed == true)      //when the hero wins, check if he pressed Enter
                {
                    globals.ResetCounters();                //set enemies remaining and score back to original amount
                    resetWorld(null);                       //restart the world
                }
            }

            else
            {
                if (Globals.keyboard.GetPress("Enter") || userInterface.resetButton.isPressed == true)     //when the hero died, check if he pressed Enter
                {
                    globals.ResetCounters();                //set enemies remaining and score back to original amount
                    resetWorld(null);                       //restart the world
                }
            }

            if (Globals.keyboard.GetSinglePress("Back"))
            {
                globals.ResetCounters();
                resetWorld(null);
                changeGameState(0);                         //change it back when back button is pushed
            }

            userInterface.Update(this);
        }

        public virtual void AddMob(object info)
        {
            Unit tempUnit = (Unit)info;

            if (user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if (aiPlayer.id == tempUnit.ownerId)
            {
                aiPlayer.AddUnit(tempUnit);
            }

            aiPlayer.AddUnit((Mob)info);
        }

        public virtual void AddProjectile(object info)
        {
            //anything you pass here will be casted as a projectile and added to the list of projectiles
            projectiles.Add((Projectile2d)info);
        }

        public virtual void Draw(Vector2 offset)
        {
            //whatever is on top draws last
            user.Draw(offset);
            aiPlayer.Draw(offset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);        //draw the projectiles
            }

            userInterface.Draw(this, user.hero);    //draw the user interface
        }
    }
}