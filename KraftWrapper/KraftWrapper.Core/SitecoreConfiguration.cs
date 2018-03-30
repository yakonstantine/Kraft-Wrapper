using KraftWrapper.Interfaces;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace KraftWrapper.Core
{
    public class SitecoreConfiguration : ISitecoreConfiguration
    {
        private SecurityDisabler _securityDisabler;
        private LanguageSwitcher _languageSwitcher;

        public void DisableSecurity()
        {
            EnableSecurity();

            _securityDisabler = new SecurityDisabler();
        }

        public void EnableSecurity()
        {
            if (_securityDisabler == null)
            {
                return;
            }

            _securityDisabler.Dispose();
            _securityDisabler = null;
        }

        public void ResetLanguage()
        {
            if (_languageSwitcher == null)
            {
                return;
            }

            _languageSwitcher.Dispose();
            _languageSwitcher = null;
        }

        public void SwitchLanguage(ISitecoreLanguage language)
        {
            ResetLanguage();

            _languageSwitcher = new LanguageSwitcher(language.Name);
        }
    }
}
