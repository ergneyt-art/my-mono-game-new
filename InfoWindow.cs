using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.MenuClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame
{
    public class InfoWindow
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public MenuLayout MenuLayout { get; private set; }

        public InfoWindow(string title, string message) //  SpriteFont font, Rectangle frame
        {
            Title = title;
            Message = message;
        }
    }
}
