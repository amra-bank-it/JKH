using IBP.SDKGatewayLibrary;
using InterFaceEkassir_base;
using LogicalBlock.Model;
using System;

namespace InterFaceEkassir_sett
{
    /// <summary>
    /// Меенеджер настроек.
    /// </summary>
    /// <remarks>
    /// Сообщает ядру PaySystem Server список объектов, которые требуются шлюзу для выполнения различных операций.
    /// </remarks>
    public class SettingManager : SettingManagerBase
    {
        private string parametrGateway;
        private string argsCheckStage;
        private string argsProcessStage;

        private string argsSaveCheckStage;
        private string argsSaveProcessStage;


        /// <summary>
        /// Инициализация менеджера настроек.
        /// </summary>
        /// <param name="initString">
        /// Строка инициализации. Содержит значение атрибута initsettstringN настроек шлюза SDK.
        /// </param>
        /// <remarks>
        /// Этот метод вызывается ядром сразу после создания экземпляра класса. 
        /// </remarks>

        public override void InitSettingManadger(string initString)
        {
            Logger.Instance.WriteMessage("Инициализация  Шлюза", 1);
            // TODO: Заполнить массив строк именами атрибутов конфигурации, необходимых для работы шлюза.
            // Доступ к метабазе екасира
            this.parametrGateway = "LOGIN,PASS,FULL_LOGGER";


            this.argsCheckStage = "DebtElev,DebtApart,Master,Address,ControlHouse,AccRKC,LcHous,DatePay";
            this.argsProcessStage = "DebtElev,DebtApart,Master,Address,ControlHouse,AccRKC,LcHous,DatePay";


            //
            this.argsSaveCheckStage = argsCheckStage;
            this.argsSaveProcessStage = argsProcessStage;
            //
            this.argsCheckStage += ",Account,Value,Total,Fee,Serial,Id,Number,PointName,ServerTime"; //account тоже замешаем в список входа
            this.argsProcessStage += ",Account,Value,Total,Fee,Serial,Id,Number,PointName,ServerTime"; //account тоже замешаем в список входа
            //
        }

        /// <summary>
        /// Получить список атрибутов конфигурации, необходимых для работы шлюза.
        /// </summary>
        /// <returns>
        /// Список атрибутов конфигурации.
        /// </returns>
        public override string[] GetSettingsKey()
        {
            // TODO: Заполнить массив строк именами атрибутов конфигурации, необходимых для работы шлюза.
            //
            Logger.Instance.WriteMessage("Получаем переменные для настройки шлюза.", 1);

            string[] gatewayAttributes = this.parametrGateway.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] gatewayAttributes = parametrGateway.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (DebugMode.ON)
                foreach (var tT in gatewayAttributes)
                    Logger.Instance.WriteMessage("Ключ:" + tT, 1);

            return gatewayAttributes;
        }

        /// <summary>
        /// Получить список аргументов контекста, необходимых для выполнения каждой из операции шлюза.
        /// </summary>
        /// <param name="operation">
        /// Операция, для которой запрашивается список аргументов контекста.
        /// </param>
        /// <returns>
        /// Список аргументов контекста для каждой из операции.
        /// </returns>
        public override string[] GetPaymentContextKeys(Operation operation)
        {
            string[] contextService;
            Logger.Instance.WriteMessage("Получаем переменные для работы Контекста. Тип Операции:" + operation.ToString(), 1);

            switch (operation)
            {
                case Operation.CheckAccount:
                    foreach (string prCon in this.argsCheckStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                        argsCheckStage = this.argsCheckStage.Replace(prCon, contextServices.PaymentContext(prCon));

                    contextService = this.argsCheckStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    break;

                case Operation.Process:
                    foreach (string prCon in this.argsProcessStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                        argsProcessStage = this.argsProcessStage.Replace(prCon, contextServices.PaymentContext(prCon));

                    contextService = this.argsProcessStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    break;

                default:
                    return null;
            }

            foreach (var tT in contextService)
                Logger.Instance.WriteMessage("Ключ:" + tT, 1);

            return contextService;
        }

        /// <summary>
        /// Установить список аргументов контекста, в которых требуется сохранить данные после выполнения операции шлюза.
        /// </summary>
        /// <param name="operation">
        /// Операция, для которой требуется сохранение аргументов.
        /// </param>
        /// <returns>
        /// Список названий аргументов.
        /// </returns>
        public override string[] SetPaymentContextKeys(Operation operation)
        {
            // TODO: Используя параметр operation определить операцию, для которой требуется
            // сохранить список аргументов контекста в ядре, заполнить массив именами необходимых
            // аргументов и вернуть массив ядру.
            string[] contextService;

            Logger.Instance.WriteMessage("Получаем переменные для  сохранение в Контекст. Тип Операции:" + operation.ToString(), 1);
            switch (operation)
            {
                case Operation.CheckAccount:
                    foreach (string prCon in this.argsSaveCheckStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                        argsSaveCheckStage = this.argsSaveCheckStage.Replace(prCon, contextServices.PaymentContext(prCon));

                    contextService = this.argsSaveCheckStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    break;

                case Operation.Process:
                    foreach (string prCon in this.argsSaveProcessStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
                        argsSaveProcessStage = this.argsSaveProcessStage.Replace(prCon, contextServices.PaymentContext(prCon));

                    contextService = this.argsSaveProcessStage.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    break;

                default:
                    return null;
            }

            foreach (var tT in contextService)
                Logger.Instance.WriteMessage("Ключ:" + tT, 1);


            return contextService;
        }

        public override string[] SaveSettingKey()
        {
            return null; //SetFilterContextKeys
        }

        public override string[] GetFilterContextKeys()
        {
            return null;
        }
    }

    class GATEWAY_PARAMS
    {
        public string BASE_ATTRIBUTE { get; set; }
        public string CHECK_STAGE_ATTRIBUTE { get; set; }
        public string PAY_STAGE_ATTRIBUTE { get; set; }
    }
}
