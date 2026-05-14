using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class ActiveGrid : BaseElement
    {
        public ActiveIcon[,] Grids { get; private set; }
        private const int DefRowsCount = 10;
        private const int DefColumnsCount = 10;

        public ActiveGrid(Rectangle bounds, GameContext context, int rows = DefRowsCount, int columns = DefColumnsCount) : base(bounds, context)
        {
            Grids = new ActiveIcon[rows, columns];
            var cellWidth = bounds.Width / columns;
            var cellHeight = bounds.Height / columns;
            for (int i = 0; i < Grids.GetLength(0); i++)
            {
                for (int j = 0; j < Grids.GetLength(1); j++)
                {
                    var cellBounds = new Rectangle(
                        bounds.Left + j * (bounds.Width / columns),
                        bounds.Top + i * (bounds.Height / rows),
                        cellWidth,
                        cellHeight
                    );
                    Grids[i, j] = new ActiveIcon(cellBounds, Context);
                }
            }
        }

        public void Update()
        {
            foreach (var grid in Grids)
            {
                grid.Update();
            }
        }

        public override void Draw()
        {
            if (IsVisible)
            {
                foreach (var grid in Grids)
                {
                    grid.Draw();
                }
            }
        }
    }
}
