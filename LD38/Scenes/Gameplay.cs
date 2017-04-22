using Coldsteel;
using Coldsteel.Rendering;
using LD38.Behaviors;
using Microsoft.Xna.Framework;

namespace LD38.Scenes
{
    public class Gameplay
    {
        public Entity Camera =
            new Entity()
            .AddComponent(new Camera())
            .AddComponent(new CameraScrollerBehavior());

        public Layer UILayer =
            new Layer("ui", 90)
            {
                IsCameraSticky = true
            };

        public Layer PointerLayer =
            new Layer("pointer", 100)
            {
                IsCameraSticky = true
            };

        public Entity Ship =
            new Entity()
                .SetPosition(600, 200)
                .AddComponent(new SpriteRenderer("ship"))
                .AddComponent(new SelectableBehavior())
                .AddComponent(new ShipBehavior());

        public Entity Pointer =
            new Entity()
                .AddComponent(new SpriteRenderer("pointer")
                {
                    Origin = Vector2.Zero,
                    Layer = "pointer",
                    Color = Color.Red
                })
                .AddComponent(new PointerBehavior());

        public static Scene Scene()
        {
            var gp = new Gameplay();
            return new Scene(new SceneElement[] {
                gp.Camera,
                gp.UILayer,
                gp.PointerLayer,
                gp.Ship,
                gp.Pointer,
                new Entity()
                    .SetPosition(160, 360)
                    .AddComponent(new SpriteRenderer("sidebar")
                    {
                        Layer = "ui",
                    })
            })
            {
                BackgroundColor = Color.Black
            };
        }
    }
}
