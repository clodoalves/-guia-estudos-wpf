using Quartz;
using System.Threading.Tasks;
using WPFSample.Service.Contract;

namespace WPFSample.App.Quartz.Job
{
    public class UpdateQuantityProductJob : IJob
    {
        private readonly IProductService _productService;

        public Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            int quantity = (int)dataMap.Get("quantity");

            _productService.UpdateQuantityProducts(quantity);

            return Task.CompletedTask;
        }

        public UpdateQuantityProductJob(IProductService productService)
        {
            this._productService = productService;
        }
    }
}
