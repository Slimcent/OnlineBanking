using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBanking.Domain.Folder
{
    public static class AccountNumberGenerator
    {
        public static string GenerateAccountNumber()
        {
            string startWith = "22";
            var miliseconds = string.Format("{0:000}", DateTime.Now.Millisecond);
            var year = DateTime.Now.ToString("yy");         
            var day = (RandomNumberGenerator.GetInt32(100, 999)).ToString();


            var accountNumber = startWith + miliseconds + year + day;

            return accountNumber;
        }
        
    }
}
