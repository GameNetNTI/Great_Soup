using System.Collections.Generic;
using Entitas;
using Game.Services.Camera;

namespace Ecs.Game.Systems
{
    public class CameraUpdateRotationSystem : ReactiveSystem<GameEntity>
    {
        private readonly ICameraService _cameraService;

        public CameraUpdateRotationSystem(IContext<GameEntity> context, ICameraService cameraService) : base(context)
        {
            _cameraService = cameraService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.CameraRotation);

        protected override bool Filter(GameEntity entity) => entity.hasCameraRotation;

        protected override void Execute(List<GameEntity> entities)=>_cameraService.UpdateRotation();
    }
}