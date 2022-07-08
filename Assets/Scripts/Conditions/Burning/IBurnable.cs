namespace Assets.Scripts.Conditions.Burning {
    interface IBurnable {
        float BurnThreshold { get; }
        float BurnResistance { get; }
        float BurnRecovery { get; }

        bool CanBurn();
        void BurnTick();
    }
}
