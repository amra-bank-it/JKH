//----------------------------------------
using IBP.SDKGatewayLibrary;
//using sql_Gera;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Security.Cryptography;
using System.Net.Mail;
using LogicalBlock;
using InterFaceEkassir_base;
using ComplexLogger;
using LogicalBlock.Model;

namespace InterFaceEkassir_base
{
    public static class DebugMode
    {
        public static bool ON;
    }
    public  class GatewayCore : GatewayCoreBase
    {
        Hashtable hashtable;

        public override void InitGateway(Hashtable settigs)
        {
            // TODO: Выполнить инициализацию шлюза.
            //
            hashtable = settigs;
            switch (hashtable["FULL_LOGGER"].ToString())
            {
                case "0": mLogger.changeMode(L_Mode.TestPlatform); break; 
                case "1": mLogger.changeMode(L_Mode.Release); break;
                case "2": mLogger.changeMode(L_Mode.debugFULL); break;
                default:mLogger.changeMode(L_Mode.debugFULL); break;
            }


        }

    

        public override void CheckAccount(ref Context context)
        {
            try
            {

                BaseLogic Blogic = new BaseLogic(context, Operation.CheckAccount, hashtable);
                Blogic.StartProcessing(StageEkasir.Check);
                context.Status = State.AccountExists;
                context = Blogic.getContext();

            }
            catch (Exception err)
            {
                context.Description = err.Message;
                context.Status = State.AccountNotExists;
                mLogger.WriteMessage("Ошибка в шлюзе на стадий проверки, Подробнее:" + err.Message);
                mLogger.WriteMessage("Время Конец:" + DateTime.Now);
            }
        }
        public override void Process(ref Context context)
        {

            try
            {
                BaseLogic Blogic = new BaseLogic(context, Operation.Process, hashtable);
                Blogic.StartProcessing(StageEkasir.Process);
                context.Status = State.Finalized;
                context = Blogic.getContext();

            }
            catch (Exception err)
            {
                context.Description = err.Message;
                context.Status = State.Rejected;
                mLogger.WriteMessage("Ошибка в шлюзе на стадий проверки, Подробнее:" + err.Message);
                mLogger.WriteMessage("Время Конец:" + DateTime.Now);
            }
            return;

        }


        /// <summary>
        /// Проверить состояние платежа.
        /// </summary>
        /// <param name="context">
        /// Контектс ядра.
        /// </param>
        public override void CheckProcessStatus(ref Context context)
        {
            //gPrint.writeLog("ProcStatus >>>>");
            // TODO: Получить из контектса ядра необходимые аргументы и проверить состояние платежа.
            // Например:
            //
            // string paySystemNumber = ((int)context["PaymentContext.Payment.Serial"]).ToString();
            // Result result = gatewayApi.CheckProcess(paySystemNumber);
            //
            //	switch (result.AcceptStatus)
            //	{
            //      case AcceptStatus.PayStatusDelayed:
            //			context.Status = State.Processing;
            //			break;
            //		case AcceptStatus.ErrorSuccess:
            //			context.Status = State.Finalized;
            //			break;
            //		case AcceptStatus.PayStatusDenied:
            //		case AcceptStatus.PayStatusAbandoned:
            //		case AcceptStatus.ErrorPayNotFound:
            //			context.Status = State.Rejected;
            //			context.Description = e.UnsuccessfulResponse.AcceptNote;
            //			return;
            //		case AcceptStatus.PayStatusAbandoning:
            //			context.Status = State.Recalling;
            //			context.Description = e.UnsuccessfulResponse.AcceptNote;
            //			return;
            //		default:
            //			context.Status = State.Rejected;
            //			context.Description = e.UnsuccessfulResponse.AcceptNote;
            //			return;
            //	}
            //gPrint.writeLog("ProcStatus <<<<");
        }

        /// <summary>
        /// Отозвать платеж.
        /// </summary>
        /// <param name="context">
        /// Контекст ядра.
        /// </param>
        public override void RecallPayment(ref Context context)
        {
            // TODO: Получить из контектса ядра необходимые аргументы и проверить состояние платежа.
            // Например:
            // string paySystemNumber = ((int)context["PaymentContext.Payment.Serial"]).ToString();
            // Result result = gatewayApi.Recall(paySystemNumber);
            //
            // switch (result.AcceptStatus)
            // {
            //     case AcceptStatus.PayStatusAbandoning:
            //         // Платеж отзывается.
            //         context.Status = State.Recalling;
            //         context.Description = e.UnsuccessfulResponse.AcceptNote;
            //         return;
            //     default:
            //         // Неудачный отзыв.
            //         context.Status = State.Finalized;
            //         context.Description = e.UnsuccessfulResponse.AcceptNote;
            //         return;
            // }

            // Если не произошло исключений, то сообщить об успехе операции.
            //

            //context.Status = State.Rejected;
        }
        public override void CheckRecallStatus(ref Context context)
        {
            //CheckProcessStatus(ref context);
        }
        public override void Dispose() { }
        public override Hashtable SaveSettings()
        {
            return null;
        }

        public override bool CanRecallPayment(ref Context context)
        {
            return false;
        }
    }

}
