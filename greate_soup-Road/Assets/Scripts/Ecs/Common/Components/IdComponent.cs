using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Ecs.Common.Components
{
    [Stationary, Unit]
    public class IdComponent : IComponent
    {
        [PrimaryEntityIndex] public int Value;
    }
}