using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public const string SceneCamera = "SceneCamera";

    public struct Tags
    {
        public const string Player = "Player";
        public const string Stampable = "Stampable";
        public const string Damage = "Damage";
        public const string Block = "Block";

        // Items
        public const string Mushroom = "Mushroom";
        public const string FireFlower = "FireFlower";
        public const string OneUpMushroom = "OneUpMushroom";
        public const string Star = "Star";
    }

    public struct Layers
    {
        public const int IgnorePlayer = 6;
        public const int Player = 7;
        public const int InvinciblePlayer = 9;
    }
}
