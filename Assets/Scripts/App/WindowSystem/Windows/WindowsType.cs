namespace App.WindowSystem.Windows {

    public enum WindowsType {
        DEFAULT,
        SETTINGS
    }
    
    
    public static class WindowsTypeMapper {
        
        private const string DEFAULT = "";
        private const string SETTINGS = "/SettingsWindow";

        
        public static string GetReadableString(WindowsType legalEntityType) {
            switch (legalEntityType) {
                case WindowsType.SETTINGS : return SETTINGS;
                default : return DEFAULT;
            }
        }

        public static WindowsType GetType(string legalEntityType) {
            switch (legalEntityType) {
                case SETTINGS : return WindowsType.SETTINGS;
                default : return WindowsType.DEFAULT;
            }
        }
        
    }
    
}


    