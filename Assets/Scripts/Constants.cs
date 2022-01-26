public static class Constants
{
    public class PPname
    {
        public const string CoinNumb = "CoinNumb";
        public const string SelectedSkin = "SelectedSkin";
        public const string ControlSensivity = "ControlSensivity";
        public const string NumbActiveLevel = "NumbActiveLevel";
    }

    public class Layers
    {
        public const int Ball = 6;
        public const int Trampoline = 7;
        public const int Trap = 8;
    }

    public const string ImmunitySphereAmount = "_Amount";
}

public enum DataLoaders
{
    AdsManager,
    FirebaseManager,
    Bank
}

public enum Rarity
{
    Common,
    Rare,
    Mythical
}

public enum MainLevelList
{
    Preloader,
    Menu,
    Shop,
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
