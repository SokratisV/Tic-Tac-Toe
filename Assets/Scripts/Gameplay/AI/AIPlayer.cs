using System;

namespace TicTacToe.Gameplay
{
    public class AIPlayer
    {
        private IDecisionAlgorithm _algorithm;
        private Action<int> _propagateInput;

        public AIPlayer(AiDifficulty difficulty, Action<int> propagateInput)
        {
            _algorithm = AssignAiDifficulty(difficulty);
            _propagateInput = propagateInput;
        }

        public void MakeChoice(int[,] board)
        {
            var output = _algorithm.Decide(board);
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