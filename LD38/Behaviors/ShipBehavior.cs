using Coldsteel.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LD38.Behaviors
{
    public class ShipBehavior : Behavior
    {
        public override void Activate()
        {
            Entity.WithComponent<SelectableBehavior>(
                c => c.OnDoAction = GoToThisLocation
                );
        }

        private void GoToThisLocation(Vector2 location) =>
            this.Entity.SetPosition(location);
    }
}
