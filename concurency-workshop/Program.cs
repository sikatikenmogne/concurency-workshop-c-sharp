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
        public delegate void ParameterizedDelegate(Object message);

        /// Q5 pool thread
        public delegate void DelegateThreadFive(Object obj);

        /// <summary>
        /// Q1 - Delegate* Let the method 'int method (int v1, int v2)'. This method adds two values and returns the result. Write the delegate who will invoke this method
        /// </summary>
        ///  <param name="id">The unique user ID.</param>
        /// <returns>A string containing the username for the specified ID.</returns>

        public static int addMethod(int i1, int i2)
        {
            return i1 + i2;
        }


        static void Main(string[] args)
        {

            /// <summary>
            ///     Q1 - Q2 Delegate* Let the method 'int method (int v1, int v2)'. This method adds two values and returns the result. Write the delegate who will invoke this method
            /// </summary>

            // Using addMethod()
            // MyDelegateMethod myDelegateMethod = addMethod;

            // Using Lambda Expression
            MyDelegateMethod myDelegateMethod = (int x1, int x2) => x1 + x2;

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

            /// -------------------------------

            /// <summary>
            ///     Q3 Implement an anonymous type that has an 'int', a 'string'. Exposing its use
            /// </summary>

            var johnDo = new {name = "unknow", id = 1 };

            Console.WriteLine(johnDo.name);
            Console.WriteLine(johnDo.name.GetType());
            Console.WriteLine("-------------------");
            Console.WriteLine(johnDo.id);
            Console.WriteLine(johnDo.id.GetType());
            /// -------------------------------

            /// Thread & Thread Param
            /// <summary>
            ///     Q4
            /// </summary>
            //CLpara cLpara= new CLpara();

            ParameterizedDelegate parameterizedDelegate = (Object message) => {
                for (int i = 0; i <= 9; i++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine( message + " #" + (i + 1));
                }
            };

            String str = "Hi";

            ParameterizedThreadStart threadStartDelegate = new ParameterizedThreadStart(parameterizedDelegate); // Assuming no argument is needed
            Thread thread = new Thread(threadStartDelegate);

            thread.Start(str);
            thread.Join();
            /// -------------------------------
        }
    }
}
