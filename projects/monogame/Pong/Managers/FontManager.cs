using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public static class FontManager
    {
        public static SpriteFont TitleFont;
        public static SpriteFont MenuFont;
        public static SpriteFont GameFont;

        public static void Initalise(ContentManager Content)
        {
            TitleFont = Content.Load<SpriteFont>("TitleFont");
            MenuFont = Content.Load<SpriteFont>("MenuFont");
            GameFont = Content.Load<SpriteFont>("GameFont");
        }
    }
}
