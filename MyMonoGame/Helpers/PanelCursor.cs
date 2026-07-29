using Microsoft.Xna.Framework;
using System;

namespace MyMonoGame.Helpers
{
    /// <summary>
    /// Tracks a current position inside a rectangular area and returns rectangles sequentially.
    /// </summary>
    public class PanelCursor
    {
        /// <summary>
        /// Area where this cursor is allowed to place rectangles.
        /// </summary>
        public Rectangle CurrentArea {  get; private set; }

        /// <summary>
        /// Current horizontal cursor position.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Current vertical cursor position.
        /// </summary>
        public int Y { get; private set; }

        private const int _defaultSpacing = 10;

        /// <summary>
        /// Creates a cursor positioned at the top-left corner of the given area.
        /// </summary>
        public PanelCursor(Rectangle area) 
        { 
            CurrentArea = area;
            X = area.Left;
            Y = area.Top;
        }

        /// <summary>
        /// Moves the cursor by spacing in the given direction.
        /// </summary>
        public void MoveCursor(Direction direction, int spacing = _defaultSpacing)
        {
            switch (direction) 
            {
                case Direction.Left:
                    if (X - spacing < CurrentArea.Left) throw new Exception("Cursor moved out of bounds");
                    X -= spacing;
                    break;
                case Direction.Right:
                    if (X + spacing > CurrentArea.Right) throw new Exception("Cursor moved out of bounds");
                    X += spacing;
                    break;
                case Direction.Up:
                    if (Y - spacing < CurrentArea.Top) throw new Exception("Cursor moved out of bounds");
                    Y -= spacing;
                    break; 
                case Direction.Down:
                    if (Y + spacing > CurrentArea.Bottom) throw new Exception("Cursor moved out of bounds");
                    Y += spacing;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the current cursor point. The point must be inside CurrentArea.
        /// </summary>
        public void SetPosition(int x, int y)
        {
            if (x < CurrentArea.Left || x > CurrentArea.Right || y < CurrentArea.Top || y > CurrentArea.Bottom)
                throw new Exception("Position out of bounds");
            X = x;
            Y = y;
        }

        /// <summary>
        /// Moves the cursor to the center point of CurrentArea.
        /// </summary>
        public void SetCenter()
        {
            X = CurrentArea.Center.X;
            Y = CurrentArea.Center.Y;
        }

        /// <summary>
        /// Moves the cursor to the bottom-center point of CurrentArea.
        /// </summary>
        public void SetBottomCenter()
        {
            X = CurrentArea.Center.X;
            Y = CurrentArea.Bottom;
        }

        /// <summary>
        /// Moves the cursor to the top-center point of CurrentArea.
        /// </summary>
        public void SetTopCenter()
        {
            X = CurrentArea.Center.X;
            Y = CurrentArea.Top;
        }

        /// <summary>
        /// Returns a rectangle at the current cursor position and advances the cursor.
        /// </summary>
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

    /// <summary>
    /// Direction used by PanelCursor when placing or moving elements.
    /// </summary>
    public  enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
