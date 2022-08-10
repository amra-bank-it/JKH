using System;
using System.Collections.Generic;
using System.Linq;
using LogicalBlock.Model;
using ComplexLogger;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace LogicalBlock.Router
{
    class Executor
    {
        public static Dictionary<string, object> allData;
        StageEkasir GLOBAL_EKASS_STAGE;
        public void Begin( StageEkasir IN_EKASS_STAGE)
        {
            GLOBAL_EKASS_STAGE = IN_EKASS_STAGE;
            allData = new Dictionary<string, object>();
            allData = allData.Concat(contextServices.getDictionary()).ToDictionary(x => x.Key, x => x.Value);


            switch (IN_EKASS_STAGE)
            {
                case StageEkasir.Check:
                    {
                        var Response = GetInfoCustomer(contextServices.Account);
                        mLogger.WriteMessageDBG("Результат="+ Response);
                        InfoCus localInfoCus = JsonConvert.DeserializeObject<InfoCus>(Response);
                        if (localInfoCus == null)
                            throw new Exception("Клиент с таким лиц/счетом не найден!");

                        contextServices.AccRKC = localInfoCus.AccRKC;
                        contextServices.DatePay = localInfoCus.DatePay;
                        contextServices.Address = localInfoCus.Address;
                        contextServices.Master = localInfoCus.Master;
                        contextServices.ControlHouse = localInfoCus.ControlHouse;
                        contextServices.DebtApart = localInfoCus.DebtApart;
                        contextServices.DebtElev = localInfoCus.DebtElev;

                        break;
                    }


                case StageEkasir.Process:
                    {
                        var Response = CreatePayment(contextServices.Account,contextServices.Value);
                        mLogger.WriteMessageDBG("Ответ от сервера при оплате:"+Response);
                        ResponsePayment localRespPay = JsonConvert.DeserializeObject<ResponsePayment>(Response);
                        if (localRespPay == null)
                            throw new Exception("Произошли ошибка при оплате! Требуется уточнить статус операции");

                        if (localRespPay.Status != "OK" )
                            throw new Exception("Произошли ошибка при оплате! Статус:"+ localRespPay.Status);

                        break;
                    }
            }

        }

        private string CreatePayment(string account, decimal value)
        {
            string uniqueKey = Guid.NewGuid().ToString();
            string Response = "";
            string reqKomunalka1 = $@"https://w.amra-bank.com/ServicesKomunalka1/hs/lk/pay?account={account}&amount={value}&type=%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0%D0%9E%D0%BD%D0%BB%D0%B0%D0%B9%D0%BD";
            string reqKomunalka2 = $@"https://w.amra-bank.com/ServicesKomunalka2/hs/lk/pay?account={account}&amount={value}&type=%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0%D0%9E%D0%BD%D0%BB%D0%B0%D0%B9%D0%BD";
            string reqKomunalka3 = $@"https://w.amra-bank.com/ServicesKomunalka3/hs/lk/pay?account={account}&amount={value}&type=%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0%D0%9E%D0%BD%D0%BB%D0%B0%D0%B9%D0%BD";
            string reqKomunalka4 = $@"https://w.amra-bank.com/ServicesKomunalka4/hs/lk/pay?account={account}&amount={value}&type=%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0%D0%9E%D0%BD%D0%BB%D0%B0%D0%B9%D0%BD";
            string reqKomunalka5 = $@"https://w.amra-bank.com/ServicesKomunalka5/hs/lk/pay?account={account}&amount={value}&type=%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0%D0%9E%D0%BD%D0%BB%D0%B0%D0%B9%D0%BD";
            
            

            switch (account.Substring(0,4))
            {

                case "2021":// 1 ДУ
                    {
                        Response = sendRequest(reqKomunalka1);
                        break;
                    }
                case "2419":// 2 ДУ
                    {
                        Response = sendRequest(reqKomunalka2);
                        break;
                    }
                case "2404":// 3 ДУ
                    {
                        Response = sendRequest(reqKomunalka3);
                        break;
                    }
                case "1219": //4 ДУ
                    {
                        Response = sendRequest(reqKomunalka4);
                        break;
                    }
                case "1518":// 5 ДУ
                    {
                        Response = sendRequest(reqKomunalka5);
                        break;
                    }

                default: throw new Exception("Лицевой счет не корректный");

            }


            return Response;
        }

        private string sendRequest(string req)
        {
            var client = new RestClient(req);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.Credentials = new NetworkCredential("admin", "123");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        private string GetInfoCustomer(string account)
        {
            string Response = "";
            string reqKomunalka1 = $@"https://w.amra-bank.com/ServicesKomunalka1/hs/lk/term?account={account}";
            string reqKomunalka2 = $@"https://w.amra-bank.com/ServicesKomunalka2/hs/lk/term?account={account}";
            string reqKomunalka3 = $@"https://w.amra-bank.com/ServicesKomunalka3/hs/lk/term?account={account}";
            string reqKomunalka4 = $@"https://w.amra-bank.com/ServicesKomunalka4/hs/lk/term?account={account}";
            string reqKomunalka5 = $@"https://w.amra-bank.com/ServicesKomunalka5/hs/lk/term?account={account}";
            
            

            switch (account.Substring(0, 4))
            {

                case "2021":// 1 ДУ
                    {
                        Response = sendRequest(reqKomunalka1);
                        break;
                    }
                case "2419":// 2 ДУ
                    {
                        Response = sendRequest(reqKomunalka2);
                        break;
                    }
                case "2404":// 3 ДУ
                    {
                        Response = sendRequest(reqKomunalka3);
                        break;
                    }
                case "1219": // 4 ДУ
                    {
                        Response = sendRequest(reqKomunalka4);
                        break;
                    }
                case "1518":// 5 ДУ
                    {
                        Response = sendRequest(reqKomunalka5);
                        break;
                    }

                default: throw new Exception("Лицевой счет не корректный");
            }


            return Response;

        }

    }
}
