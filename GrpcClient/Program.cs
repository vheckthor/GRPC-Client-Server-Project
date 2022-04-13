using Grpc.Core;
using Grpc.Net.Client;
using learngrpc;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var request = new HelloRequest { Name = "Ade" };
			var channel = GrpcChannel.ForAddress("https://localhost:5001");
			var client = new Greeter.GreeterClient(channel);
			var reply = await client.SayHelloAsync(request);
			Console.WriteLine(reply.Message);
			var customerClient = new Customer.CustomerClient(channel);
			Console.WriteLine("Please input a number: ");
			var input = int.Parse(Console.ReadLine());
			var customerRequest = new CustomerLookupModel { UserId = input };
			var response = await customerClient.GetCustomerInfoAsync(customerRequest);
			Console.WriteLine($"{response.FirstName} {response.LastName} \n");
			Console.WriteLine("''''''''''''''''''''");
			Console.WriteLine("New Customers List");
			Console.WriteLine("''''''''''''''''''''\n");
			using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
			{
				while (await call.ResponseStream.MoveNext())
				{
					var curentCustomer = call.ResponseStream.Current;
					Console.WriteLine($" Name: {curentCustomer.FirstName} {curentCustomer.LastName} \n Age: {curentCustomer.Age} " +
						$"\n IsAlive: {curentCustomer.IsAlive} \n Email Address: {curentCustomer.EmailAddress}");
					Console.WriteLine(" \n ");
				}
			}
			Console.ReadKey();
		}
	}
}
