using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions
{
    public static byte[] ExtractBytes(this string emoji) => Encoding.UTF8.GetBytes(emoji);

    public static bool ContainsByteKey(this Dictionary<byte[], byte> dict, byte[] key, out byte? founded)
    {
        founded = null;
        
        foreach (var k in dict.Keys)
        {
            if (k.SequenceEqual(key))
            {
                founded = dict[k];
                return true;
            }
        }

        return false;
    }
}