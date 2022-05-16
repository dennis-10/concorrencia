using System.Threading.Channels;

namespace ProducerSubscriber
{
   public class Consumer
    {
        // Receives object for reading in the channel.
        private ChannelReader<int> _reader;

        // Constructor receives object that contains the methods for writing in the channel.
        public Consumer(ChannelReader<int> reader) 
        {
            _reader = reader;

            // Awaites the execution of the Run method for reading the channel data.
            Task.WaitAll(this.Run());
        }

        private async Task Run() 
        {
            // While method returns true for allowiong the reading.
            while(await _reader.WaitToReadAsync())
            {
                // Reads the item in the channel.
                var item = await _reader.ReadAsync();
                Console.Write($"Read {item}");
                Thread.Sleep(7000);
            }
        }
    }
}