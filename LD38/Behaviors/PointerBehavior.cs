using Coldsteel;
using Coldsteel.Rendering;
using Coldsteel.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD38.Behaviors
{
    public class PointerBehavior : Behavior
    {
        private IPositionalControl _pointerControl;
        private IButtonControl _selectControl;
        private IButtonControl _unselect;
        private Entity selectedEntity;
        private CameraScrollerBehavior cameraScroller;
       
        public override void Activate()
        {
            _pointerControl = Input.GetPositionalControl(Controls.Pointer);
            _selectControl = Input.GetButtonControl(Controls.Select);
            _unselect = Input.GetButtonControl(Controls.Unselect);
            this.cameraScroller = Scene.Elements.OfType<Entity>()
                .FirstOrDefault(e => e.Components.OfType<CameraScrollerBehavior>().Any())
                .GetComponent<CameraScrollerBehavior>();
        }

        public override void Update()
        {
            this.Transform.Position = _pointerControl.GetPosition();

            if (this.Transform.Position.X < 0) cameraScroller.ScrollLeft(Delta);
            else if (this.Transform.Position.X > 1280) cameraScroller.ScrollRight(Delta);
            if (this.Transform.Position.Y < 0) cameraScroller.ScrollUp(Delta);
            else if (this.Transform.Position.Y > 720) cameraScroller.ScrollDown(Delta);

            if (_selectControl.WasPressed())
                DoSelect();
            else if (_unselect.WasPressed())
                DoUnselect();
        }

        private void DoSelect()
        {
            var newSelectedEntity = Scene.Elements
                .OfType<Entity>()
                .Where(e => e.Components.OfType<SelectableBehavior>().Any())
                .Where(e => e.Components.OfType<SelectableBehavior>().First().IsHit(_pointerControl.GetPosition()))
                .FirstOrDefault();

            if (newSelectedEntity != null)
            {
                DoUnselect();
                selectedEntity = newSelectedEntity;
                selectedEntity?.WithComponent<SelectableBehavior>(
                    c => c.Select(this.Transform.Position)
                );
            }
            else
            {
                selectedEntity?.WithComponent<SelectableBehavior>(
                    c => c.DoAction(this.Transform.Position)
                    );
            }

        }

        private void DoUnselect()
        {
            selectedEntity?
                .Components
                .OfType<SelectableBehavior>()
                .First()
                .Unselect();

            selectedEntity = null;
        }
    }
}
