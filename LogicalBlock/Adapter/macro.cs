using LogicalBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithData.Basic.Adapter
{
    public static  class macro
    {

        public static void FillDocument( Dictionary<string, object> allContext, ref string outBody)
        {
            
            foreach (string key in allContext.Keys)
            {
                object value = null;
                if (!allContext.TryGetValue(key, out value))
                    continue;

                if (value == null)
                    continue;


                //if (typeof(decimal) == value.GetType())
                //    value = value.ToString().Replace(',', '.');

                //if (typeof(DateTime) == value.GetType())
                //    value = ((DateTime)value).ToString("yyyyMMdd");

                outBody = outBody.Replace("%" + key + "%", value.ToString());
            }

        }

    }
}
