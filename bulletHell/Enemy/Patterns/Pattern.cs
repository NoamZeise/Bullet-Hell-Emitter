using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace bulletHell.Enemy.Patterns
{
    public class Pattern
    {
        double _timer;
        public double _delay;

        public int _shooterNum;
        public float _currentRotation = 45;
        public float _spin;
        public float _speed;
        public float _seperation;
        public float _initalRotation;
        Vector2[] Velocities;
        public List<Bullet> Bullets;
        public Vector2 Position;
        public Pattern(Vector2 position, int shooterNum, double delay, float spin, float speed, float separation, float initialRotation)
        {
            Position = position;
            _shooterNum = shooterNum;
            Velocities = new Vector2[_shooterNum];
            _delay = delay;
            _timer = delay;
            _spin = spin;
            _speed = speed;
            _seperation = separation;
            _currentRotation = initialRotation;
            setVelocites();
            Bullets = new List<Bullet>();
        }
        public void Update(GameTime gameTime)
        {
            if (_shooterNum < 1)
                _shooterNum = 1;
            _timer += gameTime.ElapsedGameTime.TotalSeconds;
            _currentRotation += _spin * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentRotation = normaliseAngle(_currentRotation);

            setVelocites();
            if(_timer > _delay)
            {
                _timer = 0;
                shoot();
            }
            foreach (var bullet in Bullets)
                bullet.Update(gameTime);

            for (int i = 0; i < Bullets.Count; i++)
                if (Bullets[i].isRemoved)
                    Bullets.RemoveAt(i--);
            Console.WriteLine("Shooter Number: " + _shooterNum);
            Console.WriteLine("Speed: " + _speed);
            Console.WriteLine("Spin: " + _spin);
            Console.WriteLine("rotation: " + _currentRotation);
            Console.WriteLine("delay : " + _delay);
            Console.WriteLine("Separation: " + _seperation);
        }

        void setVelocites()
        {
            float deltaAngle = _seperation;
            if(_seperation == 0)
                deltaAngle = 360f / (float)_shooterNum;
            float[] angles = new float[_shooterNum];
            Velocities = new Vector2[_shooterNum];
            for (int i = 0; i < _shooterNum; i++)
            {
                angles[i] = _currentRotation + (i * deltaAngle);
                angles[i] = normaliseAngle(angles[i]);

                Velocities[i].X = (float)Math.Sin(MathHelper.ToRadians(angles[i])) * _speed;
                Velocities[i].Y = (float)Math.Cos(MathHelper.ToRadians(angles[i])) * _speed;
            }
        }

        private static float normaliseAngle(float angle)
        {
            while (angle >= 360)
            {
                angle -= 360;
            }
            while (angle <= -360)
            {
                angle += 360;
            }
            return angle;
        }

        void shoot()
        {
            for (int i = 0; i < _shooterNum; i++)
            {
                Bullets.Add(new Bullet(Position, Velocities[i]));
            }
        }
    }
}
