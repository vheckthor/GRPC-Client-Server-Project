using Grpc.Core;
using learngrpc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learngrpc.Services
{
	public class CustomersService : Customer.CustomerBase
	{
		private readonly ILogger<CustomersService> _logger;

		public CustomersService(ILogger<CustomersService> logger)
		{
			_logger = logger;
		}

		public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
		{
			CustomerModel output = new CustomerModel();

			if (request.UserId == 1)
			{
				output.FirstName = "oreoluwa";
				output.LastName = "Farodoye";
			}
			else if (request.UserId == 2)
			{
				output.FirstName = "Jagaban";
				output.LastName = "Cravings";
			}
			else if (request.UserId == 3)
			{
				output.FirstName = "Angular";
				output.LastName = "C sharper";
			}
			else
			{
				output.FirstName = "None";
				output.LastName = "nill";
			}
			return Task.FromResult(output);
		}

		public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
		{
			List<CustomerModel> customers = new List<CustomerModel>
			{
				new CustomerModel
				{
					FirstName = "asdjf",
					Age = 23,
					LastName = "fjgsd",
					EmailAddress = "fjfdj@gmail.com",
					IsAlive = true
				},
				new CustomerModel
				{
					FirstName = "gtlkry",
					Age = 20,
					LastName = "dfgoggopswtwrt",
					EmailAddress = "fghlkhhj@gmail.com",
					IsAlive = true
				},
				new CustomerModel
				{
					FirstName = "bjfoert",
					Age = 33,
					LastName = "lergiusd",
					EmailAddress = "gkjlg@gmail.com",
					IsAlive = false
				}
			};

			customers.ForEach(async(x) => await responseStream.WriteAsync(x));
		}
	}
}
