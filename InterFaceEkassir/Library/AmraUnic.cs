using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unistream.Business;
using Unistream.Business.Providers;
using Unistream.Infrastructure.Unistream.Api.Dto;
using Unistream.Utilities.DI.Autowiring;
using Unistream.Utilities.Payloads;
using Newtonsoft.Json;


namespace amraGateSet
{
    class tr_bus
    {
        public static methodsBus methP;
        internal tr_bus()
        {
            methP = null;
            methP = getM();
        }
        static methodsBus getM()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var services = new ServiceCollection();
            services.AddBusiness(configuration);
            //services.AddScoped<ICustomerProvider, CustomerProvider>();
            //((IServiceCollection)services).AddAutowiringDependencies();


            ServiceProviderContainer.ServiceProvider = services.BuildServiceProvider();

            methodsBus mBus = new methodsBus();
            return mBus;

        }
        public ClientDto Get_cus(string nDoc)
        {
            methodsBus tt = getM();
            return tt._getCus(nDoc).Value.Items[0];
        }
        public void newCus(string jsBody)
        {
            methodsBus tt = getM();
            Root_CUS RootBody = JsonConvert.DeserializeObject<Root_CUS>(jsBody);
            ClientDto clientDto = new ClientDto();
            clientDto.Address = fullinAddress(RootBody.address);
            clientDto.BirthDate = RootBody.birthDate;
            clientDto.BirthPlace = RootBody.birthPlace;
            clientDto.CountryOfResidence = RootBody.countryOfResidence;
            clientDto.Documents = fullinDocuments(RootBody.documents);
            clientDto.FirstName = RootBody.firstName;
            clientDto.Gender = (Gender)RootBody.gender;
            clientDto.Id = RootBody.id;
            clientDto.KazId = (string)RootBody.kazId;
            clientDto.LastName = RootBody.lastName;
            clientDto.MiddleName = RootBody.middleName;
            clientDto.PhoneNumber = RootBody.phoneNumber;

            tt._newCus(clientDto);
        }
        public DocumentDto[] fullinDocuments(List<Document> inDocc)
        {
            DocumentDto retDocc = new DocumentDto();
            retDocc.Type = inDocc[0].type;

            Fields fields = inDocc[0].fields;
            retDocc.Fields = fields.GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => prop.GetValue(fields, null).ToString());
            DocumentDto[] documentDtos = new DocumentDto[1];
            //Пришлепка
            string IssueDate = "";
            string expiryDate = "";
            //retDocc.Fields.GetValue("", IssueDate);


            documentDtos[0] = retDocc;
            return documentDtos;
        }

        public AddressDto fullinAddress(Address inAddr)
        {
            AddressDto retAddr = new AddressDto();
            retAddr.AddressString = inAddr.addressString;
            retAddr.Apartment = inAddr.apartment;
            retAddr.Building = inAddr.building;
            retAddr.City = inAddr.city;
            retAddr.CountryCode = inAddr.countryCode;
            retAddr.House = inAddr.house;
            retAddr.Postcode = inAddr.postcode;
            retAddr.State = inAddr.state;
            retAddr.Street = inAddr.street;

            return retAddr;
        }

        public void newPay(string jsBody, string OperID)
        {
            methodsBus tt = getM();
            OperationInfoDto InfoDto = new OperationInfoDto();

            Root RootBody = JsonConvert.DeserializeObject<Root>(jsBody);


            ClientContext CliCont = RootBody.clientContext;
            InfoDto.ClientContext = CliCont.formatToDTO();


            Data data = RootBody.data;
            InfoDto.Data = data.GetType()
                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(data, null));


            InfoDto.ExternalId = RootBody.externalId == null ? null : RootBody.externalId.ToString();
            InfoDto.PaymentInstrument = RootBody.paymentInstrument == null ? null : RootBody.paymentInstrument.ToString();

            var ans = tt._newPay(body: InfoDto, operationtype: "Transfer", operationID: OperID);
            
            if(ans.Value==null || ans.IsFailure)
            {
                throw new Exception("Bad");
            }
           
            tt._confirmPay(body: ans.Value, operationID: OperID);
        }

 
    }
    public class ServiceProviderContainer
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
    public class methodsBus
    {
        public R<ClientsDto> _getCus(string nDoc)
        {
            var customerProvider = ServiceProviderContainer.ServiceProvider.GetRequiredService<ICustomerProvider>();

            try
            {
                var customerProviderProcess = customerProvider.FindCustomersAsync(nDoc).GetAwaiter().GetResult();

                if (customerProviderProcess.IsFailure) return customerProviderProcess.Failures;

                return customerProviderProcess.Value;
            }
            catch (Exception e)
            {
                return ("DI exception raised", "DI exception raised");
            }
        }
        public R<N> _newCus(ClientDto cliBody)
        {
            var customerProvider = ServiceProviderContainer.ServiceProvider.GetRequiredService<ICustomerProvider>();

            try
            {
                var customerProviderProcess = customerProvider.CreateCustomerAsync(cliBody).GetAwaiter().GetResult();

                if (customerProviderProcess.IsFailure) return customerProviderProcess.Failures;

                return customerProviderProcess.Value;
            }
            catch (Exception e)
            {
                return ("DI exception raised", "DI exception raised");
            }
        }
        public R<OperationDto> _newPay(OperationInfoDto body, string operationtype, string operationID)
        {
            var paymentProvider = ServiceProviderContainer.ServiceProvider.GetRequiredService<IPaymentProvider>();

            try
            {
                var paymentProviderProcess = paymentProvider.CreatePaymentAsync(body, operationtype, operationID).GetAwaiter().GetResult();

                if (paymentProviderProcess.IsFailure) return paymentProviderProcess.Failures;

                return paymentProviderProcess.Value;
            }
            catch (Exception e)
            {
                return ("DI exception raised", "DI exception raised");
            }
        }
        public R<CommandStatusDto> _confirmPay(OperationDto body, string operationID)
        {
            var paymentProvider = ServiceProviderContainer.ServiceProvider.GetRequiredService<IPaymentProvider>();

            try
            {
                var paymentProviderProcess = paymentProvider.ConfirmPaymentAsync(body, operationID).GetAwaiter().GetResult();

                if (paymentProviderProcess.IsFailure) return paymentProviderProcess.Failures;

                return paymentProviderProcess.Value;
            }
            catch (Exception e)
            {
                return ("DI exception raised", "DI exception raised");
            }
        }

    }
    public class ClientContext
    {
        public string clientId { get; set; }
        public List<string> documents { get; set; }
        public object phone { get; set; }
        public object firstName { get; set; }
        public object lastName { get; set; }
        public object middleName { get; set; }
        public object confidantId { get; set; }
        public object loyaltyCardNumber { get; set; }

        public ClientContextDto formatToDTO()
        {
            ClientContextDto returnDTO = new ClientContextDto();
            returnDTO.ClientId = clientId;
            returnDTO.ConfidantId = confidantId == null ? null: confidantId.ToString();
            returnDTO.Documents = documents.ToArray();
            returnDTO.Phone = phone == null ? null: phone.ToString();
            returnDTO.FirstName = firstName == null ? null : firstName.ToString();
            returnDTO.LastName = lastName == null ? null : lastName.ToString();
            returnDTO.MiddleName = middleName==null ? null: middleName.ToString();
            returnDTO.LoyaltyCardNumber = loyaltyCardNumber == null ? null:loyaltyCardNumber.ToString();
            return returnDTO;
        }

    }

    public class Data
    {
        public string FundsSource { get; set; }
        public string Country { get; set; }
        public string BeneficiaryLastName { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryMiddleName { get; set; }
        public string BirthDate { get; set; }
        public string AcceptedCurrency { get; set; }
        public string AmountType { get; set; }
        public string Amount { get; set; }
        public string WithdrawCurrency { get; set; }
    }

    public class Root
    {
        public ClientContext clientContext { get; set; }
        public Data data { get; set; }
        public object externalId { get; set; }
        public object paymentInstrument { get; set; }
    }

    public class Address
    {
        public string addressString { get; set; }
        public string apartment { get; set; }
        public string building { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string house { get; set; }
        public string postcode { get; set; }
        public string state { get; set; }
        public string street { get; set; }
    }

    public class Fields
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public string Issuer { get; set; }
        public string IssuerDepartmentCode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string state { get; set; }
    }

    public class Document
    {
        public string type { get; set; }
        public Fields fields { get; set; }
    }

    public class Root_CUS
    {
        public string id { get; set; }
        public string countryOfResidence { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public int gender { get; set; }
        public string birthPlace { get; set; }
        public DateTime birthDate { get; set; }
        public string phoneNumber { get; set; }
        public string taxpayerIndividualIdentificationNumber { get; set; }
        public object kazId { get; set; }
        public Address address { get; set; }
        public List<Document> documents { get; set; }
    }
}
