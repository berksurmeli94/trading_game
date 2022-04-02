using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TG.Core.Extensions
{
    public static class Extensions
    {

        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public static decimal GenerateRandomDecimal(int from, int to)
        {
            Random r = new Random();
            int range = r.Next(from, to);
            return Decimal.Round((decimal)r.NextDouble() * range, 2, MidpointRounding.AwayFromZero);
        }

        public static int GenerateRandomInt(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to);
        }
    }
}
