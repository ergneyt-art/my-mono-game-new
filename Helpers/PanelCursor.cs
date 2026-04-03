using Microsoft.Xna.Framework;
using System;

namespace MyMonoGame.Helpers
{
    public class PanelCursor
    {
        public Rectangle CurrentArea {  get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        private const int _defaultSpacing = 10;

        public PanelCursor(Rectangle area) 
        { 
            CurrentArea = area;
            X = area.Left;
            Y = area.Top;
        }

        public void SetPosition(int x, int y)
        {
            if (x < CurrentArea.Left || x > CurrentArea.Right || y < CurrentArea.Top || y > CurrentArea.Bottom)
                throw new Exception("Position out of bounds");
            X = x;
            Y = y;
        }

        public Rectangle GetNextRect(Direction direction, int width, int height, int spacing = _defaultSpacing)
        {
            var x = X;
            var y = Y;
            switch (direction) 
            {
                case Direction.Left:
                    if (x - width < CurrentArea.Left) throw new Exception("Cursor moved out of bounds");
                    x -= width;
                    X -= (width + spacing);
                    break;
                case Direction.Right:
                    if (x + width > CurrentArea.Right) throw new Exception("Cursor moved out of bounds");
                    X += width + spacing;
                    break;
                case Direction.Up:
                    if (y - height < CurrentArea.Top) throw new Exception("Cursor moved out of bounds");
                    y -= height;
                    Y -= (height + spacing);
                    break; 
                case Direction.Down:
                    if (y + height > CurrentArea.Bottom) throw new Exception("Cursor moved out of bounds");
                    Y += height + spacing;
                    break;
                default:
                    break;
            }
            return new Rectangle(x, y, width, height);
        }
    }

    public  enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
