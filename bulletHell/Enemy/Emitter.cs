using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bulletHell.Enemy
{
    public class Emitter
    {
        public Vector2 Position;
        public List<Bullet> Bullets;
        public Emitter(Vector2 position)
        {
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            foreach(var bullet in Bullets)
                bullet.Update(gameTime);



            for (int i = 0; i < Bullets.Count; i++)
                if (Bullets[i].isRemoved)
                    Bullets.RemoveAt(i--);
        }
    }
}
