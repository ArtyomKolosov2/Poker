using System.Collections.Generic;
using Poker.Domain.Game.Units;

namespace Poker.Domain.Game
{
    public interface IDeck : IReadOnlyCollection<Card>
    {
    }
}