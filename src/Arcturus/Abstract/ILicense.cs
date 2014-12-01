using System;

namespace Arcturus.Domain
{
    public interface ILicense
    {
        int LicenseId { get; set; }
        DateTime ExpirationDate { get; set; }
        Guid Guid { get; set; }
        bool IsActive { get; set; }
    }
}