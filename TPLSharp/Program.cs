using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPLSharp
{
    class Program
    {

        public static void Write(char c)
        {
            int i = 1000;
            while (i --> 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i --> 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}....");
            return o.ToString().Length;
        }
        
        
        
        static void Main(string[] args)
        {
            //creates a task and starts it as well
            // Task.Factory.StartNew(() =>
            // {
            //     Write('.');
            // });
            
            //making a task, but not starting it. You need to start it manually
            
            // var t = new Task(() =>
            // {
            //     Write('?');
            // });
            //
            // t.Start();


            //Task.Factory.StartNew(Write,"Hello");

            // string text1 = "testing", text2 = "this";
            //
            // var task1 = new Task<int>(TextLength,text1);
            //
            // task1.Start();


            //Result is a blocking operation
            // Task<int> task2 = Task.Factory.StartNew<int>(TextLength, text2);
            // Console.WriteLine($"Length of {text1} is {task1.Result}");
            // Console.WriteLine($"Length of {text2} is {task2.Result}");
            
            //cancelling tasks


            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested");
            });
            
            var t = Task.Factory.StartNew(() =>
            {
                
                int i = 0;
                while ( i < 100000)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            },token);

            Console.ReadKey();
            cts.Cancel();
            
            Console.WriteLine($"Main program is done");

            Console.ReadKey();

        }
    }
}