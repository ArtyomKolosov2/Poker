using System.Collections.Generic;
using Poker.Domain.Game.Units;

namespace Poker.Domain.Game
{
    public interface IHand : IReadOnlyCollection<Card>
    {
    }
}