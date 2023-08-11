using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using vimmvc.ViewModels;

namespace vimmvc.Controllers
{
    public class PaystackPaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private PayStackApi Paystack { get; set; }
        private readonly string token;
        private readonly string _baseUrl;
        private readonly ApplicationDbContext _context;
        private readonly EnvironmentMethods _environmentMethods;
        private readonly string environment;
        public PaystackPaymentController(IConfiguration configuration, ApplicationDbContext context, EnvironmentMethods environmentMethods)
        {
            _configuration = configuration;
            Paystack = new PayStackApi(token);
            _context = context;
            _environmentMethods = environmentMethods;
            environment = _environmentMethods.GetRunningEnvironment();
            _baseUrl = environment == "Development" ? _configuration["Urls:AppDomain"] : Environment.GetEnvironmentVariable(Constants.AppDomain);
            token = environment == "Development" ?_configuration["Payment:PaystackSk"] : Environment.GetEnvironmentVariable(Constants.PaystackKey);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentInitializeModel inputModel)
        {
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = inputModel.Amount * 100,
                Email = inputModel.Email,
                Reference = GenerateReference().ToString(),
                Currency = "NGN",
                CallbackUrl = $"{_baseUrl}paystackpayment/verify"

            };
            TransactionInitializeResponse response = Paystack.Transactions.Initialize(request);
            if (response.Status)
            {
                var transcation = new CourseTransaction
                {
                    Amount = inputModel.Amount,
                    Email = inputModel.Email,
                    TransRefrence = request.Reference,
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    UserId = inputModel.UserId,
                };
                await _context.Transactions.AddAsync(transcation);
                await _context.SaveChangesAsync();
                return Redirect(response.Data.AuthorizationUrl); 
            }
            //will add error notification
            return View(request);
        }

        public async Task<IActionResult> Verify(string reference)
        {
            TransactionVerifyResponse response = Paystack.Transactions.Verify(reference);
            if(response.Data.Status == "success")
            {
                var transaction = await _context.Transactions.Where(x => x.TransRefrence == reference)
                    .FirstOrDefaultAsync();
                if(transaction != null)
                {
                    transaction.IsSuceeded = true;
                    _context.Transactions.Update(transaction);
                    await _context.SaveChangesAsync();
                    //We will redirect to dashboard
                }
            }
            return View();
        }

        private static int GenerateReference()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999);
        }
    }
}
