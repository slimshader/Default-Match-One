using DefaultEcs;
using DefaultEcs.System;
using DefaultMatchOne.Messages;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultMatchOne
{
    public struct Board
    {
        public Vector2Int Size;
    }

    public struct Piece
    {
        public int Type;
    }

    public struct Position
    {
        public Vector2Int Value;

        public override string ToString() => $"{Value}";
    }

    public struct Asset
    {
        public string Value;
    }

    public struct InputComponent
    {
        public Vector2Int Value;
    }

    public struct Score
    {
        public int Value;
    }

    public struct IsMovable { }
    public struct IsInteractable { }
    public struct IsDestroyed { }
    public struct BurstMode { }

    namespace Messages
    {
        public struct NewScore
        {
            public int Value;
        }
    }

    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ScriptableGameConfig _gameConfig;

        [SerializeField]
        private ScriptablePieceColorsConfig _pieceColorsConfig;

        [SerializeField]
        private TMP_Text _scoreLabel;

        [SerializeField]
        private TMP_Text _burstModeText;

        [SerializeField]
        private Button _burstModeButton;

        [SerializeField]
        private CameraView _cameraView;

        World _world;

        ISystem<float> _systems;

        void Start()
        {
            var random = new System.Random(DateTime.UtcNow.Millisecond);
            UnityEngine.Random.InitState(random.Next());
            Rand.game = new Rand(random.Next());

            _world = new World();
            _world.Set<IGameConfig>(_gameConfig);
            _world.Set<IPieceColorsConfig>(_pieceColorsConfig);
            _world.Set<Score>();

            _world.Subscribe(this);

            _world.SubscribeWorldComponentAdded((World world, in Board board) =>
            {
                _cameraView.OnAnyBoard(board.Size);
            });

            _world.SubscribeWorldComponentAdded((World world, in BurstMode burstMode) =>
            {
                _burstModeText.text = "Burst Mode: on";
            });

            _world.SubscribeWorldComponentRemoved((World world, in BurstMode burstMode) =>
            {
                _burstModeText.text = "Burst Mode: off";
            });

            _burstModeText.text = "Burst Mode: off";

            _burstModeButton.onClick.AddListener(() =>
            {
                if (_world.Has<BurstMode>())
                    _world.Remove<BurstMode>();
                else
                    _world.Set<BurstMode>();
            });

            _systems = new SequentialSystem<float>(

                // Input
                new InpuSystem(_world, Camera.main),
                new ProcessInputSystem(_world),

                // Update
                new FallSystem(_world),
                new FillSystem(_world),
                new ScoreSystem(_world),

                // View
                new AddViewSystem(_world),

                // Events
                new PositionViewSystem(_world),

                // Cleanup
                new RemoveViewSystem(_world),

                new CleanupInputSystem(_world),
                new DisposeDestroyedSystem(_world)
                );


            var config = _world.Get<IGameConfig>();

            _world.Set(new Board() { Size = config.BoardSize });

            for (var y = 0; y < config.BoardSize.y; y++)
                for (var x = 0; x < config.BoardSize.x; x++)
                    if (Rand.game.Bool(config.BlockerProbability))
                        _world.CreateBlocker(x, y);
                    else
                        _world.CreateRandomPiece(x, y);
        }

        void Update()
        {
            _systems.Update(Time.deltaTime);
        }

        [Subscribe]
        private void On(in NewScore score)
        {
            _scoreLabel.text = "Score " + score.Value;
        }
    }
}