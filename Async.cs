using System;
using System.Linq;

public class Async{

	public class KafkaConsumerService
	{
		public KafkaConsumerService(string a) {}
		public async Task StartAsync()
		{
			Thread.Sleep(2000);
			Console.WriteLine("1");
			return;
		}
	}
	public static IEnumerable<KafkaConsumerService> _kafkaConsumerServices;
	static async Task Main()
	{
		_kafkaConsumerServices = new List<KafkaConsumerService>
		{
			new KafkaConsumerService("topic1"),
			new KafkaConsumerService("topic2"),
			new KafkaConsumerService("topic3")
		}.AsEnumerable();

		Console.WriteLine("Main started");
		Console.WriteLine("Thread id: " + Thread.CurrentThread.ManagedThreadId);
		var a = new Async();
		a.StartAsync();

		Console.WriteLine("Main finished"); 

		Thread.Sleep(10000);
	}

	public async Task StartAsync()
	{
		var tasks = _kafkaConsumerServices.Select(service => service.StartAsync());

		Console.WriteLine("Thread id: " + Thread.CurrentThread.ManagedThreadId);
		

		await Console.Out.WriteLineAsync("Starting Kafka consumers..."); //same as sync Console.WriteLine
		// await Task.WhenAll(tasks); //block the main thread, because firstly it iterates over all tasks,
									// creates Task object and only then calls await Task.WhenAll(tasks);
		await Task.Run( () => Task.WhenAll(tasks)); //passes to another thread before creating Task object
	}
}