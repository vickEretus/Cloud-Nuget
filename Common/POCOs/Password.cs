using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.POCOs;
public class Password : POCO
{
    public string RawPassword { set; get; }

    public Password(string rawPassword) => RawPassword = rawPassword;
}
