using Arcturus.Abstract;

namespace Arcturus.Concrete
{
    public class Licensing
    {
        internal static ILicenseProvider Instance { get; set; }

        public static void SetLicenseProvider(ILicenseProvider provider)
        {
            Instance = provider;
        }
    }
}