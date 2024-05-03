using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace concurency_workshop
{
    internal class CLpara
    {
        public void methode_para()
        {

            for(int i=0; i<=9; i++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("Console message display #" + i+1);
                }
        }
    }
}
