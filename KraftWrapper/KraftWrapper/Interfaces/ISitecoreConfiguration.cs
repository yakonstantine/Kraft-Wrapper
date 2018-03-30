namespace KraftWrapper.Interfaces
{
    public interface ISitecoreConfiguration
    {
        void DisableSecurity();
        void EnableSecurity();
        void SwitchLanguage(ISitecoreLanguage language);
        void ResetLanguage();
    }
}
