using ComplexLogger;
using IBP.SDKGatewayLibrary;
using InterFaceEkassir_base;
using LogicalBlock.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;

namespace TestGateWay
{

    class Program
    {

        static void Main(string[] args)
        {
            for (int ttt = 100000; ttt < 100010; ttt++)
            {

                try
                {
                    mLogger.changeMode(L_Mode.TestPlatform);
                    mLogger.WriteMessage("Платеж:"+ ttt);
                    //User us = new User();
                    //us.oo();
                    //return;


                    mLogger.WriteMessage("нач:" + DateTime.Now.ToString());

                    GatewayCore gateway = new GatewayCore();
                    Context context = new Context();
                    context.Add(contextServices.PaymentContext("PointName"), "TEST");
                    context.Add(contextServices.PaymentContext("Value"), 11.00m);
                    context.Add(contextServices.PaymentContext("Id"), Guid.NewGuid());
                    context.Add(contextServices.PaymentContext("Serial"), ttt);
                    context.Add(contextServices.PaymentContext("ServerTime"), DateTime.Now);
                    context.Add(contextServices.PaymentContext("Account"), "1518000000001");
                    context.Add(contextServices.PaymentContext("DebtElev"), "0");
                    //context.Add(contextServices.PaymentContext("typeBlocked"), "О");
                    //context.Add(contextServices.PaymentContext("Purpose"), "Блокируем потому что так надо было (тесты)");

                    Hashtable RFR = new Hashtable();
                   RFR.Add("LOGIN", "admin");
                    RFR.Add("PASS", "123");
                    //RFR.Add("SETT", "1"); // Данные по счету
                    //RFR.Add("SETT", "3");

                    RFR.Add("FULL_LOGGER", "0");
                    gateway.InitGateway(RFR);
                    gateway.Process(ref context);
                    int rer = 0;
    
                    mLogger.WriteMessage("кон:" + DateTime.Now.ToString());

                }
                catch (Exception e)
                {
                    int rr = 0;
                }
            }
            Console.ReadKey();
        }
    }
}
