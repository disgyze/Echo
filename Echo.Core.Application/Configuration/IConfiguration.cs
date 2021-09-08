using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Core.Configuration
{
    public interface IConfiguration
    {
        TSettings GetSettings<TSettings>();
        void UpdateSettings<TSettings>(TSettings settings);
    }
}