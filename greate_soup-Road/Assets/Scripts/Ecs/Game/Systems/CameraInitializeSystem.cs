using Game.Loop;
using Game.Providers.Map;
using Game.Services.Camera;

namespace Ecs.Game.Systems
{
    public class CameraInitializeSystem : IStartGameSystem
    {
        private readonly GameContext _gameContext;
        private readonly IMapSettingsProvider _mapSettingsProvider;
        private readonly ICameraService _cameraService;

        public CameraInitializeSystem(
            GameContext gameContext,
            IMapSettingsProvider mapSettingsProvider,
            ICameraService cameraService
        )
        {
            _gameContext = gameContext;
            _mapSettingsProvider = mapSettingsProvider;
            _cameraService = cameraService;
        }

        public void OnStart()
        {
            var mapSize = _mapSettingsProvider.Size;
            _gameContext.ReplaceCameraPosition(mapSize / 2);
            _gameContext.ReplaceCameraZooming(0);
            _gameContext.ReplaceCameraRotation(0);

            _cameraService.UpdatePosition();
            _cameraService.UpdateRotation();
        }
    }
}