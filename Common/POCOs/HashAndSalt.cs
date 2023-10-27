using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.POCOs;
public class HashAndSalt : POCO
{
    public byte[] Hash { set; get; }
    public byte[] Salt { set; get; }

    public HashAndSalt(byte[] hash, byte[] salt)
    {
        this.Hash = hash;
        this.Salt = salt;
    }
}
