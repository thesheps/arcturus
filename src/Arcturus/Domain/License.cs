using System;

namespace Arcturus.Abstract
{
    public class License
    {
        public int LicenseId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
    }
}