using DefaultEcs;
using DefaultEcs.System;
using DefaultMatchOne.Messages;
using System;
using TMPro;
using UnityEngine;

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
    }

    public struct Asset
    {
        public string Value;
    }

    public struct ViewComponent
    {
        public IView Value;
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

            _systems = new SequentialSystem<float>(

                // Input
                new InpuSystem(_world, Camera.main),
                new ProcessInputSystem(_world),

                // Update
                new ScoreSystem(_world),

                // View
                new CameraViewSystem(_world, Camera.main),
                new AddViewSystem(_world),
                new RemoveViewSystem(_world),

                // Events
                new PositionViewSystem(_world),

                // Cleanup
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

        // Update is called once per frame
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