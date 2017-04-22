using Coldsteel;
using Coldsteel.Input;
using System.Collections.Generic;

namespace LD38
{
    public class Controls
    {
        public const string Pointer = nameof(Pointer);
        public const string Select = nameof(Select);
        public const string Unselect = nameof(Unselect);

        public static IEnumerable<IControl> Get() =>
            new IControl[] 
            {
                new PositionalControl(Pointer)
                    .BindTo(new MousePosition()),

                new ButtonControl(Select)
                    .BindTo(new MouseButton(MouseButtons.Left)),

                new ButtonControl(Unselect)
                    .BindTo(new MouseButton(MouseButtons.Right))
            };
    }
}
