using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Ecs.Game.Components
{
    [Game, Unique]
    public class CameraRotationComponent : IComponent
    {
        public float Value;
    }
}