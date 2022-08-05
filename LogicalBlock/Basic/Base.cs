using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBP.SDKGatewayLibrary;
using System.Collections;
using LogicalBlock.Model;
using ComplexLogger;
using LogicalBlock.Router;
using System.Net;

namespace LogicalBlock
{
    public class BaseLogic
    {
        Context bContext;
        Hashtable bHashTable;
        Operation bOperation;
        public BaseLogic(Context inContext,Operation inOperation,Hashtable inHashTable)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

            contextServices.ClearContext();

            bContext = new Context();

            bContext = inContext;

            foreach (var RT in inHashTable.Keys)
            {
                if (RT.ToString() == "OTHER_DATA")
                    continue;

                bContext.Add(contextServices.PaymentContext(RT.ToString()), inHashTable[RT]);
            }
            bOperation = inOperation;
            bHashTable = inHashTable;
            contextServices.ReadContext(bContext);
        }



        public void StartProcessing(StageEkasir IN_EKASS_STAGE)
        {
            mLogger.WriteMessage( String.Format("Стадия:{0}. Account:{1}", bOperation.ToString(), contextServices.Account));

            Executor lExecute = new Executor();
            try
            {
                lExecute.Begin( IN_EKASS_STAGE);
            }
            catch(Exception Err)
            {
                throw new Exception("Логический Блок, ошибка подробнее:" + Err.ToString() );
            }
        }

        public Context getContext()
        {
            contextServices.getContext(ref bContext);
            return bContext;
        }

    }
}
