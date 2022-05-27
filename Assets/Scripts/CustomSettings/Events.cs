namespace pixelook
{
    public static class Events
    {
        // level progression events
        public const string GAME_STARTED = "GameStarted";
        public const string PLAYER_DIED = "PlayerDied";
        public const string GAME_STATE_CHANGED = "GameStateChanged";

        public const string PORTAL_ENTERED = "PortalEntered";
        public const string PORTAL_LEAVED = "PortalLeaved";
        public const string MAZE_GENERATION_FINISHED = "MazeGenerationFinished";
        public const string SOUL_TAKEN = "SouldTaken";
        public const string SOUL_SAVED = "SoulSaved";
        public const string FENIX_SPAWNED = "FenixSpawned";
        public const string FENIX_KILLED = "FenixKilled";

        // camera events
        public const string CAMERA_SHAKE_BIG = "CameraShakeBig";
        public const string CAMERA_SHAKE_SMALL = "CameraShakeSmall";
        
        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
        public const string PURCHASE_FINISHED = "PurchaseFinished";
    }
}