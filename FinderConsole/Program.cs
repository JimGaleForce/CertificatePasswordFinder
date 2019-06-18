using CertificatePasswordFinder;
using System;
using System.Collections.Generic;

namespace FinderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Certificate Password Finder v1.00");
            Console.WriteLine("");
            Console.WriteLine(" Let's say you think the password starts with either aaa, bbb, ccc, or ddd,");
            Console.WriteLine(" then continues with 111, 222, 333, or 444,");
            Console.WriteLine(" then ends with qqq, www, or eee.");
            Console.WriteLine(" To try them all, enter 'aaa bbb ccc ddd' as segment #1, '111 222 333 444' as #2, 'qqq www eee' as #3 without quotes.");
            Console.WriteLine("");

            Console.Write("Enter path of cert/pfx : ");
            var path = Console.ReadLine();

            if (path.StartsWith("\""))
            {
                path = path.Substring(1, path.Length - 2);
            }

            var list = new List<string>();

            Console.WriteLine("Enter each segment with words separated by spaces (i.e. this that those)");
            Console.WriteLine("Enter a blank line to end the segments");

            var line = "x";
            while (!string.IsNullOrWhiteSpace(line))
            {
                Console.Write("Enter segment #" + (list.Count + 1) + " : ");
                line = Console.ReadLine();
                list.Add(line + " ");
            }

            Console.WriteLine();

            var certManager = new CertificateManager(path);
            var password = certManager.PermutatePasswords(list.ToArray());
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("password not found");
            } 
            else
            {
                Console.WriteLine("password found : " + password);
            }
        }
    }
}
