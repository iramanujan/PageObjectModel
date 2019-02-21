using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.Interfaces.HtmlElements
{
    public interface INamedHtmlElementLocatorHandler
    {
        IHtmlElementLocator HtmlElementLocator { get; }
        string Name { get; }
    }
}
