using Game;
using Ui;
using Ui.Game;
using Zenject;

namespace Ecs.Game.Systems
{
    public class TestRunGameSystem : IInitializable
    {
        private readonly GameStream _gameStream;
        private readonly UiStream _uiStream;

        public TestRunGameSystem(GameStream gameStream, UiStream uiStream)
        {
            _gameStream = gameStream;
            _uiStream = uiStream;
        }

        public void Initialize()
        {
            _gameStream.Fire(EGameEvent.Start);
            _uiStream.Fire(new UiEvent(EUiEventType.Open, EViewType.Game));
            _uiStream.Fire(new UiEvent(EUiEventType.Open, EViewType.BuildingButton));
        }
    }
}