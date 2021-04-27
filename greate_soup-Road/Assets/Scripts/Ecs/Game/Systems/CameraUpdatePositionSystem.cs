using System.Collections.Generic;
using Entitas;
using Game.Loop;
using Game.Services.Camera;

namespace Ecs.Game.Systems
{
    public class CameraUpdatePositionSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICameraService _cameraService;

        public CameraUpdatePositionSystem(IContext<GameEntity> context, ICameraService cameraService) : base(context)
        {
            _cameraService = cameraService;
        }
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.CameraPosition);

        protected override bool Filter(GameEntity entity)
            => entity.hasCameraPosition;

        protected override void Execute(List<GameEntity> entities) => _cameraService.UpdatePosition();
    }
}