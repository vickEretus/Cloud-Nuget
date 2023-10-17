namespace Common.POCOs;
public class SingleToken : POCO
{
    public SingleToken(string token) => Token = token;

    public string Token { get; set; }
}
