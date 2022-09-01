using ComplexLogger;
using IBP.SDKGatewayLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace LogicalBlock.Model
{
    public static class contextServices
    {
        //Стандартные параметры сервера
        public static string Account;
        public static decimal Value;
        public static decimal Total;
        public static decimal Fee;
        public static decimal Amount;
        public static long Serial;
        public static DateTime ServerTime;
        public static Guid Id;
        public static string Number;
        public static string PointName;

        //Уникальные параметры под услугу 
        public static double DebtElev;
        public static double DebtApart;
        public static string Master;
        public static string Address;
        public static string ControlHouse;
        public static string AccRKC;
        public static string LcHous;
        public static string DatePay;
        public static string INN;
        public static string KPP;

        //Параметры настройки шлюза
        //Дефолт
        public static string LOGIN;
        public static string PASS;
        public static string FULL_LOGGER;
        //Уник
        //....


        public static Dictionary<string, string> OTHER_DATA;

        public static void ClearContext()
        {
            //Стандартные параметры сервера
            Account = "";
            Value = 0.00m;
            Total = 0.00m;
            Fee = 0.00m;
            Amount = 0.00m;
            Serial = 0;
            Id = Guid.Empty;
            Number = "";
            PointName = "";
            //Уникальные параметры под услугу
            DebtElev = 0;
            DebtApart = 0;
            Master = "";
            Address = "";
            ControlHouse = "";
            LcHous = "";
            AccRKC = "";
            DatePay = "";

        }
        public static void ReadContext(Context inContext)
        {
            FieldInfo[] fi = typeof(contextServices).GetFields();
            try
            {
                foreach (FieldInfo one_fi in fi)
                {
                    mLogger.WriteMessageDBG("Ключ:" + one_fi.Name);
                    object ot = inContext[PaymentContext(one_fi.Name)];
                    if (ot == null)
                    {
                        mLogger.WriteMessageDBG("Пустое значение,НУЛЛ:");
                        continue;
                    }

                    mLogger.WriteMessageDBG("ТипОбъекта:" + ot.GetType().Name);
                    mLogger.WriteMessageDBG("Значение объекта :" + ot.ToString());
                    object ot_g = Convert.ChangeType(ot, one_fi.FieldType);
                    mLogger.WriteMessageDBG("ТипОбъекта После изменений :" + ot_g.GetType().Name);
                    one_fi.SetValue(one_fi, ot_g);


                    //mLogger.WriteMessage("Ключ:"+ one_fi.Name + " . Значение:"+ one_fi.GetValue(one_fi).ToString());
                }

            }
            catch (Exception er)
            {
                mLogger.WriteMessageDBG("Ошибка в блоке ReadContext :" + er.ToString());
                throw new Exception("Ошибка конвертирования!." + er.ToString());
            }

        }
        public static void getContext(ref Context inContext)
        {
            FieldInfo[] fi = typeof(contextServices).GetFields();
            foreach (FieldInfo one_fi in fi)
            {
                inContext[PaymentContext(one_fi.Name)] = one_fi.GetValue(one_fi);
            }
        }
        public static Dictionary<string, object> getDictionary()
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            FieldInfo[] fi = typeof(contextServices).GetFields();
            foreach (FieldInfo one_fi in fi)
            {
                keyValues.Add(one_fi.Name, one_fi.GetValue(one_fi));
            }
            return keyValues;
        }
        public static string PaymentContext(string field)
        {
            if ("Account,Value,Id,Serial,Number,Total,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment." + field;
            else if ("Fee,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Comission";
            else if ("latitude,longitude,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point[\"" + field + "\"]";
            else if ("PointName,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Name";
            else if ("PointSerial,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Serial";
            else if ("RealAccount,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Account.RealAccount";
            else if ("AccountABS,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Account";
            else if ("ServiceAlias,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service.Alias";
            else if ("ServiceCount,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service.Count";
            else if ("Service,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service";
            else if ("AccountName,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Account.Name";
            else if ("ServerTime,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.ServerTime";
            else
                return "PaymentContext.Payment[\"" + field + "\"]";
        }
    }

}

