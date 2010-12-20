using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spread.Core.UI.Resources
{
    public enum eResourceType
    {
        Auto,
        Javascript,
        CSS,
        Image,

        Other = 100
    }

    public interface IStaticFilesRepository
    {
        string GetUrl(string relativeFilePath, eResourceType resType = eResourceType.Auto);
    }
}
