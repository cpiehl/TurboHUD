using System.Collections.Generic;

namespace Turbo.Plugins.Default
{

    public class WorldDecoratorCollection: ITransparentCollection
    {
        public bool Enabled { get; set; }
        public List<IWorldDecorator> Decorators { get; private set; }

        public WorldDecoratorCollection(params IWorldDecorator[] decorators)
        {
            Enabled = true;
            Decorators = new List<IWorldDecorator>(decorators);
        }

        public void Add(IWorldDecorator decorator)
        {
            Decorators.Add(decorator);
        }

        public void Paint(WorldLayer layer, IActor actor, IWorldCoordinate coord, string text)
        {
            if (!Enabled) return;
            foreach (var decorator in Decorators)
            {
                if (decorator.Enabled && (decorator.Layer == layer))
                {
                    decorator.Paint(actor, coord, text);
                }
            }
        }

        public void ToggleDecorators<T>(bool enabled) where T: IWorldDecorator
        {
            foreach (var decorator in Decorators)
            {
                if (decorator is T)
                {
                    decorator.Enabled = enabled;
                }
            }
        }

        public IEnumerable<T> GetDecorators<T>() where T: IWorldDecorator
        {
            foreach (var decorator in Decorators)
            {
                if (decorator is T)
                {
                    yield return (T)decorator;
                }
            }
        }

        public IEnumerable<ITransparent> GetTransparents()
        {
            foreach (var decorator in Decorators)
            {
                foreach (var deco in decorator.GetTransparents())
                {
                    yield return deco;
                }
            }
        }
    }

}