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
            Console.WriteLine("Q1 - ===========Delegate using method===========");

            MyDelegateMethod myDelegateMethod = addMethod;

            int x1 = 1;
            int x2 = 2;

            Console.WriteLine();
            Console.WriteLine("    int addMethod(int x, int y) {return x + y;}");
            Console.WriteLine($"    myDelegateMethod = addMethod; => myDelegateMethod({x1}, {x2}) == {myDelegateMethod(x1, x2)}");
            Console.WriteLine();

            Console.WriteLine("Q1 - ==============END=============");
            
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Q2 - =======Delegate using Lambda expression======");

            myDelegateMethod = (int x, int y) => x + y;
            
            Console.WriteLine();
            Console.WriteLine($"      myDelegateMethod = (int x, int y) => x + y; => myDelegateMethod({x1}, {x2}) == {myDelegateMethod(x1, x2)}");
            Console.WriteLine();

            Console.WriteLine("Q2 - ==============END=============");

            Console.WriteLine();
            Console.WriteLine();


            /// -------------------------------

            /// <summary>
            ///     Q3 Implement an anonymous type that has an 'int', a 'string'. Exposing its use
            /// </summary>

            Console.WriteLine("Q3 - =======Anonymous Type=======");

            var johnDo = new {name = "unknow", id = 1 };
            
            Console.WriteLine();
            Console.WriteLine("     Anonymous Type definition:");
            Console.WriteLine("     new {name = \"unknow\", id = 1 }");
            Console.WriteLine();
            Console.WriteLine("     Reading anonymously typed variable");
            Console.WriteLine($"     jonhDo: {johnDo}");
            Console.WriteLine();

            Console.WriteLine("Q3 - ==============END=============");

            Console.WriteLine();
            Console.WriteLine();
            /// -------------------------------

            /// Thread & Thread Param
            /// <summary>
            ///     Q4
            /// </summary>
            /// 

            Console.WriteLine("Q4 - =======Thread & Thread Param=======");

            Console.WriteLine();
            //CLpara cLpara= new CLpara();

            ParameterizedDelegate parameterizedDelegate = (Object message) => {
                for (int i = 0; i <= 9; i++)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("      " + message + " #" + (i + 1));
                }
            };

            String str = "Hi";

            ParameterizedThreadStart threadStartDelegate = new ParameterizedThreadStart(parameterizedDelegate); // Assuming no argument is needed
            Thread thread = new Thread(threadStartDelegate);

            thread.Start(str);
            thread.Join();

            Console.WriteLine();

            Console.WriteLine("Q4 - ==============END=============");

            Console.WriteLine();

            /// -------------------------------

            /// <summary>
            ///     Q5 - Pool Threads
            /// </summary>

            Console.WriteLine("Q5 - =======Pool Threads=======");

            // Delegate definition for thread execution
            DelegateThreadFive delegateThreadFive = (obj) =>
            {
                String msg = obj.ToString();
                Console.WriteLine("");

                int i = 0;
                while (i <= 9)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("      Message :" + msg + " #" + (i + 1));
                    i++;
                }
            };


            // Create a synchronization event for thread completion
            ManualResetEvent mre = new ManualResetEvent(false);  // Synchronization event

            // Queue a task to the thread pool
            ThreadPool.QueueUserWorkItem((state) => 
            {

                // Create and start three parameterized threads
                Thread t1 = new Thread(new ParameterizedThreadStart(delegateThreadFive));
                t1.Name = "Thread " + delegateThreadFive.Method.Name + " 1";

                Thread t2 = new Thread(new ParameterizedThreadStart(delegateThreadFive));
                t2.Name = "Thread " + delegateThreadFive.Method.Name + " 2";

                Thread t3 = new Thread(new ParameterizedThreadStart(delegateThreadFive));
                t3.Name = "Thread " + delegateThreadFive.Method.Name + " 3";

                // Start the threads with specific messages as arguments
                t1.Start("Maiva-hub");
                t2.Start("noumendarryl");
                t3.Start("sikatikenmogne");

                // Wait for all three threads to finish
                t1.Join();
                t2.Join();
                t3.Join();

                // Signal the main thread that background threads have started
                mre.Set();

            });

            // Wait for background threads to finish
            mre.WaitOne(); // Block the main thread until the event is signaled

            Console.WriteLine("Q5 - ==============END=============");

            Console.WriteLine("Main thread terminating...");


        }
    }
}
