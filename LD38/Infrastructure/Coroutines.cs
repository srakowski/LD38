using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LD38
{
    public static class Coroutines
    {
        static List<Coroutine> Pending { get; } = new List<Coroutine>();
        static List<Coroutine> Active { get; } = new List<Coroutine>();

        public static void Update(GameTime gameTime)
        {
            Active.AddRange(Pending);
            Pending.Clear();
            Active.ForEach(c => c.Update(gameTime));
            Active.RemoveAll(c => c.IsComplete);
        }

        public static void Add(Coroutine coroutine) => Pending.Add(coroutine);
    }
}
