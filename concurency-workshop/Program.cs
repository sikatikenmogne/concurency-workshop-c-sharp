using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace concurency_workshop
{

    internal class Program
    {
        private static Func<int, int> square;

        delegate int MyDelegateMethod(int i1, int i2);
        delegate int MySecondDelegateMethod(int i2);
        public delegate void ParameterizedThreadStart();

        public static int addMethod(int i1, int i2)
        {
            return i1 + i2;
        }


        static void Main(string[] args)
        {

            // Let the method 'int method (int v1, int v2)'. This method adds two values and returns the result. Write the delegate who will invoke this method
            MyDelegateMethod myDelegateMethod = addMethod;

            int result = myDelegateMethod(1, 2);

            Console.WriteLine(result);
            // ---------------

            // Construct using a lambda expression a method that will calculate the square of a number
            square = (int i) => { return i * i; };

            result = square(2);

            // TODO: C'est quoi la différence entre un Functeur (Func<int, int>) et les delegates
            Console.WriteLine(result);

            Console.WriteLine("or");
            // or

            MySecondDelegateMethod mySecondDelegateMethod = (int i) => { return i * i; };

            result = mySecondDelegateMethod(2);

            Console.WriteLine(result);
            // ---------------

            // Implement an anonymous type that has an 'int', a 'string'. Exposing its use
            var johnDo = new {name = "unknow", id = 1 };

            Console.WriteLine(johnDo.name);
            Console.WriteLine(johnDo.name.GetType());
            Console.WriteLine("-------------------");
            Console.WriteLine(johnDo.id);
            Console.WriteLine(johnDo.id.GetType());
            // ---------

            // THREAD
            //CLpara cLpara= new CLpara();

            ParameterizedThreadStart parameterizedThreadStart = CLpara.methode_para;

            Thread thread = new Thread(() => { parameterizedThreadStart(); });

            thread.Start();

        }
    }
}
