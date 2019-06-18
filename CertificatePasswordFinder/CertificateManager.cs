using System;
using System.Security.Cryptography.X509Certificates;

namespace CertificatePasswordFinder
{
    public class CertificateManager
    {
        private string path { get; set; }

        public CertificateManager(string certOrPfxPath)
        {
            this.path = certOrPfxPath;
        }

        public bool TryPassword(string password)
        {
            try
            {
                var certificate = new X509Certificate2(path, password);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string PermutatePasswords(string[] segments)
        {
            return PermutatePasswords(segments, "", 0);
        }

        private string PermutatePasswords(string[] parts, string text, int num)
        {
            if (num == parts.Length)
            {
                if (TryPassword(text))
                {
                    return text;
                }

                return "";
            }

            var seg = parts[num].Split(' ');

            for (var i = 0; i < seg.Length; i++)
            {
                var pw = PermutatePasswords(parts, text + seg[i], num + 1);
                if (!string.IsNullOrWhiteSpace(pw))
                {
                    return pw;
                }

                if (seg[i].Length > 0 && char.IsLower(seg[i][0]))
                {
                    pw = PermutatePasswords(parts, text + seg[i][0].ToString().ToUpper() + seg[i].Substring(1), num + 1);
                    if (!string.IsNullOrWhiteSpace(pw))
                    {
                        return pw;
                    }

                    pw = PermutatePasswords(parts, text + seg[i].ToUpper(), num + 1);
                    if (!string.IsNullOrWhiteSpace(pw))
                    {
                        return pw;
                    }
                }
            }

            return "";
        }
    }
}
