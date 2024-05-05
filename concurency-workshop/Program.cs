using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public delegate void AsyncDelegate(object obj);
        
        // Q5 pool thread
        public delegate void DelegateThreadFive(Object obj);

        // Q7 Evt
        private delegate void DELG(object o); 
        
        // Q8 - Q9 Synchronization
        // Define a lock object
        private static readonly object lockObject = new object();
        private static int var = 0;
        
        // Q9 Synchronization
        // private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        // <summary>
        // Q1 - Delegate* Let the method 'int method (int v1, int v2)'. This method adds two values and returns the result. Write the delegate who will invoke this method
        // </summary>
        //  <param name="id">The unique user ID.</param>
        // <returns>A string containing the username for the specified ID.</returns>

        public static int addMethod(int i1, int i2)
        {
            return i1 + i2;
        }

        public delegate void SafeDelegate(object state);

        static void Main(string[] args)
        {

            // <summary>
            //     Q1 - Q2 Delegate* Let the method 'int method (int v1, int v2)'. This method adds two values and returns the result. Write the delegate who will invoke this method
            // </summary>

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


            // -------------------------------

            // <summary>
            //     Q3 Implement an anonymous type that has an 'int', a 'string'. Exposing its use
            // </summary>

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
            // -------------------------------

            // Thread & Thread Param
            // <summary>
            //     Q4
            // </summary>
            // 

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

            // -------------------------------

            // <summary>
            //     Q5 - Pool Threads
            // </summary>

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
            
            Console.WriteLine();
            Console.WriteLine("Q5 - ==============END=============");
            Console.WriteLine();


            // -------------------------------

            // <summary>
            //     Q6 - Delegate Async
            // </summary>

            Console.WriteLine("Q6 - =======Delegate Async=======");
            
            AsyncDelegate asyncDelegate = (obj) =>
            {
                string msg = obj.ToString();
                for (int i = 0; i <= 9; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Message :" + msg + " #" + (i + 1));
                }
            };

            // BeginInvoke method
            AsyncCallback callback = (IAsyncResult ar) =>
            {
                AsyncDelegate delegateInstance = (AsyncDelegate)((AsyncResult)ar).AsyncDelegate;
                delegateInstance.EndInvoke(ar);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Callback completed");
                Console.ResetColor();
            };
            
            // BeginInvoke with callback
            IAsyncResult asyncResult = asyncDelegate.BeginInvoke("T1", callback, null);

            // asyncResult.AsyncWaitHandle.WaitOne();
            while (!asyncResult.IsCompleted)
            {
                Console.WriteLine("The main thread is waiting for the call back end");
                Thread.Sleep(2000);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Callback end");
            Console.ResetColor();
            
            Console.WriteLine("Q6 - ==============END=============");
            Console.WriteLine();
            
            
            // -------------------------------

            // <summary>
            //     Q7 - Evt
            // </summary>

            Console.WriteLine("Q7 - =========Evt=========");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("Initialisation du thread principal...ok"); 
            DELG delg; 
            NS_SERVER.CLserver server = new NS_SERVER.CLserver(); 
            NS_CLIENT.CLclient client1 = new NS_CLIENT.CLclient(server, "C1"); 
            NS_CLIENT.CLclient client2 = new NS_CLIENT.CLclient(server, "C2"); 
            string[] messages = {"msg1","msg2","msg3"}; 
            delg = (o) => 
                { 
                    for (int i = 0; i < messages.Length; i++) 
                    { 
                        server.Msg = messages[i]; 
                        System.Threading.Thread.Sleep(4000); 
                    } 
                }; 
            Console.WriteLine("Début traitement asynchrone...ok"); 
            IAsyncResult asr = delg.BeginInvoke(((object)("nostate")), 
                (asR)=> 
                { 
                    delg.EndInvoke(asR); 
                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.WriteLine("Fin traitement asynchrone...ok"); 
                },delg); 
            while (!asr.IsCompleted) 
            { 
                Console.ForegroundColor = ConsoleColor.Green; 
                Console.WriteLine("Traitement en cours sur le thread principal"); 
                System.Threading.Thread.Sleep(3000); 
            } 
            
            // Console.Read(); 
            Console.ResetColor();
            
            Console.WriteLine("Q7 - ==============END=============");
            Console.WriteLine();

            // -------------------------------

            // <summary>
            //     Q8 - Synchronization
            // </summary>

            Console.WriteLine("Q8 - =========Synchronization=========");
            Console.WriteLine();

            SafeDelegate safeDelegate = (object state) =>
            {
                lock (lockObject)
                {
                    string name_thread = (string)state;
                    ++var;
                    Console.WriteLine("Thread -> {0} -- var --> {1}", name_thread, var.ToString());
                    Thread.Sleep(2000);
                }
            };
            
            Thread t4 = new Thread(new ParameterizedThreadStart(safeDelegate));
            Thread t5 = new Thread(new ParameterizedThreadStart(safeDelegate));
            
            t4.Start("T4");
            t5.Start("T5");

            t4.Join();
            t5.Join();

            Console.WriteLine();

            Console.WriteLine("Q8 - ==============END=============");

            Console.WriteLine();
            // -------------------------------
            
            // <summary>
            //     Q9 - Synchronization
            // </summary>

            Console.WriteLine("Q9 - =========Synchronization=========");
            Console.WriteLine();

            var = 0;
            
            SafeDelegate safeDelegate2 = (object state) =>
            {
                for (int i = 0; i < 3; i++)
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.Enter(lockObject, ref lockTaken);
                        string name_thread = (string)state;
                        ++var;
                        Console.WriteLine("Thread -> {0} -- var -> {1}", name_thread, var.ToString());
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(lockObject);
                        }
                    }
                }            
            };
            
            Thread t6 = new Thread(new ParameterizedThreadStart(safeDelegate2));
            Thread t7 = new Thread(new ParameterizedThreadStart(safeDelegate2));
            
            t6.Start("T1");
            t7.Start("T2");

            t6.Join();
            t7.Join();
            
            Console.WriteLine();

            Console.WriteLine("Q9 - ==============END=============");

            Console.WriteLine();

            // -------------------------------

            Console.WriteLine("Main thread terminating...");
            
        }
    }
}
