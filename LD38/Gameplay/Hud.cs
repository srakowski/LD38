using LD38.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LD38.Gameplay
{
    public struct HudElement
    {
        public string TextureKey { get; }
        public Transform Transform { get; }
        public Vector2 Position => Transform.Position;
        public Color Color { get; }
        public HudElement(string textureKey, Vector2 position, Color color)
        {
            TextureKey = textureKey;
            Transform = new Transform(position, 0f, 1f);
            Color = color;
        }
    }

    public static class Hud
    {
        public static int SideBarLeftEdge => Sizes.ScreenBounds.Right - 320;

        public static HudElement Underlay { get; } =
            new HudElement(
                "sprites/sidebar", 
                new Vector2(SideBarLeftEdge, 0),
                GameColors.TrimColor);
        public static Camera Camera { get; internal set; }

        public static MiniMap MiniMap = new MiniMap();

        public static TextSprite Title { get; set; }

        public static void Init(ContentManager content)
        {
            Hud.MiniMap.LoadContent(content);
            Title = new TextSprite(Global.FontTexture, "TBD", new Vector2(SideBarLeftEdge + 10, 4));
        }
    }
}
