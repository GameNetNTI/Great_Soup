using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Ecs.Game.Components
{
    [Game, Unique]
    public class CameraZoomingComponent : IComponent
    {
        public float Value;
    }
}