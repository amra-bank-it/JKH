using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using IBP.SDKGatewayLibrary;

namespace InterFaceEkassir_Context
{
    public class SDK
    {

    //    static public void _contextWrite(ref Context context)
    //    {

  
    //        foreach (KEY_VALUE pair in kvTarget.Key_Value )
    //        {
    //            string fGood = pair.KEY.Trim();
    //            fGood = goodField(fGood);
    //            string value = _goodValue(pair.KEY, pair.VALUE);
    //            gPrint.writeLog("..." + pair.KEY + "..." + value);
    //            context[PaymentContext(fGood)] = value;
    //        }
    //        gPrint.writeLog("Запись в контекст конец");
    //    }
    //    static public string goodField(string field)
    //    {
    //        field = field.Trim();
    //        field = field.Replace(":int", "");
    //        field = field.Replace(":point", "");
    //        return field;
    //    }
    //    static public string _goodValue(string field, string valueInp, printGera gPrint = null)
    //    {
    //        string value = valueInp;
    //        if (field.IndexOf(":int") >= 0)
    //        {
    //            value = value.Replace(",", ".");
    //            value = value + ".";
    //            int pos = value.IndexOf(".");
    //            value = value.Substring(0, pos); //отсекли дробную часть
    //        }
    //        else if (field.IndexOf(":point") >= 0 && field.IndexOf("Value") >= 0)
    //        {
    //            if (gPrint != null)
    //            {
    //                gPrint.writeLog("<<----------goodValue ---------->>>");
    //                gPrint.writeLog("<<------------BLOCK---------->>>");
    //                gPrint.writeLog("point:" + valueInp);
    //                gPrint.writeLog("field:" + field);

    //            }
    //            valueInp = valueInp.Replace(",", ".");
    //            if (gPrint != null)
    //                gPrint.writeLog("valueInp :" + valueInp);

    //            decimal d = decimal.Parse(valueInp, CultureInfo.InvariantCulture);
    //            value = d.ToString("#.##");
    //            if (gPrint != null)
    //                gPrint.writeLog("value d.ToString(#.##) :" + value);
    //            if (value.IndexOf(".") <= 0)
    //            {
    //                value = value + ".00";
    //                if (gPrint != null)
    //                    gPrint.writeLog("value Иключение:" + value);
    //            }
    //            if (gPrint != null)
    //                gPrint.writeLog("value :" + value);
    //        }
    //        else
    //        {
    //            return valueInp;
    //        }
    //        return value;
    //    }

    //    static public string PaymentContext(string pfield)
    //    {
    //        string field = goodField(pfield);
    //        if ("Account,Value,Id,Serial,Number,Total,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Payment." + field;
    //        else if ("Fee,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Payment.Comission";
    //        else if ("latitude,longitude,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Point[\"" + field + "\"]";
    //        else if ("PointName,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Point.Name";
    //        else if ("PointSerial,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Point.Serial";
    //        else if ("RealAccount,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Point.Account.RealAccount";
    //        else if ("AccountABS,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Point.Account";
    //        else if ("ServiceAlias,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Payment.Service.Alias";
    //        else if ("ServiceCount,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Payment.Service.Count";
    //        else if ("Service,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Payment.Service";
    //        else if ("AccountName,".IndexOf(field + ",") >= 0)
    //            return "PaymentContext.Account.Name";
    //        else
    //            return "PaymentContext.Payment[\"" + field + "\"]";
    //    }
    }
}
