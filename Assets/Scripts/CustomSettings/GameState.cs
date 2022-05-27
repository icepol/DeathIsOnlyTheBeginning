namespace pixelook
{
    public static class GameState
    {
        private static int _soulsSaved;
        private static int _comboMultiplier;

        public static int SoulsSaved
        {
            get => _soulsSaved;
            set
            {
                _soulsSaved = value;
                
                EventManager.TriggerEvent(Events.GAME_STATE_CHANGED);
            }
        }

        public static void OnApplicationStarted()
        {
            SoulsSaved = 0;
        }

        public static void OnGameStarted()
        {
            SoulsSaved = 0;
        }
    }
}