using System;

namespace Arcturus.Abstract
{
    public interface ILicenseProvider
    {
        bool IsLicensed();
        bool ActivateLicense(Guid guid);
    }
}