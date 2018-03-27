using KraftWrapper.Interfaces;
using Sitecore.SecurityModel;

namespace KraftWrapper.Core
{
    public class SitecoreConfiguration : ISitecoreConfiguration
    {
        private SecurityDisabler _securityDisabler;

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
    }
}
