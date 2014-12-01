using System;

namespace Arcturus.Abstract
{
    public interface ILicenseProvider
    {
        bool IsLicensed();
        bool UpdateLicense(Guid guid);
        License GetLicense();
    }
}