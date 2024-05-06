using System;
using System.Collections.Generic;
using _Scripts.Core.Game.Data.Currency;
using _Scripts.Core.Game.Data.Position;
using _Scripts.Core.Game.Data.Story;

public class GameDataWrapper
{
    public DifficultyType difficulty;
    public List<string> events;
    public CurrencyData currencyData;
    public PositionData positionData;
    public StoryData storyData;
}