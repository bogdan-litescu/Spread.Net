using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Spread.Core.UI.Resources
{
    public class LocalRepository : IStaticFilesRepository
    {
        public string BaseUrl { get; set; }

        public LocalRepository(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        #region IStaticFilesRepository Members

        public string GetUrl(string relativeFilePath, eResourceType resType = eResourceType.Auto)
        {
            string resPath = BaseUrl; // HttpRuntime.AppDomainAppVirtualPath;
            
            if (resType == eResourceType.Auto) {
                // determine based on extensions
                switch (System.IO.Path.GetExtension(relativeFilePath).ToLower()) {
                    case ".js":
                        resType = eResourceType.Javascript;
                        break;
                    case ".css":
                        resType = eResourceType.CSS;
                        break;
                    case ".png":
                    case ".jpg":
                    case ".gif":
                        resType = eResourceType.Image;
                        break;

                    default:
                        resType = eResourceType.Other;
                        break;
                }
            }

            // we now should have the appropriate time, translate to folder
            switch (resType) {
                case eResourceType.Javascript:
                    resPath = resPath.TrimEnd('/') + "/js";
                    break;
                case eResourceType.CSS:
                    resPath = resPath.TrimEnd('/') + "/css";
                    break;
                case eResourceType.Image:
                    resPath = resPath.TrimEnd('/') + "/images";
                    break;
                //case eResourceType.Other: // if it's other leave it like it is
                //    break;
            }

            // finally, return full relative path
            return resPath.TrimEnd('/') + "/" + relativeFilePath.TrimStart('/');
        }

        #endregion
    }
}
