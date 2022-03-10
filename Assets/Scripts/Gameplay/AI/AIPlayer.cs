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

        public AIPlayer(AiDifficulty difficulty, int numberOfPlayers, Action<int> propagateInput)
        {
            _algorithm = AssignAiDifficulty(difficulty, numberOfPlayers);
            _propagateInput = propagateInput;
        }

#if ASYNC
        public async void MakeChoiceAsync(int[,] board, int valueToCheck, Func<int[,], int, (int, int), int> winConditions)
        {
            _toggleUserInput?.Invoke(false);
            var output = await Task.Run(() => _algorithm.Decide(board, valueToCheck, winConditions));
            if (output == null) return;
            _propagateInput?.Invoke(output.Value);
        }
#endif

        public void MakeChoice(int[,] board, int valueToCheck, Func<int[,], int, (int, int), int> winConditions)
        {
            _toggleUserInput?.Invoke(false);
            var output = _algorithm.Decide(board, valueToCheck, winConditions);
            if (output == null) return;
            _propagateInput?.Invoke(output.Value);
        }

        private static IDecisionAlgorithm AssignAiDifficulty(AiDifficulty difficulty, int numberOfPlayers)
        {
            return difficulty switch
            {
                AiDifficulty.Easy => new RandomChoiceAlgorithm(),
                AiDifficulty.Medium => new MinimaxAlgorithm(numberOfPlayers, true),
                AiDifficulty.Hard => new MinimaxAlgorithm(numberOfPlayers),
                _ => new RandomChoiceAlgorithm()
            };
        }
    }
}