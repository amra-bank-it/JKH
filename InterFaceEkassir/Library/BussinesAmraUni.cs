using System;
using System.Collections.Generic;
using Unistream.Business.Infrastructure.UnistreamAdapter;
using Unistream.Business.Providers;
using Unistream.Infrastructure.Unistream.Api.Dto;
using amraGateSet.eNumUnik;
using nsKey_Value;
using PrintGera;
using sql_Gera;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Unistream.Business;
using Unistream.Utilities.DI.Autowiring;
using Microsoft.Extensions.Configuration;
using Unistream.Utilities.Payloads;

namespace amraGateSet.Library
{
    //public class BussinesAmraUni
    //{
    //    public printGera gPrint;
    //    public SQL_Gera xSQL;
    //    public  BussinesAmraUni(printGera gPrint_,SQL_Gera xSQL_)
    //    {
    //        gPrint = gPrint_;
    //        xSQL = xSQL_;
    //    }

    //    public 


    //    public int methods(UniOperations uniOper,KeyValue inData)
    //    {
    //        apiU.onApi();
    //        switch (uniOper)
    //        {
    //            case UniOperations.CheckPay:
    //                {
    //                    getCustomer(inData.Key2Value("numberDoc"));
    //                    break;
    //                }
    //            case UniOperations.NewPay:
    //                {


    //                    break;
    //                }
    //            case UniOperations.ConfirmPay:
    //                {
    //                    break;
    //                }
    //            case UniOperations.StatusPay:
    //                {
    //                    break;
    //                }
    //        }

    //        return 0;
    //    }
    //    public ClientsDto getCustomer(string numberDoc)
    //    {
    //        apiU.onApi();
    //        CustomerProvider cus = new CustomerProvider((Microsoft.Extensions.Logging.ILogger<CustomerProvider>)apiU.UniLogg, apiU.uniApi);
    //        var dd = cus.FindCustomersAsync(numberDoc).Result;
    //        if (dd.IsFailure) return dd.Value;

    //        ClientDto nCli = newCustomer(numberDoc);
    //        var resTask = cus.CreateCustomerAsync(nCli).Result;
    //        if (resTask.IsFailure) return null;
    //        return getCustomer(numberDoc);

    //        return null;
    //    }
    //    public ClientDto newCustomer(string numberDoc)
    //    {
    //        ClientDto cli = new ClientDto();
    //        KeyValue cliData = get_cliDataAmra(numberDoc);
    //        //get address and fullin 
    //        AddressDto address = new AddressDto();
    //        address.AddressString = cliData.Key2Value("AddressString");
    //        address.Apartment = cliData.Key2Value("Apartment");
    //        address.Building= cliData.Key2Value("Building");
    //        address.City= cliData.Key2Value("City");
    //        address.CountryCode = cliData.Key2Value("CountryCode");
    //        address.House= cliData.Key2Value("House");
    //        address.Postcode = cliData.Key2Value("Postcode");
    //        address.State= cliData.Key2Value("State");
    //        address.Street= cliData.Key2Value("Street");

    //        cli.Address = address;

    //        //fullin ident data
    //        cli.BirthDate = cliData.Key2ValueDt("BirthDate");
    //        cli.BirthPlace = cliData.Key2Value("BirthPlace");
    //        cli.CountryOfResidence = cliData.Key2Value("CountryOfResidence");
    //        cli.FirstName = cliData.Key2Value("FirstName");
    //        cli.Gender = cliData.Key2ValueB("Gender") ? Gender.Female:Gender.Male;
    //        cli.Id = cliData.Key2Value("Id");
    //        cli.KazId = cliData.Key2Value("KazId");
    //        cli.LastName= cliData.Key2Value("LastName");
    //        cli.LoyaltyCardNumber= cliData.Key2Value("LoyaltyCardNumber");
    //        cli.MiddleName = cliData.Key2Value("MiddleName");
    //        cli.PhoneNumber = cliData.Key2Value("PhoneNumber");
    //        cli.TaxpayerIndividualIdentificationNumber= cliData.Key2Value("TaxpayerIndividualIdentificationNumber");

    //        //fullin doc
    //        DocumentDto document = new DocumentDto();
    //        document.Type = cliData.Key2Value("Type");
    //        Dictionary<string, string> dfcli = new Dictionary<string, string>();
    //        dfcli.Add("Series", cliData.Key2Value("Series"));
    //        dfcli.Add("Number", cliData.Key2Value("Number"));
    //        dfcli.Add("Issuer", cliData.Key2Value("Issuer"));
    //        dfcli.Add("IssuerDepartmentCode", cliData.Key2Value("IssuerDepartmentCode"));
    //        dfcli.Add("IssueDate", cliData.Key2Value("IssueDate"));
    //        dfcli.Add("expiryDate", cliData.Key2Value("expiryDate"));

    //        document.Fields = dfcli;
    //        document.Legend = cliData.Key2Value("Legend");
    //        document.UniqueNumber = cliData.Key2Value("UniqueNumber");
    //        return cli;
    //    }
    //    public KeyValue get_cliDataAmra(string numberDoc)
    //    {
    //        string request = "";
    //        return xSQL.Read1LineSQL("GET USER DATA", request);
    //    }

 

    //}
    //public static class apiU
    //{
    //    public static UnistreamLogger UniLogg ;
    //    public static UnistreamApiAdapter uniApi;

    //    public static void onApi()
    //    {
    //        apiSett aSett = new apiSett();
    //        UnistreamApiAdapterSettings UAAS = aSett.getApi();
    //        //UniLogg = aSett.getLogger();
    //        uniApi = aSett.apiAdapter(null, UAAS);


    //    }
    //    public static void offApi()
    //    {
    //        uniApi = null;
    //        UniLogg = null;
    //    }


    //}
    //public class apiSett
    //{
    //    public  UnistreamApiAdapterSettings getApi()
    //    {
    //        UnistreamApiAdapterSettings settApi = new UnistreamApiAdapterSettings();
    //        settApi.SecurityPosId = "110291";
    //        settApi.ApplicationId = "8AADD8B4AB51E7925A17";
    //        settApi.ApplicationSecret = "xc4i6qXgNErqLWE2TDG0/CoaJm9g+eShY2UlxpC3rHifrQ7UNpTOrL2dTEQ4c9Bji6BfqxSCNCpRU1nv";
    //        settApi.ApiUrl = "https://slt-test.api.unistream.com";
    //        settApi.CertificateFilePath = "cert.pfx";
    //        settApi.CertificatePassword = "5158";

    //        return settApi;
    //    }

    //    public ILogger<UnistreamLogger> getLogger()
    //    {
    //        ILogger<UnistreamLogger> uniLogg = null;

    //        return uniLogg;
    //    }
    //    public ILogger<UnistreamApiAdapter> getLogger_api()
    //    {
    //        ILogger<UnistreamApiAdapter> uniLogg = null;

    //        return uniLogg;
    //    }

    //    public UnistreamApiAdapter apiAdapter(ILogger<UnistreamLogger> uniLogg, UnistreamApiAdapterSettings settApi)
    //    {
    //        UnistreamApiAdapter uniApi = new UnistreamApiAdapter(
    //            getLogger_api()
    //            , (Microsoft.Extensions.Options.IOptions<UnistreamApiAdapterSettings>)settApi);

    //        return uniApi;
    //    }

    //}
  
    public static class Layer_DI
    {
        public static R<ClientsDto> GetCus(string numDoc)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var services = new ServiceCollection();
            services.AddBusiness(configuration);
            //services.AddScoped<ICustomerProvider, CustomerProvider>();
            ((IServiceCollection)services).AddAutowiringDependencies();


            ServiceProviderContainer.ServiceProvider = services.BuildServiceProvider();

            TestDiClass testDiClass = new TestDiClass();
            var result = testDiClass.Customer_Handler(numDoc); 
            return result;
        }

    }
    public class ServiceProviderContainer
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
    public class TestDiClass
    {
        public R<ClientsDto> Customer_Handler(string numDoc)
        {
            var customerProvider = ServiceProviderContainer.ServiceProvider.GetRequiredService<ICustomerProvider>();

            try
            {
                var customerProviderProcess = customerProvider.FindCustomersAsync(numDoc).GetAwaiter().GetResult();

                if (customerProviderProcess.IsFailure) return customerProviderProcess.Failures;

                return customerProviderProcess.Value;
            }
            catch (Exception e)
            {
                return ("DI exception raised", "DI exception raised");
            }
        }
    }

}

namespace amraGateSet.eNumUnik
{
    public enum UniOperations
    {
        CheckPay = 0
    , NewPay = 1
    , ConfirmPay = 2
    , StatusPay = 3
    }

}
