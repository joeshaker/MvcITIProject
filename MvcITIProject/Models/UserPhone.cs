using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class UserPhone
{
    public string UserSsn { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public virtual User UserSsnNavigation { get; set; } = null!;
}
