using Game.Loop;
using Game.Services.Pathfinding;

namespace Ecs.Game.Systems
{
    public class PathMeshServiceRecalculationSystem : IGameExecuteSystem
    {
        private readonly IPathMeshService _pathMeshService;

        public PathMeshServiceRecalculationSystem(IPathMeshService pathMeshService)
        {
            _pathMeshService = pathMeshService;
        }

        public void Execute()
        {
            if(_pathMeshService.RequireRecalculation)
                _pathMeshService.Recalculate();
        }
    }
}