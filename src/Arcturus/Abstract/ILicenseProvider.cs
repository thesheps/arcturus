using System;

namespace Arcturus.Abstract
{
    public interface ILicenseProvider
    {
        bool IsLicensed();
        DateTime GetExpirationDate();
        bool ActivateLicense(Guid guid);
    }
}