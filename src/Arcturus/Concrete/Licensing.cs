using Arcturus.Abstract;

namespace Arcturus.Concrete
{
    public class Licensing
    {
        public static ILicenseProvider Instance { get; set; }

        public static void SetLicenseProvider(ILicenseProvider provider)
        {
            Instance = provider;
        }
    }
}