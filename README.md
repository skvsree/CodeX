# Why?

Objective is to make developer to code less and do more. It increases developer's productivity and makes sure that developer does not have to maintain his code to be reused in another project.

# What? #

**CodeX.Core** - Extends Core .net DataTypes like string, enums, Datatables, Generic Collections, Files, DateTime and more ...

**CodeX.Windows.Forms** - Extends Windows Form to RichForm and RichConsole with theming and animation. Extends Colors, Message Box and Provides ability to check UAC and more...

# How? #

You can download the dlls from [GitHub](https://github.com/skvsree/CodeX/tree/master/CodeX-dev/bin/Release) and Nuget.

*Install-Package CodeX.Windows.Forms2*

*Install-Package CodeX2*

# Supports #
.net Framework 4, 4.5, 4.5.1

# Getting Started #
Goto the project you want to use Code Extensions and "Add Reference" to CodeX.dll

## Example 
A normal code would look like this.

```
var algo = Rijndael.Create();
var deriveBytes = new Rfc2898DeriveBytes(sharedSecret, Encoding.UTF8.GetBytes(salt));
algo.Key = deriveBytes.GetBytes(algo.KeySize / 8);
ICryptoTransform encryptor = algo.CreateEncryptor(algo.Key, algo.IV);


using (var msEncrypt = new MemoryStream())
{
    msEncrypt.Write(BitConverter.GetBytes(algo.IV.Length), 0, sizeof(int));
    msEncrypt.Write(algo.IV, 0, algo.IV.Length);
    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    {
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(s);
        }
    }
    return Convert.ToBase64String(msEncrypt.ToArray());
}
```

using CodeX you can short it as 

```
var encryptedString = testString.Encrypt("sharedkey")
``` 

you can find more examples in [http://codex.azurewebsites.net/](http://codex.azurewebsites.net/) and documentation at [http://codexdoc.azurewebsites.net/](http://codexdoc.azurewebsites.net/)