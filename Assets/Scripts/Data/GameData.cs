﻿namespace Assets.Scripts.Data
{
    public class GameData
    {
        public int playerCount { private set; get; }
        public static bool startNextTurn { set; get; }

        public GameData()
        {
            playerCount = 4;
        }
    }
}