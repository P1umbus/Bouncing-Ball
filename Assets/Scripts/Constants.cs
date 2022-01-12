using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public class PPname
    {
        public const string CoinNumb = "CoinNumb";
    }
    public const string ControlSensivity = "ControlSensivity";

    public const string NumbActiveLevel = "NumbActiveLevel";
    public const string ImmunitySphereAmount = "_Amount";

    public enum Rarity
    {
        Common,
        Rare,
        Mythical
    }

    public enum GameLevelList
    {
        Level1 = 1,
        Level1_1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level9_1,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Level16,
    }
    public enum MainLevelList
    {
        Preloader,
        Menu,
        Shop,
    }
}
