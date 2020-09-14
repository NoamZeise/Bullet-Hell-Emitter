using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bulletHell.Enemy
{
    public class Bullet
    {
        public bool isRemoved = false;
        public Vector2 Position;
        public Vector2 Velocity;
        public Bullet(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += new Vector2(Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (!(new Rectangle(-Game1.SCREEN_WIDTH, -Game1.SCREEN_HEIGHT, Game1.SCREEN_WIDTH * 2, Game1.SCREEN_HEIGHT * 2).Contains(Position)))
                isRemoved = true;
        }
    }
}
