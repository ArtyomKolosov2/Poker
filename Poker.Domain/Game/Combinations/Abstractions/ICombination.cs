
using System;

namespace Poker.Domain.Game.Combinations.Abstractions
{
    public interface ICombination : IComparable<ICombination>
    {
        IHand Hand { get; }
    }
}