using System.Threading.Channels;

namespace ProducerSubscriber
{
    class Producer
    {
        // Receives object for writing in the channel.
        private ChannelWriter<int> _writer;

        // Constructor receives object that contains the methods for writing in the channel.
        public Producer(ChannelWriter<int> writer) 
        {
           _writer = writer;

           // Runs Run() method for writing in the channel and waits until it finishes.
           Task.WaitAll(this.Run());
        }

        private async Task Run() 
        {
            // Object for generating random values;
            var r =  new Random();

            // While the method returns a boolean flag true meaning that its allowed to write
            // and returns false when the channel cannot be writen anymore.
            while (await _writer.WaitToWriteAsync())
            {
                // Generates a value bigger than 0 and smaller than 100
                var item = r.Next(100);
                
                // Writes in the channel
                await _writer.WriteAsync(item);
                Console.WriteLine($"\nWriting {item}.");
                Thread.Sleep(7000);
            }
        }
    }
}