namespace program
{
    public class Program
    {
        static Mutex _mutex = new Mutex();

        static Semaphore _empty = new Semaphore(1, 1);

        static Semaphore _full = new Semaphore(0, 1);
    
        static void Main(string[] args)
        {
            var buffer = new List<int>();
            var random = new Random();

            var producer = new Thread(() => {
                while(true)
                {
                    _empty.WaitOne(); // Decrement 
                    _mutex.WaitOne();
                   
                    buffer.Add(random.Next(100));
                    Console.WriteLine($"Adding value '{buffer[0]}' to the buffer.");
                    
                    _mutex.ReleaseMutex();
                    _full.Release(); // Increment
                }
            });

            var consumer1 = new Thread(() => {
                while(true)
                {
                    Thread.Sleep(2000);
                    _full.WaitOne();
                    _mutex.WaitOne();

                    Console.WriteLine($"Consumer 1 removing '{buffer[0]}' from memory buffer.");
                    buffer.RemoveAt(0);

                    _mutex.ReleaseMutex();
                    _empty.Release();
                }
            });

            var consumer2 = new Thread(() => {
                while(true)
                {
                    Thread.Sleep(2000);
                    _full.WaitOne();
                    _mutex.WaitOne();

                    Console.WriteLine($"Consumer 2 removing '{buffer[0]}' from memory buffer.");
                    buffer.RemoveAt(0);

                    _mutex.ReleaseMutex();
                    _empty.Release();
                }
            });

            
            var consumer3 = new Thread(() => {
                while(true)
                {
                    Thread.Sleep(2000);
                    _full.WaitOne();
                    _mutex.WaitOne();

                    Console.WriteLine($"Consumer 3 removing '{buffer[0]}' from memory buffer.");
                    buffer.RemoveAt(0);

                    _mutex.ReleaseMutex();
                    _empty.Release();
                }
            });

            
            var consumer4 = new Thread(() => {
                while(true)
                {
                    Thread.Sleep(2000);
                    _full.WaitOne();
                    _mutex.WaitOne();

                    Console.WriteLine($"Consumer 4 removing '{buffer[0]}' from memory buffer.");
                    buffer.RemoveAt(0);

                    _mutex.ReleaseMutex();
                    _empty.Release();
                }
            });

            producer.Start();

            Task.WaitAll();
            consumer1.Start();
            consumer2.Start();
            consumer3.Start();
            consumer4.Start();
        }
    }
}