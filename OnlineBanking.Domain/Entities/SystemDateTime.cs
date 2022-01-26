using OnlineBanking.Domain.Interfaces;
using System;

namespace OnlineBanking.Domain.Entities
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Greeting 
        {
            get { return DateTime.Now; }
        }
    }
}
