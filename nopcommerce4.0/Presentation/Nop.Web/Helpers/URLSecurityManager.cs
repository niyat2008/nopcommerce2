using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Helpers
{
    public class URLSecurityManager
    {
        public  string redirectUrl { get; set; }

        public URLSecurityManager(string url)
        {
            this.redirectUrl = url;
        }

        public static URLSecurityManager Instance(string url)
        {
            return new URLSecurityManager(url);
        }

        public string GetSaveUrl(string url)
        {
            var saveUrl = string.Concat(this.redirectUrl, url);

            return saveUrl;
        }
    }
}
