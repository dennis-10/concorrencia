using System.Threading.Channels;

namespace ProducerSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
          Console.WriteLine("Executing...");

          // Creates a channel with a limit of itens.
          var channel = Channel.CreateBounded<int>(2);

          var producer = Task.Run(() => {

              // Passing writer object for the constructor
              new Producer(channel.Writer);
          });

          var consumerOne = Task.Run(() => {

              // Passing Reader object to the consumer constructor.
              new Consumer(channel.Reader);
          });

          var consumerTwo = Task.Run(() => {
              new Consumer(channel.Reader);
          });
        
          // Awaits for the conclusion of each task running.
          Task.WaitAll(
              producer,
              consumerOne,
              consumerTwo);
        }
    }
}