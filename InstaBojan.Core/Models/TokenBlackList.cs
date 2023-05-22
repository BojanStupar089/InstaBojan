using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public static  class TokenBlackList
    {

        private static readonly List<string> BlackListedTokens = new List<string>();

        public static void AddToBlackList(string token)
        {

            lock (BlackListedTokens)
            {

                BlackListedTokens.Add(token);
            }
        }

        public static bool IsTokenBlackListed(string token)
        {

            lock (BlackListedTokens)
            {

                return BlackListedTokens.Contains(token);
            }
        }

        public static void RemoveFromBlackList(string token)
        {

            lock (BlackListedTokens)
            {

                BlackListedTokens.Remove(token);
            }
        }


    }
}
