using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game, Unique]
    public class CameraPositionComponent : IComponent
    {
        public Vector2 Value;
    }
}