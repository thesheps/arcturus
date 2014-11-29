using Arcturus.Concrete;
using Arcturus.Exceptions;
using System.Web;
using System.Web.Mvc;

namespace Arcturus.Attributes
{
    public class RequiresLicenseAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Licensing.Instance == null) throw new LicensingNotImplementedException();

            if (Licensing.Instance.IsLicensed())
            {
                return base.AuthorizeCore(httpContext);                
            }

            return false;
        }
    }
}