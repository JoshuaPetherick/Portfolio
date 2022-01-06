using Microsoft.Xna.Framework;

namespace Pong
{
    public static class Global_Functions
    {
        public static bool CheckCollision(Rectangle box, Point point) => box.Contains(point);

        public static bool CheckCollision(Rectangle box1, Rectangle box2) => box1.Intersects(box2);
    }
}