using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public interface IInterfaceElement
    {
        public Rectangle Bounds { get; set; }

        void Draw(SpriteBatch spriteBatch, Texture2D pixel);
    }
}
