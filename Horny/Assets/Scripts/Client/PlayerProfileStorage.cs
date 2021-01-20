using System.Collections.Generic;

namespace Client
{
    public class PlayerProfileStorage
    {
        private const string PlayerProfileKey = "PlayerProfile"; 
        private readonly PrefsStorage _prefsStorage;
        private readonly PlayerProfile _playerProfile;

        public PlayerProfileStorage()
        {
            _prefsStorage = new PrefsStorage();

            if (_prefsStorage.HasKey(PlayerProfileKey))
                _playerProfile = _prefsStorage.Deserialize<PlayerProfile>(PlayerProfileKey);
            else
            {
                _playerProfile = new PlayerProfile();
                _prefsStorage.Serialize(PlayerProfileKey, _playerProfile);
            }
            
        }

        public int LastCompletedWorld
        {
            get => _playerProfile.LastCompletedWorld;

            set
            {
                _playerProfile.LastCompletedWorld = value; 
                _prefsStorage.Serialize(PlayerProfileKey, _playerProfile);
            }
        }

        public List<WorldInfo> WorldInfos
        {
            get => _playerProfile.WorldInfos;

            set
            {
                _playerProfile.WorldInfos = value; 
                _prefsStorage.Serialize(PlayerProfileKey, _playerProfile);
            }
        }
    }
}