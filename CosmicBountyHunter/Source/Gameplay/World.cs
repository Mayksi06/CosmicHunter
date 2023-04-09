﻿using CosmicHunter.Source.Gameplay.World.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicHunter
{
    public class World
    {
        public Vector2 offset;
        public Hero hero;
        public List<Projectile2d> projectiles = new List<Projectile2d>();

        public World()
        {
            hero = new Hero("2d\\ship", new Vector2(300, 300), new Vector2(100, 100));

            GameGlobals.PassProjectile = AddProjectile;

            offset = new Vector2(0, 0);
        }

        public virtual void Update()
        {
            hero.Update();

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, null);

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddProjectile(object info)
        {
            projectiles.Add((Projectile2d)info);
        }

        public virtual void Draw(Vector2 offset)
        {
            hero.Draw(offset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }
        }
    }
}