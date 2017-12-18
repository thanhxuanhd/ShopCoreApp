using ShopCoreApp.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Infrastructure.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}
