using ServiceContract;
using System.Threading.Tasks;
using Xunit.Abstractions;
using CoreWCF;
using Xunit.Sdk;

namespace Services
{
	[ServiceBehavior]
	public class OneWayService : IOneWayContract
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0002026C File Offset: 0x0001E46C
		private ITestOutputHelper _output= new TestOutputHelper();

		public Task OneWay(string s)
		{
			Task task = new Task(delegate
            {
				_output.WriteLine(string.Format("Invoked oneway operation with {0}.", s));
			});
			task.Start();
			return task;
		}
	}
}
