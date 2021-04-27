using System.Collections.Generic;
using Entitas;
using Game.Loop;
using Game.Services.Camera;

namespace Ecs.Game.Systems
{
    public class CameraUpdateZoomingSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICameraService _cameraService;

        public CameraUpdateZoomingSystem(IContext<GameEntity> context, ICameraService cameraService) : base(context)
        {
            _cameraService = cameraService;
        }


        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.CameraZooming);

        protected override bool Filter(GameEntity entity) => entity.hasCameraZooming;

        protected override void Execute(List<GameEntity> entities)
        {
            _cameraService.UpdatePosition();
            _cameraService.UpdateRotation();
        }
    }
}