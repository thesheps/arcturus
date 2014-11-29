using Arcturus.Abstract;
using Arcturus.Domain;
using System;
using System.Linq;

namespace Arcturus.Concrete
{
    public class LicenseProvider : ILicenseProvider
    {
        public LicenseProvider(IGenericRepository<License> licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public bool IsLicensed()
        {
            var license = _licenseRepository
                .Get(l => l.IsActive)
                .OrderBy(l => l.ExpirationDate)
                .LastOrDefault();

            return license == null ? false : true;
        }

        public bool UpdateLicense(Guid guid)
        {
            var license = _licenseRepository
                .Get(l => l.Guid == guid)
                .FirstOrDefault();

            if (license == null) return false;

            var licenses = _licenseRepository
                .Get(l => l.IsActive);

            foreach (var l in licenses)
            {
                l.IsActive = false;
            }

            license.IsActive = true;
            _licenseRepository.Update(licenses);
            _licenseRepository.Update(license);

            return true;
        }

        private readonly IGenericRepository<License> _licenseRepository;
    }
}