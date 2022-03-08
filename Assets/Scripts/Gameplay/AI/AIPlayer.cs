using System;
using System.Threading.Tasks;

namespace TicTacToe.Gameplay
{
    public class AIPlayer
    {
        private IDecisionAlgorithm _algorithm;
        private Action<int> _propagateInput;
        private Action<Action, float> _delayedInvocation;
        private readonly Action<bool> _toggleUserInput;

        public AIPlayer(AiDifficulty difficulty, Action<int> propagateInput)
        {
            _algorithm = AssignAiDifficulty(difficulty);
            _propagateInput = propagateInput;
        }

        public async void MakeChoice(int[,] board)
        {
            _toggleUserInput?.Invoke(false);
            var output = await Task.Run(() => _algorithm.Decide(board));
            if (output == null) return;
            _propagateInput?.Invoke(output.Value);
        }

        private static IDecisionAlgorithm AssignAiDifficulty(AiDifficulty difficulty)
        {
            return difficulty switch
            {
                AiDifficulty.Easy => new RandomChoiceAlgorithm(),
                AiDifficulty.Medium => new RandomChoiceAlgorithm(),
                AiDifficulty.Hard => new MinimaxAlgorithm(),
                _ => new RandomChoiceAlgorithm()
            };
        }
    }
}